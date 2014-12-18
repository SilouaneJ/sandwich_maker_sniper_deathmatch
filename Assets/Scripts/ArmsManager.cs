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

	public void SetCollider(BoxCollider left_box_collider, BoxCollider right_box_collider)
	{
		PlayerArms [0].GetComponent< PlayerArm > ().SetBoxCollider (left_box_collider, right_box_collider);
		PlayerArms [1].GetComponent< PlayerArm > ().SetBoxCollider (left_box_collider, right_box_collider);
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
		Topping.transform.localPosition -= Vector3.up * 0.05f;
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
