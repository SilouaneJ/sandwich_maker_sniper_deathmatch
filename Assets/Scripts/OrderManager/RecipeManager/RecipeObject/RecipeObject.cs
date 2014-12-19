using UnityEngine;
using System.Collections;

public class RecipeObject : MonoBehaviour {

	[ SerializeField ] string
		RecipeName;

	[ SerializeField ] int
		AmountOfIngredients;

	[ SerializeField ] string[]
		IngredientOrderList = new string[10];

	int[] 
		IngredientsAmount = new int[10];

	string[]
		IngredientsList = new string[10];

	[ SerializeField ] int
		RecipeScore;

	// Use this for initialization
	void Start () {
	
	}

	public void SetUpRecipeObject(){

		int ingredients_counter = 0;

		for (int n=0; n < 10; n++){

			IngredientsList[n] = "nothing";
			IngredientsAmount[n] = 0;
		}

		for (int i=0; i < 10; i++){

			if (IngredientOrderList[i] != null){

				bool ingredient_in_list = false;

				for (int j=0; j < 10; j++){

					if (IngredientsList[j] == IngredientOrderList[i]){

						ingredient_in_list = true;
						IngredientsAmount[j] += 1;
					}
				}

				if (!ingredient_in_list){

					IngredientsList[ingredients_counter] = IngredientOrderList[i];
					IngredientsAmount[ingredients_counter] = 1;

					ingredients_counter++;
				}
			}
		}
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

	public int[] GetIngredientsAmount(){
		int[] ingredients_amount = IngredientsAmount;
		return ingredients_amount;
	}

	public string[] GetIngredientsList(){
		string[] ingredients_list = IngredientsList;
		return ingredients_list;
	}

	public int GetRecipeScore(){
		int recipe_score = RecipeScore;
		return recipe_score;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
