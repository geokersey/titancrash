using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;

public class tech {
	
	public string name;
	public string description;
	public List<int> prereqs;
	public int price, ttc; //Research cost in resources and turns, respectively
	
	// Use this for initialization
	void Start () {
	}
	
	public tech()
	{
		name = null;
		description = null;
		prereqs = new List<int>();
		price = 15;
		ttc = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void setprereqs(List<int> p)
	{
		for (int i = 0; i < p.Count; i++)
		{
			prereqs.Add(p[i]);
		}
	}
}

public class techstuff : MonoBehaviour {
	
	FileInfo techs = null;
	StreamReader sr = null;
	List<tech> techTree;
	int state = 0;
	int selectedTech = 0;
	public Grid world;
	
	// Use this for initialization
	void Start () {
		techs = new FileInfo("Assets/Other fun things/techs.txt");
		sr = techs.OpenText();
		techTree = new List<tech>();
		
		string l = sr.ReadLine();
		
		while(l != null)
		{
			tech t = new tech();
			string[] s = l.Split(','); //splits the line into <techname>, <description> and <prereqs>
			t.name = s[0];
			
			s[1] = s[1].Replace(";", ",");
			s[1] = s[1].Replace("|", "\n");
			
			t.description = s[1];
			
			string[] p = s[2].Split(';'); //splits up the prereqs
			
			List<int> b = new List<int>();
			
			foreach (string a in p)
			{
				int c = Convert.ToInt32(a);
				b.Add(c);
			}
			
			t.setprereqs(b);
			
			techTree.Add(t);
			
			world.players[0].techAvailable.Add(0);
			world.players[1].techAvailable.Add(0);
			
			l = sr.ReadLine();
		}
		
		for (int i = 0; i < techTree.Count; i++)
		{
			if (techTree[i].prereqs[0] == 0)
			{
				world.players[0].techAvailable[i] = 1;
				world.players[1].techAvailable[i] = 1;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		/*for (int i = 0; i < techTree.Count; i++)
		{
			if (techTree[i].available)
			{
				continue;
			}
			else
			{
				bool b = true;
				
				for (int j = 0; j < techTree[i].prereqs.Count; j++)
				{
					if (!researched[techTree[i].prereqs[j] - 1])
					{
						b = false;
						break;
					}
				}
				
				techTree[i].available = b;
			}
		}*/
	}
	
	public void checkAvailability()
	{
		for (int i = 0; i < techTree.Count; i++)
		{
			if (world.players[world.activePlayer].techAvailable[i] != 0)
			{
				continue;
			}
			else
			{
				bool b = true;
				
				for (int j = 0; j < techTree[i].prereqs.Count; j++)
				{
					if (world.players[world.activePlayer].techAvailable[techTree[i].prereqs[j] - 1] != 2)
					{
						//print (techTree[i].name);
						b = false;
						break;
					}
				}
				
				if (b)
				{
					world.players[world.activePlayer].techAvailable[i] = 1;
				}
			}
		}
	}
	
	void OnGUI()
	{
		if (state == 0)
		{
			if (GUI.Button(new Rect(1250, 600, 125, 50), "Open Tech Tree"))
			{
				state = 1;
			}
		}
		else if (state == 1)
		{
			for (int i = 0; i < techTree.Count; i++)
			{
				if (world.players[world.activePlayer].techAvailable[i] == 0)
				{
					GUI.backgroundColor = Color.red;
				}
				else if (world.players[world.activePlayer].techAvailable[i] == 1)
				{
					GUI.backgroundColor = Color.blue;
				}
				else if (world.players[world.activePlayer].techAvailable[i] == 2)
				{
					GUI.backgroundColor = Color.green;
				}
				else
				{
					GUI.backgroundColor = Color.white;
				}
				
				if (GUI.Button(new Rect((155 * (i % 5)) + 50, (50 * (i / 5)) + 50, 155, 50), techTree[i].name))
				{
					selectedTech = i;
					state = 2;
				}
			}
			
			GUI.backgroundColor = Color.white;
			
			if (GUI.Button(new Rect(1250, 600, 125, 50), "Close Tech Tree"))
			{
				state = 0;
			}
		}
		else if (state == 2)
		{
			string s = "Requirements\n";
			
			for (int i = 0; i < techTree.Count; i++)
			{
				if (world.players[world.activePlayer].techAvailable[i] == 0)
				{
					GUI.backgroundColor = Color.red;
				}
				else if (world.players[world.activePlayer].techAvailable[i] == 1)
				{
					GUI.backgroundColor = Color.blue;
				}
				else if (world.players[world.activePlayer].techAvailable[i] == 2)
				{
					GUI.backgroundColor = Color.green;
				}
				else
				{
					GUI.backgroundColor = Color.white;
				}
				
				if (GUI.Button(new Rect((155 * (i % 5)) + 50, (50 * (i / 5)) + 50, 155, 50), techTree[i].name))
				{
					selectedTech = i;
				}
			}
			
			GUI.backgroundColor = Color.white;
			
			if (techTree[selectedTech].prereqs[0] != 0)
			{
				s += techTree[techTree[selectedTech].prereqs[0] - 1].name;
				
				for (int i = 1; i < techTree[selectedTech].prereqs.Count; i++)
				{
					s += "\n";
					s += techTree[techTree[selectedTech].prereqs[i] - 1].name;
				}
			}
			else
			{
				s += "None";
			}
			
			GUI.Box(new Rect(Screen.width - 300, 0, 300, 350), techTree[selectedTech].name + "\n\n" + techTree[selectedTech].description + "\n\n" + s);
			
			if (GUI.Button(new Rect(1250, 600, 125, 50), "Close Tech Tree"))
			{
				state = 0;
			}
			
			if (world.players[world.activePlayer].techAvailable[selectedTech] == 0)
			{
				GUI.backgroundColor = Color.red;
				GUI.Button(new Rect(Screen.width - 200, 350, 100, 75), "Unavailable");
			}
			else if (world.players[world.activePlayer].techAvailable[selectedTech] == 2)
			{
				GUI.backgroundColor = Color.green;
				GUI.Button(new Rect(Screen.width - 200, 350, 100, 75), "Researched");
			}
			else
			{
				if (GUI.Button(new Rect(Screen.width - 200, 350, 100, 75), "Research") && world.players[world.activePlayer].researched == false && world.players[world.activePlayer].resources[0] >= 2)
				{
					world.players[world.activePlayer].techAvailable[selectedTech] = 2;
					world.players[world.activePlayer].researched = true;
					world.players[world.activePlayer].resources[0] -= 2;
					state = 0;
				}
			}
			
			
		}
	}
}


