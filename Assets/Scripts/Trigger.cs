using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {

	[SerializeField]
	private GameObject ToppingPrefab;

	public GameObject GetTopping()
	{
		return ToppingPrefab;
	}

	void OnTriggerStay(Collider other)
	{
		Hand hand;
		hand = other.gameObject.GetComponent<Hand>();

		if ( hand != null && !hand.HasTopping() )
		{
			Bounds this_bounds = collider.bounds;
			Bounds hand_bounds = hand.collider.bounds;

			if( this_bounds.Contains( hand_bounds.center ) &&
			   this_bounds.min.x < hand_bounds.min.x &&
			   this_bounds.min.y < hand_bounds.min.y &&
			   this_bounds.min.z < hand_bounds.min.z &&
			   this_bounds.max.x > hand_bounds.max.x &&
			   this_bounds.max.y > hand_bounds.max.y &&
			   this_bounds.max.z > hand_bounds.max.z &&
			   ToppingPrefab != null
		    )
			{
				hand.GrabTopping( ToppingPrefab );
			}
		}
	}
}
