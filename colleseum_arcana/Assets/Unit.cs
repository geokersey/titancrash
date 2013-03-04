using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	public Grid world;
	public int x = 1;
	public int y = 1;
	public int owner;
	public int startingMovePoints = 3;
	public int sight = 5;
	int availableMovePoints = 3;
	//float wait = 0;
	// Use this for initialization
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
	public bool goTo(Tile target){
		if (owner != world.activePlayer){
			return false;
		}
		Path temp = world.map[x,y].movePoints(availableMovePoints + world.map[x,y].pointsRequired, target, this);
		if (temp.pointsRemaining >= 0){
			world.map[x,y].occupyer = null;
			x = target.x;
			y = target.y;
			transform.position =  new Vector3((float)(x+(.5*y)),0f,(float)y);
			availableMovePoints = temp.pointsRemaining;
			world.map[x,y].occupyer = this;
			return true;
		}
		return false;
	}
	public int move(Path steps){
		for (int i = 0; i<steps.path.Count; ++i){
			
		}
		return steps.pointsRemaining;
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