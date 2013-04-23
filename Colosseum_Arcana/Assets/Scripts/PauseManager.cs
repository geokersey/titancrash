using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour
{
	private bool IsPaused = false;
	private Grid World;
	private SpellManager Spells;
	private techstuff Tech;
	// Use this for initialization
	void Start () {
		World = GameObject.Find("grid").GetComponent<Grid>();
		Spells = GameObject.Find("CameraHolder").GetComponent<SpellManager>();
		Tech = GameObject.Find("CameraHolder").GetComponent<techstuff>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if(Input.GetKey("p") && !IsPaused)
		{
			IsPaused = true;
		}
	}
	void OnGUI () {
		if(IsPaused)
		{
			World.suspended = true;
			Spells.hide();
			Tech.hide();
			
			//Pause menu
			
			GUI.Label(new Rect(100,100,300,30), "Game is Paused");
				
			if(GUI.Button(new Rect(100,200,100,30), "Resume"))
			{
				IsPaused = false;
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
