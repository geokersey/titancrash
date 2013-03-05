using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SummoningFont : MonoBehaviour {
	
	public Grid world;
	public bool shown = false;//Denotes player 1 or 2, never has a different value
	//int entity; //Currently placeholder, will be used to contain game entities
	//int state; //0: Standard UI, 1: //Unit selection player 1, 2: //Unit selection player 2
	
	//List<//Unit> //monsters;
	
	//public //Unit uScript;
	
	// Use this for initialization
	void Start () {
		////monsters = new List<//Unit>();
		
		//At any time you need to generate a new //Unit, create it on the uScript object
		//and use the copy constructor on a temp //Unit before adding it to the list.
		//Otherwise the list will not 'remember' which prefab is supposed to be attached
		//to given //Unit instances
		////Unit t = generate//Unit ('f', 't', 0, 0, 1);
		//////Unit t = new //Unit(uScript);
		////monsters.Add(t);
		
		////Unit t = generate//Unit ('a', 't', 1, 0, 2);
		//////Unit t2 = new //Unit(uScript);
		
		////monsters.Add(t2);
		
		//print (//monsters.Count);
	
	}
	public void show(){
		shown = true;
	}
	public void hide(){
		shown = false;
	}
	
	// Update is called once per frame
	void Update () {
		/*for (int i = 0; i < //monsters.Count; i++)
		{
			if (//monsters[i].hp <= 0)
			{
				//Here I make sure that the object is being referenced by a temp variable
				//before deleting it because I think the list will dislike it if I just
				//delete it straight, but I haven't actually tried it, so who knows.
				//I replace the deleted //Unit here because I have a bunch of hard-coded
				//stuff in the test code that needs things in certain orders, but in the
				//actual game it shouldn't be necessary.
				//Unit t = generate//Unit('f', 't', i * 3, 0, //monsters[i].owner);
				////Unit temp = new //Unit(//monsters[i]);
				temp.destroy//Unit();
				////Unit t2 = new //Unit(uScript);
				//monsters.RemoveAt(i);
				//monsters.Insert(i, t2);
			}
		}*/
	}
	
	//public void init (Grid world_){
	//	world = world_;
	//}
	
	void OnGUI()
	//Generic GUI stuff. The only really interesting thing is I use an int called 'state'
	//to track which buttons are active at any given time. The buttons to create new //Units
	//replace the old ones so prefabs can't get out of hand for the purposes of the test
	//scenario. In the game, it should just tack a new //Unit onto the end of the list.
	{
			if(shown){
			/*GUI.Label(new Rect(1300, 0, 100, 20), "Player 1 HP: " + //monsters[0].hp);
			GUI.Label(new Rect(1300, 25, 100, 20), "Player 2 HP: " + //monsters[1].hp);
			
			if (GUI.Button(new Rect(0, 600, 150, 25), "Player 1: Attack!"))
			{
				fight(//monsters[0], //monsters[1]);
			}
			
			if (GUI.Button(new Rect(0, 630, 150, 25), "Player 2: Attack!"))
			{
				fight(//monsters[0], //monsters[1]);
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
			{*/
			if (GUI.Button(new Rect(0, 0, 99, 49), "Tiny Air"))
			{
				////monsters[0].destroy//Unit();
				world.selected.occupyer = (Unit)Instantiate (world.tinyAirPrefab, world.selected.transform.position, Quaternion.identity);
				world.selected.occupyer.x = world.selected.x;
				world.selected.occupyer.y = world.selected.y;
				hide();
				////Unit t = generate//Unit('a', 't', -3, 0, 1);
				////Unit t = new //Unit(uScript);
				////monsters[0] = t;
				
				//state = 0;
			}
			
			if (GUI.Button(new Rect(0, 50, 99, 49), "Small Air"))
			{
				////monsters[0].destroy//Unit();
				
				world.selected.occupyer = (Unit)Instantiate (world.smallAirPrefab, world.selected.transform.position, Quaternion.identity);
				////Unit t = generate//Unit('a', 's', -3, 0, 1);
				////Unit t = new //Unit(uScript);
				////monsters[0] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(0, 100, 99, 49), "Medium Air"))
			{
				////monsters[0].destroy//Unit();
				
				world.selected.occupyer = (Unit)Instantiate (world.mediumAirPrefab, world.selected.transform.position, Quaternion.identity);
				////Unit t = generate//Unit('a', 'm', -3, 0, 1);
				////Unit t = new //Unit(uScript);
				////monsters[0] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(0, 150, 99, 49), "Large Air"))
			{
				////monsters[0].destroy//Unit();
				
				world.selected.occupyer = (Unit)Instantiate (world.largeAirPrefab, world.selected.transform.position, Quaternion.identity);
				////Unit t = generate//Unit('a', 'l', -3, 0, 1);
				////Unit t = new //Unit(uScript);
				////monsters[0] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(100, 0, 99, 49), "Tiny Earth"))
			{
				////monsters[0].destroy//Unit();
				
				world.selected.occupyer = (Unit)Instantiate (world.tinyEarthPrefab, world.selected.transform.position, Quaternion.identity);
				////Unit t = generate//Unit('e', 't', -3, 0, 1);
				////Unit t = new //Unit(uScript);
				////monsters[0] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(100, 50, 99, 49), "Small Earth"))
			{
				////monsters[0].destroy//Unit();
				
				world.selected.occupyer = (Unit)Instantiate (world.smallEarthPrefab, world.selected.transform.position, Quaternion.identity);
				////Unit t = generate//Unit('e', 's', -3, 0, 1);
				////Unit t = new //Unit(uScript);
				////monsters[0] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(100, 100, 99, 49), "Medium Earth"))
			{
				////monsters[0].destroy//Unit();
				
				world.selected.occupyer = (Unit)Instantiate (world.mediumEarthPrefab, world.selected.transform.position, Quaternion.identity);
				//Unit t = generate//Unit('e', 'm', -3, 0, 1);
				////Unit t = new //Unit(uScript);
				////monsters[0] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(100, 150, 99, 49), "Large Earth"))
			{
				////monsters[0].destroy//Unit();
				//Unit t = generate//Unit('e', 'l', -3, 0, 1);
				
				world.selected.occupyer = (Unit)Instantiate (world.largeEarthPrefab, world.selected.transform.position, Quaternion.identity);
				////Unit t = new //Unit(uScript);
				////monsters[0] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(200, 0, 99, 49), "Tiny Fire"))
			{
				////monsters[0].destroy//Unit();
				//Unit t = generate//Unit('f', 't', -3, 0, 1);
				
				world.selected.occupyer = (Unit)Instantiate (world.tinyFirePrefab, world.selected.transform.position, Quaternion.identity);
				////Unit t = new //Unit(uScript);
				////monsters[0] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(200, 50, 99, 49), "Small Fire"))
			{
				////monsters[0].destroy//Unit();
				//Unit t = generate//Unit('f', 's', -3, 0, 1);
				
				world.selected.occupyer = (Unit)Instantiate (world.smallFirePrefab, world.selected.transform.position, Quaternion.identity);
				////Unit t = new //Unit(uScript);
				////monsters[0] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(200, 100, 99, 49), "Medium Fire"))
			{
				////monsters[0].destroy//Unit();
				//Unit t = generate//Unit('f', 'm', -3, 0, 1);
				
				world.selected.occupyer = (Unit)Instantiate (world.mediumFirePrefab, world.selected.transform.position, Quaternion.identity);
				////Unit t = new //Unit(uScript);
				////monsters[0] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(200, 150, 99, 49), "Large Fire"))
			{
				////monsters[0].destroy//Unit();
				//Unit t = generate//Unit('f', 'l', -3, 0, 1);
				
				world.selected.occupyer = (Unit)Instantiate (world.largeFirePrefab, world.selected.transform.position, Quaternion.identity);
				////Unit t = new //Unit(uScript);
				////monsters[0] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(300, 0, 99, 49), "Tiny Water"))
			{
				////monsters[0].destroy//Unit();
				//Unit t = generate//Unit('w', 't', -3, 0, 1);
				world.selected.occupyer = (Unit)Instantiate (world.tinyWaterPrefab, world.selected.transform.position, Quaternion.identity);
				////Unit t = new //Unit(uScript);
				////monsters[0] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(300, 50, 99, 49), "Small Water"))
			{
				////monsters[0].destroy//Unit();
				//Unit t = generate//Unit('w', 's', -3, 0, 1);
				world.selected.occupyer = (Unit)Instantiate (world.smallWaterPrefab, world.selected.transform.position, Quaternion.identity);
				////Unit t = new //Unit(uScript);
				////monsters[0] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(300, 100, 99, 49), "Medium Water"))
			{
				////monsters[0].destroy//Unit();
				//Unit t = generate//Unit('w', 'm', -3, 0, 1);
				world.selected.occupyer = (Unit)Instantiate (world.mediumWaterPrefab, world.selected.transform.position, Quaternion.identity);
				////Unit t = new //Unit(uScript);
				////monsters[0] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(300, 150, 99, 49), "Large Water"))
			{
				////monsters[0].destroy//Unit();
				//Unit t = generate//Unit('w', 'l', -3, 0, 1);
				world.selected.occupyer = (Unit)Instantiate (world.largeWaterPrefab, world.selected.transform.position, Quaternion.identity);
				////Unit t = new //Unit(uScript);
				////monsters[0] = t;
				//state = 0;
			}
		}
		/*if (GUI.Button(new Rect(0, 250, 400, 200), "Player 2"))
		{
			state = 2;
		}*/
		/*}
		else if (state == 2)
		{
			if (GUI.Button(new Rect(0, 0, 400, 200), "Player 1"))
			{
				state = 1;
			}
			
			if (GUI.Button(new Rect(0, 250, 99, 49), "Tiny Air"))
			{
				////monsters[1].destroy//Unit();
				//Unit t = generate//Unit('a', 't', 3, 0, 2);
				////Unit t = new //Unit(uScript);
				////monsters[1] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(0, 300, 99, 49), "Small Air"))
			{
				////monsters[1].destroy//Unit();
				//Unit t = generate//Unit('a', 's', 3, 0, 2);
				////Unit t = new //Unit(uScript);
				////monsters[1] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(0, 350, 99, 49), "Medium Air"))
			{
				////monsters[1].destroy//Unit();
				//Unit t = generate//Unit('a', 'm', 3, 0, 2);
				////Unit t = new //Unit(uScript);
				////monsters[1] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(0, 400, 99, 49), "Large Air"))
			{
				////monsters[1].destroy//Unit();
				//Unit t = generate//Unit('a', 'l', 3, 0, 2);
				////Unit t = new //Unit(uScript);
				////monsters[1] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(100, 250, 99, 49), "Tiny Earth"))
			{
				////monsters[1].destroy//Unit();
				//Unit t = generate//Unit('e', 't', 3, 0, 2);
				////Unit t = new //Unit(uScript);
				////monsters[1] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(100, 300, 99, 49), "Small Earth"))
			{
				//monsters[1].destroy//Unit();
				//Unit t = generate//Unit('e', 's', 3, 0, 2);
				////Unit t = new //Unit(uScript);
				//monsters[1] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(100, 350, 99, 49), "Medium Earth"))
			{
				//monsters[1].destroy//Unit();
				//Unit t = generate//Unit('e', 'm', 3, 0, 2);
				////Unit t = new //Unit(uScript);
				//monsters[1] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(100, 400, 99, 49), "Large Earth"))
			{
				//monsters[1].destroy//Unit();
				//Unit t = generate//Unit('e', 'l', 3, 0, 2);
				////Unit t = new //Unit(uScript);
				//monsters[1] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(200, 250, 99, 49), "Tiny Fire"))
			{
				//monsters[1].destroy//Unit();
				//Unit t = generate//Unit('f', 't', 3, 0, 2);
				////Unit t = new //Unit(uScript);
				//monsters[1] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(200, 300, 99, 49), "Small Fire"))
			{
				//monsters[1].destroy//Unit();
				//Unit t = generate//Unit('f', 's', 3, 0, 2);
				////Unit t = new //Unit(uScript);
				//monsters[1] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(200, 350, 99, 49), "Medium Fire"))
			{
				//monsters[1].destroy//Unit();
				//Unit t = generate//Unit('f', 'm', 3, 0, 2);
				////Unit t = new //Unit(uScript);
				//monsters[1] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(200, 400, 99, 49), "Large Fire"))
			{
				//monsters[1].destroy//Unit();
				//Unit t = generate//Unit('f', 'l', 3, 0, 2);
				////Unit t = new //Unit(uScript);
				//monsters[1] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(300, 250, 99, 49), "Tiny Water"))
			{
				//monsters[1].destroy//Unit();
				//Unit t = generate//Unit('w', 't', 3, 0, 2);
				////Unit t = new //Unit(uScript);
				//monsters[1] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(300, 300, 99, 49), "Small Water"))
			{
				//monsters[1].destroy//Unit();
				//Unit t = generate//Unit('w', 's', 3, 0, 2);
				////Unit t = new //Unit(uScript);
				//monsters[1] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(300, 350, 99, 49), "Medium Water"))
			{
				//monsters[1].destroy//Unit();
				//Unit t = generate//Unit('w', 'm', 3, 0, 2);
				////Unit t = new //Unit(uScript);
				//monsters[1] = t;
				//state = 0;
			}
			
			if (GUI.Button(new Rect(300, 400, 99, 49), "Large Water"))
			{
				//monsters[1].destroy//Unit();
				//Unit t = generate//Unit('w', 'l', 3, 0, 2);
				////Unit t = new //Unit(uScript);
				//monsters[1] = t;
				//state = 0;
			}
		}*/
	}
	/*public void generate//Unit(char ele, char type, int x, int y, int o)
	//This generates the prefab for the //Unit in question. The method is probably a little overly
	//complex, but I had a whole slew of errors trying to get this to work, so when I finally did
	//I just ran with it.
	{
		//Initializations of various things
		print ("stuck");
		hp = 10;
		actions = 2;
		xPos = x;
		yPos = y;
		owner = o;
		atk = 4;
		def = 3;
		Object temp;
		
		switch(ele)
		{
		case 'a':
			type1 = element.air;
			switch(type)
			{
			case 't':
				type2 = monsterType.tiny;
				monster = (Unit)Instantiate (a1, new Vector3(x, y, 0), Quaternion.identity);
				monsterInst = (Transform)monster;
				//monsterInst.gameObject = temp;
				break;
				
			case 's':
				type2 = monsterType.small;
				monster = (Unit)Instantiate (a2, new Vector3(x, y, 0), Quaternion.identity);
				monsterInst = (Transform)monster;
				//monsterInst.gameObject = temp;
				break;
				
			case 'm':
				type2 = monsterType.medium;
				monster = (Unit)Instantiate (a3, new Vector3(x, y, 0), Quaternion.identity);
				monsterInst = (Transform)monster;
				//monsterInst.gameObject = temp;
				break;
				
			case 'l':
				type2 = monsterType.large;
				monster = (Unit)Instantiate (a4, new Vector3(x, y, 0), Quaternion.identity);
				monsterInst = (Transform)monster;
				//monsterInst.gameObject = temp;
				break;
				
			default:
				break;
			}
			break;
		
		case 'e':
			type1 = element.earth;
			switch(type)
			{
			case 't':
				type2 = monsterType.tiny;
				monster = (Unit)Instantiate (e1, new Vector3(x, y, 0), Quaternion.identity);
				monsterInst = (Transform)monster;
				//monsterInst.gameObject = temp;
				break;
				
			case 's':
				type2 = monsterType.small;
				monster = (Unit)Instantiate (e2, new Vector3(x, y, 0), Quaternion.identity);
				monsterInst = (Transform)monster;
				//monsterInst.gameObject = temp;
				break;
				
			case 'm':
				type2 = monsterType.medium;
				monster = (Unit)Instantiate (e3, new Vector3(x, y, 0), Quaternion.identity);
				monsterInst = (Transform)monster;
				//monsterInst.gameObject = temp;
				break;
				
			case 'l':
				type2 = monsterType.large;
				monster = (Unit)Instantiate (e4, new Vector3(x, y, 0), Quaternion.identity);
				monsterInst = (Transform)monster;
				//monsterInst.gameObject = temp;
				break;
				
			default:
				break;
			}
			break;
			
		case 'f':
			type1 = element.fire;
			switch(type)
			{
			case 't':
				type2 = monsterType.tiny;
				monster = (Unit)Instantiate (f1, new Vector3(x, y, 0), Quaternion.identity);
				monsterInst = (Transform)monster;
				//monsterInst.gameObject = temp;
				break;
				
			case 's':
				type2 = monsterType.small;
				monster = (Unit)Instantiate (f2, new Vector3(x, y, 0), Quaternion.identity);
				monsterInst = (Transform)monster;
				//monsterInst.gameObject = temp;
				break;
				
			case 'm':
				type2 = monsterType.medium;
				monster = (Unit)Instantiate (f3, new Vector3(x, y, 0), Quaternion.identity);
				monsterInst = (Transform)monster;
				//monsterInst.gameObject = temp;
				break;
				
			case 'l':
				type2 = monsterType.large;
				monster = (Unit)Instantiate (f4, new Vector3(x, y, 0), Quaternion.identity);
				monsterInst = (Transform)monster;
				//monsterInst.gameObject = temp;
				break;
				
			default:
				break;
			}
			break;
		
		case 'w':
			type1 = element.water;
			switch(type)
			{
			case 't':
				type2 = monsterType.tiny;
				monster = (Unit)Instantiate (w1, new Vector3(x, y, 0), Quaternion.identity);
				monsterInst = (Transform)monster;
				//monsterInst.gameObject = temp;
				break;
				
			case 's':
				type2 = monsterType.small;
				monster = (Unit)Instantiate (w2, new Vector3(x, y, 0), Quaternion.identity);
				monsterInst = (Transform)monster;
				//monsterInst.gameObject = temp;
				break;
				
			case 'm':
				type2 = monsterType.medium;
				monster = (Unit)Instantiate (w3, new Vector3(x, y, 0), Quaternion.identity);
				monsterInst = (Transform)monster;
				//monsterInst.gameObject = temp;
				break;
				
			case 'l':
				type2 = monsterType.large;
				monster = (Unit)Instantiate (w4, new Vector3(x, y, 0), Quaternion.identity);
				monsterInst = (Transform)monster;
				//monsterInst.gameObject = temp;
				break;
				
			default:
				break;
			}
			break;
			
		default:
			break;
		}
	}*/
}
