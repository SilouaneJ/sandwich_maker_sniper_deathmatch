using UnityEngine;
using System.Collections;

public enum ToppingType
{
	TOMATO,
	LETTUCE,
	CHEESE,
	BUN,
	PATTY
}

public class Topping : MonoBehaviour
{
	[SerializeField]
	private ToppingType _Type;
	[HideInInspector]
	public bool MustBeDestroyed;
	[HideInInspector]
	public bool ItIsInHand;

	public ToppingType Type
	{ 
		get { return _Type; }		
	}
}

