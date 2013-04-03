using UnityEngine;
using System.Collections;

public class Buff{
	public int turns;
	public int health;
	public int attack;
	public int defense;
	public int movement;
	public int sight;
	public int damage;
	public Buff(){
		health = attack = defense =  movement = sight = damage = 0;
	}
	
}
public class Unit : MonoBehaviour {
	public System.Collections.Generic.List<Buff> buffs;
	public Grid world;
	public int hp;
	public int atk;
	public int def;
	public int element;
	public int x;
	public int y;
	public int owner;
	public int startingMovePoints = 3;
	public int sight = 5;
	public int availableMovePoints = 0;
	public bool selected = false;
	public bool acting = false;
	public Path steps;
	public float dT;
	void Start () {
		buffs = new System.Collections.Generic.List<Buff>();
		steps = new Path();
		
	}
	public void init(int x_, int y_, Grid world_){
		x = x_;
		y = y_;
		world = world_;
	}
	
	// Update is called once per frame
	void Update () {
		if (acting){
			world.suspended = true;
			
			if (steps.path.Count >1){
				
				if(steps.path.Count == 2 && steps.defender != null){
					
					//start fight animations
					
					if (!fight (this, steps.defender)){
						steps = new Path();
						Debug.Log ("lost a fight, should stop moving");
						acting = false;
						world.suspended = false;
						world.map[x,y].choose ();
					}
					else{
						steps.defender = null;
						Debug.Log ("won a fight, should keep moving");
						steps.path[1].occupyer = null;
						x = steps.path[0].x;
						y = steps.path[0].y;
						world.map[x,y].occupyer = this;
						world.map[x,y].capture (owner);
						Debug.Log ("won a fight to capture "+ x +", " +y);
						
					}
				}
				if (dT > 1){
					dT = 0;
					steps.path.RemoveAt (steps.path.Count -1);
					
				}
				else if (steps.path.Count > 1){
					dT+= Time.deltaTime;
					transform.position = Vector3.Lerp (steps.path[steps.path.Count-1].transform.position, steps.path[steps.path.Count-2].transform.position, dT);
				
				}
			}
			else{steps = new Path();
			acting = false;
			world.suspended = false;
			world.map[x,y].choose ();
			}
		}
		else{
			steps = new Path();
			//acting = false;
			//world.suspended = false;
		}
		
		
		if (hp <= 0){
			if (world.map[x,y].occupyer == this){
				world.map[x,y].occupyer = null;
			}
			world.players[owner].resources[element]++;
			Destroy (gameObject);
		}
		
		
	
	}
	public void beginTurn(){
		availableMovePoints = startingMovePoints;
		int i = 1;
		int temp;
		while (buffs.Count >0 && i <= buffs.Count){
			temp = buffs.Count-i;
			
			buffs[temp].turns--;
			hp += buffs[temp].damage;
			if (buffs[temp].turns<=0){
				hp-=buffs[temp].health;
				atk-=buffs[temp].attack;
				def-=buffs[temp].defense;
				startingMovePoints-=buffs[temp].movement;
				sight-=buffs[temp].sight;
				
				buffs.RemoveAt(temp);
			}
			else{
				++i;
			}
		}

	}
	public Tile goTo(Tile target){
		Debug.Log ("in the goto func");
		if (owner != world.activePlayer){
			return world.map[x,y];
		}
		steps = world.map[x,y].movePoints(availableMovePoints + world.map[x,y].pointsRequired, target, this, false);
		if (steps.pointsRemaining >=0){
			world.map[x,y].outRange ();
			acting = true;
			//Debug.Log ("suspending the world");
			world.suspended = true;
			move ();
		}
		else {
			steps = new Path();
		}
		return world.map[x,y];
	}
	public void move(){
		
		int start = steps.path.Count -2;
		int end;
		
		if (steps.defender == null){
			end = 0;
		}
		else{
			end = 1;
		}
		steps.path[steps.path.Count-1].occupyer=null;
		for (int i= start; i>=end; --i){
			
			see (steps.path[i].x,steps.path[i].y);
			steps.path[i].capture(world.activePlayer);
			
		}
		x = steps.path[end].x;
		y = steps.path[end].y;
		steps.path[end].occupyer = this;
		availableMovePoints= steps.pointsRemaining;
		
	}
	bool fight(Unit attacker, Unit defender)
	{
		
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
		if (defender.hp <=0){
			if(attacker.hp <=0){
				
				return false;
				
			}
			else{
				
				return true;
			}
		}
		if (attacker.hp <=0){
			
			return false;
		}
		else{
			return false;	
		}
	}
	public void see(int x_, int y_){
		world.map[x_,y_].see (sight);
	}
	void OnGUI(){
		if (selected){
			GUI.Box(new Rect(0, 100, 100, 25), "Health: " + hp);
			GUI.Box(new Rect(0, 200, 150, 25), "Action Points: " + availableMovePoints);
			
		}
	}
	public void choose(){
		if (owner == world.activePlayer){
			selected = true;
			world.map[x,y].inRange(availableMovePoints+world.map[x,y].pointsRequired, false);
			
		}
	}
	public void deselect(){
		selected = false;
		
	}
	public void hide(){
		renderer.enabled = false;
	}
	public void show(){
		renderer.enabled = true;
	}
}