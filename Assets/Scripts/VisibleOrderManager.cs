using UnityEngine;
using System.Collections;
using System.Collections.Generic;

enum MoveState
{
	In,
	StepIn,
	StepOut,
	Out
};

struct VisibleOrder
{
	public GameObject Order;
	public MoveState CurrentState;
	public Vector3[] InOutPositions;
	public float CurrentSpeed;
	public Order OrderObject;

	public VisibleOrder(Vector3 in_pos, Vector3 out_pos, GameObject prefab)
	{
		CurrentState = MoveState.Out;
		InOutPositions = new Vector3[2];
		InOutPositions [0] = in_pos;
		InOutPositions [1] = out_pos;
		CurrentSpeed = 0.0f;

		Order = (GameObject)GameObject.Instantiate (prefab, InOutPositions[1], Quaternion.identity);
		OrderObject = Order.GetComponent< Order > ();
	}
}

public class VisibleOrderManager : MonoBehaviour 
{
	[SerializeField]
	GameObject OrderPrefab;
	VisibleOrder[] VisibleOrders;

	// Use this for initialization
	void Start ()
	{
		VisibleOrders = new VisibleOrder[2];
		VisibleOrders [0] = new VisibleOrder (new Vector3 (-0.19f, -0.32f, -0.8f), new Vector3 (-0.5f, -0.32f, -0.8f), OrderPrefab);
		VisibleOrders [1] = new VisibleOrder (new Vector3 (0.19f, -0.32f, -0.8f), new Vector3 (0.5f, -0.32f, -0.8f), OrderPrefab);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKey ("e"))
		{
			PushNewPlate(0);
			PushNewPlate(1);
		}

		if(Input.GetKey ("t"))
		{
			PullOldPlate(0);
			PullOldPlate(1);
		}

		for( int i = 0; i < VisibleOrders.Length; i++)
		{
			Vector3 current_position;
			MoveState move_state = VisibleOrders[i].CurrentState;

			current_position = VisibleOrders[i].Order.transform.position;

			switch(move_state)
			{
				case MoveState.In:
				{
				}
				break;

				case MoveState.Out:
				{
				}
				break;

				case MoveState.StepIn:
				{
					if( UpdatePosition(
							ref current_position,
							ref VisibleOrders[i].CurrentSpeed,
							VisibleOrders[i].InOutPositions[0],
							3.5f,
							3.5f
							)
				   	)
					{
						VisibleOrders[i].CurrentState = MoveState.In;
					}
				}
				break;

				case MoveState.StepOut:
				{
					if( UpdatePosition(
							ref current_position,
							ref VisibleOrders[i].CurrentSpeed,
							VisibleOrders[i].InOutPositions[1],
							3.5f,
							3.5f
							)
				   	)
					{
						VisibleOrders[i].CurrentState = MoveState.Out;
						VisibleOrders[i].OrderObject.DestroyAllToppings();
					}
				}
				break;
			}

			VisibleOrders[i].Order.transform.position = current_position;
		}
	}
	
	bool UpdatePosition(ref Vector3 current_position, ref float current_speed, Vector3 got_to_position, float max_speed, float acceleration)
	{
		float previous_speed, remaining_distance, traveled_distance;
		Vector3 direction;
		bool it_is_done;

		it_is_done = false;
		
		direction = got_to_position - current_position;
		remaining_distance = Vector3.Magnitude(direction);
		direction.Normalize();
		
		previous_speed = current_speed;
		
		if(current_speed < max_speed)
		{
			current_speed += acceleration * Time.deltaTime;
			current_speed = Mathf.Min (current_speed, max_speed);
		}
		
		traveled_distance = Time.deltaTime * (previous_speed + current_speed) * 0.5f;
		
		if(traveled_distance >= remaining_distance)
		{
			traveled_distance = remaining_distance;
			current_speed = 0.0f;

			it_is_done = true;
		}
		
		current_position += traveled_distance * direction;

		return it_is_done;
	}

	void PushNewPlate(int player_index)
	{
		VisibleOrders[player_index].CurrentState = MoveState.StepIn;
	}
	
	void PullOldPlate(int player_index)
	{
		VisibleOrders[player_index].CurrentState = MoveState.StepOut;
		VisibleOrders [player_index].OrderObject.FreezeAllToppings ();
	}
}
