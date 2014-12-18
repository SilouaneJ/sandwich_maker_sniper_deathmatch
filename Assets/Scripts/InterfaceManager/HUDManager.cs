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

	public void ResetHUDManager(){

		ResetHUDObject();
		ResetLeftOrder();
		ResetRightOrder();
	}

	public void ResetHUDObject(){

		GameObject hud_object = HUDObject;
		HUDObject = null;
		Destroy(hud_object);
	}

	public void InstantiateHUDObject(){

		HUDObject = Instantiate(HUDObjectPrefab,this.transform.position,Quaternion.identity) as GameObject;
		HUDObject.transform.parent = this.transform;
		HUDObject.name = "HUD_Object";
	}

	public void StartHUDManager(){

		InstantiateHUDObject();
	}

	public void ResetLeftOrder(){

		GameObject left_order = LeftOrder;
		LeftOrder = null;
		Destroy(left_order);
	}
	
	public void ResetRightOrder(){
		
		GameObject right_order = RightOrder;
		RightOrder = null;
		Destroy(right_order);
	}

	public void InstantiateLeftOrder(GameObject left_order_object){

		LeftOrder = Instantiate(LeftOrderPrefab,this.transform.position,Quaternion.identity) as GameObject;
		LeftOrder.transform.parent = this.transform;
		LeftOrder.name = "Left_Order";

		string order_name = left_order_object.GetComponent<RecipeObject>().GetRecipeName();
		string[] ingredients_list = left_order_object.GetComponent<RecipeObject>().GetIngredientsList();
		int[] ingredients_amount = left_order_object.GetComponent<RecipeObject>().GetIngredientsAmount();

		LeftOrder.GetComponent<HUDOrderObject>().SetUpHUDOrderObject(order_name,ingredients_list,ingredients_amount);
	}

	public void InstantiateRightOrder(GameObject right_order_object){

		RightOrder = Instantiate(RightOrderPrefab,this.transform.position,Quaternion.identity) as GameObject;
		RightOrder.transform.parent = this.transform;
		RightOrder.name = "Right_Order";
		
		string order_name = right_order_object.GetComponent<RecipeObject>().GetRecipeName();
		string[] ingredients_list = right_order_object.GetComponent<RecipeObject>().GetIngredientsList();
		int[] ingredients_amount = right_order_object.GetComponent<RecipeObject>().GetIngredientsAmount();
		
		RightOrder.GetComponent<HUDOrderObject>().SetUpHUDOrderObject(order_name,ingredients_list,ingredients_amount);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateHUDManager(){


	}
}
