using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Order : MonoBehaviour 
{
	private List<Topping> ToppingTable;

	// Use this for initialization
	void Start ()
	{
		ToppingTable = new List<Topping>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	void OnTriggerEnter(Collider collider)
	{
		Topping topping = collider.gameObject.GetComponent<Topping>();

		if( topping != null )
		{
			ToppingTable.Add( topping );
		}
	}

	void OnTriggerExit(Collider collider)
	{
		Topping topping = collider.gameObject.GetComponent<Topping>();

		if( topping != null )
		{
			ToppingTable.Remove( topping );
		}
	}

	void OnCollisionEnter(Collision collision)
	{
	}

	void OnCollisionExit(Collision collision)
	{
	}

	public void FreezeAllToppings()
	{
		foreach(Topping topping in ToppingTable)
		{
			topping.transform.parent = this.transform;
		}
	}

	public void DestroyAllToppings()
	{
		Debug.Log ("Destroy ");
		foreach(Topping topping in ToppingTable)
		{
			Destroy(topping);
		}

		ToppingTable.Clear ();
	}
}
