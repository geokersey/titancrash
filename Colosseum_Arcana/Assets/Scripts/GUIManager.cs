using UnityEngine;
using System.Collections;
using System.IO;

public class GUIManager : MonoBehaviour 
{
	private bool SelectedMap = false;
	private bool Toggle1 = true;
	private bool Saving = false;
	private bool Loading = false;
	private string SaveFilename = "NewMap";
	private string LoadFilename = "NewMap";
	private bool OpenMenu = false;
	private bool Tab = false;
	private bool Hotkeys = false;
	private bool Quitting = false;
	private string CurrentTileSelection = "T1_With_None";
	private bool Overwrite = false;
	public GUISkin GUIstuff;
	
	private string HelpTooltip1 = "\n\n\n\n" +
		"1 = T1_With_None\n" +
		"2 = T2_With_None\n" +
		"3 = T3_With_None\n" +
		"4 = Player1Start\n" +
		"5 = Player2Start\n\n\n\n" +
		"q = T1_With_R1\n" +
		"w = T1_With_R2\n" +
		"e = T1_With_R3\n" +
		"r = T1_With_R4\n" +
		"t = T1_With_R5\n" +
		"y = T1_With_Font\n" +
		"u = T1_With_Tower\n";
	private string HelpTooltip2 = "\n\n\n\n" +
		"a = T2_With_R1\n" +
		"s = T2_With_R2\n" +
		"d = T2_With_R3\n" +
		"f = T2_With_R4\n" +
		"g = T2_With_R5\n" +
		"h = T2_With_Font\n" +
		"j = T2_With_Tower\n\n" +
		"z = T3_With_R1\n" +
		"x = T3_With_R2\n" +
		"c = T3_With_R3\n" +
		"v = T3_With_R4\n" +
		"b = T3_With_R5\n" +
		"n = T3_With_Font\n" +
		"m = T3_With_Tower\n";
		
	
	public GUIStyle Gstyle;
	
	//private float ReplaceTimer = 0f;

	void Update()
	{
		if(!OpenMenu)
		{
			RaycastHit hit;
	   		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			//if(Toggle1)
			//{
				if(Input.GetMouseButton(0))
				{
					if(Physics.Raycast(ray,out hit ,100))
					{
						//ReplaceTimer = Time.time;
						this.transform.gameObject.GetComponent<ChangeTile>().Replace(hit.transform.gameObject, CurrentTileSelection);
					}
				}
			//}
			
			if(Input.GetMouseButton(1))
			{
				if(Physics.Raycast(ray,out hit ,100))
				{
					this.transform.gameObject.GetComponent<ChangeTile>().Replace(hit.transform.gameObject, "Blank");
				}
			}
		}
	}
	
