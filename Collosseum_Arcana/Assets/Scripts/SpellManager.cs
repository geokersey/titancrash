using UnityEngine;
using System.Collections;

public class SpellManager : MonoBehaviour 
{
	//temp variables to make script compile. Only used for variables accessable in the project not currently included
	private GameObject[,] GridTiles = new GameObject[99,99];
	public GameObject grid;
	Grid other;
	public GameObject Fireball;
	//other variables
	public bool SpellCasting = false;
	private bool found = false;
	private bool toggleTxt = false;
	private bool visible = true;
	public Unit tornadoVictim;
	public int spell;
	void Start ()
	{
		tornadoVictim = null;
		other = grid.GetComponent<Grid>();
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
			if (GUI.Button(new Rect(100, 0, 99, 49), "hawkeye")){
				spell = 0;
			}
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
		if (spell == 0){
			if(target.findTower(10)){
			//hawkeye
				target.see (2);
				other.players[other.activePlayer].resO.Add (new ResOccupied(1,1,1));
				other.players[other.activePlayer].resources[1]--;
				spell = -1;
			}
		}
		
		else if (spell == 1){
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
				
		}
		else if (spell ==2){
			//tornado
			if(target.findTower (4)){
				if (target.occupyer != null){
					spell = 3;
					tornadoVictim = target.occupyer;
				}
			}
		}
		else if (spell == 3){
			//tornado landing
			if(target.findTower (4)&&tornadoVictim != null && target.occupyer == null){
				//other.players[other.activePlayer].resO.Add (new ResOccupied(1,1,2));
				//other.players[other.activePlayer].resources[1]-=2;
				
				
				Tile old = other.map[tornadoVictim.x,tornadoVictim.y];
				old.deselect ();
				old.occupyer = null;
				target.occupyer = tornadoVictim;
				tornadoVictim.x = target.x;
				tornadoVictim.y = target.y;
				tornadoVictim.transform.position = target.transform.position;
				target.capture (tornadoVictim.owner);
				spell = -1;
			}
				
		}
		else if (spell == 4){
			//wind walk
			if(target.occupyer!=null && target.findTower (2)){
				other.players[other.activePlayer].resO.Add (new ResOccupied(1,1,2));
				other.players[other.activePlayer].resources[1]-=2;
				Buff temp = new Buff();
				temp.movement = 2;
				temp.turns = 2;
				target.occupyer.startingMovePoints+=2;
				target.occupyer.availableMovePoints+=2;
				target.occupyer.buffs.Add (temp);
				spell = -1;
				
			}		
			
		}
		else if (spell == 5){
			//lightning bolt
			if (target.findTower (2)&&target.occupyer!=null){
				other.players[other.activePlayer].resO.Add (new ResOccupied(1,1,4));
				other.players[other.activePlayer].resources[1]-=4;

				target.occupyer.hp -= 8;
				spell =-1;
			}
		}
		else if (spell == 6){
			//scorched earth
			if (target.findTower (5)){
				other.players[other.activePlayer].resO.Add (new ResOccupied(4,2,1));
				other.players[other.activePlayer].resources[2]-=1;
				target.scorch = 4*2;
				spell = -1;
			}
		}
		else if (spell == 7){		
			//fireball
			if (target.occupyer != null && target.findTower (3)){
				other.players[other.activePlayer].resO.Add (new ResOccupied(1,2,2));
				other.players[other.activePlayer].resources[2]-=2;
				
				target.occupyer.hp-=3;
				Buff temp = new Buff();
				temp.damage = 1;
				temp.turns = 5;
				spell = -1;
			}
		}
		else if (spell == 8){
			// raging fire
			if (target.occupyer != null && target.findTower(5)){
				other.players[other.activePlayer].resO.Add (new ResOccupied(3,2,3));
				other.players[other.activePlayer].resources[2]-=3;
				
				Buff temp = new Buff();
				temp.attack = 3;
				temp.turns = 3;
				spell = -1;
				
			}	
		}
		else if (spell == 9){
			//eruption.
			if(target.findTower (3)){
				other.players[other.activePlayer].resO.Add (new ResOccupied(4,2,5));
				other.players[other.activePlayer].resources[2]-=5;
				
				if (target.terrain >=0){
					target.terrain = 2; //change to whatever mountain is
					target.pointsRequired = 3;
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
			if (target.occupyer != null && target.findTower(3)){
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
				target.occupyer.buffs.Add (temp);
				
				spell = -1;
			}
		}
		else if (spell == 11){
			//glacial wall
			if(target.findTower (3)){
				
				other.players[other.activePlayer].resO.Add (new ResOccupied(3,4,2));
				other.players[other.activePlayer].resources[4]-=2;
			
				target.terrain = 2; //change to whatever mountain is
				target.pointsRequired = 3;
				target.transformTime = 3*2;
				//change model
				
				spell = -1;
			}
		}
		else if (spell == 12){
			//wineter's call
			if(target.occupyer!= null && target.findTower (3)){
				other.players[other.activePlayer].resO.Add (new ResOccupied(4,4,2));
				other.players[other.activePlayer].resources[4]-=2;
				//sleepxors?
				spell = -1;
			}
		}
		else if (spell == 13){
			//frost
			if(target.findTower (4)){
				other.players[other.activePlayer].resO.Add (new ResOccupied(4,4,3));
				other.players[other.activePlayer].resources[4]-=3;
				if(target.frost <=0){
					target.pointsRequired += 2;
				}
				target.frost = 4;
				spell = -1;
			}
		}
		else if (spell == 14){
			//natures bounty
			if(target.occupyer!=null&&target.findTower (2)){
				other.players[other.activePlayer].resO.Add (new ResOccupied(1,3,1));
				other.players[other.activePlayer].resources[3]-=1;
				
				target.occupyer.hp +=3;
				spell = -1;				
			}
		}
		else if (spell == 15){
			//stone armor
			if(target.occupyer!=null&&target.findTower (2)){
				other.players[other.activePlayer].resO.Add (new ResOccupied(3,3,2));
				other.players[other.activePlayer].resources[3]-=2;
				
				Buff temp = new Buff();
				temp.defense = 3;
				temp.turns = 3;
				
				target.occupyer.def += 3; 
				target.occupyer.buffs.Add (temp);
				spell = -1;
			}
		}
		else if (spell == 16){
			
			//one with nature
			//ignore for now possibly forever
			if(target.occupyer!=null&&target.findTower (4)){
				other.players[other.activePlayer].resO.Add (new ResOccupied(2,3,3));
				other.players[other.activePlayer].resources[3]-=3;
			}
		}
		else if (spell == 17){
			//heaven to earth
			//ignore for now
			if(target.occupyer!=null&&target.findTower (2)){
				other.players[other.activePlayer].resO.Add (new ResOccupied(3,3,3));
				other.players[other.activePlayer].resources[3]-=3;
			}
		}
		else if (spell == 18){
			//counterspell
			//ignore for now
			if(target.occupyer!=null&&target.findTower (10)){
				other.players[other.activePlayer].resO.Add (new ResOccupied(1,0,3));
				other.players[other.activePlayer].resources[0]-=3;
			}
		}
		else if (spell == 19){
			//dispel magic
			//ignore for now
			if(target.occupyer!=null&&target.findTower (5)){
				other.players[other.activePlayer].resO.Add (new ResOccupied(1,0,2));
				other.players[other.activePlayer].resources[0]-=2;
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
}
