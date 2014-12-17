using UnityEngine;
using System.Collections;
using System.Collections.Specialized;

enum ActionState
{
	Normal,
	Grab,
	Drop,
	BumpStart,
	BumpEnd,
	BumpDown,
	BumpedDown,
	BumpedWait,
	BumpedEnd,
	Slap,
	Slapped
};

public class PlayerArm : MonoBehaviour
{
	ActionState CurrentState;
	public int PlayerIndex;
	public GameObject ForeArm, Arm, Hand;
	public PlayerArm OtherPlayer;
	private Rigidbody ArmRigidBody;
	private string ForwardAxisName, SideAxisName, GrabDropButtonName, BumpButtonName, SlapButtonName;
	public Vector2 MinBound, MaxBound;
	private float NormalArmSpeed = 1.0f;
	private float MaxBumpStartVerticalSpeed = 2.5f;
	private float BumpStartVerticalAcceleration = 5.5f;
	private float MaxBumpDownVerticalSpeed = 2.5f;
	private float BumpDownVerticalAcceleration = 5.5f;
	private float MaxBumpEndVerticalSpeed = 0.4f;
	private float BumpEndVerticalAcceleration = 0.4f;
	private float MaxSlappedHorizontalSpeed = 80.0f;
	private float SlappedHorizontalAcceleration = 80.0f;
	private float CurrentSpeed;
	private float WaitTimer;
	private bool BumpedOtherHasBeenNotified;
	private Vector3 BumpStartPosition, BumpPreviousPosition, BumpEndPosition, SlappedPosition;
	private BoxCollider DisableDropCollider;