	void OnGUI()
	{
		//Map size selection
		if(!SelectedMap)
		{
			if(GUI.Button (new Rect (Screen.width *.5f - 45,10,90,75), "Small Map", GUIstuff.button)) 
			{
				SelectedMap = true;
				this.transform.gameObject.GetComponent<GenerateMap>().Generate (10);
			}
			else if(GUI.Button (new Rect (Screen.width *.5f - 45,90,90,75), "Medium Map", GUIstuff.button)) 
			{
				SelectedMap = true;
				this.transform.gameObject.GetComponent<GenerateMap>().Generate (20);
			}
			else if(GUI.Button (new Rect (Screen.width *.5f - 45,170,90,75), "Large Map", GUIstuff.button)) 
			{
				SelectedMap = true;
				this.transform.gameObject.GetComponent<GenerateMap>().Generate (30);
			}
			else if(GUI.Button (new Rect (Screen.width *.5f - 45,250,90,75), "Close Editor", GUIstuff.button)) 
			{
				Application.LoadLevel("StartingScreen");
			}
		}
		else
		{
			//Current Tile Selection
			GUI.Box(new Rect(Screen.width * .7f, 20, 150,60), "\nCurrent Tile Selection", GUIstuff.box);
			GUI.Label (new Rect (Screen.width * .7f + 20f, 90, 100, 30), "\t" + CurrentTileSelection, GUIstuff.label);
			
			//If no menus are currently open
			if(!OpenMenu)
			{
				//Toggle1 = GUI.Toggle (new Rect (200, 25, 100, 30), Toggle1, "Toggle1");
				
				if(Input.GetKeyDown("tab"))
				{
					OpenMenu = true;
					Tab = true;
					//print ("opened tab");
				}
				
				//Other hotkeys for changing tiles
				//Terrain type T1
				else if(Input.GetKeyDown("1"))
				{
					//T1 w/ nothing
					CurrentTileSelection = "T1_With_None";
				}
				else if(Input.GetKeyDown("q"))
				{
					//T1 w/ R1
					CurrentTileSelection = "T1_With_R1";
				}
				else if(Input.GetKeyDown("w"))
				{
					//T1 w/ R2
					CurrentTileSelection = "T1_With_R2";
				}
				else if(Input.GetKeyDown("e"))
				{
					//T1 w/ R3
					CurrentTileSelection = "T1_With_R3";
				}
				else if(Input.GetKeyDown("r"))
				{
					//T1 w/ R4
					CurrentTileSelection = "T1_With_R4";
				}
				else if(Input.GetKeyDown("t"))
				{
					//T1 w/ R5
					CurrentTileSelection = "T1_With_R5";
				}
				else if(Input.GetKeyDown("y"))
				{
					//T1 w/ Font
					CurrentTileSelection = "T1_With_Font";
				}
				else if(Input.GetKeyDown("u"))
				{
					//T1 w/ Tower
					CurrentTileSelection = "T1_With_Tower";
				}
				
				//Terrain type T2
				else if(Input.GetKeyDown("2"))
				{
					//T2 w/ nothing
					CurrentTileSelection = "T2_With_None";
				}
				else if(Input.GetKeyDown("a"))
				{
					//T2 w/ R1
					CurrentTileSelection = "T2_With_R1";
				}
				else if(Input.GetKeyDown("s"))
				{
					//T2 w/ R2
					CurrentTileSelection = "T2_With_R2";
				}
				else if(Input.GetKeyDown("d"))
				{
					//T2 w/ R3
					CurrentTileSelection = "T2_With_R3";
				}
				else if(Input.GetKeyDown("f"))
				{
					//T2 w/ R4
					CurrentTileSelection = "T2_With_R4";
				}
				else if(Input.GetKeyDown("g"))
				{
					//T2 w/ R5
					CurrentTileSelection = "T2_With_R5";
				}
				else if(Input.GetKeyDown("h"))
				{
					//T2 w/ Font
					CurrentTileSelection = "T2_With_Font";
				}
				else if(Input.GetKeyDown("j"))
				{
					//T2 w/ Tower
					CurrentTileSelection = "T2_With_Tower";
				}
				
				//Terrain type T3
				else if(Input.GetKeyDown("3"))
				{
					//T3 w/ nothing
					CurrentTileSelection = "T3_With_None";
				}
				else if(Input.GetKeyDown("z"))
				{
					//T3 w/ R1
					CurrentTileSelection = "T3_With_R1";
				}
				else if(Input.GetKeyDown("x"))
				{
					//T3 w/ R2
					CurrentTileSelection = "T3_With_R2";
				}
				else if(Input.GetKeyDown("c"))
				{
					//T3 w/ R3
					CurrentTileSelection = "T3_With_R3";
				}
				else if(Input.GetKeyDown("v"))
				{
					//T3 w/ R4
					CurrentTileSelection = "T3_With_R4";
				}
				else if(Input.GetKeyDown("b"))
				{
					//T3 w/ R5
					CurrentTileSelection = "T3_With_R5";
				}
				else if(Input.GetKeyDown("n"))
				{
					//T3 w/ Font
					CurrentTileSelection = "T3_With_Font";
				}
				else if(Input.GetKeyDown("m"))
				{
					//T3 w/ Tower
					CurrentTileSelection = "T3_With_Tower";
				}
				
				//Player Starting locations
				else if(Input.GetKeyDown("4"))
				{
					CurrentTileSelection = "Player1Start";	
				}
				else if(Input.GetKeyDown("5"))
				{
					CurrentTileSelection = "Player2Start";	
				}
			}
			else if(Tab)
			{
				if(Saving || Loading || Hotkeys || Quitting)
				{
					if(Loading)
					{
						LoadFilename = GUI.TextField (new Rect (Screen.width * .5f - 100f, Screen.height * .5f - 60f, 200, 40), LoadFilename, GUIstuff.textField);
						if (GUI.Button (new Rect (Screen.width * .5f - 50f, Screen.height * .5f, 100, 30), "Load", GUIstuff.button))
						{
							//CHECK IF FILE EXISTS
							if(File.Exists("C:/ElementalFury/Maps/" + LoadFilename + ".xml"))
							{
								//LOAD THAT SHIT
								this.transform.gameObject.GetComponent<MapSaveLoad>().Load(LoadFilename);
								Loading = false;
							}
							else
							{
								//FILE NOT FOUND FUCKER
								Debug.Log ("WE AINT FOUND SHIT");
							}
						}
						if (GUI.Button (new Rect (Screen.width * .5f - 50f, Screen.height * .5f + 50f, 100, 30), "Cancel", GUIstuff.button))
						{
							Loading = false;
						}
					}
					else if(Saving)
					{
						SaveFilename = GUI.TextField (new Rect (Screen.width * .5f - 100f, Screen.height * .5f - 60f, 200, 40), SaveFilename, GUIstuff.textField);
						if (GUI.Button (new Rect (Screen.width * .5f - 50f, Screen.height * .5f, 100, 30), "Save", GUIstuff.button))
						{
							//CHECK FOR OVERWRITING A SAVED FILE
							if(!File.Exists("C:/ElementalFury/Maps/" + SaveFilename + ".xml"))
							{
								this.transform.gameObject.GetComponent<MapSaveLoad>().Save(SaveFilename);
								Saving = false;
							}
							else
							{
								//Display new button?
								Overwrite = true;
								Debug.Log ("SHIT SON YOU FUCKED UP");	
							}
						}
						if (GUI.Button (new Rect (Screen.width * .5f - 50f, Screen.height * .5f + 50f, 100, 30), "Cancel", GUIstuff.button))
						{
							Saving = false;
						}
						if(Overwrite)
						{
							GUI.Box(new Rect(Screen.width * .5f - 75f, Screen.height*.5f - 150, 150, 30), "Overwrite save file?", GUIstuff.box);
							if(GUI.Button(new Rect(Screen.width * .5f - 50f, Screen.height*.5f - 100, 100, 30), "Yes", GUIstuff.button))
							{
								Overwrite = false;
								this.transform.gameObject.GetComponent<MapSaveLoad>().Save(SaveFilename);
								Saving = false;
							}
						}
					}
					else if(Hotkeys)
					{
						GUI.Box(new Rect(Screen.width*.3f,Screen.height*.3f, Screen.width*.2f,400), HelpTooltip1, GUIstuff.box);
						GUI.Box(new Rect(Screen.width*.5f,Screen.height*.3f, Screen.width*.2f,400), HelpTooltip2, GUIstuff.box);
						if (GUI.Button (new Rect (Screen.width * .5f - 50f, Screen.height * .8f - 35f, 100, 30), "Return", GUIstuff.button)) 
						{
							Hotkeys = false;
						}
					}
					else if(Quitting)
					{
						GUI.Box(new Rect(Screen.width * .5f - 75f,Screen.height * .5f - 130f,150,130), "\nAre you sure?", GUIstuff.box);
						if (GUI.Button (new Rect (Screen.width * .5f - 65f, Screen.height * .5f - 60f, 50, 30), "Yes", GUIstuff.button))
						{
							//Application.Quit ();
							//Debug.Log ("Quitting Application");
							SelectedMap = false;
							SaveFilename = "NewMap";
							LoadFilename = "NewMap";
							OpenMenu = false;
							Quitting = false;
							Tab = false;
							this.gameObject.GetComponent<GenerateMap>().DestroyMap();
						}
						if (GUI.Button (new Rect (Screen.width * .5f + 15f, Screen.height * .5f - 60f, 50, 30), "No", GUIstuff.button)) 
						{
							Quitting = false;
						}
					}
				}
				else
				{
					//GUI.Box(new Rect(Screen.width * .5f - 65f, Screen.height * .5f - 145f, 130,250), " ", GUIstuff.box);
					GUI.Box(new Rect(Screen.width * .5f - 65f, Screen.height * .5f - 150f, 130,300), "\n\n\nMenu", GUIstuff.box);
					//Display Hotkey list
					if (GUI.Button (new Rect (Screen.width * .5f - 50f, Screen.height * .5f - 75f, 100, 30), "Hotkeys", GUIstuff.button)) 
					{
						Hotkeys = true;
						//Display popup
					}
					//Save map
					else if (GUI.Button (new Rect (Screen.width * .5f - 50f, Screen.height * .5f - 35f, 100, 30), "Save", GUIstuff.button)) 
					{
						Saving = true;
					}
					//Load map
					else if (GUI.Button (new Rect (Screen.width * .5f - 50f, Screen.height * .5f + 5f, 100, 30), "Load", GUIstuff.button)) 
					{
						Loading = true;
					}
					//Quit Application
					else if (GUI.Button (new Rect (Screen.width * .5f - 50f, Screen.height * .5f + 45f, 100, 30), "Quit", GUIstuff.button)) 
					{
						Quitting = true;
						//Display "Are you sure? popup"
					}
					//Escape to quit menu and return
					else if (GUI.Button (new Rect (Screen.width * .5f + 40f, Screen.height * .5f - 100f, 20, 20), "x", GUIstuff.button)) 
					{
						Tab = false;
						OpenMenu = false;
					}
					if(Input.GetKeyDown("escape"))
					{
						Tab = false;
						OpenMenu = false;
					}
				}
			}
		}
    }
}
