using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
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
	bool acting = false;
	Path steps;
	float dT;
	void Start () {
		steps = new Path();
		//availableMovePoints = startingMovePoints;// + world.map[x,y].pointsRequired;
//		world.map[x,y].occupyer = this;
	
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
		}
		if (steps.path.Count >1){
			//Debug.Log ("we have a path in the update function");
			if(steps.path.Count == 2 && steps.defender != null){
				
				//start fight animations
				
				if (!fight (this, steps.defender)){
					steps = new Path();
					Debug.Log ("lost a fight, should stop moving");
					acting = false;
					world.suspended = false;
				}
				else{
					steps.defender = null;
					Debug.Log ("won a fight, should keep moving");
					steps.path[1].occupyer = null;
					//steps.path[0].capture(owner);
					x = steps.path[0].x;
					y = steps.path[0].y;
					world.map[x,y].occupyer = this;
					world.map[x,y].capture (owner);
					Debug.Log ("won a fight to capture "+ x +", " +y);
					
				}
			}
			if (dT > 1){
				//Debug.Log ("dT should be resetting");
				dT = 0;
				//x = steps.path[steps.path.Count -1].x;
				//y = steps.path[steps.path.Count -1].y;
				//steps.path[steps.path.Count-1].capture(owner);
				steps.path.RemoveAt (steps.path.Count -1);
				
			}
			else if (steps.path.Count > 1){
				//Debug.Log("things should be moving right now, dT = "+dT);
				dT+= Time.deltaTime;
				transform.position = Vector3.Lerp (steps.path[steps.path.Count-1].transform.position, steps.path[steps.path.Count-2].transform.position, dT);
				//transform.position = steps.path[steps.path.Count-2].transform.position;
			}
		}
		else{
			steps = new Path();
			acting = false;
			world.suspended = false;
		}
		//else {
		//	transform.position = world.map[x,y].transform.position;
			//Debug.Log ("jumping to x y position");
		//}
		
		if (hp <= 0){
			if (world.map[x,y].occupyer == this){
				world.map[x,y].occupyer = null;
			}
			world.players[owner].resources[element]++;
			Destroy (gameObject);
		}
		/*if (Input.GetKeyDown("a")){
			x -= 1;
		}
		if (Input.GetKeyDown("d")){
			x += 1;
		}
		if (Input.GetKeyDown("w")){
			y += 1;
			x -= y%2;
		}
		if (Input.GetKeyDown("x")){
			y -= 1;
			x += 1-y%2;
			
		}
		if (Input.GetKeyDown("e")){
			y += 1;
			x += 1-y%2;
			
		}
		if (Input.GetKeyDown("z")){
			y -= 1;
			x -= y%2;
		}*/
		//transform.position =  new Vector3((float)(x+(.5*y)),0f,(float)y);
			
	
		
	
	}
	public void beginTurn(){
		availableMovePoints = startingMovePoints;
		//Debug.Log ("starting turn");
	}
	public Tile goTo(Tile target){
		//Debug.Log ("in the goto func");
		if (owner != world.activePlayer){
			return world.map[x,y];
		}
		steps = world.map[x,y].movePoints(availableMovePoints + world.map[x,y].pointsRequired, target, this);
		if (steps.pointsRemaining >=0){
			acting = true;
			world.suspended = true;
			move ();
		}
		
		return world.map[x,y];
	}
	public void move(){
		//LogType.Warning("in the move function, a think that shouldn't really happen");
		int start = steps.path.Count -2;
		int end;
		//float t = 0;
		if (steps.defender == null){
			end = 0;
		}
		else{
			end = 1;
		}
		steps.path[steps.path.Count-1].occupyer=null;
		for (int i= start; i>=end; --i){
			//steps.path[i].visible = true;
			see (steps.path[i].x,steps.path[i].y);
			steps.path[i].capture(world.activePlayer);
			
		}
		x = steps.path[end].x;
		y = steps.path[end].y;
		steps.path[end].occupyer = this;
		availableMovePoints= steps.pointsRemaining;
		//return true;
		/*if (steps.defender == null){
			//Debug.Log ("no enemy to fight");
			return true;
		}
		else if (fight(this, steps.defender)){
			return true;
		}
		else{
			return false;
		}*/
	}
	bool fight(Unit attacker, Unit defender)
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
		if (defender.hp <=0){
			if(attacker.hp <=0){
				//destroy both
				//world.map[defender.x, defender.y].occupyer = null;
				//Destroy (defender.gameObject);
				//world.players[defender.owner].res--;
				//world.map[x,y].occupyer = null;
				//Destroy (attacker.gameObject);
				//Debug.Log ("battle was a tie, both units destroyed");
				return false;
				
			}
			else{
				//world.map[defender.x, defender.y].occupyer = null;
				//Destroy (defender.gameObject);
				//world.players[defender.owner].res--;
				//Debug.Log ("victory for attacker");
				return true;
			}
		}
		if (attacker.hp <=0){
			//destroy attacker
			//Debug.Log ("defender is victorious");
			return false;
		}
		else{
			//Debug.Log ("stalemate");
			return false;	
		}
	}
	
	
	public void see(int _x, int _y){
		if (world.activePlayer==0){
			for (int i = -sight; i<= sight; ++i){
				if (i+_x>0 && i+_x<world.size){
					for (int j = -sight; j<= sight; ++j){
						if (j+_y>0 && j+_y<world.size){
							if (Mathf.Abs(i+j)<=sight){
								world.map[i+_x,j+_y].gameObject.layer = 9;
								world.map[i+_x,j+_y].gameObject.tag = "visible0";
							}
							//Debug.Log ("seeing things");
						}
					}
				}
			}
		}
		if (world.activePlayer==1){
			for (int i = -sight; i<= sight; ++i){
				if (i+_x>0 && i+_x<world.size){
					for (int j = -sight; j<= sight; ++j){
						if (j+_y>0 && j+_y<world.size){
							if (Mathf.Abs(i+j)<=sight){
								world.map[i+_x,j+_y].gameObject.layer = 9;
								world.map[i+_x,j+_y].gameObject.tag = "visible1";
							}
							//Debug.Log ("seeing things");
						}
					}
				}
			}
		}	
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
			world.map[x,y].inRange(availableMovePoints+world.map[x,y].pointsRequired);
			//Debug.Log ("the chosen one");
		}
	}
	public void deselect(){
		selected = false;
		
	}
}