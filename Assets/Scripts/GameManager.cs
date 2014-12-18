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
	GameObject VisibleOrderManagerPrefab;
	GameObject VisibleOrderManager;

	[ SerializeField ] GameObject
		OrderManagerPrefab;

	GameObject
		OrderManager;

	[SerializeField]
	GameObject ConveyerPrefab;
	[SerializeField]
	GameObject ConveyerPosition;
	GameObject ConveyerManager;

	// Use this for initialization
	void Start ()
	{
		ToppingDispenser = new GameObject[2];
		ToppingDispenser [0] = (GameObject)Instantiate (LeftToppingDispenserPrefab, LeftDispenserPosition.transform.position, Quaternion.identity);
		ToppingDispenser [1] = (GameObject)Instantiate (RightToppingDispenserPrefab, RightDispenserPosition.transform.position, Quaternion.identity);
		
		ArmsManager = (GameObject)Instantiate (ArmsManagerPrefab);
		ArmsManager.GetComponent< ArmsManager > ().SetCollider (ToppingDispenser [0].GetComponent< BoxCollider > (), ToppingDispenser [1].GetComponent< BoxCollider > ());

		VisibleOrderManager = (GameObject)Instantiate (VisibleOrderManagerPrefab);
		
		ConveyerManager = (GameObject)Instantiate (ConveyerPrefab, ConveyerPosition.transform.position, Quaternion.identity);
		
		InstantiateOrderManager();
	}

	public void InstantiateOrderManager(){

		OrderManager = Instantiate(OrderManagerPrefab,Vector3.zero,Quaternion.identity) as GameObject;
		OrderManager.transform.parent = this.transform;
		OrderManager.name = "Order_Manager";

		OrderManager.GetComponent<OrderManager>().SetUpOrderManager(VisibleOrderManager);
		OrderManager.GetComponent<OrderManager>().StartOrderManager();
	}

	public void TriggerOrderSent(bool left_order_sent){

		OrderManager.GetComponent<OrderManager>().OrderSent(left_order_sent);
	}

	// Update is called once per frame
	void Update ()
	{
		if (OrderManager.GetComponent<OrderManager>().GetLeftPlayerShot()){

		}
		else if (OrderManager.GetComponent<OrderManager>().GetLeftPlayerShot()){

		}
	}
}
