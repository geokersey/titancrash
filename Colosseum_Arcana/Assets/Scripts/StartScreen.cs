using UnityEngine;
using UnityEditor;
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
	
	void Awake()
	{
		LevelName = "DefaultMatch";
		if(!Directory.Exists("C:/ElementalFury/Maps"))
		{
			Debug.Log ("Created a directory!");
			Directory.CreateDirectory("C:/ElementalFury/Maps");
		}
		if(!File.Exists("C:/ElementalFury/Maps/DefaultMatch.xml"))
		{
			Debug.Log ("Couldn't find default map. Copying from install");
			FileUtil.CopyFileOrDirectory ("Assets/DefaultMatch.xml", "C:/ElementalFury/Maps/DefaultMatch.xml");
		}
	}
	
	void OnGUI()
	{
		if(!PlayGame && !EnterEditor && !QuitGame)
		{
			if(GUI.Button(new Rect(100,100,100,30), "Play Game"))
			{
				PlayGame = true;
			}
			if(GUI.Button(new Rect(100,200,100,30), "Map Editor"))
			{
				EnterEditor = true;
			}
			if(GUI.Button(new Rect(100,300,100,30), "Quit"))
			{
				QuitGame = true;
			}
		}
		if(PlayGame)
		{
			GUI.Label(new Rect(100,100,300,30), "Load the default map or load a saved map");
			LevelName = GUI.TextField(new Rect(100,200,100,30), LevelName);
			if(GUI.Button(new Rect(100,250,100,30), "Play"))
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
					GUI.Label(new Rect(100,150,300,30), "Map '" + ErrorLevel + "' does not exist.");
				}
			if(GUI.Button(new Rect(100,300,100,30), "Return"))
			{
				PlayGame = false;
				LoadError = false;
				ErrorLevel = "null";
			}
		}
		if(EnterEditor)
		{
			GUI.Label(new Rect(100,100,300,30), "Are you sure you want to start the editor?");
			if(GUI.Button(new Rect(100,200,100,30), "Start Editor"))
			{
				//Load Map Editor
			}
				
			if(GUI.Button(new Rect(100,300,100,30), "Return"))
			{
				EnterEditor = false;
			}
		}
		if(QuitGame)
		{
			GUI.Label(new Rect(100,100,300,30), "Are you sure you want to quit?");
			if(GUI.Button(new Rect(100,200,100,30), "Yes"))
			{
				Application.Quit();
			}
				
			if(GUI.Button(new Rect(100,300,100,30), "No"))
			{
				QuitGame = false;
			}
		}
	}
}
