using UnityEngine;
using System.Collections;

public class MainManager : MonoBehaviour {

	[ SerializeField ] GameObject
		MainObjectPrefab;
	
	GameObject
		MainObject;

	// Use this for initialization
	void Start () {
	
	}

	public void SetUpMainManager(){

		ResetMainObject();
	}

	public void ResetMainManager(){

		ResetMainObject();
	}

	public void ResetMainObject(){

		GameObject main_object = MainObject;
		MainObject = null;
		Destroy(main_object);
	}

	public void StartMainManager(){

		InstantiateMainObject();
	}

	public void InstantiateMainObject(){

		MainObject = Instantiate(MainObjectPrefab,this.transform.position,Quaternion.identity) as GameObject;
		MainObject.transform.parent = this.transform;
		MainObject.name = "Main_Object";
	}

	// Update is called once per frame
	void Update () {
	
	}
}
