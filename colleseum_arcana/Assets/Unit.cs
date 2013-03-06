using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	public Grid world;
	public int hp;
	public int atk;
	public int def;
	public int x = 1;
	public int y = 1;
	public int owner;
	public int startingMovePoints = 3;
	public int sight = 5;
	int availableMovePoints = 3;
	void Start () {
		availableMovePoints = startingMovePoints;// + world.map[x,y].pointsRequired;
		world.map[x,y].occupyer = this;
	
	}
	
	// Update is called once per frame
	void Update () {
		availableMovePoints = startingMovePoints;// + world.map[x,y].pointsRequired;
		
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
		transform.position =  new Vector3((float)(x+(.5*y)),0f,(float)y);
			
	
		
	
	}
	public Tile goTo(Tile target){
		if (owner != world.activePlayer){
			return world.map[x,y];
		}
		Path temp = world.map[x,y].movePoints(availableMovePoints + world.map[x,y].pointsRequired, target, this);
		Debug.Log (temp.path.Count.ToString ()+" steps in path");
		if (temp.pointsRemaining >= 0){
			
			if (move (temp)){
				world.map[x,y].occupyer = null;
				x = temp.path[0].x;
				y = temp.path[0].y;
				transform.position =  new Vector3((float)(x+(.5*y)),0f,(float)y);
				availableMovePoints = temp.pointsRemaining;
				world.map[x,y].occupyer = this;
			}
			else{
				if (world.map[x,y]!= temp.path[1]){
					world.map[x,y].occupyer = null;
				}
					
				x = temp.path[1].x;
				y = temp.path[1].y;
				transform.position =  new Vector3((float)(x+(.5*y)),0f,(float)y);
				availableMovePoints = temp.pointsRemaining;
				world.map[x,y].occupyer = this;
			}
			
			
		}
		return world.map[x,y];
	}
	public bool move(Path steps){
		for (int i= 1; i<steps.path.Count; ++i){
			steps.path[i].visible = true;
			steps.path[i].owner = owner;
		}
		if (steps.defender == null){
			Debug.Log ("no enemy to fight");
			return true;
		}
		else if (fight(this, steps.defender)){
			return true;
		}
		else{
			return false;
		}
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
				Debug.Log ("battle was a tie, both units destroyed");
				return false;
				
			}
			else{
				//destroy defender
				Debug.Log ("victory for attacker");
				return true;
			}
		}
		if (attacker.hp <=0){
			//destroy attacker
			Debug.Log ("defender is victorious");
			return false;
		}
		else{
			Debug.Log ("stalemate");
			return false;	
		}
	}
	public void see(int _x, int _y){
		for (int i = -sight; i<= sight; ++i){
			if (i+_x>0 && i+_x<world.size){
				for (int j = -sight; j<= sight; ++j){
					if (j+_y>0 && j+_y<world.size){
						world.map[i+_x,j+_y].gameObject.layer = 9;
					}
				}
			}
		}
			
	}
}