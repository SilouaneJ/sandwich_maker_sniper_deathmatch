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

	bool
		HasScore;

	// Use this for initialization
	void Start () {

	}

	public void SetUpInterfaceManager(){

		InMain = false;
		InHUD = false;
		InScore = false;

		HasScore = false;

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

	public void LaunchScore(bool left_player_dead,GameObject order_manager){

		if (!HasScore){

			int left_player_score = order_manager.GetComponent<OrderManager>().GetLeftPlayerScore();
			int right_player_score = order_manager.GetComponent<OrderManager>().GetRightPlayerScore();

			ScoreManager.GetComponent<ScoreManager>().StartScoreManager(left_player_dead,left_player_score,right_player_score);

			HasScore = true;
		}
	}

	public void ResetScore(){

		HasScore = false;
		ScoreManager.GetComponent<ScoreManager>().ResetScoreManager();
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
