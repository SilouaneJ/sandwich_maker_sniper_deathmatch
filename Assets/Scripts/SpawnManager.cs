using UnityEngine;
using System.Collections;
using System.Collections.Generic;

struct PendantObject
{
	public GameObject Object;
	public float Timer;
	
	public PendantObject(GameObject game_object)
	{
		Object = game_object;
		Timer = 0.0f;
	}
}

public class SpawnManager : MonoBehaviour
{
	List< PendantObject > PendantObjectTable;

	// Use this for initialization
	void Start ()
	{
		PendantObjectTable = new List< PendantObject >();
	}
	
	// Update is called once per frame
	void Update ()
	{
		for(int i = 0; i < PendantObjectTable.Count; i++)
		{
			PendantObject pendant_object = PendantObjectTable[i];
			
			if (pendant_object.Object != null)
			{
				if(!pendant_object.Object.GetComponent< Topping >().ItIsInHand && pendant_object.Object.GetComponent< Topping >().MustBeDestroyed)
				{
					pendant_object.Timer += Time.deltaTime;
					PendantObjectTable[i] = pendant_object;

					if (pendant_object.Timer > 3.0f)
					{
						Destroy(PendantObjectTable[i].Object);
						PendantObjectTable.RemoveAt(i);
					}
				}
			}
			else
			{
				PendantObjectTable.RemoveAt(i);
			}
		}
	}

	public void AddPendantObject(GameObject pendant_object)
	{
		PendantObjectTable.Add (new PendantObject(pendant_object));
	}
}
