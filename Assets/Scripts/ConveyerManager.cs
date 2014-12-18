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

	// Use this for initialization
	void Start ()
	{
		ObjectTable = new List< GameObject > ();
		TextureOffset = Vector2.zero;
		TextureSpeed = 0.1f;
		ObjectSpeed = 0.043f;
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
	}
	
	void OnTriggerEnter(Collider collider)
	{
		Topping topping = collider.gameObject.GetComponent<Topping>();
		
		if( topping != null )
		{
			ObjectTable.Add( collider.gameObject );
		}
	}
	
	void OnTriggerExit(Collider collider)
	{
		Topping topping = collider.gameObject.GetComponent<Topping>();
		
		if( topping != null )
		{
			ObjectTable.Remove( collider.gameObject );
			Destroy (collider.gameObject);
		}
	}
}
