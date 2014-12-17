using UnityEngine;
using System.Collections;

public enum ToppingType
{
	TOMATO,
	LETTUCE,
	CHEESE,
	BUN
}

public class Topping : MonoBehaviour {

	[SerializeField]
	private ToppingType _Type;

	public ToppingType Type
	{ 
		get { return _Type; }		
	}
}

