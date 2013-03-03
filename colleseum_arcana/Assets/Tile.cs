using UnityEngine;
using System.Collections;

public struct Pair{
	public int first;
	public System.Collections.Generic.List<Tile> second;
	public Pair(int x, System.Collections.Generic.List<Tile> y){
		first = x;
		second = y;
	}
	
}
public class Tile : MonoBehaviour {
	public Grid world;
	public int x;
	public int y;
	public int terrain = 0;
	public int owner;
	public bool visible;
	//public Unit Selected;
	public int pointsRequired;
	public Unit occupyer;
	public Building building;
	
	// Use this for initialization
	void Start () {
	
	}
	public void init(int x_, int y_, int terr){
		
		x = x_;
		y = y_;
		terrain = terr;
		pointsRequired = terr + 1;
	}
	
	// Update is called once per frame
	void Update () {
		//if (world.activePlayer==owner){
		//	gameObject.layer = 8;
		//}
		//else {
		//	gameObject.layer = 10;
		//}
		//badbadbad
	}
	public Pair movePoints (int available, Tile target, Unit mover){
		System.Collections.Generic.List<Tile> temp = new System.Collections.Generic.List<Tile>();
		//Tuple returnVal = new Tuple <int, ArrayList>();
		if (terrain == -1){
			//return -1;
			return new Pair(-1,temp);
		}
		if (target != this && available <= pointsRequired){
			//return -1;
			return new Pair(-1,temp);
		}
		if (this.occupyer != null && this.occupyer != mover){
			return new Pair(-1,temp);
		}
		
		if (target == this && available < pointsRequired){
			//return 0;
			temp.Add (this);
			return new Pair(0,temp);
		}
		if (target == this){
			//return available - pointsRequired;
			temp.Add(this);
			return new Pair(available -pointsRequired,temp);
		}
		/*if (y%2 == 1){
			return Mathf.Max (world.map[x-1,y].movePoints (available - pointsRequired,target),
				world.map[x+1,y].movePoints (available - pointsRequired,target),
				world.map[x,y+1].movePoints (available - pointsRequired,target),
				world.map[x+1,y+1].movePoints (available - pointsRequired,target),
				world.map[x,y-1].movePoints (available - pointsRequired,target),
				world.map[x+1,y-1].movePoints (available - pointsRequired,target));
		}
		else if (y%2 == 0){
			return Mathf.Max (world.map[x-1,y].movePoints (available - pointsRequired,target),
				world.map[x+1,y].movePoints (available - pointsRequired,target),
				world.map[x,y+1].movePoints (available - pointsRequired,target),
				world.map[x-1,y+1].movePoints (available - pointsRequired,target),
				world.map[x,y-1].movePoints (available - pointsRequired,target),
				world.map[x-1,y-1].movePoints (available - pointsRequired,target));
		}*/
		
		Pair max = world.map[x-1,y].movePoints (available - pointsRequired,target, mover);
		Pair current = world.map[x+1,y].movePoints (available - pointsRequired,target, mover);
		if(current.first>max.first){
			max = current;
		}
		current = world.map[x-1,y+1].movePoints (available - pointsRequired,target, mover);
		if(current.first>max.first){
			max = current;
		}
		current = world.map[x+1,y-1].movePoints (available - pointsRequired,target, mover);
		if(current.first>max.first){
			max = current;
		}
		current = world.map[x,y-1].movePoints (available - pointsRequired,target, mover);
		if(current.first>max.first){
			max = current;
		}
		current = world.map[x,y+1].movePoints (available - pointsRequired,target, mover);
		if(current.first>max.first){
			max = current;
		}
		max.second.Add (this);
		return max;
		
		/*return Mathf.Max (
				world.map[x-1,y].movePoints (available - pointsRequired,target),
				world.map[x+1,y].movePoints (available - pointsRequired,target),
				world.map[x-1,y+1].movePoints (available - pointsRequired,target),
				world.map[x+1,y-1].movePoints (available - pointsRequired,target),
				world.map[x,y-1].movePoints (available - pointsRequired,target),
				world.map[x,y+1].movePoints (available - pointsRequired,target));
				*/
		
		//Debug.LogWarning ("problem in pathfinding algorithm");
		//return -2;
		
		
		
		
		
	}
	public void OnMouseOver (){
		//int temp = movePoints(selected.availableMovePoints + pointsRequired,
		if (Input.GetMouseButton (0)){
			if(world.selected!=null){
				world.selected.deselect();}
			world.selected = this;
			world.highlight.transform.position = transform.position;
			//instantiat GUI
			
		}
		if (Input.GetMouseButtonUp (1)&&this!=world.selected&&world.selected.occupyer!=null){
			if (world.selected.occupyer.goTo(this)){
				world.selected.deselect();
				world.selected = this;
				world.highlight.transform.position = transform.position;
				owner = world.activePlayer;
			}
		}
	}
	/*public void see(){
		gameObject.layer = 10;
		if (occupyer != null){
			occupyer.see();
			Debug.Log ("seeing things");
		}
	}*/
	public void deselect(){
		//destroy GUI
	}
}
