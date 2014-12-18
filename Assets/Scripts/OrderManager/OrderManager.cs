using UnityEngine;
using System.Collections;

public class OrderManager : MonoBehaviour {

	[ SerializeField ] GameObject
		ClientManagerPrefab,
		ClientManagerPosition,
		RecipeManagerPrefab;

	GameObject
		ClientManager,
		RecipeManager;

	bool
		LeftHasOrder,
		RightHasOrder;

	GameObject
		LeftOrder,
		RightOrder;

	GameObject
		VisibleOrderManager;

	bool
		LeftPlayerShot,
		RightPlayerShot;

	// Use this for initialization
	void Start () {
	
	}

	public void SetUpOrderManager(GameObject visible_order_manager){

		ResetClientManager();
		ResetRecipeManager();

		ResetLeftOrder();
		ResetRightOrder();

		this.GetComponent<DifficultyManager>().SetUpDifficultyManager();

		VisibleOrderManager = visible_order_manager;

		LeftPlayerShot = false;
		RightPlayerShot = false;
	}

	public void StartOrderManager(){

		ResetClientManager();
		ResetRecipeManager();

		InstantiateClientManager();
		InstantiateRecipeManager();

		ResetLeftOrder();
		ResetRightOrder();

		LeftPlayerShot = false;
		RightPlayerShot = false;
	}

	public void InstantiateClientManager(){

		Quaternion client_manager_orientation = ClientManagerPrefab.transform.rotation;

		ClientManager = Instantiate(ClientManagerPrefab,ClientManagerPosition.transform.position,client_manager_orientation) as GameObject;
		ClientManager.transform.parent = this.transform;
		ClientManager.name = "Client_Manager";

		ClientManager.GetComponent<ClientManager>().SetUpClientManager();
	}

	public void ResetClientManager(){

		GameObject client_manager = ClientManager;
		ClientManager = null;
		Destroy(client_manager);
	}

	public void InstantiateRecipeManager(){

		RecipeManager = Instantiate(RecipeManagerPrefab,this.transform.position,Quaternion.identity) as GameObject;
		RecipeManager.transform.parent = this.transform;
		RecipeManager.name = "Recipe_Manager";

		RecipeManager.GetComponent<RecipeManager>().SetUpRecipeManager();
	}

	public void ResetRecipeManager(){

		GameObject recipe_manager = RecipeManager;
		RecipeManager = null;
		Destroy(recipe_manager);
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

	public void InstantiateLeftOrder(){

		if (LeftOrder != null){
			ResetLeftOrder();
		}

		int left_order_id = this.GetComponent<DifficultyManager>().GetLeftRecipeID();
		LeftOrder = RecipeManager.GetComponent<RecipeManager>().InstantiateRecipeObject(left_order_id);

		VisibleOrderManager.GetComponent<VisibleOrderManager>().PushNewPlate(0);
	}

	public void InstantiateRightOrder(){
		
		if (RightOrder != null){
			ResetRightOrder();
		}

		int right_order_id = this.GetComponent<DifficultyManager>().GetLeftRecipeID();
		RightOrder = RecipeManager.GetComponent<RecipeManager>().InstantiateRecipeObject(right_order_id);

		VisibleOrderManager.GetComponent<VisibleOrderManager>().PushNewPlate(1);
	}

	public GameObject GetLeftOrder(){
		GameObject left_order = LeftOrder;
		return left_order;
	}

	public GameObject GetRightOrder(){
		GameObject right_order = RightOrder;
		return right_order;
	}

	public bool GetLeftPlayerShot(){
		bool left_player_shot = LeftPlayerShot;
		return left_player_shot;
	}

	public bool GetRightPlayerShot(){
		bool right_player_shot = RightPlayerShot;
		return right_player_shot;
	}

	public void OrderSent(bool left_order){

		GameObject order_object = new GameObject();

		if (left_order){
			order_object = LeftOrder;
		}
		else if (!left_order){
			order_object = RightOrder;
		}

		ClientManager.GetComponent<ClientManager>().TriggerClientServed(left_order);
	}
	
	// Update is called once per frame
	void Update () {

		UpdateOrderManager();
	}

	public void UpdateOrderManager(){

		if (!LeftHasOrder){

			if (ClientManager != null){

				if (ClientManager.GetComponent<ClientManager>().GetLeftClientAtCounter()){

					LeftHasOrder = true;
					InstantiateLeftOrder();
				}
			}
		}
		else if (LeftHasOrder){

			if (ClientManager.GetComponent<ClientManager>().GetLeftClientObjectAtCounter().GetComponent<ClientObject>().GetIsShooting()){
				LeftPlayerShot = true;
			}
		}

		if (!RightHasOrder){

			if (ClientManager != null){

				if (ClientManager.GetComponent<ClientManager>().GetRightClientAtCounter()){

					RightHasOrder = true;
					InstantiateRightOrder();
				}
			}
		}
		else if (RightHasOrder){
			
			if (ClientManager.GetComponent<ClientManager>().GetRightClientObjectAtCounter().GetComponent<ClientObject>().GetIsShooting()){
				RightPlayerShot = true;
			}
		}
	}
}
