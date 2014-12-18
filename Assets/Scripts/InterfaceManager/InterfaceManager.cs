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
	}

	public void LaunchHUD(){


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
