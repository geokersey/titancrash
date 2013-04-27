using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour 
{
	private Vector3 StartPosition;
	
	private float ScrollAcceleration = 1f;
	private float MaxScrollSpeed = 1.0f;
	private float LRCurrentScrollSpeed = 0.0f;//LeftRight
	private float UDCurrentScrollSpeed = 0.0f;//UpDown
	private bool LRpressed = false;
	private bool UDpressed = false;
	
	private float MousePosx;
	private float MousePosy;
	
	//private string size;
	private Grid Manager;
	private float MapXLimit = 0;
	private float MapYLimit = 0;
	
	void Start()
	{
		Manager = GameObject.Find("grid").GetComponent<Grid>();
		StartPosition = new Vector3(0,15,10);
		this.transform.position = StartPosition;
	}
	
	void FixedUpdate () 
	{
		int s = Manager.map.GetLength(0);
		if(s == null)
		{
			s= 19;
		}
		string size = "Small";
		if(s == 39)
		{
			size = "Medium";
		}
		else if(s == 59)
		{
			size = "Large";	
		} 
		
		if(size == "Small")
		{
			MapXLimit = 8;
			MapYLimit = 8;
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
		
		
		
		
		if(Input.GetKey ("c"))
		{
			this.transform.position = StartPosition;
			LRCurrentScrollSpeed = 0;
	 		UDCurrentScrollSpeed = 0;
		}
		MousePosx = Input.mousePosition.x;
		MousePosy = Input.mousePosition.y;

			if (transform.position.x < MapXLimit && MousePosx < (Screen.width * .01))
			{
				if(LRCurrentScrollSpeed > 0)
				{
					LRCurrentScrollSpeed = 0;
				}
				LRCurrentScrollSpeed -= ScrollAcceleration * Time.deltaTime;
				//transform.Translate(Vector3.right * -MaxScrollSpeed * Time.deltaTime * 15);
			}

			if (transform.position.x > -MapXLimit && MousePosx >= Screen.width-(Screen.width * .01))
			{
				if(LRCurrentScrollSpeed < 0)
				{
					LRCurrentScrollSpeed = 0;
				}
				LRCurrentScrollSpeed += ScrollAcceleration * Time.deltaTime;
				//transform.Translate(Vector3.right * MaxScrollSpeed * Time.deltaTime * 15);
			}

			if (transform.position.z < 5 + MapYLimit && MousePosy < (Screen.height * .01))
			{
				if(UDCurrentScrollSpeed > 0)
				{
					UDCurrentScrollSpeed = 0;
				}
				UDCurrentScrollSpeed -= ScrollAcceleration * Time.deltaTime;
				//transform.Translate(Vector3.forward * -MaxScrollSpeed * Time.deltaTime * 15);
			}
		
			if (transform.position.z > 5 - MapYLimit && MousePosy >= Screen.height-(Screen.height * .01))
			{
				if(UDCurrentScrollSpeed < 0)
				{
					UDCurrentScrollSpeed = 0;
				}
				UDCurrentScrollSpeed += ScrollAcceleration * Time.deltaTime;
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
		if(transform.position.z <= 5+MapYLimit && Input.GetKey("w") && !Input.GetKey("s"))
		{
			if(UDCurrentScrollSpeed < 0)
			{
				UDCurrentScrollSpeed = 0;
			}
			UDCurrentScrollSpeed += ScrollAcceleration * Time.deltaTime;
		}
		else if(transform.position.z >= 5-MapYLimit && Input.GetKey("s") && !Input.GetKey("w"))
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
		
		if(transform.position.x <= MapXLimit && Input.GetKey("d") && !Input.GetKey ("a"))
		{
			if(LRCurrentScrollSpeed < 0)
			{
				LRCurrentScrollSpeed = 0;
			}
			LRCurrentScrollSpeed += ScrollAcceleration * Time.deltaTime;
		}
		else if(transform.position.x >= -MapXLimit && Input.GetKey("a") && !Input.GetKey ("d"))
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
	
	
	
	
	/* Old camera code
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
		StartPosition = new Vector3(0,10,10);
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
		if (MousePosx < (Screen.width * .01))
		{
			if (MousePosx < (Screen.width * .01))
			{
				transform.Translate(Vector3.right * -MaxScrollSpeed * Time.deltaTime * 30);
			}
			else
			{
				transform.Translate(Vector3.right * -MaxScrollSpeed * Time.deltaTime * 15);
			}
		}
		if (MousePosx >= Screen.width-(Screen.width * .01)) 
		{
			if (MousePosx >= Screen.width-(Screen.width * .01))
			{
				transform.Translate(Vector3.right * MaxScrollSpeed * Time.deltaTime * 30);
			}
			else
			{
				transform.Translate(Vector3.right * MaxScrollSpeed * Time.deltaTime * 15);
			}
		}
		if (MousePosy < (Screen.height * .01))
		{
			if (MousePosy < (Screen.height * .01))
			{
				transform.Translate(Vector3.forward * -MaxScrollSpeed * Time.deltaTime * 30);
			}
			else
			{	
				transform.Translate(Vector3.forward * -MaxScrollSpeed * Time.deltaTime * 15);
			}
		}	
		if (MousePosy >= Screen.height-(Screen.height * .01))
		{
			if (MousePosy >= Screen.height-(Screen.height * .01))
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
	}*/
}
