﻿using UnityEngine;
using System.Collections;

public class SniperManager : MonoBehaviour
{
	public GameObject CrossHair;
	public Camera SniperCamera;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3
			current_position;
		current_position = CrossHair.transform.position;
		//current_position.x += Time.deltaTime * 0.1f;
		current_position.x = 0.0f;
		CrossHair.transform.position = current_position;
	}
}
