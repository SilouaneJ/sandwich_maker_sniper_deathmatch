using UnityEngine;
using System.Collections;

public class ObjectScaleScript : MonoBehaviour {
	
	[ SerializeField ] float
		ScaleSpeed,
		ScaleDistance;
	
	float 
		ScaleDirection;
	
	Vector3
		ScaleOrigin,
		ScaleTarget;
	
	bool
		ScaleOn,
		ScaleLoop,
		ScaleLooping;
	
	// Use this for initialization
	void Start () {
		
	}
	
	public void SetUpScale(){
		
		ScaleOn = false;
		
		ScaleLoop = false;
		
		ScaleLooping = false;
		
		ScaleOrigin = this.transform.localScale;
		
		ScaleTarget = ScaleOrigin;
	}
	
	public void TriggerScale(float scale_direction){
		
		ScaleOn = true;
		
		ScaleDirection = scale_direction;
		
		ScaleTarget = ScaleOrigin;
		
		ScaleTarget.x += ScaleOrigin.x * ScaleDistance * ScaleDirection;
		ScaleTarget.y += ScaleOrigin.y * ScaleDistance * ScaleDirection;
		ScaleTarget.z += ScaleOrigin.z * ScaleDistance * ScaleDirection;
	}
	
	public void TriggerLoopingScale(float scale_direction){
		
		if (ScaleOn){
			
			this.transform.localScale = ScaleOrigin;
		}
		
		TriggerScale(scale_direction);
		
		ScaleLoop = true;
		
		ScaleLooping = false;
	}
	
	public void StopScale(){
		
		ScaleOn = false;
		
		ScaleLoop = false;
		
		ScaleLooping = false;
		
		ScaleDirection = 0.0f;
		
		this.transform.localScale = ScaleOrigin;
	}
	
	public bool GetScaleOn(){
		bool scale_on = ScaleOn;
		return scale_on;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (ScaleOn){
			
			Vector3 object_scale = this.transform.localScale;
			
			object_scale.x += ScaleSpeed * ScaleDirection * Time.deltaTime;
			object_scale.y += ScaleSpeed * ScaleDirection * Time.deltaTime;
			object_scale.z += ScaleSpeed * ScaleDirection * Time.deltaTime;
			
			this.transform.localScale = object_scale;
			
			if (ScaleLoop){
				
				if (!ScaleLooping){
					
					if (ScaleDirection < 0.0f){
						
						if (this.transform.localScale.x <= ScaleTarget.x && this.transform.localScale.y <= ScaleTarget.y && this.transform.localScale.z <= ScaleTarget.z){
							
							ScaleDirection = - ScaleDirection;
							ScaleLooping = true;
						}
					}
					else if (ScaleDirection > 0.0f){
						
						if (this.transform.localScale.x >= ScaleTarget.x && this.transform.localScale.y >= ScaleTarget.y && this.transform.localScale.z >= ScaleTarget.z){
							
							ScaleDirection = - ScaleDirection;
							ScaleLooping = true;
						}
					}
				}
				else if (ScaleLooping){
					
					if (ScaleDirection > 0.0f){
						
						if (this.transform.localScale.x >= ScaleOrigin.x || this.transform.localScale.y >= ScaleOrigin.y || this.transform.localScale.z >= ScaleOrigin.z){
							
							ScaleOn = false;
							
							ScaleLooping = false;
							
							this.transform.localScale = ScaleOrigin;
						}
					}
					else if (ScaleDirection < 0.0f){
						
						if (this.transform.localScale.x <= ScaleOrigin.x || this.transform.localScale.y <= ScaleOrigin.y || this.transform.localScale.z <= ScaleOrigin.z){
							
							ScaleOn = false;
							
							ScaleLooping = false;
							
							this.transform.localScale = ScaleOrigin;
						}
					}
				}
			}
			else if (!ScaleLooping){
				
				/*
				if (this.transform.localScale.x > ScaleTarget.x || this.transform.localScale.y > ScaleTarget.y || this.transform.position.z > ScaleTarget.z){

					ScaleOn = false;
				}
				*/
			}
		}
	}
}
