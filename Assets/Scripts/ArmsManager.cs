using UnityEngine;
using System.Collections;

public class ArmsManager : MonoBehaviour
{
	public GameObject[] PlayerArms;
	GameObject Topping;
	bool RequestGrabDrop;

	// Use this for initialization
	void Start ()
	{
		RequestGrabDrop = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(HasTopping())
		{
			if(RequestGrabDrop)
			{
				DropTopping();
			}
		}
	}

	public void SetCollider(BoxCollider box_collider, int player_index)
	{
		PlayerArms [player_index].GetComponent< PlayerArm > ().SetBoxCollider (box_collider);
	}

	public void RequestDropTopping()
	{
		RequestGrabDrop = true;
	}

	public void GrabTopping(GameObject prefab, GameObject hand)
	{
		Topping = (GameObject) GameObject.Instantiate( prefab );
		Topping.rigidbody.useGravity = false;
		Topping.rigidbody.isKinematic = true;
		Topping.transform.parent = hand.transform;
		Topping.transform.localPosition = Vector3.zero;
		Topping.tag = "BurgerItem";
	}
	
	void DropTopping()
	{
		Topping.transform.parent = null;
		Topping.transform.localPosition -= Vector3.up * 0.1f;
		Topping.rigidbody.isKinematic = false;
		Topping.rigidbody.useGravity = true;
		Topping = null;
		RequestGrabDrop = false;
	}
	
	public bool HasTopping()
	{
		return Topping != null;
	}
}
