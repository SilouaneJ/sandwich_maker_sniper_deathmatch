using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDObject : MonoBehaviour {

	[ SerializeField ] GameObject
		LeftScoreWhite,
		LeftScoreBlue,
		LeftScorePink,
		RightScoreWhite,
		RightScoreBlue,
		RightScorePink;

	// Use this for initialization
	void Start () {
	
	}

	public void SetUpHUDObject(){

		UpdateLeftScore(0);
		UpdateRightScore(0);
	}

	public void UpdateLeftScore(int score_update){

		LeftScoreWhite.GetComponent<Text>().text = score_update.ToString();
		LeftScoreBlue.GetComponent<Text>().text = score_update.ToString();
		LeftScorePink.GetComponent<Text>().text = score_update.ToString();
	}

	public void UpdateRightScore(int score_update){

		RightScoreWhite.GetComponent<Text>().text = score_update.ToString();
		RightScoreBlue.GetComponent<Text>().text = score_update.ToString();
		RightScorePink.GetComponent<Text>().text = score_update.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
