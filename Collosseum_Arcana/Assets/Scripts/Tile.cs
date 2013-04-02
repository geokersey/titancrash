using UnityEngine;
using System.Collections;

public class Path{
	public int pointsRemaining;
	public System.Collections.Generic.List<Tile> path;
	public Unit defender;
	public Path(int x, System.Collections.Generic.List<Tile> p, Unit d){
		pointsRemaining = x;
		path = p;
		defender = d;
	}
	public Path(){
		pointsRemaining = -1;
		path = new System.Collections.Generic.List<Tile>();
		defender = null;
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
	public int resource = -1;
	public int resourceQuantity = 2;
	public bool hasTower;
	public GameObject buildingModel;
	//public Unit Selected;
	public int pointsRequired;
	public Unit occupyer;
	public int towerRange = 5;
	public int maxPoints;
	
	// Use this for initialization
	void Start () {
	
	}
	public void init(int x_, int y_, int terr, bool hasFont_, bool hasTower_, int resource_, Grid world_){
		
		world = world_;
		x = x_;
		y = y_;
		terrain = terr;
		pointsRequired = terr + 1;
		hasFont = hasFont_;
		hasTower = hasTower_;
		resource = resource_;
		if (hasFont){
			buildingModel = (GameObject)Instantiate (world.fontPrefab, transform.position, Quaternion.identity);
		}
		else if (hasTower){
			buildingModel = (GameObject)Instantiate (world.towerPrefab, transform.position, Quaternion.identity);
		}
		else if (resource == 0){
			buildingModel = (GameObject)Instantiate (world.arcanaResPrefab, transform.position, Quaternion.identity);
		}
		else if (resource == 1){
			buildingModel = (GameObject)Instantiate (world.airResPrefab, transform.position, Quaternion.identity);
		}
		else if (resource == 2){
			buildingModel = (GameObject)Instantiate (world.earthResPrefab, transform.position, Quaternion.identity);
		}
		else if (resource == 3){
			buildingModel = (GameObject)Instantiate (world.fireResPrefab, transform.position, Quaternion.identity);
		}
		
		else if (resource == 4){
			buildingModel = (GameObject)Instantiate (world.waterResPrefab, transform.position, Quaternion.identity);
		}
		
			
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
				return new Path(Mathf.Max(0, available-pointsRequired), temp, this.occupyer);
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
		if(!world.suspended){
			if (Input.GetMouseButton (0)){
				if(world.selected!=null){
					world.selected.deselect();}
				world.selected = this;
				world.selected.choose ();
				if (occupyer == null && hasFont && owner == world.activePlayer){
					world.summoningFont.show ();
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
				
			}
		}
	}
	public void inRange(int points, bool ignoreTerrain){
		if ((points > maxPoints && gameObject.tag == "range")||(points>0&&gameObject.tag!="range")){
			if (terrain < 0){
				return;
			}
			//Debug
			//Debug.Log(x+ ", "+y+":  "+points);
			//gameObject.tag = "range";
			maxPoints = points;
			if (world.activePlayer == 0){
				if (gameObject.tag == "vis1" || gameObject.tag == "visBoth"){
						gameObject.tag = "visBoth";
					}
					else {
						gameObject.tag = "vis0";
					}
				}
				else if (world.activePlayer == 1){
					if (gameObject.tag == "vis0" || gameObject.tag == "visBoth"){
						gameObject.tag = "visBoth";
					}
					else {
						gameObject.tag = "vis1";
					}
				}
			gameObject.layer = 12;
			if(ignoreTerrain){
				points -=1;
			}
			else{
				points -= pointsRequired;
			}
			world.map[x+1,y].inRange (points, ignoreTerrain);
			world.map[x-1,y].inRange (points, ignoreTerrain);
			world.map[x+1,y-1].inRange (points, ignoreTerrain);
			world.map[x-1,y+1].inRange (points, ignoreTerrain);
			world.map[x,y+1].inRange (points, ignoreTerrain);
			world.map[x,y-1].inRange (points, ignoreTerrain);
		}
	}
	public void outRange(){
		if (terrain < 0){
				return;
		}
		if (gameObject.layer == 12){
			gameObject.layer = 10 + world.activePlayer;
			world.map[x+1,y].outRange ();
			world.map[x-1,y].outRange ();
			world.map[x+1,y-1].outRange ();
			world.map[x-1,y+1].outRange ();
			world.map[x,y+1].outRange ();
			world.map[x,y-1].outRange ();
		}
	}
			
	public void deselect(){
		outRange();
		if (this.occupyer != null){
			
			this.occupyer.deselect();
		}
		//destroy GUI
	}
	public void choose(){
		if (this.occupyer != null){
			this.occupyer.choose();
		}
		else if (hasTower && owner == world.activePlayer){
			inRange (towerRange, true);
		}
		//show GUI
	}
	public void capture(int player){
		if (player != owner){
			if(hasFont){
				Destroy (buildingModel);
				if (player == 0){
					buildingModel = (GameObject)Instantiate (world.fontPrefab0, transform.position, Quaternion.identity);
				}
				else if (player == 1){
					buildingModel = (GameObject)Instantiate (world.fontPrefab1, transform.position, Quaternion.identity);
				}
			}
			else if(hasTower){
				Destroy (buildingModel);
				if (player == 0){
					buildingModel = (GameObject)Instantiate (world.towerPrefab0, transform.position, Quaternion.identity);
				}
				else if (player == 1){
					buildingModel = (GameObject)Instantiate (world.towerPrefab1, transform.position, Quaternion.identity);
				}
			}
			if(resource >=0){
				if (owner >=0){
					world.players[owner].resources[resource]-=resourceQuantity;
				}
			
				world.players[player].resources[resource]+=resourceQuantity;
				//Debug.Log ("just added "+ resourceQuantity + " of resource " + resource + " to player " +player);
				Destroy (buildingModel);
				if (player == 0){
					if (resource == 0){
						buildingModel = (GameObject)Instantiate (world.arcanaResPrefab0, transform.position, Quaternion.identity);
					}
					else if (resource == 1){
						buildingModel = (GameObject)Instantiate (world.airResPrefab0, transform.position, Quaternion.identity);
					}
					else if (resource == 2){
						buildingModel = (GameObject)Instantiate (world.earthResPrefab0, transform.position, Quaternion.identity);
					}
					else if (resource == 3){
						buildingModel = (GameObject)Instantiate (world.fireResPrefab0, transform.position, Quaternion.identity);
					}
					else if (resource == 4){
						buildingModel = (GameObject)Instantiate (world.waterResPrefab0, transform.position, Quaternion.identity);
					}
				}
				else if (player == 1){
					if (resource == 0){
						buildingModel = (GameObject)Instantiate (world.arcanaResPrefab0, transform.position, Quaternion.identity);
					}
					else if (resource == 1){
						buildingModel = (GameObject)Instantiate (world.airResPrefab0, transform.position, Quaternion.identity);
					}
					else if (resource == 2){
						buildingModel = (GameObject)Instantiate (world.earthResPrefab0, transform.position, Quaternion.identity);
					}
					else if (resource == 3){
						buildingModel = (GameObject)Instantiate (world.fireResPrefab0, transform.position, Quaternion.identity);
					}
					else if (resource == 4){
						buildingModel = (GameObject)Instantiate (world.waterResPrefab0, transform.position, Quaternion.identity);
					}
				}
			}
				
			
			
			
		}
		owner = player;
	}
}