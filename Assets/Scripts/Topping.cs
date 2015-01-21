using UnityEngine;
using System.Collections;

public enum ToppingType
{
	TOMATO,
	LETTUCE,
	CHEESE,
	BUN,
	PATTY,
	PICKLE,
	JALAPENO,
	BACON,
	ONION
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

	public string GetName()
	{
		switch ( _Type )
		{
			case ToppingType.BUN :
			{
				return "bun";
			}
			break;

			case ToppingType.CHEESE :
			{
				return "cheese";
			}
			break;

			case ToppingType.LETTUCE :
			{
				return "salad";
			}
			break;

			case ToppingType.PATTY :
			{
				return "burger";
			}
			break;

			case ToppingType.TOMATO :
			{
				return "tomato";
			}
			break;

			case ToppingType.BACON :
			{
				return "bacon";
			}
			break;

			case ToppingType.JALAPENO :
			{
				return "jalapenos";
			}
			break;

			case ToppingType.ONION :
			{
				return "onion";
			}
			break;

			case ToppingType.PICKLE :
			{
				return "pickle";
			}
			break;
		}

		return "";
	}
}

