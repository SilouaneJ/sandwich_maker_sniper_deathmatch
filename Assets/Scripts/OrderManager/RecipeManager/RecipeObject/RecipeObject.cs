using UnityEngine;
using System.Collections;

public class RecipeObject : MonoBehaviour {

	[ SerializeField ] int
		AmountOfIngredients;

	[ SerializeField ] int[]
		IngredientOrderList = new int[10];

	// Use this for initialization
	void Start () {
	
	}

	public void SetUpRecipeObject(){

	}

	public int GetAmountOfIngredients(){
		int amount_of_ingredients = AmountOfIngredients;
		return amount_of_ingredients;
	}

	public int[] GetIngredientOrderList(){
		int[] ingredient_order_list = IngredientOrderList;
		return ingredient_order_list;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
