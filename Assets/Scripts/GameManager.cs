using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	GameObject ArmsManagerPrefab;
	GameObject ArmsManager;
	
	[SerializeField]
	GameObject LeftToppingDispenserPrefab;
	[SerializeField]
	GameObject RightToppingDispenserPrefab;

	[ SerializeField ] GameObject
		LeftDispenserPosition,
		RightDispenserPosition;

	GameObject[] ToppingDispenser;

	[SerializeField]
		GameObject OrderPrefab;
		GameObject Order;	// Use this for initialization

	[SerializeField]
		GameObject OrderManagerPrefab;
	GameObject OrderManager;

	[SerializeField]
		GameObject VisibleOrderManagerPrefab;
	GameObject VisibleOrderManager;

	[SerializeField]
		GameObject ConveyerPrefab;
	GameObject ConveyerManager;

	[SerializeField]
		GameObject ConveyerPosition;

	[ SerializeField ] GameObject
		InterfaceManager;

	bool GameIsStarted;

	void Start ()
	{
		GameIsStarted = false;

		InterfaceManager.GetComponent<InterfaceManager>().SetUpInterfaceManager();
	}

	public void InstantiateOrderManager(){

		OrderManager = Instantiate(OrderManagerPrefab, Vector3.zero,Quaternion.identity) as GameObject;
		OrderManager.transform.parent = this.transform;
		OrderManager.name = "Order_Manager";

		OrderManager.GetComponent<OrderManager>().SetUpOrderManager(VisibleOrderManager);
		OrderManager.GetComponent<OrderManager>().StartOrderManager();
		
		Order = (GameObject)Instantiate (OrderPrefab, new Vector3(-0.19f, -0.32f, -0.8f), Quaternion.identity);
	}

	public void TriggerOrderSent(bool left_order_sent)
	{
		OrderManager.GetComponent<OrderManager>().OrderSent(left_order_sent);

		if (left_order_sent){
			VisibleOrderManager.GetComponent<VisibleOrderManager>().PullOldPlate(0);
		}
		else if (!left_order_sent){
			VisibleOrderManager.GetComponent<VisibleOrderManager>().PullOldPlate(1);
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (GameIsStarted)
		{
			if (OrderManager.GetComponent<OrderManager>().GetLeftPlayerShot())
			{

			}
			else if (OrderManager.GetComponent<OrderManager>().GetRightPlayerShot())
			{

			}

			if(Input.GetKey ("p"))
			{
				StopGame();
			}
		}
		else
		{
			if(Input.GetKey ("o"))
			{
				StartGame();
			}
		}
	}

	void StartGame()
	{
		ToppingDispenser = new GameObject[2];
		ToppingDispenser [0] = (GameObject)Instantiate (LeftToppingDispenserPrefab, LeftDispenserPosition.transform.position, Quaternion.identity);
		ToppingDispenser [1] = (GameObject)Instantiate (RightToppingDispenserPrefab, RightDispenserPosition.transform.position, Quaternion.identity);
		
		ArmsManager = (GameObject)Instantiate (ArmsManagerPrefab);
		ArmsManager.GetComponent< ArmsManager > ().SetCollider (ToppingDispenser [0].GetComponent< BoxCollider > (), ToppingDispenser [1].GetComponent< BoxCollider > ());
		
		VisibleOrderManager = (GameObject)Instantiate (VisibleOrderManagerPrefab);
		
		ConveyerManager = (GameObject)Instantiate (ConveyerPrefab, ConveyerPosition.transform.position, Quaternion.identity);
		
		InstantiateOrderManager();

		GameIsStarted = true;
	}

	void StopGame()
	{
		this.GetComponent<SpawnManager> ().DestroyAll ();
		VisibleOrderManager.GetComponent< VisibleOrderManager > ().DestroyAll ();
		OrderManager.GetComponent< OrderManager > ().ResetOrderManager ();
		Destroy(ToppingDispenser[0]);
		Destroy(ToppingDispenser[1]);
		Destroy (ArmsManager);
		Destroy (VisibleOrderManager);
		Destroy (ConveyerManager);

		Destroy (OrderManager);
		Destroy (Order);

		GameIsStarted = false;
	}
}
