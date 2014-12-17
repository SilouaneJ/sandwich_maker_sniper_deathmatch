using UnityEngine;
using System.Collections;

public class RecipeObject : MonoBehaviour {

	[ SerializeField ] string
		RecipeName;

	[ SerializeField ] int
		AmountOfIngredients;

	[ SerializeField ] string[]
		IngredientOrderList = new string[10];

	// Use this for initialization
	void Start () {
	
	}

	public void SetUpRecipeObject(){

	}

	public string GetRecipeName(){
		string recipe_name = RecipeName;
		return recipe_name;
	}

	public int GetAmountOfIngredients(){
		int amount_of_ingredients = AmountOfIngredients;
		return amount_of_ingredients;
	}

	public string[] GetIngredientOrderList(){
		string[] ingredient_order_list = IngredientOrderList;
		return ingredient_order_list;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
