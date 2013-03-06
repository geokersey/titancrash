using UnityEngine;
using System.Collections;

public struct Path{
	public int pointsRemaining;
	public System.Collections.Generic.List<Tile> path;
	public Unit defender;
	public Path(int x, System.Collections.Generic.List<Tile> p, Unit d){
		pointsRemaining = x;
		path = p;
		defender = d;
	}
	
}
public class Tile : MonoBehaviour {
	public Grid world;
	public int x;
	public int y;
	public int terrain = 0;
	public int owner = -1;
	public bool visible;
	public bool hasFont;
	public bool hasRes;
	//public Unit Selected;
	public int pointsRequired;
	public Unit occupyer;
	
	// Use this for initialization
	void Start () {
	
	}
	public void init(int x_, int y_, int terr, bool hasFoont_, bool hasRes_){
		
		x = x_;
		y = y_;
		terrain = terr;
		pointsRequired = terr + 1;
		hasFont = hasFoont_;
		hasRes = hasRes_;
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
	public Path movePoints (int available, Tile target, Unit mover){
		System.Collections.Generic.List<Tile> temp = new System.Collections.Generic.List<Tile>();
		//Tuple returnVal = new Tuple <int, ArrayList>();
		if (terrain == -1){
			//return -1;
			//Debug.Log ("terrain = -1");
			return new Path(-1,temp, null);
		}
		if(target != this){
			if (available <= pointsRequired){
				//return -1;
				//Debug.Log ("out of move points");
				return new Path(-1,temp, null);
			}
			if (this.occupyer != null && this.owner != world.activePlayer && this.occupyer != mover){
				//Debug.Log("enemy unit in the way");
				return new Path(-1,temp, null);
			}
			
		}
		if (target == this){
			if (this.occupyer != null && this.owner != world.activePlayer){
				temp.Add (this);
				//Debug.Log ("attacking");
				return new Path(Mathf.Max (0, available-pointsRequired), temp, this.occupyer);
			}
			if (this.occupyer != null){
				//Debug.Log ("can't move onto own player");
				return new Path (-1, temp, null);
			}
			//Debug.Log ("moving to empty space");
			temp.Add (this);
			return new Path(Mathf.Max (0, available-pointsRequired), temp, null);
		}
		
		
			/*if (target == this && available < pointsRequired){
				//return 0;
				temp.Add (this);
				return new Path(0,temp, false);
			}
			if (target == this){
				//return available - pointsRequired;
				temp.Add(this);
				return new Path(available -pointsRequired,temp, false);
				
			}*/
		//}
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
		
		Path max = world.map[x-1,y].movePoints (available - pointsRequired,target, mover);
		Path current = world.map[x+1,y].movePoints (available - pointsRequired,target, mover);
		if(current.pointsRemaining>max.pointsRemaining){
			max = current;
		}
		current = world.map[x-1,y+1].movePoints (available - pointsRequired,target, mover);
		if(current.pointsRemaining>max.pointsRemaining){
			max = current;
		}
		current = world.map[x+1,y-1].movePoints (available - pointsRequired,target, mover);
		if(current.pointsRemaining>max.pointsRemaining){
			max = current;
		}
		current = world.map[x,y-1].movePoints (available - pointsRequired,target, mover);
		if(current.pointsRemaining>max.pointsRemaining){
			max = current;
		}
		current = world.map[x,y+1].movePoints (available - pointsRequired,target, mover);
		if(current.pointsRemaining>max.pointsRemaining){
			max = current;
		}
		if (max.defender != null && max.path.Count == 1 && this.occupyer != null && this.occupyer != mover){
			//Debug.Log ("enemy on target, this space occupied");
			return new Path(-1,temp, null);
		}
		max.path.Add (this);
		//Debug.Log ("intermediate space");
		return max;
		
		/*return Mathf.Max (
				world.map[x-1,y].movePoints (available - pointsRequired,target),
				world.map[x+1,y].movePoints (available - pointsRequired,target),
				world.map[x-1,y+1].movePoints (available - pointsRequired,target),
				world.map[x+1,y-1].movePoints (available - pointsRequired,target),
				world.map[x,y-1].movePoints (available - pointsRequired,target),
				world.map[x,y+1].movePoints (available - pointsRequired,target));
				*/
		
		////Debug.LogWarning ("problem in pathfinding algorithm");
		//return -2;
		
		
		
		
		
	}
	public void OnMouseOver (){
		//int temp = movePoints(selected.availableMovePoints + pointsRequired,
		if (Input.GetMouseButton (0)){
//			Debug.Log (x.ToString ()+" , "+y.ToString ());
			if(world.selected!=null){
				world.selected.deselect();}
			world.selected = this;
			world.selected.choose ();
			if (occupyer == null && hasFont && owner == world.activePlayer){
				world.summoningFont.show ();
				// = Instantiate (sFontPrefab);
				//sFont.init(world);
			}
			else{
				world.summoningFont.hide ();
			}
			world.highlight.transform.position = transform.position;
			//instantiat GUI
		}
		
		if (Input.GetMouseButtonUp (1)&&this!=world.selected&&world.selected.occupyer!=null){
			Tile temp = world.selected.occupyer.goTo(this);
			world.selected.deselect();
			world.selected = temp;
			world.selected.choose();
			world.highlight.transform.position = temp.transform.position;
			//temp.owner = world.activePlayer;
			/*if (world.selected.occupyer.goTo(this)){
				world.selected.deselect();
				world.selected = this;
				world.highlight.transform.position = transform.position;
				owner = world.activePlayer;
			}*/
		}
	}
	public void deselect(){
		if (this.occupyer != null){
			this.occupyer.selected = false;
		}
		//destroy GUI
	}
	public void choose(){
		if (this.occupyer != null){
			this.occupyer.selected = true;
		}
		//show GUI
	}
}