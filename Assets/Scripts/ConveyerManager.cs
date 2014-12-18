using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConveyerManager : MonoBehaviour
{
	[SerializeField]
	GameObject Model;
	private List<GameObject> ObjectTable;
	Vector2 TextureOffset;
	float TextureSpeed, ObjectSpeed;
	[SerializeField]
	GameObject PattyPrefab;
	float PattyTimer, NextPattySpawnTime;
	float MinPattySpawnTime = 3.0f;
	float MaxPattySpawnTime = 5.0f;
	float MinPattyXPosition = -0.04f;
	float MaxPattyXPosition = 0.04f;
	[SerializeField]
	GameObject SpawnPosition;
	SpawnManager SpawnManager;

	// Use this for initialization
	void Start ()
	{
		ObjectTable = new List< GameObject > ();
		TextureOffset = Vector2.zero;
		TextureSpeed = 0.1f;
		ObjectSpeed = 0.043f;
		PattyTimer = 0.0f;
		NextPattySpawnTime = Random.Range (MinPattySpawnTime, MaxPattySpawnTime);
		SpawnPosition = GameObject.FindGameObjectWithTag ("PattySpawn");
		SpawnManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent< SpawnManager >();
	}
	
	// Update is called once per frame
	void Update ()
	{
		TextureOffset -= Vector2.up * TextureSpeed * Time.deltaTime;
		Model.renderer.material.SetTextureOffset ("_MainTex", TextureOffset);

		foreach(GameObject game_object in ObjectTable)
		{
			Vector3 current_velocity;

			current_velocity = -ObjectSpeed * Time.deltaTime * transform.forward;
			game_object.transform.position = game_object.transform.position + current_velocity;
		}

		PattyTimer += Time.deltaTime;

		if(PattyTimer >= MaxPattySpawnTime)
		{
			SpawnPatty(Random.Range (MinPattyXPosition, MaxPattyXPosition));
			PattyTimer = 0.0f;
			NextPattySpawnTime = Random.Range (MinPattySpawnTime, MaxPattySpawnTime);
		}
	}
	
	void OnTriggerEnter(Collider collider)
	{
		Topping topping = collider.gameObject.GetComponent<Topping>();
		
		if( topping != null )
		{
			ObjectTable.Add( collider.gameObject );
			collider.gameObject.GetComponent< Topping >().MustBeDestroyed = false;

			if(collider.gameObject.GetComponent< Topping >().Type == ToppingType.PATTY)
			{
				collider.gameObject.tag = "Patty";
			}
		}
	}
	
	void OnTriggerExit(Collider collider)
	{
		Topping topping = collider.gameObject.GetComponent<Topping>();
		
		if( topping != null )
		{
			ObjectTable.Remove( collider.gameObject );
			collider.gameObject.GetComponent< Topping >().MustBeDestroyed = true;
		}
	}

	public void SpawnPatty(float x_position)
	{
		Vector3 spawn_position;
		GameObject patty_object;

		spawn_position = SpawnPosition.transform.position;
		spawn_position.x = x_position;

		patty_object = (GameObject)Instantiate (PattyPrefab, spawn_position, Quaternion.identity);

		SpawnManager.AddPendantObject (patty_object);
	}
}
