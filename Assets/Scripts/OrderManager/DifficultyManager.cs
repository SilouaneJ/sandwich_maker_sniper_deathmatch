using UnityEngine;
using System.Collections;

public class DifficultyManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public void SetUpDifficultyManager(){

	}

	public int GetLeftRecipeID(){
		int left_recipe_id = Random.Range(0,8);
		return left_recipe_id;
	}

	public int GetRightRecipeID(){
		int right_recipe_id = Random.Range(0,8);
		return right_recipe_id;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
