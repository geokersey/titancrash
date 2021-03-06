using UnityEngine;
using System.Collections;

public class SpellManager : MonoBehaviour 
{
	//temp variables to make script compile. Only used for variables accessable in the project not currently included
	private GameObject[,] GridTiles = new GameObject[99,99];
	public GameObject grid;
	Grid other;
	public GameObject Fireball;
	public GameObject Lightningbolt;
	public GameObject Tornado;
	public GameObject RagingFire;
	public GameObject NaturesBounty;
	public GameObject StoneArmor;
	public GameObject Windwalk;
	public GameObject Chill;
	public GameObject Hawkeye;
	public GameObject Counterspell;
	public GameObject Dispel;
	public SoundManager Sounds;
	//other variables
	public bool SpellCasting = false;
	private bool found = false;
	private bool toggleTxt = false;
	private bool visible = true;
	public Unit tornadoVictim;
	public int spell = -1;
	public int[] spellRanges;
	
	void Start ()
	{
		tornadoVictim = null;
		other = grid.GetComponent<Grid>();
		spellRanges = new int[20];
		spellRanges[0] = 10; //hawkeye
		spellRanges[1] = 0; //reveal X
		spellRanges[2] = 4; //Tornado1
		spellRanges[3] = 4; // Tornado2
		spellRanges[4] = 3;//2; // wind walk
		spellRanges[5] = 3;//2; // lightning bolt
		spellRanges[6] = 5; // scorched earth
		spellRanges[7] = 3; // fireball
		spellRanges[8] = 5; // raging fire
		spellRanges[9] = 3; // eruption
		spellRanges[10] = 3; //chill
		spellRanges[11] = 3; // glacial wall
		spellRanges[12] = 3; // winters call
		spellRanges[13] = 4; //frost
		spellRanges[14] = 3;//2; // nature's bounty
		spellRanges[15] = 3;//2; // stone armor
		spellRanges[16] = 0; // one with nature X
		spellRanges[17] = 0; // heaven to earth X
		spellRanges[18] = 10; // counterspell
		spellRanges[19] = 5; // dispel
	}
	
	void Update () 
	{
		int x, y;
		RaycastHit hit;
   		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		if(toggleTxt && Input.GetMouseButtonDown(0) && Physics.Raycast(ray,out hit ,100))
		{ //check if the user is casting a spell, then if the mouse left button is pressed, then if the user is hovering over a tile to be selected
			
			//ApplySpell(hit.transform.position.x,hit.transform.position.y);
			for(int i = 0; i < other.size; i++)
			{
				if(found)
				{
					break;
				}
				for(int j = 0; j < other.size; j++)
				{
					if(found)
					{
						break;
					}
					//Debug.Log (found);
					//Debug.Log (hit.transform.gameObject);
					if(hit.transform == other.map[i,j].transform)
					{
						//found the tile selected
						
						/*if(IsInRange(i,j,k))
						{
							ApplySpell(i,j);
							found = true;
						}*/
						
						//Assuming infinite range
						ApplySpell(other.map[i,j]);
						found = true;
						//Debug.Log ("Fireball!");
					}
				}
			}
		}
		found = false;
		
		
		if(Input.GetKeyDown("b"))
		{
			if(!toggleTxt)
			{
				toggleTxt = true;
			}
			else
			{
				toggleTxt = false;
			}
		}
	}
	
