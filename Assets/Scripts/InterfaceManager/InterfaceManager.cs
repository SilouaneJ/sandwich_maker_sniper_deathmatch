using UnityEngine;
using System.Collections;

public class InterfaceManager : MonoBehaviour {

	[ SerializeField ] GameObject
		MainManager,
		HUDManager,
		ScoreManager;

	bool
		InMain,
		InHUD,
		InScore;

	// Use this for initialization
	void Start () {

	}

	public void SetUpInterfaceManager(){

		InMain = false;
		InHUD = false;
		InScore = false;

		HUDManager.GetComponent<HUDManager>().SetUpHUDManager();
		MainManager.GetComponent<MainManager>().SetUpMainManager();
		ScoreManager.GetComponent<ScoreManager>().SetUpScoreManager();
	}

	public void LaunchMain(){

		MainManager.GetComponent<MainManager>().StartMainManager();
	}

	public void ResetMain(){

		MainManager.GetComponent<MainManager>().ResetMainManager();
	}

	public void LaunchHUD(){

		HUDManager.GetComponent<HUDManager>().StartHUDManager();
	}

	public void ResetHUD(){

		HUDManager.GetComponent<HUDManager>().ResetHUDManager();
	}

	public void LaunchScore(){

		ScoreManager.GetComponent<ScoreManager>().StartScoreManager(false,500,1500);
	}

	public GameObject GetHUDManager(){
		GameObject hud_manager = HUDManager;
		return hud_manager;
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateInterfaceManager(){

		if (InHUD){

			HUDManager.GetComponent<HUDManager>().UpdateHUDManager();
		}
	}
}
