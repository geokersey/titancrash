using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour 
{
	private Vector3 StartPosition;
	
	private float ScrollAcceleration = 3.5f;
	private float MaxScrollSpeed = 1.0f;
	private float LRCurrentScrollSpeed = 0.0f;//LeftRight
	private float UDCurrentScrollSpeed = 0.0f;//UpDown
	private bool LRpressed = false;
	private bool UDpressed = false;
	
	private float MousePosx;
	private float MousePosy;
	
	void Start()
	{
		StartPosition = new Vector3(18,10,20);
		this.transform.position = StartPosition;
	}
	
	void FixedUpdate () 
	{
		if(Input.GetKey ("c"))
		{
			this.transform.position = StartPosition;	
		}
		MousePosx = Input.mousePosition.x;
		MousePosy = Input.mousePosition.y;
		if (MousePosx < (Screen.width * .08))
		{
			if (MousePosx < (Screen.width * .04))
			{
				transform.Translate(Vector3.right * -MaxScrollSpeed * Time.deltaTime * 30);
			}
			else
			{
				transform.Translate(Vector3.right * -MaxScrollSpeed * Time.deltaTime * 15);
			}
		}
		if (MousePosx >= Screen.width-(Screen.width * .08)) 
		{
			if (MousePosx >= Screen.width-(Screen.width * .04))
			{
				transform.Translate(Vector3.right * MaxScrollSpeed * Time.deltaTime * 30);
			}
			else
			{
				transform.Translate(Vector3.right * MaxScrollSpeed * Time.deltaTime * 15);
			}
		}
		if (MousePosy < (Screen.height * .08))
		{
			if (MousePosy < (Screen.height * .04))
			{
				transform.Translate(Vector3.forward * -MaxScrollSpeed * Time.deltaTime * 30);
			}
			else
			{	
				transform.Translate(Vector3.forward * -MaxScrollSpeed * Time.deltaTime * 15);
			}
		}	
		if (MousePosy >= Screen.height-(Screen.height * .08))
		{
			if (MousePosy >= Screen.height-(Screen.height * .04))
			{
				transform.Translate(Vector3.forward * MaxScrollSpeed * Time.deltaTime * 30);
			}
			else
			{
				transform.Translate(Vector3.forward * MaxScrollSpeed * Time.deltaTime * 15);
			}
		}
		
		//Speed Limiters
		if(LRCurrentScrollSpeed > 0 && LRCurrentScrollSpeed > MaxScrollSpeed)
		{
			LRCurrentScrollSpeed = MaxScrollSpeed;
		}
		if(LRCurrentScrollSpeed < 0 && LRCurrentScrollSpeed < (MaxScrollSpeed * -1f))
		{
			LRCurrentScrollSpeed = MaxScrollSpeed * -1;
		}
		if(UDCurrentScrollSpeed > 0 && UDCurrentScrollSpeed > MaxScrollSpeed)
		{
			UDCurrentScrollSpeed = MaxScrollSpeed;
		}
		if(UDCurrentScrollSpeed < 0 && UDCurrentScrollSpeed < (MaxScrollSpeed * -1f))
		{
			UDCurrentScrollSpeed = MaxScrollSpeed * -1;
		}
		
		//Move Camera
		transform.Translate (LRCurrentScrollSpeed,0,UDCurrentScrollSpeed);
		
		//Key Acceleration
		if(Input.GetKey("w") && !Input.GetKey("s"))
		{
			UDCurrentScrollSpeed += ScrollAcceleration * Time.deltaTime;
		}
		else if(Input.GetKey("s") && !Input.GetKey("w"))
		{
			UDCurrentScrollSpeed -= ScrollAcceleration * Time.deltaTime;
		}
		else
		{
			if(Mathf.Abs (UDCurrentScrollSpeed) < .25f)
			{
				UDCurrentScrollSpeed = 0;
			}
			else if(UDCurrentScrollSpeed < 0)
			{
				UDCurrentScrollSpeed += ScrollAcceleration * 1f * Time.deltaTime;	
			}
			else
			{
				UDCurrentScrollSpeed -= ScrollAcceleration * 1f * Time.deltaTime;	
			}
		}
		
		if(Input.GetKey("d") && !Input.GetKey ("a"))
		{
			LRCurrentScrollSpeed += ScrollAcceleration * Time.deltaTime;
		}
		else if(Input.GetKey("a") && !Input.GetKey ("d"))
		{
			LRCurrentScrollSpeed -= ScrollAcceleration * Time.deltaTime;
		}
		else
		{
			if(Mathf.Abs (LRCurrentScrollSpeed) < .25f)
			{
				LRCurrentScrollSpeed = 0;
			}
			else if(LRCurrentScrollSpeed < 0)
			{
				LRCurrentScrollSpeed += ScrollAcceleration * 1f * Time.deltaTime;	
			}
			else
			{
				LRCurrentScrollSpeed -= ScrollAcceleration * 1f * Time.deltaTime;	
			}
		}
	}
}
