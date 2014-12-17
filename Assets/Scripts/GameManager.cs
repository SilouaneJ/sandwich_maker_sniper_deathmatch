using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	GameObject ArmsManagerPrefab;
	GameObject ArmsManager;
	
	[SerializeField]
	GameObject ToppingDispenserPrefab;
	GameObject[] ToppingDispenser;

	// Use this for initialization
	void Start ()
	{
		ArmsManager = (GameObject)Instantiate (ArmsManagerPrefab);

		ToppingDispenser = new GameObject[2];
		ToppingDispenser [0] = (GameObject)Instantiate (ToppingDispenserPrefab, new Vector3(-0.19f, -0.3f, -0.6f), Quaternion.identity);
		ToppingDispenser [1] = (GameObject)Instantiate (ToppingDispenserPrefab, new Vector3(0.19f, -0.3f, -0.6f), Quaternion.identity);

		ArmsManager.GetComponent< ArmsManager > ().SetCollider (ToppingDispenser [0].GetComponent< BoxCollider > (), 0);
		ArmsManager.GetComponent< ArmsManager > ().SetCollider (ToppingDispenser [1].GetComponent< BoxCollider > (), 1);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
