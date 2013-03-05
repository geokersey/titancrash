using UnityEngine;
using System.Collections;

public class unit : MonoBehaviour {
	
	public enum element
	//I thought we might need this to refer to any elemental interactions in the game.
	//If we don't end up needing this, we can take it out.
	{
		air,
		earth,
		fire,
		water
	}
	
	public enum monsterType
	//This whole thing is totally unnecessary, I'll remove it later
	{
		tiny,
		small,
		medium,
		large
	}
	
	public int hp, atk, def, actions, xPos, yPos;
	public int owner; //Denotes owning player, 0: neutral, 1: player 1, 2: player 2
	public element type1; //Denotes the element of the creature
	public monsterType type2; //Denotes the class of the creature
	public Transform monsterInst; //holds the particular instance of the monster
	public bool generated; //Denotes whether a model has been generated
	public object monster;
	
	//air monsters
	public Transform a1; //tiny
	public Transform a2; //small
	public Transform a3; //medium
	public Transform a4; //large
	
	//earth monsters
	public Transform e1; //tiny
	public Transform e2; //small
	public Transform e3; //medium
	public Transform e4; //large
	
	//fire monsters
	public Transform f1; //tiny
	public Transform f2; //small
	public Transform f3; //medium
	public Transform f4; //large
	
	//water monsters
	public Transform w1; //tiny
	public Transform w2; //small
	public Transform w3; //medium
	public Transform w4; //large
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public unit()
	{
		
	}
	
	public unit(unit old)
	//Copy constructor to make sure that making new things works properly without
	//having to hard-code stupid numbers of things
	{
		hp = old.hp;
		actions = old.actions;
		owner = old.owner;
		monsterInst = old.monsterInst;
		generated = old.generated;
		atk = old.atk;
		def = old.def;
	}
	
	public void generateUnit(char ele, char type, int x, int y, int o)
	//This generates the prefab for the unit in question. The method is probably a little overly
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
				monster = Instantiate (a1, new Vector3(x, y, 0), Quaternion.identity);
				monsterInst = (Transform)monster;
				//monsterInst.gameObject = temp;
				break;
				
			case 's':
				type2 = monsterType.small;
				monster = Instantiate (a2, new Vector3(x, y, 0), Quaternion.identity);
				monsterInst = (Transform)monster;
				//monsterInst.gameObject = temp;
				break;
				
			case 'm':
				type2 = monsterType.medium;
				monster = Instantiate (a3, new Vector3(x, y, 0), Quaternion.identity);
				monsterInst = (Transform)monster;
				//monsterInst.gameObject = temp;
				break;
				
			case 'l':
				type2 = monsterType.large;
				monster = Instantiate (a4, new Vector3(x, y, 0), Quaternion.identity);
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
				monster = Instantiate (e1, new Vector3(x, y, 0), Quaternion.identity);
				monsterInst = (Transform)monster;
				//monsterInst.gameObject = temp;
				break;
				
			case 's':
				type2 = monsterType.small;
				monster = Instantiate (e2, new Vector3(x, y, 0), Quaternion.identity);
				monsterInst = (Transform)monster;
				//monsterInst.gameObject = temp;
				break;
				
			case 'm':
				type2 = monsterType.medium;
				monster = Instantiate (e3, new Vector3(x, y, 0), Quaternion.identity);
				monsterInst = (Transform)monster;
				//monsterInst.gameObject = temp;
				break;
				
			case 'l':
				type2 = monsterType.large;
				monster = Instantiate (e4, new Vector3(x, y, 0), Quaternion.identity);
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
				monster = Instantiate (f1, new Vector3(x, y, 0), Quaternion.identity);
				monsterInst = (Transform)monster;
				//monsterInst.gameObject = temp;
				break;
				
			case 's':
				type2 = monsterType.small;
				monster = Instantiate (f2, new Vector3(x, y, 0), Quaternion.identity);
				monsterInst = (Transform)monster;
				//monsterInst.gameObject = temp;
				break;
				
			case 'm':
				type2 = monsterType.medium;
				monster = Instantiate (f3, new Vector3(x, y, 0), Quaternion.identity);
				monsterInst = (Transform)monster;
				//monsterInst.gameObject = temp;
				break;
				
			case 'l':
				type2 = monsterType.large;
				monster = Instantiate (f4, new Vector3(x, y, 0), Quaternion.identity);
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
				monster = Instantiate (w1, new Vector3(x, y, 0), Quaternion.identity);
				monsterInst = (Transform)monster;
				//monsterInst.gameObject = temp;
				break;
				
			case 's':
				type2 = monsterType.small;
				monster = Instantiate (w2, new Vector3(x, y, 0), Quaternion.identity);
				monsterInst = (Transform)monster;
				//monsterInst.gameObject = temp;
				break;
				
			case 'm':
				type2 = monsterType.medium;
				monster = Instantiate (w3, new Vector3(x, y, 0), Quaternion.identity);
				monsterInst = (Transform)monster;
				//monsterInst.gameObject = temp;
				break;
				
			case 'l':
				type2 = monsterType.large;
				monster = Instantiate (w4, new Vector3(x, y, 0), Quaternion.identity);
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
	}
	
	public void destroyUnit()
	//Kills the attached prefab
	{
		Destroy(monsterInst.gameObject);
	}

	public void moveUnit(int x, int y, int z) //Replace this with however you want to move things
	{
		monsterInst.Translate(new Vector3(x, y, z));
		
	}
}
