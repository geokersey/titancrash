using UnityEngine;
using System.Collections;

public class ZoomCamera : MonoBehaviour 
{
	private float MaxZoom = 25f;
	private float MinZoom = 10f;
	public float CurrentZoom = 20f;
	
	public float ZoomAcceleration = 2.5f;
	private float MaxZoomSpeed = 5.0f;
	public float CurrentZoomSpeed = 0.0f;
	
	void FixedUpdate () 
	{
		
		//Move Camera		
		
		if(Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			//Zoom in
			if(CurrentZoomSpeed < 0)
			{
				CurrentZoomSpeed = 0;	
			}
			if(CurrentZoom < MaxZoom)
			{
				//CurrentZoomSpeed += 1;
				CurrentZoomSpeed += ZoomAcceleration * Time.deltaTime;
			}
		}
		else if(Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			//Zoom out
			if(CurrentZoomSpeed > 0)
			{
				CurrentZoomSpeed = 0;	
			}
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
		
		CurrentZoom += CurrentZoomSpeed;
		CurrentZoom = Mathf.Round(CurrentZoom);
		transform.Translate (0,0,CurrentZoomSpeed);
	}
}
