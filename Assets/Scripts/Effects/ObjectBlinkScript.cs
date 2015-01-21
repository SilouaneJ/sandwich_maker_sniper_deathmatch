using UnityEngine;
using System.Collections;

public class ObjectBlinkScript : MonoBehaviour {

	[ SerializeField ] Material
		BlinkMaterial;

	[ SerializeField ] float
		BlinkTimer;

	Material
		OriginalMaterial;
	
	float
		BlinkClock;
	
	bool
		BlinkOn;
	
	// Use this for initialization
	void Start () {
	
	}
	
	public void SetUpBlink(){
		
		OriginalMaterial = this.renderer.material;

		BlinkOn = false;
	}
	
	public void LaunchBlink(){
		
		BlinkOn = true;

		BlinkClock = 0.0f;
		
		this.renderer.material = BlinkMaterial;
	}
	
	public bool GetBlinkOn(){
		bool blink_on = BlinkOn;
		return blink_on;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (BlinkOn){
			
			BlinkClock += Time.deltaTime;
			
			if (BlinkClock >= BlinkTimer){
				
				BlinkClock = 0.0f;
				
				BlinkOn = false;
				
				this.renderer.material = OriginalMaterial;
			}
		}
	}
}