	void OnGUI()
	{
		if(visible){
			//win spell
			if (other.players[other.activePlayer].techAvailable.Count>0&&other.players[other.activePlayer].techAvailable[28] == 2){
				if (GUI.Button(new Rect(850, 0, 99, 94), "Archmagus\nSpectacle", other.GUIfunstuff.button)){
					if(other.activePlayer == 0){
						other.p2as = 10;
					}
					else{
						other.p1as = 10;
					}
				}
			}
			//Hawkeye
			if (other.players[other.activePlayer].techAvailable.Count>0&&other.players[other.activePlayer].techAvailable[0] == 2)
			{
				GUI.backgroundColor = Color.white;
			}
			else
			{
				GUI.backgroundColor = Color.red;
			}
			if (GUI.Button(new Rect(50, 0, 99, 49), "Hawkeye", other.GUIfunstuff.button) && other.players[other.activePlayer].techAvailable[0] == 2){
				if (other.players[other.activePlayer].resources[1] >= 1)
				{
				spell = 0;
				}
				//else
				//{
				//	if (GUI.Button(new Rect(200, 200, 100, 50), "Not enough Resources", other.GUIfunstuff.button))
				//	{
				//		spell = -1;
				//	}
				//}
			}
			
			//Tornado
			if (other.players[other.activePlayer].techAvailable.Count>10&&other.players[other.activePlayer].techAvailable[10] == 2)
			{
				GUI.backgroundColor = Color.white;
			}
			else
			{
				GUI.backgroundColor = Color.red;
			}
			if (GUI.Button(new Rect(145, 0, 99, 49), "Tornado", other.GUIfunstuff.button) && other.players[other.activePlayer].techAvailable[10] == 2){
				if (other.players[other.activePlayer].resources[1] >= 2)
				{
					spell = 2;
				}
				//else
				//{
				//	if (GUI.Button(new Rect(200, 200, 100, 50), "Not enough Resources", other.GUIfunstuff.button))
				//	{
				//		spell = -1;
				//	}
				//}
			}
			
			//Wind Walk
			if (other.players[other.activePlayer].techAvailable.Count>17&&other.players[other.activePlayer].techAvailable[16] == 2)
			{
				GUI.backgroundColor = Color.white;
			}
			else
			{
				GUI.backgroundColor = Color.red;
			}
			if (GUI.Button(new Rect(240, 0, 99, 49), "Wind Walk", other.GUIfunstuff.button) && other.players[other.activePlayer].techAvailable[16] == 2){
				if (other.players[other.activePlayer].resources[1] >= 2)
				{
					spell = 4;
				}
				//else
				//{
				//	if (GUI.Button(new Rect(200, 200, 100, 50), "Not enough Resources", other.GUIfunstuff.button))
				//	{
				//		spell = -1;
				//	}
				//}
			}
			
			//Lightning Bolt
			if (other.players[other.activePlayer].techAvailable.Count>22&&other.players[other.activePlayer].techAvailable[21] == 2)
			{
				GUI.backgroundColor = Color.white;
			}
			else
			{
				GUI.backgroundColor = Color.red;
			}
			if (GUI.Button(new Rect(335, 0, 99, 49), "Lightning Bolt", other.GUIfunstuff.button) && other.players[other.activePlayer].techAvailable[21] == 2){
				if (other.players[other.activePlayer].resources[1] >= 4)
				{
					spell = 5;
				}
				//else
				//{
				//	if (GUI.Button(new Rect(200, 200, 100, 50), "Not enough Resources", other.GUIfunstuff.button))
				//	{
				//		spell = -1;
				//	}
				//}
			}
			
			//Scorch Earth
			if (other.players[other.activePlayer].techAvailable.Count>1&&other.players[other.activePlayer].techAvailable[1] == 2)
			{
				GUI.backgroundColor = Color.white;
			}
			else
			{
				GUI.backgroundColor = Color.red;
			}
			if (GUI.Button(new Rect(430, 0, 99, 49), "Scorch Earth", other.GUIfunstuff.button) && other.players[other.activePlayer].techAvailable[1] == 2){
				if (other.players[other.activePlayer].resources[3] >= 1)
				{
				spell = 6;
				}
				//else
				//{
				//	if (GUI.Button(new Rect(200, 200, 100, 50), "Not enough Resources", other.GUIfunstuff.button))
				//	{
				//		spell = -1;
				//	}
				//}
			}
			
			//Fireball
			if (other.players[other.activePlayer].techAvailable.Count>11&&other.players[other.activePlayer].techAvailable[11] == 2)
			{
				GUI.backgroundColor = Color.white;
			}
			else
			{
				GUI.backgroundColor = Color.red;
			}
			if (GUI.Button(new Rect(525, 0, 99, 49), "Fireball", other.GUIfunstuff.button) && other.players[other.activePlayer].techAvailable[11] == 2){
				if (other.players[other.activePlayer].resources[3] >= 2)
				{
					spell = 7;
				}
				//else
				//{
				//	if (GUI.Button(new Rect(200, 200, 100, 50), "Not enough Resources", other.GUIfunstuff.button))
				//	{
				//		spell = -1;
				//	}
				//}
			}
			
			//Raging Fire
			if (other.players[other.activePlayer].techAvailable.Count>19&&other.players[other.activePlayer].techAvailable[18] == 2)
			{
				GUI.backgroundColor = Color.white;
			}
			else
			{
				GUI.backgroundColor = Color.red;
			}
			if (GUI.Button(new Rect(620, 0, 99, 49), "Raging Fire", other.GUIfunstuff.button) && other.players[other.activePlayer].techAvailable[18] == 2){
				if (other.players[other.activePlayer].resources[3] >= 3)
				{
					spell = 8;
				}
				//else
				//{
				//	if (GUI.Button(new Rect(200, 200, 100, 50), "Not enough Resources", other.GUIfunstuff.button))
				//	{
				//		spell = -1;
				//	}
				//}
			}
			
			//Eruption
			if (other.players[other.activePlayer].techAvailable.Count>23&&other.players[other.activePlayer].techAvailable[22] == 2)
			{
				GUI.backgroundColor = Color.white;
			}
			else
			{
				GUI.backgroundColor = Color.red;
			}
			if (GUI.Button(new Rect(715, 0, 99, 49), "Eruption", other.GUIfunstuff.button) && other.players[other.activePlayer].techAvailable[22] == 2){
				if (other.players[other.activePlayer].resources[3] >= 5)
				{
					spell = 9;
				}
				//else
				//{
				//	if (GUI.Button(new Rect(200, 200, 100, 50), "Not enough Resources", other.GUIfunstuff.button))
				//	{
				//		spell = -1;
				//	}
				//}
			}
			
			//Chill
			if (other.players[other.activePlayer].techAvailable.Count>2&&other.players[other.activePlayer].techAvailable[2] == 2)
			{
				GUI.backgroundColor = Color.white;
			}
			else
			{
				GUI.backgroundColor = Color.red;
			}
			if (GUI.Button(new Rect(50, 45, 99, 49), "Chill", other.GUIfunstuff.button) && other.players[other.activePlayer].techAvailable[2] == 2){
				if (other.players[other.activePlayer].resources[4] >= 1)
				{
					spell = 10;
				}
				//else
				//{
				//	if (GUI.Button(new Rect(200, 200, 100, 50), "Not enough Resources", other.GUIfunstuff.button))
				//	{
				//		spell = -1;
				//	}
				//}
			}
			
			//Glacial Wall
			if (other.players[other.activePlayer].techAvailable.Count>13&&other.players[other.activePlayer].techAvailable[12] == 2)
			{
				GUI.backgroundColor = Color.white;
			}
			else
			{
				GUI.backgroundColor = Color.red;
			}
			if (GUI.Button(new Rect(145, 45, 99, 49), "Glacial Wall", other.GUIfunstuff.button) && other.players[other.activePlayer].techAvailable[12] == 2){
				if (other.players[other.activePlayer].resources[4] >= 2)
				{
					spell = 11;
				}
				//else
				//{
				//	if (GUI.Button(new Rect(200, 200, 100, 50), "Not enough Resources", other.GUIfunstuff.button))
				//	{
				//		spell = -1;
				//	}
				//}
			}
			
			//Winter's Call
			if (other.players[other.activePlayer].techAvailable.Count>14&&other.players[other.activePlayer].techAvailable[13] == 2)
			{
				GUI.backgroundColor = Color.white;
			}
			else
			{
				GUI.backgroundColor = Color.red;
			}
			if (GUI.Button(new Rect(240, 45, 99, 49), "Winter's Call", other.GUIfunstuff.button) && other.players[other.activePlayer].techAvailable[13] == 2){
				if (other.players[other.activePlayer].resources[4] >= 2)
				{
					spell = 12;
				}
				//else
				//{
				//	if (GUI.Button(new Rect(200, 200, 100, 50), "Not enough Resources", other.GUIfunstuff.button))
				//	{
				//		spell = -1;
				//	}
				//}
			}
			
			//Frost
			if (other.players[other.activePlayer].techAvailable.Count>15&&other.players[other.activePlayer].techAvailable[14] == 2)
			{
				GUI.backgroundColor = Color.white;
			}
			else
			{
				GUI.backgroundColor = Color.red;
			}
			if (GUI.Button(new Rect(335, 45, 99, 49), "Frost", other.GUIfunstuff.button) && other.players[other.activePlayer].techAvailable[14] == 2){
				if (other.players[other.activePlayer].resources[4] >= 3)
				{
					spell = 13;
				}
				//else
				//{
				//	if (GUI.Button(new Rect(200, 200, 100, 50), "Not enough Resources", other.GUIfunstuff.button))
				//	{
				//		spell = -1;
				//	}
				//}
			
			//Nature's Bounty
			}
			if (other.players[other.activePlayer].techAvailable.Count>3&&other.players[other.activePlayer].techAvailable[3] == 2)
			{
				GUI.backgroundColor = Color.white;
			}
			else
			{
				GUI.backgroundColor = Color.red;
			}
			if (GUI.Button(new Rect(430, 45, 99, 49), "Nature's Bounty", other.GUIfunstuff.button) && other.players[other.activePlayer].techAvailable[3] == 2){
				if (other.players[other.activePlayer].resources[2] >= 1)
				{
					spell = 14;
				}
				//else
				//{
				//	if (GUI.Button(new Rect(200, 200, 100, 50), "Not enough Resources", other.GUIfunstuff.button))
				//	{
				//		spell = -1;
				//	}
				//}
			}
			
			//Stone Armor
			if (other.players[other.activePlayer].techAvailable.Count>16&&other.players[other.activePlayer].techAvailable[15] == 2)
			{
				GUI.backgroundColor = Color.white;
			}
			else
			{
				GUI.backgroundColor = Color.red;
			}
			if (GUI.Button(new Rect(525, 45, 99, 49), "Stone Armor", other.GUIfunstuff.button) && other.players[other.activePlayer].techAvailable[15] == 2){
				if (other.players[other.activePlayer].resources[2] >= 2)
				{
					spell = 15;
				}
				//else
				//{
				//	if (GUI.Button(new Rect(200, 200, 100, 50), "Not enough Resources", other.GUIfunstuff.button))
				//	{
				//		spell = -1;
				//	}
				//}
			}
			
			//Counterspell
			if (other.players[other.activePlayer].techAvailable.Count>4&&other.players[other.activePlayer].techAvailable[4] == 2)
			{
				GUI.backgroundColor = Color.white;
			}
			else
			{
				GUI.backgroundColor = Color.red;
			}
			if (GUI.Button(new Rect(620, 45, 99, 49), "Counterspell", other.GUIfunstuff.button) && other.players[other.activePlayer].techAvailable[4] == 2){
				if (other.players[other.activePlayer].resources[0] >= 1)
				{
					spell = 18;
				}
				//else
				//{
				//	if (GUI.Button(new Rect(200, 200, 100, 50), "Not enough Resources", other.GUIfunstuff.button))
				//	{
				//		spell = -1;
				//	}
				//}
			}
			
			//Dispel Magic
			if (other.players[other.activePlayer].techAvailable.Count>5&&other.players[other.activePlayer].techAvailable[5] == 2)
			{
				GUI.backgroundColor = Color.white;
			}
			else
			{
				GUI.backgroundColor = Color.red;
			}
			if (GUI.Button(new Rect(715, 45, 99, 49), "Dispel Magic", other.GUIfunstuff.button) && other.players[other.activePlayer].techAvailable[5] == 2){
				if (other.players[other.activePlayer].resources[0] >= 2)
				{
					spell = 19;
				}
				//else
				//{
				//	if (GUI.Button(new Rect(200, 200, 100, 50), "Not enough Resources", other.GUIfunstuff.button))
				//	{
				//		spell = -1;
				//	}
				//}
			}
			Debug.Log ("Spell selected: " + spell);
		}
	}
	
	
	
