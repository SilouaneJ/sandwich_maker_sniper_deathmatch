using UnityEngine;
using System.Collections;

enum AudioState
{
	Playing,
	FadeIn,
	FadeOut,
	NotPlaying
};

public enum SFX
{
	Anger = 0,
	Grab,
	Grill,
	Gun,
	GunLoad1,
	GunLoad2,
	Money,
	Slap1,
	Slap2
};

public class AudioManager : MonoBehaviour
{
	[SerializeField]
	AudioClip[] MusicTable;
	int CurrentMusic, FollowingtMusic, CurrentSource, FollowingSource;
	AudioSource[] SourceTable;
	[SerializeField]
	AudioClip[] SFXTable;
	AudioState MusicState;
	float MusicFadeSpeed = 0.5f;
	float MusicMaximumVolume = 0.6f;
	float SFXMaximumVolume = 0.8f;

	// Use this for initialization
	void Start ()
	{
		MusicState = AudioState.NotPlaying;
		CurrentMusic = 0;
		SourceTable = this.GetComponents< AudioSource > ();

		foreach(AudioSource audio_source in SourceTable)
		{
			audio_source.volume = 0.0f;
		}

		CurrentSource = 0;
		FollowingSource = -1;
		FollowingtMusic = -1;
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch(MusicState)
		{
			case AudioState.FadeIn:
			{
				if(FollowingSource != -1)
				{
					bool it_is_done;

					it_is_done = true;

					if(SourceTable[CurrentSource].volume > 0.0f)
					{
						SourceTable[CurrentSource].volume -= Time.deltaTime * MusicFadeSpeed;
						it_is_done = false;
					}

					if(SourceTable[FollowingSource].volume < MusicMaximumVolume)
					{
						SourceTable[FollowingSource].volume += Time.deltaTime * MusicFadeSpeed;
						it_is_done = false;
					}

					if(it_is_done)
					{
						MusicState = AudioState.Playing;
						CurrentSource = FollowingSource;
					}
				}
				else
				{
					if(SourceTable[CurrentSource].volume < MusicMaximumVolume)
					{
						SourceTable[CurrentSource].volume += Time.deltaTime * MusicFadeSpeed;
					}
					else
					{
						MusicState = AudioState.Playing;
					}
				}
			}
			break;

			case AudioState.FadeOut:
			{
				if(FollowingSource != -1)
				{
					bool it_is_done;

					it_is_done = true;

					if(SourceTable[CurrentSource].volume < MusicMaximumVolume)
					{
						SourceTable[CurrentSource].volume += Time.deltaTime * MusicFadeSpeed;
						it_is_done = false;
					}

					if(SourceTable[FollowingSource].volume > 0.0f)
					{
						SourceTable[FollowingSource].volume -= Time.deltaTime * MusicFadeSpeed;
						it_is_done = false;
					}

					if(it_is_done)
					{
						MusicState = AudioState.Playing;
						CurrentSource = FollowingSource;
					}
				}
				else
				{
					if(SourceTable[CurrentSource].volume > 0.0f)
					{
						SourceTable[CurrentSource].volume -= Time.deltaTime * MusicFadeSpeed;
					}
					else
					{
						MusicState = AudioState.NotPlaying;
					}
				}
			}
			break;

			case AudioState.Playing:
			{
			}
			break;

			case AudioState.NotPlaying:
			{
				if(FollowingtMusic != -1)
				{
					CurrentMusic = FollowingtMusic;
					FollowingtMusic = -1;
					PlayMusic();
				}
			}
			break;
		}
	}

	public void PlayMusic()
	{
		MusicState = AudioState.FadeIn;

		if(FollowingSource != -1)
		{
			SourceTable[FollowingSource].clip = MusicTable [CurrentMusic];
			SourceTable[FollowingSource].Play ();
			Invoke("NextMusic", SourceTable[FollowingSource].clip.length - 5.0f);
		}
		else
		{
			SourceTable[CurrentSource].clip = MusicTable [CurrentMusic];
			SourceTable[CurrentSource].Play ();
			Invoke("NextMusic", SourceTable[FollowingSource].clip.length - 5.0f);
		}
	}

	public void NextMusic()
	{
		MusicState = AudioState.FadeIn;
		FollowingtMusic = CurrentMusic + 1;

		if(FollowingtMusic >= MusicTable.Length)
		{
			FollowingtMusic = 0;
		}

		FollowingSource = (CurrentSource == 0) ? 1 : 0;

		SourceTable[FollowingSource].clip = MusicTable [FollowingtMusic];
		SourceTable[FollowingSource].Play ();

		CurrentMusic = FollowingtMusic;
		FollowingtMusic = -1;

		Invoke("NextMusic", SourceTable[FollowingSource].clip.length - 5.0f);
	}

	public void PreviousMusic()
	{
		MusicState = AudioState.FadeIn;
		FollowingtMusic = CurrentMusic - 1;
		
		if(FollowingtMusic < 0)
		{
			FollowingtMusic = MusicTable.Length - 1;
		}

		FollowingSource = (CurrentSource == 0) ? 1 : 0;

		SourceTable[FollowingSource].clip = MusicTable [FollowingtMusic];
		SourceTable[FollowingSource].Play ();

		CurrentMusic = FollowingtMusic;
		FollowingtMusic = -1;

		Invoke("NextMusic", SourceTable[FollowingSource].clip.length - 5.0f);
	}

	public void PlaySfx(SFX audio_sfx)
	{
		SourceTable [2].volume = SFXMaximumVolume;
		SourceTable [2].clip = SFXTable [(int)audio_sfx];
		SourceTable [2].Play ();
	}
}
