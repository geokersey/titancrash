using UnityEngine;
using System.Collections;

public class MoveCamera1 : MonoBehaviour 
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
	
	//private string size;
	private GameObject Manager;
	private float MapXLimit = 0;
	private float MapYLimit = 0;
	
	void Start()
	{
		Manager = GameObject.FindGameObjectWithTag("Manager");
		StartPosition = new Vector3(0,15,10);
		this.transform.position = StartPosition;
	}
	
	void FixedUpdate () 
	{
		string size = Manager.GetComponent<GenerateMap>().Size;
		if(size == "Small")
		{
			MapXLimit = 8;
			MapYLimit = 7.5f;
		}
		else if(size == "Medium")
		{
			MapXLimit = 18;
			MapYLimit = 18;
		}
		else if(size == "Large")
		{
			MapXLimit = 30;
			MapYLimit = 30;
		}
		
		
		
		
		if(Input.GetKey ("space"))
		{
			this.transform.position = StartPosition;	
		}
		MousePosx = Input.mousePosition.x;
		MousePosy = Input.mousePosition.y;
		/*
			if (transform.position.x < MapXLimit && MousePosx < (Screen.width * .01))
			{
				transform.Translate(Vector3.right * MaxScrollSpeed * Time.deltaTime * 30);
			}

			if (transform.position.x > -MapXLimit && MousePosx >= Screen.width-(Screen.width * .01))
			{
				transform.Translate(Vector3.right * -MaxScrollSpeed * Time.deltaTime * 30);
			}

			if (transform.position.z < 11.5f + MapYLimit && MousePosy < (Screen.height * .01))
			{
				transform.Translate(Vector3.forward * MaxScrollSpeed * Time.deltaTime * 30);
			}
		
			if (transform.position.z > 11.5f - MapYLimit && MousePosy >= Screen.height-(Screen.height * .01))
			{
				transform.Translate(Vector3.forward * -MaxScrollSpeed * Time.deltaTime * 30);
			}
		*/
		
		if (transform.position.x < MapXLimit && MousePosx < (Screen.width * .01))
			{
				if(LRCurrentScrollSpeed > 0)
				{
					LRCurrentScrollSpeed = 0;
				}
				LRCurrentScrollSpeed += ScrollAcceleration * Time.deltaTime;
				//transform.Translate(Vector3.right * -MaxScrollSpeed * Time.deltaTime * 15);
			}

			if (transform.position.x > -MapXLimit && MousePosx >= Screen.width-(Screen.width * .01))
			{
				if(LRCurrentScrollSpeed < 0)
				{
					LRCurrentScrollSpeed = 0;
				}
				LRCurrentScrollSpeed -= ScrollAcceleration * Time.deltaTime;
				//transform.Translate(Vector3.right * MaxScrollSpeed * Time.deltaTime * 15);
			}

			if (transform.position.z < 5 + MapYLimit && MousePosy < (Screen.height * .01))
			{
				if(UDCurrentScrollSpeed > 0)
				{
					UDCurrentScrollSpeed = 0;
				}
				UDCurrentScrollSpeed += ScrollAcceleration * Time.deltaTime;
				//transform.Translate(Vector3.forward * -MaxScrollSpeed * Time.deltaTime * 15);
			}
		
			if (transform.position.z > 5 - MapYLimit && MousePosy >= Screen.height-(Screen.height * .01))
			{
				if(UDCurrentScrollSpeed < 0)
				{
					UDCurrentScrollSpeed = 0;
				}
				UDCurrentScrollSpeed -= ScrollAcceleration * Time.deltaTime;
				//transform.Translate(Vector3.forward * MaxScrollSpeed * Time.deltaTime * 15);
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
		if(transform.position.z <= 5+MapYLimit && Input.GetKey("down") && !Input.GetKey("up"))
		{
			if(UDCurrentScrollSpeed < 0)
			{
				UDCurrentScrollSpeed = 0;
			}
			UDCurrentScrollSpeed += ScrollAcceleration * Time.deltaTime;
		}
		else if(transform.position.z >= 5-MapYLimit && Input.GetKey("up") && !Input.GetKey("down"))
		{
			if(UDCurrentScrollSpeed > 0)
			{
				UDCurrentScrollSpeed = 0;
			}
			UDCurrentScrollSpeed -= ScrollAcceleration * Time.deltaTime;
		}
		else if(MousePosy >= (Screen.height * .01) && MousePosy < Screen.height-(Screen.height * .01))
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
		if(transform.position.z > 5+MapYLimit)
			{
				transform.position = new Vector3(transform.position.x,transform.position.y,5+MapYLimit);
			}
			if(transform.position.z < 5-MapYLimit)
			{
				transform.position = new Vector3(transform.position.x,transform.position.y,5-MapYLimit);
			}
		
		if(transform.position.x <= MapXLimit && Input.GetKey("left") && !Input.GetKey ("right"))
		{
			if(LRCurrentScrollSpeed < 0)
			{
				LRCurrentScrollSpeed = 0;
			}
			LRCurrentScrollSpeed += ScrollAcceleration * Time.deltaTime;
		}
		else if(transform.position.x >= -MapXLimit && Input.GetKey("right") && !Input.GetKey ("left"))
		{
			if(LRCurrentScrollSpeed > 0)
			{
				LRCurrentScrollSpeed = 0;
			}
			LRCurrentScrollSpeed -= ScrollAcceleration * Time.deltaTime;
		}
		else if(MousePosx > (Screen.width * .01) && MousePosx < Screen.width-(Screen.width * .01))
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
		if(transform.position.x > MapXLimit)
			{
				transform.position = new Vector3(MapXLimit,transform.position.y,transform.position.z);
			}
			if(transform.position.x < -MapXLimit)
			{
				transform.position = new Vector3(-MapXLimit,transform.position.y,transform.position.z);
			}
	}
}