	// Use this for initialization
	void Start ()
	{
		CurrentState = ActionState.Normal;
		ForwardAxisName = "VerticalP" + PlayerIndex;
		SideAxisName = "HorizontalP" + PlayerIndex;
		GrabDropButtonName = "Fire1P" + PlayerIndex;
		BumpButtonName = "Fire2P" + PlayerIndex;
		SlapButtonName = "JumpP" + PlayerIndex;
		ArmRigidBody = this.GetComponent< Rigidbody > ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 current_arm_position, new_arm_position, move_direction;
		float move_distance;
		RaycastHit hit_info;
		
		current_arm_position = transform.position;
		new_arm_position = current_arm_position;

		switch(CurrentState)
		{
			case ActionState.Normal:
			{
				float delta_x, delta_z;
				
				delta_x = Input.GetAxis (SideAxisName) * Time.deltaTime * NormalArmSpeed;
				delta_z = Input.GetAxis (ForwardAxisName) * Time.deltaTime * NormalArmSpeed;
				
				new_arm_position.x += delta_x;
				new_arm_position.z += delta_z;
				
				new_arm_position = FilterArmPosition(new_arm_position);
				
				if (Input.GetButton(GrabDropButtonName))
				{
					GrabDropAction();
				}
				else if (Input.GetButtonUp(BumpButtonName))
				{
					BumpAction();
				}
				else if (Input.GetButtonUp(SlapButtonName))
				{
					SlapAction();
				}

				move_direction = new_arm_position - current_arm_position;
				move_distance = Vector3.Magnitude(move_direction);
				move_direction.Normalize ();

				if(ArmRigidBody.SweepTest(move_direction, out hit_info, move_distance) && hit_info.collider.gameObject.tag == GetOpponentName())
				{
					ArmRigidBody.MovePosition(current_arm_position + hit_info.distance * 0.3f * move_direction);
				}
				else
				{
					ArmRigidBody.MovePosition(new_arm_position);
				}
			}
			break;
		
			case ActionState.Grab:
			{
				CurrentState = ActionState.Normal;
			}
			break;

			case ActionState.Drop:
			{
				CurrentState = ActionState.Normal;
			}
			break;

			case ActionState.BumpStart:
			{
				UpdateArmPosition(
					ref new_arm_position,
					ref CurrentSpeed,
					BumpStartPosition,
					MaxBumpStartVerticalSpeed,
					BumpStartVerticalAcceleration,
					ActionState.BumpDown
					);

				ArmRigidBody.MovePosition(new_arm_position);
			}
			break;

			case ActionState.BumpDown:
			{
				UpdateArmPosition(
					ref new_arm_position,
					ref CurrentSpeed,
					BumpEndPosition,
					MaxBumpDownVerticalSpeed,
					BumpDownVerticalAcceleration,
					ActionState.BumpEnd
					);

				move_direction = new_arm_position - current_arm_position;
				move_distance = Vector3.Magnitude(move_direction);
				move_direction.Normalize ();

				if(!BumpedOtherHasBeenNotified && ArmRigidBody.SweepTest(move_direction, out hit_info, move_distance))
				{
					OtherPlayer.OnBumpedFromOtherPlayer();
					BumpedOtherHasBeenNotified = true;
				}

				ArmRigidBody.MovePosition(new_arm_position);
			}
			break;

			case ActionState.BumpEnd:
			{
				UpdateArmPosition(
					ref new_arm_position,
					ref CurrentSpeed,
					BumpPreviousPosition,
					MaxBumpEndVerticalSpeed,
					BumpEndVerticalAcceleration,
					ActionState.Normal
					);

				ArmRigidBody.MovePosition(new_arm_position);
			}
			break;

			case ActionState.BumpedDown:
			{
				UpdateArmPosition(
					ref new_arm_position,
					ref CurrentSpeed,
					BumpEndPosition,
					MaxBumpDownVerticalSpeed,
					BumpDownVerticalAcceleration,
					ActionState.BumpedWait
					);

				ArmRigidBody.MovePosition(new_arm_position);
				WaitTimer = 0.0f;
			}
			break;

			case ActionState.BumpedWait:
			{
				WaitTimer += Time.deltaTime;

				if(WaitTimer > 0.2f)
				{
					CurrentState = ActionState.BumpedEnd;
				}
			}
			break;

			case ActionState.BumpedEnd:
			{
				UpdateArmPosition(
					ref new_arm_position,
					ref CurrentSpeed,
					BumpPreviousPosition,
					MaxBumpEndVerticalSpeed,
					BumpEndVerticalAcceleration,
					ActionState.Normal
					);

				ArmRigidBody.MovePosition(new_arm_position);
			}
			break;

			case ActionState.Slap:
			{
				OtherPlayer.OnSlappedFromOtherPlayer();
				CurrentState = ActionState.Normal;
			}
			break;

			case ActionState.Slapped:
			{
				UpdateArmPosition(
					ref new_arm_position,
					ref CurrentSpeed,
					SlappedPosition,
					MaxSlappedHorizontalSpeed,
					SlappedHorizontalAcceleration,
					ActionState.Normal
					);

				ArmRigidBody.MovePosition(new_arm_position);
			}
			break;
		}
	}

	public void SetBoxCollider(BoxCollider box_collider)
	{
		DisableDropCollider = box_collider;
	}

	void GrabDropAction()
	{
		ArmsManager arms_manager;

		arms_manager = this.GetComponentInParent<ArmsManager> ();

		if(arms_manager.HasTopping())
		{
			if(!DisableDropCollider.bounds.Contains(Hand.transform.position))
			{
				CurrentState = ActionState.Drop;
				arms_manager.RequestDropTopping();
			}
		}
		else
		{
			RaycastHit[] hit_table;

			CurrentState = ActionState.Grab;

			hit_table = Physics.RaycastAll(new Ray(Hand.transform.position, -Vector3.up));
			
			if (hit_table.Length != 0)
			{
				foreach(RaycastHit hit in hit_table)
				{
					if (hit.collider != null && hit.collider.gameObject.GetComponent<Trigger>() != null)
					{
						Trigger current_topping;

						current_topping = hit.collider.gameObject.GetComponent<Trigger>();

						arms_manager.GrabTopping(current_topping.GetTopping(), Hand.gameObject);

						return;
					}
				}
			}
		}
	}
	
