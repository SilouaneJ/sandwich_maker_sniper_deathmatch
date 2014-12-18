using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDOrderObject : MonoBehaviour {

	[ SerializeField ] Sprite
		BurgerImage,
		SaladImage,
		TomatoImage,
		CheeseImage,
		BaconImage,
		OnionImage,
		PickleImage,
		JalapenosImage;

	[ SerializeField ] GameObject
		RecipeName,
		RecipeNameShadow,
		RecipeNotes;

	[ SerializeField ] GameObject[]
		IngredientsArray = new GameObject[5],
		IngredientsAmount = new GameObject[5];

	// Use this for initialization
	void Start () {
	
	}

	public void SetUpHUDOrderObject(string recipe_name, string[] ingredients_list, int[] ingredients_amount){

		RecipeName.GetComponent<Text>().text = recipe_name;
		RecipeNameShadow.GetComponent<Text>().text = recipe_name;

		for (int i=0; i < ingredients_list.Length; i++){

			if (ingredients_list[i] != "nothing" && ingredients_list[i] != ""){

				Debug.Log(ingredients_list[i] + " with " + ingredients_amount[i]);

				IngredientsAmount[i].GetComponent<Text>().text = "X" + ingredients_amount[i].ToString();

				if (ingredients_list[i] == "burger"){

					IngredientsArray[i].GetComponent<Image>().sprite = BurgerImage;
				}
				if (ingredients_list[i] == "salad"){
					
					IngredientsArray[i].GetComponent<Image>().sprite = SaladImage;
				}
				else if (ingredients_list[i] == "tomato"){
					
					IngredientsArray[i].GetComponent<Image>().sprite = TomatoImage;
				}
				else if (ingredients_list[i] == "cheese"){
					
					IngredientsArray[i].GetComponent<Image>().sprite = CheeseImage;
				}
				else if (ingredients_list[i] == "bacon"){
					
					IngredientsArray[i].GetComponent<Image>().sprite = BaconImage;
				}
				else if (ingredients_list[i] == "onion"){
					
					IngredientsArray[i].GetComponent<Image>().sprite = OnionImage;
				}
				else if (ingredients_list[i] == "pickle"){
					
					IngredientsArray[i].GetComponent<Image>().sprite = PickleImage;
				}
				else if (ingredients_list[i] == "jalapenos"){
					
					IngredientsArray[i].GetComponent<Image>().sprite = JalapenosImage;
				}
			}
			else if (ingredients_list[i] == "nothing" || ingredients_list[i] == ""){

				if (i < 5){

					IngredientsAmount[i].GetComponent<Text>().text = "";
					IngredientsArray[i].GetComponent<Image>().enabled = false;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
