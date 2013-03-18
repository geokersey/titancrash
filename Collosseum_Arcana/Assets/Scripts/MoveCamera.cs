using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour 
{
	void FixedUpdate () 
	{
		if(Input.GetKey("w"))// && transform.position.z < Current_size[0]) // left
		{
			transform.Translate (0,0,1);
		}
		if(Input.GetKey("s"))// && transform.position.z > 0) // right
		{
			transform.Translate(0,0,-1);
		}
		if(Input.GetKey("d"))// && transform.position.x < Current_size[1] - 13) // forward
		{
			transform.Translate(1,0,0);
		}
		if(Input.GetKey("a"))// && transform.position.x > - 10) // back
		{
			transform.Translate(-1,0,0);
		}
	}
}
