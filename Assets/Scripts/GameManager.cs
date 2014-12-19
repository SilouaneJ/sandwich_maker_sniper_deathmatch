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

	[SerializeField]
	GameObject AudioManagerPrefab;
	GameObject AudioManagerObject;
	AudioManager AudioManager;

	bool
		GameIsStarted,
		GameIsOver;

	void Start ()
	{
		GameIsStarted = false;
		GameIsOver = false;

		AudioManagerObject = (GameObject)Instantiate (AudioManagerPrefab);
		AudioManager = AudioManagerObject.GetComponent< AudioManager > ();

		InterfaceManager.GetComponent<InterfaceManager>().SetUpInterfaceManager();
		InterfaceManager.GetComponent<InterfaceManager>().LaunchMain();
	}

	public void InstantiateOrderManager(){

		OrderManager = Instantiate(OrderManagerPrefab, Vector3.zero,Quaternion.identity) as GameObject;
		OrderManager.transform.parent = this.transform;
		OrderManager.name = "Order_Manager";

		OrderManager.GetComponent<OrderManager>().SetUpOrderManager(InterfaceManager,VisibleOrderManager);
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
			if (!GameIsOver){

				if (OrderManager.GetComponent<OrderManager>().GetLeftPlayerShot())
				{
					GameIsOver = true;

					InterfaceManager.GetComponent<InterfaceManager>().ResetHUD();
					InterfaceManager.GetComponent<InterfaceManager>().LaunchScore(OrderManager);
				}
				else if (OrderManager.GetComponent<OrderManager>().GetRightPlayerShot())
				{
					GameIsOver = false;
					
					InterfaceManager.GetComponent<InterfaceManager>().ResetHUD();
					InterfaceManager.GetComponent<InterfaceManager>().LaunchScore(OrderManager);
				}
			}
			else if (GameIsOver){

				if(Input.GetKey ("p") || Input.GetButton("Fire1P1") || Input.GetButton("Fire1P2"))
				{
					StopGame();
				}
			}
		}
		else
		{
			if(Input.GetKey ("o") || Input.GetButton("Fire1P1") || Input.GetButton("Fire1P2"))
			{
				StartGame();
			}
		}

		if(Input.GetAxis("Trigger1P1") <= -1 || Input.GetAxis("Trigger1P2") <= -1)
		{
			AudioManager.NextMusic();
		}
		else if(Input.GetAxis("Trigger1P1") >= 1 || Input.GetAxis("Trigger1P2") >= 1)
		{
			AudioManager.PreviousMusic();
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

		AudioManager.PlayMusic ();

		GameIsStarted = true;

		InterfaceManager.GetComponent<InterfaceManager>().ResetMain();
		InterfaceManager.GetComponent<InterfaceManager>().LaunchHUD();
	}

	void StartScore(){

	}

	void StopGame()
	{
		AudioManager.StopMusic ();
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

		InterfaceManager.GetComponent<InterfaceManager>().ResetScore();
		InterfaceManager.GetComponent<InterfaceManager>().LaunchMain();
	}

	public void PlaySfx(SFX sfx_sound)
	{
		AudioManager.PlaySfx (sfx_sound);
	}
}
