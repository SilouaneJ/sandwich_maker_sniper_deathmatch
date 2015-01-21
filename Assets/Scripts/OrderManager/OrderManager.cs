using UnityEngine;
using System.Collections;

public class OrderManager : MonoBehaviour {

	GameObject
		InterfaceManager;

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

	int
		LeftPlayerScore,
		RightPlayerScore;

	GameManager GameManager;

	// Use this for initialization
	void Start ()
	{
		GameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent< GameManager > ();
	}

	public void SetUpOrderManager(GameObject interface_manager, GameObject visible_order_manager){

		ResetClientManager();
		ResetRecipeManager();

		ResetLeftOrder();
		ResetRightOrder();

		this.GetComponent<DifficultyManager>().SetUpDifficultyManager();

		InterfaceManager = interface_manager;

		VisibleOrderManager = visible_order_manager;

		LeftPlayerShot = false;
		RightPlayerShot = false;
	}

	public void ResetOrderManager(){

		ResetClientManager();
		ResetRecipeManager();

		ResetLeftOrder();
		ResetRightOrder();
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

	public int GetLeftPlayerScore(){
		int left_player_score = LeftPlayerScore;
		return left_player_score;
	}

	public int GetRightPlayerScore(){
		int right_player_score = RightPlayerScore;
		return right_player_score;
	}

	public void OrderSent(bool left_order, string[] ingredient_list){

		GameManager.PlaySfx (SFX.Money);

		if (left_order){

			if(IsRecipeGood(ingredient_list, LeftOrder.GetComponent<RecipeObject>().GetRecipeIngredientList()))
			{
				Debug.Log("Good");
				LeftPlayerScore += LeftOrder.GetComponent<RecipeObject>().GetRecipeScore();
			}
			else
			{
				Debug.Log("Not good");
			}

			LeftHasOrder = false;
			ResetLeftOrder();
			InterfaceManager.GetComponent<InterfaceManager>().GetHUDManager().GetComponent<HUDManager>().ResetLeftOrder();
			InterfaceManager.GetComponent<InterfaceManager>().GetHUDManager().GetComponent<HUDManager>().UpdateHUDScore(true,LeftPlayerScore);
		}
		else if (!left_order){

			if(IsRecipeGood(ingredient_list, RightOrder.GetComponent<RecipeObject>().GetRecipeIngredientList()))
			{
				RightPlayerScore += RightOrder.GetComponent<RecipeObject>().GetRecipeScore();
			}

			RightHasOrder = false;
			ResetRightOrder();
			InterfaceManager.GetComponent<InterfaceManager>().GetHUDManager().GetComponent<HUDManager>().ResetRightOrder();
			InterfaceManager.GetComponent<InterfaceManager>().GetHUDManager().GetComponent<HUDManager>().UpdateHUDScore(false,RightPlayerScore);

		}

		Debug.Log(LeftHasOrder);

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

					if (!LeftHasOrder){

						LeftHasOrder = true;
						InstantiateLeftOrder();

						InterfaceManager.GetComponent<InterfaceManager>().GetHUDManager().GetComponent<HUDManager>().InstantiateLeftOrder(LeftOrder);
					}
				}
			}
		}
		else if (LeftHasOrder){

			if (ClientManager.GetComponent<ClientManager>().GetLeftClientObjectAtCounter().GetComponent<ClientObject>().GetIsShooting()){

				if (!LeftPlayerShot){

					LeftPlayerShot = true;
				}
			}
		}

		if (!RightHasOrder){

			if (ClientManager != null){

				if (ClientManager.GetComponent<ClientManager>().GetRightClientAtCounter()){

					if (!RightHasOrder){

						RightHasOrder = true;
						InstantiateRightOrder();
						
						InterfaceManager.GetComponent<InterfaceManager>().GetHUDManager().GetComponent<HUDManager>().InstantiateRightOrder(RightOrder);
					}
				}
			}
		}
		else if (RightHasOrder){
			
			if (ClientManager.GetComponent<ClientManager>().GetRightClientObjectAtCounter().GetComponent<ClientObject>().GetIsShooting()){

				if (!RightPlayerShot){

					RightPlayerShot = true;
				}
			}
		}
	}

	bool IsRecipeGood(string[] ingredient_list, string[] recipe_ingredient_list)
	{
		string[] no_repetition_ingredient_list;
		int[] amount_list;
		int unique_ingredient_count, ingredient_index;

		unique_ingredient_count = 0;
		ingredient_index = 0;

		for (int i = 0; i < recipe_ingredient_list.Length; ++i)
		{
			if (i == 0 || recipe_ingredient_list[i] != recipe_ingredient_list[i - 1])
			{
				++unique_ingredient_count;
			}
		}

		no_repetition_ingredient_list = new string[unique_ingredient_count];
		amount_list = new int[unique_ingredient_count];

		for (int i = 0; i < recipe_ingredient_list.Length; ++i)
		{
			if (i == 0 || recipe_ingredient_list[i] != recipe_ingredient_list[i - 1])
			{
				no_repetition_ingredient_list[ingredient_index] = recipe_ingredient_list[i];
				amount_list[ingredient_index] = 1;
				++ingredient_index;
			}
			else
			{
				++amount_list[ingredient_index - 1];
			}
		}

		for(int i = 0; i < no_repetition_ingredient_list.Length; ++i)
		{
			int current_ingredient_count;

			current_ingredient_count = 0;

			for(int j = 0; j < ingredient_list.Length; ++j)
			{
				if(no_repetition_ingredient_list[i] == ingredient_list[j])
				{
					++current_ingredient_count;
				}
			}

			if (amount_list[i] != current_ingredient_count)
			{
				return false;
			}
		}

		return true;
	}
}
