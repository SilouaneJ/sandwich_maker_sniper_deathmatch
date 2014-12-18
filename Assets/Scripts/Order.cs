using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Order : MonoBehaviour 
{
	private List<GameObject> ToppingTable;
	private int TopBunIndex;
	private GameManager GameManager;
	public bool ItIsLeft;

	// Use this for initialization
	void Start ()
	{
		ToppingTable = new List<GameObject>();
		TopBunIndex = -1;
		GameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent< GameManager >();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(TopBunIndex != -1 && !ToppingTable[TopBunIndex].GetComponent<Topping>().ItIsInHand)
		{
			//GameManager.TriggerOrderSent(ItIsLeft);
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		Topping topping = collider.gameObject.GetComponent<Topping>();

		if( topping != null )
		{
			ToppingTable.Add( collider.gameObject );
			topping.MustBeDestroyed = false;

			if(topping.Type == ToppingType.BUN)
			{
				TopBunIndex = ToppingTable.Count - 1;
			}
		}
	}

	void OnTriggerExit(Collider collider)
	{
		Topping topping = collider.gameObject.GetComponent<Topping>();

		if( topping != null )
		{
			ToppingTable.Remove( collider.gameObject );
			topping.MustBeDestroyed = true;

			if(topping.Type == ToppingType.BUN)
			{
				TopBunIndex = -1;
			}
		}
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
		TopBunIndex = -1;
	}
}
