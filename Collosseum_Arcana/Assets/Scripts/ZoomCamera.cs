using UnityEngine;
using System.Collections;

public class ZoomCamera : MonoBehaviour 
{
	private float MaxZoom = 29f;
	private float MinZoom = 10f;
	public float CurrentZoom = 20f;
	
	private float ZoomAcceleration = 2.5f;
	private float MaxZoomSpeed = 5.0f;
	private float CurrentZoomSpeed = 0.0f;
	
	void FixedUpdate () 
	{
		//Speed Limiters
		if(CurrentZoom < MinZoom)// || CurrentZoom > MaxZoom)
		{
			CurrentZoomSpeed = 0;
			CurrentZoom = MinZoom;
		}
		if(CurrentZoom > MaxZoom)
		{
			CurrentZoomSpeed = 0;
			CurrentZoom = MaxZoom;
		}
		
		if(CurrentZoomSpeed > 0 && CurrentZoomSpeed > MaxZoomSpeed)
		{
			CurrentZoomSpeed = MaxZoomSpeed;	
		}
		if(CurrentZoomSpeed < 0 && CurrentZoomSpeed < (MaxZoomSpeed * -1f))
		{
			CurrentZoomSpeed = MaxZoomSpeed * -1;
		}
		//Move Camera
		CurrentZoom += CurrentZoomSpeed;
		transform.Translate (0,0,CurrentZoomSpeed);
		
		if(Input.GetKey("z"))
		{
			//Zoom in
			if(CurrentZoom < MaxZoom)
			{
				CurrentZoomSpeed += ZoomAcceleration * Time.deltaTime;
			}
		}
		else if(Input.GetKey("x"))
		{
			//Zoom out
			if(CurrentZoom > MinZoom)
			{
				CurrentZoomSpeed -= ZoomAcceleration * Time.deltaTime;
			}
		}
		else
		{
			if(Mathf.Abs (CurrentZoomSpeed) < .25f)
			{
				CurrentZoomSpeed = 0;
			}
			else if(CurrentZoomSpeed < 0)
			{
				CurrentZoomSpeed += ZoomAcceleration * 1f * Time.deltaTime;	
			}
			else
			{
				CurrentZoomSpeed -= ZoomAcceleration * 1f * Time.deltaTime;	
			}
		}
	}
}