	void IsCasting(bool state)
	{
		//SpellCasting = state;	
	}
	
	void ApplySpell(Tile target)
	{
		GameObject spell;
		spell = (GameObject)Instantiate(Fireball,new Vector3(target.transform.position.x, 9, target.transform.position.z), Quaternion.identity);
		spell.SendMessage("ApplyEffect", target);
	}
	public void cast(Tile target){
		//Debug.Log("casting a spel");
		if (spell == 0){
			Debug.Log ("spell casting");
			if(target.findTower(spellRanges[0])){
				Debug.Log ("tower found");
			//hawkeye
				target.see (3);
				other.players[other.activePlayer].resO.Add (new ResOccupied(1,1,1));
				other.players[other.activePlayer].resources[1]--;
				spell = -1;
				GameObject thisObj = (GameObject)Instantiate(Hawkeye, new Vector3(target.transform.position.x, 2, target.transform.position.z), Quaternion.identity);
				Sounds.Play(0);
				Destroy (thisObj, 1f);
			}
		}
		
		/*else if (spell == 1){
			//reveal
			if(target.findTower (3)){
				target.see(2);
				other.players[other.activePlayer].resO.Add (new ResOccupied(1,1,1));
				other.players[other.activePlayer].resources[1]--;
				if (target.occupyer != null){
					//stun annd reveeall
				}
				spell = -1;
			}
				
		}*/
		else if (spell ==2){
			//tornado
			//Debug.Log ("tornado takeoff");
			if(target.findTower (spellRanges[2])){
				if (target.occupyer != null){
					spell = 3;
					tornadoVictim = target.occupyer;
				}
			}
		}
		else if (spell == 3){
			//tornado landing
			//Debug.Log ("tornado Landing");
			if(target.findTower (spellRanges[3])&&tornadoVictim != null && target.occupyer == null){
				StartCoroutine("tornado", target);
			}
				
		}
		else if (spell == 4){
			//wind walk
			if(target.occupyer!=null && target.findTower (spellRanges[4])){
				other.players[other.activePlayer].resO.Add (new ResOccupied(1,1,2));
				other.players[other.activePlayer].resources[1]-=2;
				Buff temp = new Buff();
				temp.movement = 2;
				temp.turns = 1;
				target.occupyer.startingMovePoints+=2;
				target.occupyer.availableMovePoints+=2;
				
				temp.Buffeffect = (GameObject)Instantiate(Windwalk, new Vector3(target.occupyer.transform.position.x, .5f, target.occupyer.transform.position.z), Quaternion.identity);
				temp.Buffeffect.transform.parent = target.occupyer.transform;
				//target.occupyer.buffs.Add (temp);
				target.occupyer.RenewRenderer();
				
				target.occupyer.buffs.Add (temp);
				spell = -1;
				Debug.Log ("WindWalkCast");
			}		
			
		}
		else if (spell == 5){
			//lightning bolt
			if (target.findTower (spellRanges[5])&&target.occupyer!=null){
				StartCoroutine("LightningBolt", target);
				Sounds.Play(5);
				Debug.Log ("LightningCast");
			}
		}
		else if (spell == 6){
			//scorched earth
			if (target.findTower (spellRanges[6])){
				other.players[other.activePlayer].resO.Add (new ResOccupied(4,3,1));
				other.players[other.activePlayer].resources[3]-=1;
				target.scorch = 4*2;
				spell = -1;
				Sounds.Play(6);
			}
		}
		else if (spell == 7){		
			//fireball
			if (target.occupyer != null && target.findTower (spellRanges[7])){
				Sounds.Play(7);
				StartCoroutine("FireBall", target);
			}
		}
		else if (spell == 8){
			// raging fire
			if (target.occupyer != null && target.findTower(spellRanges[8])){
				other.players[other.activePlayer].resO.Add (new ResOccupied(3,3,3));
				other.players[other.activePlayer].resources[3]-=3;
				
				Buff temp = new Buff();
				target.occupyer.atk +=3;
				temp.attack = 3;
				temp.turns = 3;
				temp.Buffeffect = (GameObject)Instantiate(RagingFire, new Vector3(target.occupyer.transform.position.x, .5f, target.occupyer.transform.position.z), Quaternion.identity);
				temp.Buffeffect.transform.parent = target.occupyer.transform;
				target.occupyer.buffs.Add (temp);
				target.occupyer.RenewRenderer();
				spell = -1;
				Sounds.Play(8);
				
			}	
		}
		else if (spell == 9){
			//eruption.
			Debug.Log ("eruption");
			//if(!(target.hasFont||target.hasTower||target.resourceQuantity >0)&&target.findTower (3)){
			if(target.findTower (spellRanges[9])){
				other.players[other.activePlayer].resO.Add (new ResOccupied(4,3,5));
				other.players[other.activePlayer].resources[3]-=5;
				
				if (target.terrain >=0){
					target.terrain = 2; //change to whatever mountain is
					target.pointsRequired = 3;
					target.mountainModel = (GameObject)Instantiate (other.mountainModelPrefab,new Vector3(target.transform.position.x,target.transform.position.y + .1f,target.transform.position.z), Quaternion.Euler(0,30 + (60*Random.Range (0,10)),0));
					//target.mountainModel.layer = 2;
					target.renderer.enabled = false;
					
					target.transformTime = 4*2;
					//switch the model
					other.map[target.x+1,target.y].scorch = 4;
					other.map[target.x-1,target.y].scorch = 4;
					other.map[target.x+1,target.y-1].scorch = 4;
					other.map[target.x-1,target.y+1].scorch = 4;
					other.map[target.x,target.y+1].scorch = 4;
					other.map[target.x,target.y-1].scorch = 4;
				}
				spell = -1;
			}
			
		}
		else if (spell == 10){
			//chill
			
			if (target.occupyer != null && target.findTower(spellRanges[10])){
				other.players[other.activePlayer].resO.Add (new ResOccupied(3,4,1));
				other.players[other.activePlayer].resources[4]-=1;
				
				Buff temp = new Buff();
				temp.turns = 3;
				temp.movement = -1;
				temp.attack = -1;
				temp.defense = -1;
				
				target.occupyer.atk -= 1;
				target.occupyer.def -=1;
				target.occupyer.availableMovePoints-=1;
				target.occupyer.startingMovePoints -=1;
				
				temp.Buffeffect = (GameObject)Instantiate(Chill, new Vector3(target.occupyer.transform.position.x, .5f, target.occupyer.transform.position.z), Quaternion.identity);
				temp.Buffeffect.transform.parent = target.occupyer.transform;
				//target.occupyer.buffs.Add (temp);
				target.occupyer.RenewRenderer();
				
				target.occupyer.buffs.Add (temp);
				
				spell = -1;
				Sounds.Play(10);
			}
		}
		else if (spell == 11){
			//glacial wall
			if(target.findTower (spellRanges[11])){
				
				other.players[other.activePlayer].resO.Add (new ResOccupied(3,4,2));
				other.players[other.activePlayer].resources[4]-=2;
			
				target.terrain = 2; //change to whatever mountain is
				target.pointsRequired = 3;
				target.mountainModel = (GameObject)Instantiate (other.mountainModelPrefab,new Vector3(target.transform.position.x,target.transform.position.y + .1f,target.transform.position.z), Quaternion.Euler(0,30 + (60*Random.Range (0,10)),0));
				//target.mountainModel.layer = 2;
				target.renderer.enabled = false;
				
				target.pointsRequired = 3;
				target.transformTime = 3*2;
				//change model
				
				spell = -1;
			}
		}
		else if (spell == 12){
			//wineter's call now sleeps until hp changes.
			if(target.occupyer!= null && target.findTower (spellRanges[12])){
				other.players[other.activePlayer].resO.Add (new ResOccupied(4,4,2));
				other.players[other.activePlayer].resources[4]-=2;
				target.occupyer.sleep = target.occupyer.hp;
				target.occupyer.availableMovePoints = 0;
				target.occupyer.sleepTime = 4;
				spell = -1;
				Sounds.Play(12);
			}
		}
		else if (spell == 13){
			//frost
			if(target.findTower (4)){
				other.players[other.activePlayer].resO.Add (new ResOccupied(4,4,3));
				other.players[other.activePlayer].resources[4]-=3;
				target.freeze(1, 4);
				Sounds.Play(13);
				spell = -1;
			}
		}
		else if (spell == 14){
			//natures bounty
			if(target.occupyer!=null&&target.findTower (spellRanges[14])){
				other.players[other.activePlayer].resO.Add (new ResOccupied(1,2,1));
				other.players[other.activePlayer].resources[2]-=1;
				Sounds.Play(14);
				GameObject thisObj = (GameObject)Instantiate(NaturesBounty, new Vector3(target.transform.position.x, 2f, target.transform.position.z), Quaternion.identity);
				Destroy (thisObj, 1f);
				target.occupyer.hp +=3;
				//if (target.occupyer.hp >target.occupyer. max
				spell = -1;	
				Debug.Log ("NaturesCast");
			}
		}
		else if (spell == 15){
			//stone armor
			if(target.occupyer!=null&&target.findTower (spellRanges[15])){
				other.players[other.activePlayer].resO.Add (new ResOccupied(3,2,2));
				other.players[other.activePlayer].resources[2]-=2;
				
				Buff temp = new Buff();
				temp.defense = 3;
				temp.turns = 3;
				Sounds.Play(15);
				temp.Buffeffect = (GameObject)Instantiate(StoneArmor, new Vector3(target.occupyer.transform.position.x, .5f, target.occupyer.transform.position.z), Quaternion.identity);
				temp.Buffeffect.transform.parent = target.occupyer.transform;
				target.occupyer.RenewRenderer();
				
				target.occupyer.def += 3; 
				target.occupyer.buffs.Add (temp);
				spell = -1;
				Debug.Log ("StoneCast");
			}
		}
		/*else if (spell == 16){
			
			//one with nature
			//ignore for now possibly forever
			if(target.occupyer!=null&&target.findTower (4)){
				other.players[other.activePlayer].resO.Add (new ResOccupied(2,3,3));
				other.players[other.activePlayer].resources[3]-=3;
			}
		}*/
		/*else if (spell == 17){
			//heaven to earth
			//ignore for now
			if(target.occupyer!=null&&target.findTower (2)){
				other.players[other.activePlayer].resO.Add (new ResOccupied(3,3,3));
				other.players[other.activePlayer].resources[3]-=3;
			}
		}*/
		else if (spell == 18){
			//counterspell
			//ignore for now
			if(!((target.x==other.x0&&target.y==other.y0)||(target.x==other.x0&&target.y==other.y0))&&target.occupyer==null&&target.findTower (spellRanges[18])){
				
				other.players[other.activePlayer].resO.Add (new ResOccupied(1,0,3));
				other.players[other.activePlayer].resources[0]-=3;
				target.owner += 1*4;
				GameObject thisObj = (GameObject)Instantiate(Counterspell, new Vector3(target.transform.position.x, .2f, target.transform.position.z), Quaternion.Euler(-90,0,0));
				Sounds.Play(18);
				Destroy (thisObj, 1f);
				spell = -1;
			}
		}
		else if (spell == 19){
			//dispel magic
			if(target.findTower (spellRanges[19])){
				other.players[other.activePlayer].resO.Add (new ResOccupied(1,0,2));
				other.players[other.activePlayer].resources[0]-=2;
				target.dispell();
				GameObject thisObj = (GameObject)Instantiate(Dispel, new Vector3(target.transform.position.x, .2f, target.transform.position.z), Quaternion.identity);
				spell = -1;
				Destroy (thisObj, 1.5f);
			}
		}
		
	}
	void IsInRange(int x, int y, int d)
	{
		//irrelevant function
	}
	bool spellAvailable(){
		if (spell == 1){
			return true;
		}
		else return true;
		
	}
	public void show(){
		visible = true;
	}
	public void hide(){
		visible = false;
	}
	IEnumerator tornado (Tile target)
	{
		other.players[other.activePlayer].resO.Add (new ResOccupied(1,1,2));
		other.players[other.activePlayer].resources[1]-=2;
		GameObject thisObj = (GameObject)Instantiate(Tornado, new Vector3(tornadoVictim.transform.position.x, .2f, tornadoVictim.transform.position.z), Quaternion.Euler(-90,0,0));
		yield return new WaitForSeconds(.5f);
		Destroy (thisObj, .75f);
		Tile old = other.map[tornadoVictim.x,tornadoVictim.y];
		old.deselect ();
		old.occupyer = null;
		target.occupyer = tornadoVictim;
		tornadoVictim.x = target.x;
		tornadoVictim.y = target.y;
		tornadoVictim.transform.position = target.transform.position;
		target.capture (tornadoVictim.owner);
		spell = -1;
		target.choose ();
	}
	IEnumerator LightningBolt (Tile target)
	{
		Debug.Log ("Ligtning");
		other.players[other.activePlayer].resO.Add (new ResOccupied(1,1,4));
		other.players[other.activePlayer].resources[1]-=4;
		GameObject thisObj = (GameObject)Instantiate(Lightningbolt, new Vector3(target.transform.position.x, 1.5f, target.transform.position.z), Quaternion.Euler(-90,0,0));
		yield return new WaitForSeconds(1.25f);
		Destroy(thisObj);
		target.occupyer.hp -= 8;
		spell =-1;
	}
	
	IEnumerator FireBall(Tile target)
	{
		other.players[other.activePlayer].resO.Add (new ResOccupied(1,3,2));
		other.players[other.activePlayer].resources[3]-=2;		
		GameObject thisObj = (GameObject)Instantiate(Fireball, new Vector3(target.transform.position.x, 9, target.transform.position.z), Quaternion.identity);
		yield return new WaitForSeconds(2);
		Destroy (thisObj);
		target.occupyer.hp-=3;
		Buff temp = new Buff();
		temp.damage = 1;
		temp.turns = 5;
		target.occupyer.buffs.Add (temp);
		spell = -1;
	}
}
