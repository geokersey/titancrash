using UnityEngine;
//using UnityEditor;
//using System.Collections;
//using System.Collections.Generic;
//using System.Xml;
//using System.Xml.Serialization;
using System.IO;

public class StartScreen : MonoBehaviour 
{
	static public string LevelName = "DefaultMatch";
	private bool PlayGame = false;
	private bool EnterEditor = false;
	private bool QuitGame = false;
	private bool LoadError = false;
	private string ErrorLevel = "null";
	public GUISkin GUIstuff;
	public GUIStyle temp;
	
	void Awake()
	{
		temp = GUIstuff.label;
		LevelName = "DefaultMatch";
		if(!Directory.Exists("C:/ElementalFury/Maps"))
		{
			Debug.Log ("Created a directory!");
			Directory.CreateDirectory("C:/ElementalFury/Maps");
		}
		if(!File.Exists("C:/ElementalFury/Maps/DefaultMatch.xml"))
		{
			Debug.Log ("Couldn't find default map. Copying from install");
			File.Copy("Assets/DefaultMatch.xml", "C:/ElementalFury/Maps/DefaultMatch.xml");
		}
	}
	
	void OnGUI()
	{
		GUI.Label(new Rect(Screen.width*.5f - 175,10,450, 100), "\n\t\t\t\t\t\t\t\tElemental Fury", GUIstuff.textArea);
		if(!PlayGame && !EnterEditor && !QuitGame)
		{
			if(GUI.Button(new Rect(Screen.width *.5f - 45,110,100,30), "Play Game", GUIstuff.button))
			{
				PlayGame = true;
			}
			if(GUI.Button(new Rect(Screen.width *.5f - 45,210,100,30), "Map Editor", GUIstuff.button))
			{
				EnterEditor = true;
			}
			if(GUI.Button(new Rect(Screen.width *.5f - 45,310,100,30), "Quit", GUIstuff.button))
			{
				QuitGame = true;
			}
		}
		if(PlayGame)
		{
			GUI.Label(new Rect(Screen.width *.5f - 100,115,300,30), "\t\tLoad the default map or load a saved map", GUIstuff.label);
			LevelName = GUI.TextField(new Rect(Screen.width *.5f - 45,200,100,30), LevelName, GUIstuff.textField);
			if(GUI.Button(new Rect(Screen.width *.5f - 50,250,100,30), "Play", GUIstuff.button))
			{
				if(File.Exists("C:/ElementalFury/Maps/" + LevelName + ".xml"))
				{
					Application.LoadLevel("scene0");
				}
				else
				{
					//GUI.Label(new Rect(100,150,300,30), "Map does not exist");
					LoadError = true;
					ErrorLevel = LevelName;
				}
				
			}
			if(LoadError)
				{
					GUI.Label(new Rect(Screen.width *.5f - 150,150,300,30), "Map '" + ErrorLevel + "' does not exist.", GUIstuff.label);
				}
			if(GUI.Button(new Rect(Screen.width *.5f - 50,300,100,30), "Return", GUIstuff.button))
			{
				PlayGame = false;
				LoadError = false;
				ErrorLevel = "null";
			}
		}
		if(EnterEditor)
		{
			GUI.Label(new Rect(100,100,300,30), "Are you sure you want to start the editor?", GUIstuff.label);
			if(GUI.Button(new Rect(Screen.width *.5f - 50,200,100,30), "Start Editor", GUIstuff.button))
			{
				//Load Map Editor
				Application.LoadLevel("Builder");
			}
				
			if(GUI.Button(new Rect(Screen.width *.5f - 50,300,100,30), "Return", GUIstuff.button))
			{
				EnterEditor = false;
			}
		}
		if(QuitGame)
		{
			GUI.Label(new Rect(Screen.width *.5f - 150,100,300,30), "Are you sure you want to quit?", GUIstuff.label);
			if(GUI.Button(new Rect(Screen.width *.5f - 50,200,100,30), "Yes", GUIstuff.button))
			{
				Application.Quit();
			}
				
			if(GUI.Button(new Rect(Screen.width *.5f - 50,300,100,30), "No", GUIstuff.button))
			{
				QuitGame = false;
			}
		}
	}
}
