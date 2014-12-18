using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Order : MonoBehaviour 
{
	private List<GameObject> ToppingTable;

	// Use this for initialization
	void Start ()
	{
		ToppingTable = new List<GameObject>();
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
			ToppingTable.Add( collider.gameObject );
			topping.MustBeDestroyed = false;
		}
	}

	void OnTriggerExit(Collider collider)
	{
		Topping topping = collider.gameObject.GetComponent<Topping>();

		if( topping != null )
		{
			ToppingTable.Remove( collider.gameObject );
			topping.MustBeDestroyed = true;
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
		foreach(GameObject topping in ToppingTable)
		{
			topping.transform.parent = this.transform;
		}
	}

	public void DestroyAllToppings()
	{
		foreach(GameObject topping in ToppingTable)
		{
			Destroy(topping);
		}

		ToppingTable.Clear ();
	}
}
