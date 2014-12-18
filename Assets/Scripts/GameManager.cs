using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	GameObject ArmsManagerPrefab;
	GameObject ArmsManager;
	
	[SerializeField]
	GameObject LeftToppingDispenserPrefab;
	[SerializeField]
	GameObject RightToppingDispenserPrefab;

	[ SerializeField ] GameObject
		LeftDispenserPosition,
		RightDispenserPosition;

	GameObject[] ToppingDispenser;

	[SerializeField]
	GameObject VisibleOrderManagerPrefab;
	GameObject VisibleOrderManager;

	// Use this for initialization
	void Start ()
	{
		ToppingDispenser = new GameObject[2];
		ToppingDispenser [0] = (GameObject)Instantiate (LeftToppingDispenserPrefab, LeftDispenserPosition.transform.position, Quaternion.identity);
		ToppingDispenser [1] = (GameObject)Instantiate (RightToppingDispenserPrefab, RightDispenserPosition.transform.position, Quaternion.identity);
		
		ArmsManager = (GameObject)Instantiate (ArmsManagerPrefab);
		ArmsManager.GetComponent< ArmsManager > ().SetCollider (ToppingDispenser [0].GetComponent< BoxCollider > (), ToppingDispenser [1].GetComponent< BoxCollider > ());

		VisibleOrderManager = (GameObject)Instantiate (VisibleOrderManagerPrefab);
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
}