	void BumpAction()
	{
		BumpedOtherHasBeenNotified = false;
		BumpPreviousPosition = transform.position;
		BumpStartPosition = BumpPreviousPosition + GetBumpOffset();
		BumpEndPosition = BumpStartPosition + new Vector3 (0.0f, -0.1f, 0.0f);
		CurrentSpeed = 0.0f;
		CurrentState = ActionState.BumpStart;
	}
	
	void SlapAction()
	{
		Vector3 move_direction;
		RaycastHit hit_info;

		move_direction = GetOpponentDirection ();

		
		if(ArmRigidBody.SweepTest(move_direction, out hit_info, 0.1f) && hit_info.collider.gameObject.tag.Equals(GetOpponentName()))
		{
			CurrentState = ActionState.Slap;
		}
	}

	Vector3 FilterArmPosition(Vector3 arm_position)
	{
		Vector3 filtered_arm_position;

		filtered_arm_position = arm_position;

		if(filtered_arm_position.x < MinBound.x)
		{
			filtered_arm_position.x = MinBound.x;
		}
		
		if(filtered_arm_position.x > MaxBound.x)
		{
			filtered_arm_position.x = MaxBound.x;
		}
		
		if(filtered_arm_position.z < MinBound.y)
		{
			filtered_arm_position.z = MinBound.y;
		}
		
		if(filtered_arm_position.z > MaxBound.y)
		{
			filtered_arm_position.z = MaxBound.y;
		}

		return filtered_arm_position;
	}

	void UpdateArmPosition(ref Vector3 arm_position, ref float current_speed, Vector3 got_to_position, float max_speed, float acceleration, ActionState next_state)
	{
		float previous_speed, remaining_distance, traveled_distance;
		Vector3 direction;
		
		direction = got_to_position - transform.position;
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
			CurrentState = next_state;
		}
		
		arm_position += traveled_distance * direction;
	}
	
	Vector3 GetBumpOffset()
	{
		switch(PlayerIndex)
		{
			case 1:
			{
				return new Vector3 (0.05f, 0.06f, 0.0f);
			}

			case 2:
			{
				return new Vector3 (-0.05f, 0.06f, 0.0f);
			}
		}
		
		return new Vector3(0.0f, 0.0f, 0.0f);
	}

	Vector3 GetSlappedOffset()
	{
		switch(PlayerIndex)
		{
			case 1:
			{
				return new Vector3 (-0.05f, 0.0f, 0.0f);
			}

			case 2:
			{
				return new Vector3 (0.05f, 0.0f, 0.0f);
			}
		}
		
		return new Vector3(0.0f, 0.0f, 0.0f);
	}

	Vector3 GetOpponentDirection()
	{
		switch(PlayerIndex)
		{
			case 1:
			{
				return new Vector3 (1.0f, 0.0f, 0.0f);
			}

			case 2:
			{
				return new Vector3 (-1.0f, 0.0f, 0.0f);
			}
		}
		
		return new Vector3(0.0f, 0.0f, 0.0f);
	}
	
	string GetOpponentName()
	{
		switch(PlayerIndex)
		{
			case 1:
			{
				return "SecondPlayer";
			}
				
			case 2:
			{
				return "FirstPlayer";
			}
		}
		
		return "None";
	}

	void OnBumpedFromOtherPlayer()
	{
		if (CurrentState == ActionState.Normal)
		{
			BumpPreviousPosition = transform.position;
			BumpEndPosition = BumpPreviousPosition + new Vector3 (0.0f, -0.1f, 0.0f);
			CurrentSpeed = 0.0f;
			CurrentState = ActionState.BumpedDown;
		}
	}

	void OnSlappedFromOtherPlayer()
	{
		if (CurrentState == ActionState.Normal)
		{
			SlappedPosition = transform.position + GetSlappedOffset();
			CurrentSpeed = 0.0f;
			CurrentState = ActionState.Slapped;
		}
	}
}
