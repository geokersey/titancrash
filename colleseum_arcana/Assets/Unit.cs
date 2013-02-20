using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	public Grid world;
	public int x = 1;
	public int y = 1;
	public int startingMovePoints = 3;
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
		int temp = world.map[x,y].movePoints(availableMovePoints + world.map[x,y].pointsRequired, target);
		if (temp >= 0){
			x = target.x;
			y = target.y;
			transform.position =  new Vector3((float)(x+(.5*y)),0f,(float)y);
			availableMovePoints = temp;
			world.map[x,y].occupyer = this;
			return true;
		}
		return false;
	}
}