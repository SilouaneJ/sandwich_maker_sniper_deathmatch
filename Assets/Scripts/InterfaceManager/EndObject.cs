using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndObject : MonoBehaviour {

	[ SerializeField ] GameObject
		LeftKillImage,
		LeftBloodImage,
		RightKillImage,
		RightBloodImage;

	[ SerializeField ] GameObject
		EndTitle,
		LeftScoreWhite,
		LeftScoreBlue,
		LeftScorePink,
		RightScoreWhite,
		RightScoreBlue,
		RightScorePink;

	// Use this for initialization
	void Start () {
	
	}

	public void SetUpEndObject(bool left_player_dead, int left_player_score, int right_player_score){

		if (left_player_dead){

			LeftKillImage.GetComponent<Image>().enabled = true;
			LeftBloodImage.GetComponent<Image>().enabled = true;
			RightKillImage.GetComponent<Image>().enabled = false;
			RightBloodImage.GetComponent<Image>().enabled = false;

			EndTitle.GetComponent<Text>().text = "Player 1 wins!";
		}
		else if (!left_player_dead){

			LeftKillImage.GetComponent<Image>().enabled = false;
			LeftBloodImage.GetComponent<Image>().enabled = false;
			RightKillImage.GetComponent<Image>().enabled = true;
			RightBloodImage.GetComponent<Image>().enabled = true;

			EndTitle.GetComponent<Text>().text = "Player 2 wins!";
		}

		LeftScoreWhite.GetComponent<Text>().text = left_player_score.ToString();
		LeftScoreBlue.GetComponent<Text>().text = left_player_score.ToString();
		LeftScorePink.GetComponent<Text>().text = left_player_score.ToString();
		RightScoreWhite.GetComponent<Text>().text = right_player_score.ToString();
		RightScoreBlue.GetComponent<Text>().text = right_player_score.ToString();
		RightScorePink.GetComponent<Text>().text = right_player_score.ToString();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
