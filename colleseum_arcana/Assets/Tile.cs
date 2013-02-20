using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	public Grid world;
	public int x;
	public int y;
	public int terrain = 0;
	public int owner;
	//public Unit Selected;
	public Unit lastSelected;
	public int minPoints;
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
	
	}
	public int movePoints (int available, Tile target){
		if (terrain == -1){
			return -1;
		}
		if (target != this && available <= pointsRequired){
			return -1;
		}
		if (target == this && available < pointsRequired){
			return 0;
		}
		if (target == this){
			return available - pointsRequired;
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
		
		return Mathf.Max (
				world.map[x-1,y].movePoints (available - pointsRequired,target),
				world.map[x+1,y].movePoints (available - pointsRequired,target),
				world.map[x-1,y+1].movePoints (available - pointsRequired,target),
				world.map[x+1,y-1].movePoints (available - pointsRequired,target),
				world.map[x,y-1].movePoints (available - pointsRequired,target),
				world.map[x,y+1].movePoints (available - pointsRequired,target));
		
		Debug.LogWarning ("problem in pathfinding algorithm");
		return -2;
		
		
		
		
		
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
			}
		}
	}
	public void deselect(){
		//destroy GUI
	}
}
