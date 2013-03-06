using UnityEngine;
using System.Collections;

public class ZoomCamera : MonoBehaviour 
{
	private int MaxZoom = 30;
	private int MinZoom = 0;
	public int CurZoom = 20;
	
	void FixedUpdate () 
	{
		if(Input.GetKey("z"))
		{
			//Zoom in
			if(CurZoom < MaxZoom)
			{
				transform.Translate (0,0,1);
				CurZoom += 1;
			}
		}
		if(Input.GetKey("x"))
		{
			//Zoom out
			if(CurZoom > MinZoom)
			{
				transform.Translate (0,0,-1);
				CurZoom -= 1;
			}
		}
	}
}
