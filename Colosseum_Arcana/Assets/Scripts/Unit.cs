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
	public string name;
	public System.Collections.Generic.List<Buff> buffs;
	public Grid world;
	int startHP;
	public int hp;
	public int atk;
	int startAtk;
	public int def;
	int startDef;
	public int element;
	public int x;
	public int y;
	public int owner;
	public int startingMovePoints = 3;
	int origMovePoints;
	public int sight = 5;
	public int availableMovePoints = 0;
	public bool selected = false;
	public bool acting = false;
	public bool flyer = false;
	public int sleep = 0;
	public int sleepTime = 0;
	public Path steps;
	public float dT;
	public AudioClip selectsound;
	public AudioClip deathsound;
	public AudioClip attacksound;
	
	
	private Renderer[] meshRenderers;
	private bool anim;
	
	void Start () {
		buffs = new System.Collections.Generic.List<Buff>();
		steps = new Path();
		int temp = Mathf.Max (startHP, hp);
		startHP = hp = temp;
		meshRenderers = GetComponentsInChildren<Renderer>();
		anim = null!= GetComponent<Animation>();
		origMovePoints = startingMovePoints;
		startAtk = atk;
		startDef = def;
		startHP = hp;
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
					if(anim){
						animation.CrossFade ("attack", .1f);
					}
				}
				if (dT > 1){
					dT = 0;
					steps.path.RemoveAt (steps.path.Count -1);
					//see (steps.path[steps.path.Count-1].x,steps.path[steps.path.Count-1].y);
					
					
				}
				else if (steps.path.Count > 1){
					dT+= Time.deltaTime*3f;
					transform.position = Vector3.Lerp (steps.path[steps.path.Count-1].transform.position, steps.path[steps.path.Count-2].transform.position, dT);
					transform.LookAt (steps.path[steps.path.Count-2].transform.position);
				}
			}
			else{
				steps = new Path();
				acting = false;
				world.suspended = false;
				world.map[x,y].choose ();
				if (anim){
					animation.CrossFade ("attack", .2f);
				}
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
		attacker.audio.clip = attacker.attacksound;
		attacker.audio.Play();
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
			defender.audio.clip = defender.deathsound;
			defender.audio.Play();
			if(attacker.hp <=0){
				attacker.audio.clip = attacker.deathsound;
				attacker.audio.Play();
				return false;
				
			}
			else{
				
				return true;
			}
		}
		if (attacker.hp <=0){
			attacker.audio.clip = attacker.deathsound;
			attacker.audio.Play();
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
			GUI.Box(new Rect(0, 100, 125, 25), "Health: " + hp + "/"+startHP, world.GUIfunstuff.box);
			GUI.Box(new Rect(0, 125, 125, 25), "Attack: " + atk, world.GUIfunstuff.box);
			GUI.Box(new Rect(0, 150, 125, 25), "Defense: " + def, world.GUIfunstuff.box);
			GUI.Box(new Rect(0, 175, 125, 25), "Action Points: " + availableMovePoints + "/" + startingMovePoints, world.GUIfunstuff.box);
			
		}
	}
	public void choose(){
		if (owner == world.activePlayer){
					
			if (!selected){
				audio.clip = selectsound;
				audio.Play();
				if (anim){
					animation.Play ("jump");
					//animation.PlayQueued ("fly");
					animation.CrossFadeQueued("fly", .1f);
				}
				selected = true;
			}
			if(!flyer){
				world.map[x,y].inRange(availableMovePoints+world.map[x,y].pointsRequired, flyer, flyer, false);
			}
			else{
				world.map[x,y].inRange(availableMovePoints+1, flyer, flyer, false);
			}
			
		}
	}
	public void deselect(){
		if(selected&&anim){
			animation.CrossFade ("attack", .1f);
		}
		selected = false;
		
	}
	public void hide(){
		foreach (Renderer r in meshRenderers){
			
			r.enabled = false;
		}
	}
	public void show(){
		foreach (Renderer r in meshRenderers){
			
			r.enabled = true;
		}
	}
	public string summary(){
		return name+"\nhealth: "+hp+"/"+startHP+"\nattack: "+atk+"("+startAtk+")\ndefence: "+def+"("+startDef+")\nmovement: "+availableMovePoints+"/"+startingMovePoints+"("+origMovePoints+")\n";
		//return "to be filled out with unit specific details and formatted for GUI";
	}
}