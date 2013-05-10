using UnityEngine;
//using UnityEditor;
//using System.Collections;
//using System.Collections.Generic;
//using System.Xml;
//using System.Xml.Serialization;
using System.IO;
//using System.Xml;

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
	
	//public XmlDocument Default;
	
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
			File.Copy(Application.streamingAssetsPath+ "/DefaultMatch.xml", "C:/ElementalFury/Maps/DefaultMatch.xml");
		}
		if(!File.Exists("C:/ElementalFury/Maps/techs.txt"))
		{
			Debug.Log ("Couldn't find techs. Copying from install");
			File.Copy(Application.streamingAssetsPath+ "/techs.txt", "C:/ElementalFury/Maps/techs.txt");
		}
	}
	
	void OnGUI()
	{
		GUI.Box(new Rect(Screen.width*.5f - 225,10,450, 100), "\n\t\t\t\t\t\t\t\tElemental Fury", GUIstuff.textArea);
		if(!PlayGame && !EnterEditor && !QuitGame)
		{
			if(GUI.Button(new Rect(Screen.width *.5f - 50,Screen.height *.5f - 150,100,30), "Play Game", GUIstuff.button))
			{
				PlayGame = true;
			}
			if(GUI.Button(new Rect(Screen.width *.5f - 50,Screen.height *.5f - 100,100,30), "Map Editor", GUIstuff.button))
			{
				EnterEditor = true;
			}
			if(GUI.Button(new Rect(Screen.width *.5f - 50,Screen.height *.5f - 50,100,30), "Quit", GUIstuff.button))
			{
				QuitGame = true;
			}
		}
		if(PlayGame)
		{
			GUI.Box(new Rect(Screen.width *.5f - 150,Screen.height *.5f - 150,300,30), "\t\tLoad the default map or load a saved map", GUIstuff.box);
			LevelName = GUI.TextField(new Rect(Screen.width *.5f - 100,Screen.height *.5f - 100,200,30), LevelName, GUIstuff.textField);
			if(GUI.Button(new Rect(Screen.width *.5f - 50,Screen.height *.5f - 50,100,30), "Play", GUIstuff.button))
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
					GUI.Box(new Rect(Screen.width *.5f - 250,Screen.height - 50,500,30), "Map '" + ErrorLevel + "' does not exist.", GUIstuff.box);
				}
			if(GUI.Button(new Rect(Screen.width *.5f - 50,Screen.height *.5f,100,30), "Return", GUIstuff.button))
			{
				PlayGame = false;
				LoadError = false;
				ErrorLevel = "null";
			}
		}
		if(EnterEditor)
		{
			GUI.Box(new Rect(Screen.width *.5f - 150,Screen.height *.5f - 150,300,30), "Are you sure you want to start the editor?", GUIstuff.box);
			if(GUI.Button(new Rect(Screen.width *.5f - 50,Screen.height *.5f - 100,100,30), "Start Editor", GUIstuff.button))
			{
				//Load Map Editor
				Application.LoadLevel("Builder");
			}
				
			if(GUI.Button(new Rect(Screen.width *.5f - 50,Screen.height *.5f - 50,100,30), "Return", GUIstuff.button))
			{
				EnterEditor = false;
			}
		}
		if(QuitGame)
		{
			GUI.Box(new Rect(Screen.width *.5f - 150,Screen.height *.5f - 150,300,30), "Are you sure you want to quit?", GUIstuff.box);
			if(GUI.Button(new Rect(Screen.width *.5f - 50,Screen.height *.5f - 100,100,30), "Yes", GUIstuff.button))
			{
				Application.Quit();
			}
				
			if(GUI.Button(new Rect(Screen.width *.5f - 50,Screen.height *.5f - 50,100,30), "No", GUIstuff.button))
			{
				QuitGame = false;
			}
		}
	}
}
