using UnityEngine;
using System.Collections;

public class ClientObject : MonoBehaviour {

	[ SerializeField ] GameObject[]
		ClientStates = new GameObject[4];

	int
		CurrentState;

	[ SerializeField ] int
		MaximumState;

	[ SerializeField ] float[]
		StatesTimer = new float[4];

	float
		StateClock;

	[ SerializeField ] float
		MoveSpeed;

	[ SerializeField ] Vector3
		QueueDirection,
		ExitDirection;

	Vector3
		MoveDirection;

	bool
		InLeftQueue;

	bool
		IsServed,
		IsWaiting,
		IsMoving,
		IsDone,
		IsShooting;

	[ SerializeField ] float
		WaitingTimer;

	// Use this for initialization
	void Start () {
	
	}

	public void SetUpClientObject(bool in_left_queue){

		InLeftQueue = in_left_queue;

		IsServed = false;
		IsWaiting = false;
		IsMoving = false;
		IsDone = false;

		IsShooting = false;

		MoveDirection = QueueDirection;

		CurrentState = 0;

		UpdateClientState(CurrentState);
	}

	public void UpdateClientState(int new_client_state){

		for (int i=0; i < ClientStates.Length; i++){

			if (i == new_client_state){
				ClientStates[i].renderer.enabled = true;
			}
			else if (i != new_client_state){
				ClientStates[i].renderer.enabled = false;
			}
		}
	}

	public void StartClientMoving(){

		IsMoving = true;
	}

	public void StopClientMoving(){

		IsMoving = false;
	}

	public void TriggerClientServed(){

		IsServed = true;

		MoveDirection = ExitDirection;

		if (!InLeftQueue){

			MoveDirection.x = - MoveDirection.x;
		}

		StartClientMoving();
	}

	public bool GetIsDone(){
		bool is_done = IsDone;
		return is_done;
	}

	public bool GetIsWaiting(){
		bool is_waiting = IsWaiting;
		return is_waiting;
	}

	public bool GetIsServed(){
		bool is_served = IsServed;
		return is_served;
	}

	public bool GetIsShooting(){
		bool is_shooting = IsShooting;
		return is_shooting;
	}

	// Update is called once per frame
	void Update () {

		if (IsMoving){

			Vector3 client_position = this.transform.position;
			client_position += MoveDirection * Time.deltaTime;
			this.transform.position = client_position;
		}
		else if (IsWaiting){

			StateClock += Time.deltaTime;

			if (StateClock >= StatesTimer[CurrentState]){

				CurrentState++;
				StateClock = 0.0f;

				if (CurrentState >= MaximumState){

					CurrentState = MaximumState - 1;
					IsShooting = true;

				}

				UpdateClientState(CurrentState);
			}
		}
	}

	public void OnTriggerEnter(Collider collider_object){

		if (!IsServed){

			if (collider_object.gameObject.tag == "Client"){

				if (IsMoving){

					StopClientMoving();
				}
			}
			else if (collider_object.gameObject.tag == "ClientEnd"){

				IsWaiting = true;
				StopClientMoving();
			}
		}
		else if (IsServed){

			if (collider_object.gameObject.tag == "ClientExit"){

				if (!IsDone){

					IsDone = true;
					StopClientMoving();
				}
			}
		}
	}

	public void OnTriggerExit(Collider collider_object){

		if (!IsServed){

			if (collider_object.gameObject.tag == "Client"){

				StartClientMoving();
			}
		}
	}
}
