using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {

	[SerializeField]
	private MeshFilter Surface;

	private Vector3 SurfaceExtents;
	private Vector3 SurfaceCenter;

	private GameObject Topping;

	// Use this for initialization
	void Start () {
		SurfaceExtents = Surface.mesh.bounds.extents;
		SurfaceCenter = Surface.mesh.bounds.center;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 HalfViewportExtent;
		Vector3 MousePosition;
		Vector3 Position;

		HalfViewportExtent = new Vector3( Screen.width / 2 , Screen.height / 2, 0 );

		MousePosition = Input.mousePosition - HalfViewportExtent;
		MousePosition.x /= HalfViewportExtent.x;
		MousePosition.y /= HalfViewportExtent.y;

		Position = transform.position;
		Position.x = MousePosition.x * SurfaceExtents.x + SurfaceCenter.x;
		Position.z = MousePosition.y * SurfaceExtents.y + SurfaceCenter.z;
		transform.position = Position;

		if ( HasTopping() && Input.GetMouseButtonDown( 0 ) && Topping )
		{
			DropTopping();
		}
	}

	public void GrabTopping(GameObject prefab)
	{
		Topping = (GameObject) GameObject.Instantiate( prefab );
		Topping.rigidbody.useGravity = false;
		Topping.rigidbody.isKinematic = true;
		Topping.transform.parent = transform;
		Topping.transform.localPosition = Vector3.zero;
	}

	void DropTopping()
	{
		Topping.transform.parent = null;
		Topping.rigidbody.isKinematic = false;
		Topping.rigidbody.useGravity = true;
		Topping = null;
	}

	public bool HasTopping()
	{
		return Topping != null;
	}
}
