using UnityEngine;
using System.Collections;

public class ClientManager : MonoBehaviour {

	[ SerializeField ] GameObject
		ClientObjectPrefab;

	GameObject[]
		LeftQueue = new GameObject[10],
		RightQueue = new GameObject[10];

	[ SerializeField ] int
		ClientAmountMax;

	int
		LeftWaiting,
		RightWaiting,
		LeftAmount,
		RightAmount,
		ClientCounter;

	bool
		LeftClientAtCounter,
		RightClientAtCounter;

	[ SerializeField ] float[]
		QueueTimer = new float[5];

	float
		LeftClock,
		RightClock;

	[ SerializeField ] GameObject
		LeftStartTrigger,
		RightStartTrigger;

	[ SerializeField ] GameObject
		LeftEndTrigger,
		RightEndTrigger;

	[ SerializeField ] GameObject
		LeftExitTrigger,
		RightExitTrigger;

	// Use this for initialization
	void Start () {

		SetUpClientManager();
		StartClientManager();
	}

	public void SetUpClientManager(){

		ResetClientQueue();
	}

	public void StartClientManager(){

		ClientCounter = 0;

		ResetClientQueue();

		InstantiateClient(true,LeftStartTrigger.transform.position,LeftQueue);
		InstantiateClient(false,RightStartTrigger.transform.position,RightQueue);

		LeftAmount = 1;
		RightAmount = 1;

		LeftWaiting = -1;
		RightWaiting = -1;
	}

	public void ResetClientQueue(){

		ResetQueue(LeftQueue);
		ResetQueue(RightQueue);
	}

	public void ResetQueue(GameObject[] client_queue){

		for (int i=0; i < 10; i++){

			if (client_queue[i] != null){

				ResetClientObject(i,client_queue);
			}
		}
	}

	public void ResetClientObject(int client_id, GameObject[] client_queue){

		GameObject client_object = client_queue[client_id];
		client_queue[client_id] = null;
		Destroy(client_object);
	}

	public void InstantiateClient(bool left_queue, Vector3 client_position, GameObject[] client_queue){

		bool client_instantiated = false;

		for (int i=0; i < 10; i++){
			
			if (!client_instantiated){
				
				if (client_queue[i] == null){

					client_instantiated = true;

					client_queue[i] = Instantiate(ClientObjectPrefab,client_position,Quaternion.identity) as GameObject;
					client_queue[i].transform.parent = this.transform;
					client_queue[i].name = "Client_" + ClientCounter;

					client_queue[i].GetComponent<ClientObject>().SetUpClientObject(left_queue);
					client_queue[i].GetComponent<ClientObject>().StartClientMoving();

					if (left_queue){

						LeftAmount++;

						if (LeftAmount >= ClientAmountMax){
							LeftAmount = ClientAmountMax - 1;
						}
					}
					else if (!left_queue){

						RightAmount++;

						if (RightAmount >= ClientAmountMax){
							RightAmount = ClientAmountMax - 1;
						}
					}

					ClientCounter++;
				}
			}
		}
	}

	public void TriggerClientServed(bool left_queue_served){

		if (left_queue_served){

			if (LeftWaiting >= 0){

				LeftQueue[LeftWaiting].GetComponent<ClientObject>().TriggerClientServed();
				LeftAmount--;

				if (LeftAmount < 0){
					LeftAmount = 0;
				}

				LeftWaiting = -1;
				LeftClientAtCounter = false;
			}
		}
		else if (!left_queue_served){

			if (RightWaiting >= 0){

				RightQueue[RightWaiting].GetComponent<ClientObject>().TriggerClientServed();
				RightAmount--;

				if (RightAmount < 0){
					RightAmount = 0;
				}

				RightWaiting = -1;
				RightClientAtCounter = false;
			}
		}
	}

	public bool GetLeftClientAtCounter(){
		bool left_client_at_counter = LeftClientAtCounter;
		return left_client_at_counter;
	}

	public bool GetRightClientAtCounter(){
		bool right_client_at_counter = RightClientAtCounter;
		return right_client_at_counter;
	}

	public GameObject GetLeftClientObjectAtCounter(){
		GameObject left_client_object_at_counter = LeftQueue[LeftWaiting];
		return left_client_object_at_counter;
	}

	public GameObject GetRightClientObjectAtCounter(){
		GameObject right_client_object_at_counter = RightQueue[RightWaiting];
		return right_client_object_at_counter;
	}

	// Update is called once per frame
	void Update () {

		//HackInput();
		UpdateClientManager();
	}

	public void UpdateClientManager(){

		LeftClock += Time.deltaTime;
		RightClock += Time.deltaTime;

		if (LeftClock >= QueueTimer[LeftAmount]){

			if (LeftAmount < ClientAmountMax){
				InstantiateClient(true,LeftStartTrigger.transform.position,LeftQueue);
			}

			LeftClock = 0.0f;
		}

		if (RightClock >= QueueTimer[RightAmount]){

			if (RightAmount < ClientAmountMax){
				InstantiateClient(false,RightStartTrigger.transform.position,RightQueue);
			}
			
			RightClock = 0.0f;
		}

		for (int i=0; i < 10; i++){

			if (LeftQueue[i] != null){

				if (LeftQueue[i].GetComponent<ClientObject>().GetIsWaiting()){
					if (!LeftQueue[i].GetComponent<ClientObject>().GetIsServed()){

						LeftWaiting = i;

						if (!LeftClientAtCounter){
							LeftClientAtCounter = true;
						}
					}
				}

				if (LeftQueue[i].GetComponent<ClientObject>().GetIsDone()){
					ResetClientObject(i,LeftQueue);
				}
			}

			if (RightQueue[i] != null){

				if (RightQueue[i].GetComponent<ClientObject>().GetIsWaiting()){
					if (!RightQueue[i].GetComponent<ClientObject>().GetIsServed()){

						RightWaiting = i;

						if (!RightClientAtCounter){
							RightClientAtCounter = true;
						}
					}
				}

				if (RightQueue[i].GetComponent<ClientObject>().GetIsDone()){
					ResetClientObject(i,RightQueue);
				}
			}
		}
	}

	public void HackInput(){

		if (Input.GetKeyDown("a")){
			TriggerClientServed(true);
		}
		if (Input.GetKeyDown("p")){
			TriggerClientServed(false);
		}
	}
}
