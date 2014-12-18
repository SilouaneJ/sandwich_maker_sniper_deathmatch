using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour {

	[ SerializeField ] GameObject
		HUDObjectPrefab;

	GameObject
		HUDObject;

	[ SerializeField ] GameObject
		LeftOrderPrefab,
		RightOrderPrefab;

	GameObject
		LeftOrder,
		RightOrder;

	// Use this for initialization
	void Start () {
	
	}

	public void SetUpHUDManager(){

		ResetHUDObject();
	}

	public void ResetHUDObject(){

		GameObject hud_object = HUDObject;
		HUDObject = null;
		Destroy(hud_object);
	}

	public void StartHUDManager(){

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateHUDManager(){


	}
}
