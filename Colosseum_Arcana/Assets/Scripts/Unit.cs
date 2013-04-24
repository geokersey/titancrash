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
	public int startHP;
	public int hp;
	public int atk;
	public int startAtk;
	public int def;
	public int startDef;
	public int element;
	public int x;
	public int y;
	public int owner;
	public int startingMovePoints = 3;
	public int sight = 5;
	public int availableMovePoints = 0;
	public bool selected = false;
	public bool acting = false;
	public bool flyer = false;
	public int sleep = 0;
	public int sleepTime = 0;
	public Path steps;
	public float dT;
	void Start () {
		buffs = new System.Collections.Generic.List<Buff>();
		steps = new Path();
		int temp = Mathf.Max (startHP, hp);
		startHP = hp = temp;
		
	}
	public void init(int x_, int y_, Grid world_){
		x = x_;
		y = y_;
		world = world_;
		
		if (world.players[owner].techAvailable[24] == 2)
		{
			availableMovePoints = startingMovePoints;
		}
		else if (world.players[owner].techAvailable[20] == 2)
		{
			int z = startingMovePoints / 2;
			if (z < 1)
			{
				availableMovePoints = 1;
			}
			else
			{
				availableMovePoints = z;
			}
		}
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
					//see (steps.path[steps.path.Count-1].x,steps.path[steps.path.Count-1].y);
					
					
				}
				else if (steps.path.Count > 1){
					dT+= Time.deltaTime;
					transform.position = Vector3.Lerp (steps.path[steps.path.Count-1].transform.position, steps.path[steps.path.Count-2].transform.position, dT);
					transform.LookAt (steps.path[steps.path.Count-2].transform.position);
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
		Debug.Log ("unit  begin turn");
		sleepTime --;
		if(hp>startHP){
			hp--;
		}
		if(sleep != hp ||sleepTime <= 0){
			sleep = 0;
			availableMovePoints = startingMovePoints;
			if (flyer && world.players[owner].techAvailable[18] == 2)
			{
				availableMovePoints++;
			}
		}
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
	public void dispell (){
		for (int i = 0; i < buffs.Count; ++i){
			hp-=buffs[i].health;
			atk-=buffs[i].attack;
			def-=buffs[i].defense;
			startingMovePoints-=buffs[i].movement;
			sight-=buffs[i].sight;
			
			}
		buffs = new System.Collections.Generic.List<Buff>();
		
		
	}
	public Tile goTo(Tile target){
		Debug.Log ("in the goto func");
		if (owner != world.activePlayer){
			return world.map[x,y];
		}
		if(flyer){
			steps = world.map[x,y].movePoints(availableMovePoints + 1, target, this, false, flyer);
		}
		else{
			steps = world.map[x,y].movePoints(availableMovePoints + world.map[x,y].pointsRequired, target, this, false, flyer);
		}
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
		Debug.Log ("unit "+x+","+y+" seeing "+ sight+" tiles");
		world.map[x_,y_].see (sight);
	}
	void OnGUI(){
		if (selected){
			GUI.Box(new Rect(0, 100, 125, 25), "Health: " + hp + "/10", world.GUIfunstuff.box);
			GUI.Box(new Rect(0, 125, 125, 25), "Attack: " + atk, world.GUIfunstuff.box);
			GUI.Box(new Rect(0, 150, 125, 25), "Defense: " + def, world.GUIfunstuff.box);
			GUI.Box(new Rect(0, 175, 125, 25), "Action Points: " + availableMovePoints + "/" + startingMovePoints, world.GUIfunstuff.box);
			
		}
	}
	public void choose(){
		if (owner == world.activePlayer){
			selected = true;
			if(!flyer){
				world.map[x,y].inRange(availableMovePoints+world.map[x,y].pointsRequired, flyer, flyer, false);
			}
			else{
				world.map[x,y].inRange(availableMovePoints+1, flyer, flyer, false);
			}
			
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