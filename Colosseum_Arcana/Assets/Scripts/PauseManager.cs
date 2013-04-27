using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour
{
	public bool IsPaused = false;
	private Grid World;
	private SpellManager Spells;
	private techstuff Tech;
	private int state = 0;
	
	public GUISkin GUIstyle;
	
	// Use this for initialization
	void Start () {
		World = GameObject.Find("grid").GetComponent<Grid>();
		Spells = GameObject.Find("CameraHolder").GetComponent<SpellManager>();
		Tech = GameObject.Find("CameraHolder").GetComponent<techstuff>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown("p") && !IsPaused)
		{
			IsPaused = true;
			state = 1;
		}
		else if(Input.GetKeyDown("p") && IsPaused)
		{
			IsPaused = false;
			state = 0;
		}
	}
	void OnGUI () {
		if(state == 1)
		{
			World.suspended = true;
			Spells.hide();
			Tech.hide();
			
			//Pause menu
			
			//GUI.Label(new Rect(Screen.width *.5f - 50,Screen.height *.5f - 200,100,30), "Game is Paused");
			GUI.Box(new Rect(Screen.width *.5f - 75,Screen.height *.5f - 100,150,30), "Game is Paused", GUIstyle.box);
				
			if(GUI.Button(new Rect(Screen.width *.5f - 50,Screen.height *.5f - 50,100,30), "Resume",GUIstyle.button))
			{
				IsPaused = false;
				state = 0;
			}
			
			if(GUI.Button(new Rect(Screen.width *.5f - 50,Screen.height *.5f,100,30), "Quit", GUIstyle.button))
			{
				state = 2;
				//IsPaused = false;
				//Application.LoadLevel("StartingScreen");
			}
		}
		else if(state == 2)
		{
			GUI.Box(new Rect(Screen.width *.5f - 150,Screen.height *.5f - 100,300,30), "Are you sure you want to quit?", GUIstyle.box);
			if(GUI.Button(new Rect(Screen.width *.5f - 50,Screen.height *.5f - 50,100,30), "Yes",GUIstyle.button))
			{
				state = 0;
				IsPaused = false;
				Application.LoadLevel("StartingScreen");
			}
			
			if(GUI.Button(new Rect(Screen.width *.5f - 50,Screen.height *.5f,100,30), "No", GUIstyle.button))
			{
				state = 1;
				
			}
		}
		else
		{
			World.suspended = false;
			Spells.show();
			Tech.show();
		}
	}
}