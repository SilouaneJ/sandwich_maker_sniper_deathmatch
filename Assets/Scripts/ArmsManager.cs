using UnityEngine;
using System.Collections;

public class ArmsManager : MonoBehaviour
{
	public GameObject[] PlayerArms;
	GameObject[] Toppings;
	bool[] RequestGrabDrop;
	SpawnManager SpawnManager;

	// Use this for initialization
	void Start ()
	{
		Toppings = new GameObject[2];
		RequestGrabDrop = new bool[2];

		for(int i = 0; i < RequestGrabDrop.Length; i++)
		{
			RequestGrabDrop[i] = false;
		}

		SpawnManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent< SpawnManager >();
	}
	
	// Update is called once per frame
	void Update ()
	{
		for(int i = 0; i < Toppings.Length; i++)
		{
			GameObject topping = Toppings[i];

			if(topping != null)
			{
				if(RequestGrabDrop[i])
				{
					DropTopping(i);
				}
			}
		}
	}

	public void SetCollider(BoxCollider left_box_collider, BoxCollider right_box_collider)
	{
		PlayerArms [0].GetComponent< PlayerArm > ().SetBoxCollider (left_box_collider, right_box_collider);
		PlayerArms [1].GetComponent< PlayerArm > ().SetBoxCollider (left_box_collider, right_box_collider);
	}

	public void RequestDropTopping(int index)
	{
		RequestGrabDrop[index] = true;
	}

	public void GrabTopping(GameObject prefab, GameObject hand, int index)
	{
		Toppings[index] = (GameObject) GameObject.Instantiate( prefab );
		Toppings[index].GetComponent< Topping > ().MustBeDestroyed = true;
		Toppings[index].GetComponent< Topping > ().ItIsInHand = true;
		Toppings[index].rigidbody.useGravity = false;
		Toppings[index].rigidbody.isKinematic = true;
		Toppings[index].transform.parent = hand.transform;
		Toppings[index].transform.localPosition = Vector3.zero;
		Toppings[index].tag = "BurgerItem";
	}
	
	void DropTopping(int index)
	{
		SpawnManager.AddPendantObject (Toppings [index]);
		Toppings [index].GetComponent< Topping > ().ItIsInHand = false;
		Toppings[index].transform.parent = null;
		Toppings[index].transform.localPosition -= Vector3.up * 0.05f;
		Toppings[index].rigidbody.isKinematic = false;
		Toppings[index].rigidbody.useGravity = true;
		Toppings[index] = null;
		RequestGrabDrop[index] = false;
	}

	public void GrabPatty(GameObject burger, GameObject hand, int index)
	{
		Toppings [index] = burger;
		Toppings [index].GetComponent< Topping > ().MustBeDestroyed = true;
		Toppings [index].GetComponent< Topping > ().ItIsInHand = true;
		Toppings [index].rigidbody.useGravity = false;
		Toppings [index].rigidbody.isKinematic = true;
		Toppings [index].transform.parent = hand.transform;
		Toppings [index].transform.localPosition = Vector3.zero;
		Toppings [index].tag = "BurgerItem";
		SpawnManager.AddPendantObject (Toppings [index]);
	}
	
	public bool HasTopping(int index)
	{
		return Toppings[index] != null;
	}
}
