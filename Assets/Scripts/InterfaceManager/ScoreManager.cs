using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	[ SerializeField ] GameObject
		EndObjectPrefab;

	GameObject
		EndObject;

	// Use this for initialization
	void Start () {
	
	}

	public void SetUpScoreManager(){

		ResetScoreManager();
	}

	public void ResetScoreManager(){

		ResetEndObject();
	}

	public void ResetEndObject(){

		GameObject end_object = EndObject;
		EndObject = null;
		Destroy(end_object);
	}

	public void StartScoreManager(bool left_player_dead, int left_player_score, int right_player_score){

		EndObject = Instantiate(EndObjectPrefab,this.transform.position,Quaternion.identity) as GameObject;
		EndObject.transform.parent = this.transform;
		EndObject.name = "End_Object";

		EndObject.GetComponent<EndObject>().SetUpEndObject(left_player_dead,left_player_score,right_player_score);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
