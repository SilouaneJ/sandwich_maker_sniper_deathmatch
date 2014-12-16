using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public GameObject SandwichMakerBoothPrefab;
	private GameObject[] SandwichBoothTable;

	// Use this for initialization
	void Start ()
	{
		SandwichBoothTable = new GameObject[2];
		CreateScene ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void CreateScene()
	{
		SandwichBoothTable[0] = ((GameObject)Instantiate(SandwichMakerBoothPrefab));
		SandwichBoothTable[1] = ((GameObject)Instantiate(SandwichMakerBoothPrefab));

		InitializeBooth (0, new Vector3 (-1.3f, 0.4f, 2.0f));
		InitializeBooth (1, new Vector3 (1.3f, 0.4f, 2.0f));
	}

	void InitializeBooth(uint booth_index, Vector3 position)
	{
		SandwichBoothTable [booth_index].transform.position = position;
	}
}
