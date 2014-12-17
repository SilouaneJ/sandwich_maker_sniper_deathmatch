using UnityEngine;
using System.Collections;

public class RecipeManager : MonoBehaviour {

	[ SerializeField ] int
		AmountOfRecipes;

	[ SerializeField ] GameObject[]
		RecipeArray = new GameObject[10];

	int
		RecipeCounter;

	// Use this for initialization
	void Start () {
	
	}

	public void SetUpRecipeManager(){

	}

	public GameObject InstantiateRecipeObject(int recipe_object_id){

		GameObject recipe_object = Instantiate(RecipeArray[recipe_object_id],this.transform.position,Quaternion.identity) as GameObject;
		recipe_object.name = "Recipe_" + RecipeCounter;
		recipe_object.GetComponent<RecipeObject>().SetUpRecipeObject();

		RecipeCounter++;

		return recipe_object;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
