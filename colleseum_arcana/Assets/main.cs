using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class main : MonoBehaviour {
	
	int activePlayer; //Denotes player 1 or 2, never has a different value
	int entity; //Currently placeholder, will be used to contain game entities
	int state; //0: Standard UI, 1: Unit selection player 1, 2: Unit selection player 2
	
	List<Unit> monsters;
	
	public Unit uScript;
	
	// Use this for initialization
	void Start () {
		monsters = new List<Unit>();
		
		//At any time you need to generate a new Unit, create it on the uScript object
		//and use the copy constructor on a temp Unit before adding it to the list.
		//Otherwise the list will not 'remember' which prefab is supposed to be attached
		//to given Unit instances
		uScript.generateUnit ('f', 't', 0, 0, 1);
		Unit t = new Unit(uScript);
		monsters.Add(t);
		
		uScript.generateUnit ('a', 't', 1, 0, 2);
		Unit t2 = new Unit(uScript);
		
		monsters.Add(t2);
		
		print (monsters.Count);
	
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < monsters.Count; i++)
		{
			if (monsters[i].hp <= 0)
			{
				//Here I make sure that the object is being referenced by a temp variable
				//before deleting it because I think the list will dislike it if I just
				//delete it straight, but I haven't actually tried it, so who knows.
				//I replace the deleted Unit here because I have a bunch of hard-coded
				//stuff in the test code that needs things in certain orders, but in the
				//actual game it shouldn't be necessary.
				uScript.generateUnit('f', 't', i * 3, 0, monsters[i].owner);
				Unit temp = new Unit(monsters[i]);
				temp.destroyUnit();
				Unit t2 = new Unit(uScript);
				monsters.RemoveAt(i);
				monsters.Insert(i, t2);
			}
		}
	}
	
	void fight(Unit attacker, Unit defender)
	{
		//This is an implementation of Geo's current damage calculations. The compiler
		//didn't like floats so I used doubles instead
		double atkdmg = 1.5 * attacker.atk - defender.def;
		
		if (atkdmg < 1)
		{
			defender.hp--;
		}
		else
		{
			int temp = (int)atkdmg;
			double t2 = atkdmg - temp;
			
			if (t2 >= 0.5)
			{
				temp++;
			}
			
			defender.hp -= temp;
		}
		
		double defdmg = 0.75 * defender.def - 0.25 * attacker.def;
		
		if (defdmg < 1)
		{
			attacker.hp--;
		}
		else
		{
			int temp = (int)defdmg;
			double t2 = defdmg - temp;
			
			if (t2 >= 0.5)
			{
				temp++;
			}
			attacker.hp -= temp;
		}
	}
	
	void OnGUI()
	//Generic GUI stuff. The only really interesting thing is I use an int called 'state'
	//to track which buttons are active at any given time. The buttons to create new Units
	//replace the old ones so prefabs can't get out of hand for the purposes of the test
	//scenario. In the game, it should just tack a new Unit onto the end of the list.
	{
		GUI.Label(new Rect(1300, 0, 100, 20), "Player 1 HP: " + monsters[0].hp);
		GUI.Label(new Rect(1300, 25, 100, 20), "Player 2 HP: " + monsters[1].hp);
		
		if (GUI.Button(new Rect(0, 600, 150, 25), "Player 1: Attack!"))
		{
			fight(monsters[0], monsters[1]);
		}
		
		if (GUI.Button(new Rect(0, 630, 150, 25), "Player 2: Attack!"))
		{
			fight(monsters[0], monsters[1]);
		}
		
		if (state == 0)
		{
			if (GUI.Button(new Rect(0, 0, 400, 200), "Player 1"))
			{
				state = 1;
			}
			
			if (GUI.Button(new Rect(0, 250, 400, 200), "Player 2"))
			{
				state = 2;
			}
		}
		else if (state == 1)
		{
			if (GUI.Button(new Rect(0, 0, 99, 49), "Tiny Air"))
			{
				monsters[0].destroyUnit();
				uScript.generateUnit('a', 't', -3, 0, 1);
				Unit t = new Unit(uScript);
				monsters[0] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(0, 50, 99, 49), "Small Air"))
			{
				monsters[0].destroyUnit();
				uScript.generateUnit('a', 's', -3, 0, 1);
				Unit t = new Unit(uScript);
				monsters[0] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(0, 100, 99, 49), "Medium Air"))
			{
				monsters[0].destroyUnit();
				uScript.generateUnit('a', 'm', -3, 0, 1);
				Unit t = new Unit(uScript);
				monsters[0] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(0, 150, 99, 49), "Large Air"))
			{
				monsters[0].destroyUnit();
				uScript.generateUnit('a', 'l', -3, 0, 1);
				Unit t = new Unit(uScript);
				monsters[0] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(100, 0, 99, 49), "Tiny Earth"))
			{
				monsters[0].destroyUnit();
				uScript.generateUnit('e', 't', -3, 0, 1);
				Unit t = new Unit(uScript);
				monsters[0] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(100, 50, 99, 49), "Small Earth"))
			{
				monsters[0].destroyUnit();
				uScript.generateUnit('e', 's', -3, 0, 1);
				Unit t = new Unit(uScript);
				monsters[0] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(100, 100, 99, 49), "Medium Earth"))
			{
				monsters[0].destroyUnit();
				uScript.generateUnit('e', 'm', -3, 0, 1);
				Unit t = new Unit(uScript);
				monsters[0] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(100, 150, 99, 49), "Large Earth"))
			{
				monsters[0].destroyUnit();
				uScript.generateUnit('e', 'l', -3, 0, 1);
				Unit t = new Unit(uScript);
				monsters[0] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(200, 0, 99, 49), "Tiny Fire"))
			{
				monsters[0].destroyUnit();
				uScript.generateUnit('f', 't', -3, 0, 1);
				Unit t = new Unit(uScript);
				monsters[0] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(200, 50, 99, 49), "Small Fire"))
			{
				monsters[0].destroyUnit();
				uScript.generateUnit('f', 's', -3, 0, 1);
				Unit t = new Unit(uScript);
				monsters[0] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(200, 100, 99, 49), "Medium Fire"))
			{
				monsters[0].destroyUnit();
				uScript.generateUnit('f', 'm', -3, 0, 1);
				Unit t = new Unit(uScript);
				monsters[0] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(200, 150, 99, 49), "Large Fire"))
			{
				monsters[0].destroyUnit();
				uScript.generateUnit('f', 'l', -3, 0, 1);
				Unit t = new Unit(uScript);
				monsters[0] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(300, 0, 99, 49), "Tiny Water"))
			{
				monsters[0].destroyUnit();
				uScript.generateUnit('w', 't', -3, 0, 1);
				Unit t = new Unit(uScript);
				monsters[0] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(300, 50, 99, 49), "Small Water"))
			{
				monsters[0].destroyUnit();
				uScript.generateUnit('w', 's', -3, 0, 1);
				Unit t = new Unit(uScript);
				monsters[0] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(300, 100, 99, 49), "Medium Water"))
			{
				monsters[0].destroyUnit();
				uScript.generateUnit('w', 'm', -3, 0, 1);
				Unit t = new Unit(uScript);
				monsters[0] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(300, 150, 99, 49), "Large Water"))
			{
				monsters[0].destroyUnit();
				uScript.generateUnit('w', 'l', -3, 0, 1);
				Unit t = new Unit(uScript);
				monsters[0] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(0, 250, 400, 200), "Player 2"))
			{
				state = 2;
			}
		}
		else if (state == 2)
		{
			if (GUI.Button(new Rect(0, 0, 400, 200), "Player 1"))
			{
				state = 1;
			}
			
			if (GUI.Button(new Rect(0, 250, 99, 49), "Tiny Air"))
			{
				monsters[1].destroyUnit();
				uScript.generateUnit('a', 't', 3, 0, 2);
				Unit t = new Unit(uScript);
				monsters[1] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(0, 300, 99, 49), "Small Air"))
			{
				monsters[1].destroyUnit();
				uScript.generateUnit('a', 's', 3, 0, 2);
				Unit t = new Unit(uScript);
				monsters[1] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(0, 350, 99, 49), "Medium Air"))
			{
				monsters[1].destroyUnit();
				uScript.generateUnit('a', 'm', 3, 0, 2);
				Unit t = new Unit(uScript);
				monsters[1] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(0, 400, 99, 49), "Large Air"))
			{
				monsters[1].destroyUnit();
				uScript.generateUnit('a', 'l', 3, 0, 2);
				Unit t = new Unit(uScript);
				monsters[1] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(100, 250, 99, 49), "Tiny Earth"))
			{
				monsters[1].destroyUnit();
				uScript.generateUnit('e', 't', 3, 0, 2);
				Unit t = new Unit(uScript);
				monsters[1] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(100, 300, 99, 49), "Small Earth"))
			{
				monsters[1].destroyUnit();
				uScript.generateUnit('e', 's', 3, 0, 2);
				Unit t = new Unit(uScript);
				monsters[1] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(100, 350, 99, 49), "Medium Earth"))
			{
				monsters[1].destroyUnit();
				uScript.generateUnit('e', 'm', 3, 0, 2);
				Unit t = new Unit(uScript);
				monsters[1] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(100, 400, 99, 49), "Large Earth"))
			{
				monsters[1].destroyUnit();
				uScript.generateUnit('e', 'l', 3, 0, 2);
				Unit t = new Unit(uScript);
				monsters[1] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(200, 250, 99, 49), "Tiny Fire"))
			{
				monsters[1].destroyUnit();
				uScript.generateUnit('f', 't', 3, 0, 2);
				Unit t = new Unit(uScript);
				monsters[1] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(200, 300, 99, 49), "Small Fire"))
			{
				monsters[1].destroyUnit();
				uScript.generateUnit('f', 's', 3, 0, 2);
				Unit t = new Unit(uScript);
				monsters[1] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(200, 350, 99, 49), "Medium Fire"))
			{
				monsters[1].destroyUnit();
				uScript.generateUnit('f', 'm', 3, 0, 2);
				Unit t = new Unit(uScript);
				monsters[1] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(200, 400, 99, 49), "Large Fire"))
			{
				monsters[1].destroyUnit();
				uScript.generateUnit('f', 'l', 3, 0, 2);
				Unit t = new Unit(uScript);
				monsters[1] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(300, 250, 99, 49), "Tiny Water"))
			{
				monsters[1].destroyUnit();
				uScript.generateUnit('w', 't', 3, 0, 2);
				Unit t = new Unit(uScript);
				monsters[1] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(300, 300, 99, 49), "Small Water"))
			{
				monsters[1].destroyUnit();
				uScript.generateUnit('w', 's', 3, 0, 2);
				Unit t = new Unit(uScript);
				monsters[1] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(300, 350, 99, 49), "Medium Water"))
			{
				monsters[1].destroyUnit();
				uScript.generateUnit('w', 'm', 3, 0, 2);
				Unit t = new Unit(uScript);
				monsters[1] = t;
				state = 0;
			}
			
			if (GUI.Button(new Rect(300, 400, 99, 49), "Large Water"))
			{
				monsters[1].destroyUnit();
				uScript.generateUnit('w', 'l', 3, 0, 2);
				Unit t = new Unit(uScript);
				monsters[1] = t;
				state = 0;
			}
		}
	}
}
