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
	public int scorch = 0;
	public int frost = -1;
	public int oldTerrain = 0;
	public int transformTime = -1;
	
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
	public bool wiz;
	public GameObject buildingModel;
	public GameObject mountainModel;
	//public Unit Selected;
	public int pointsRequired;
	public Unit occupyer;
	public int towerRange = 5;
	public int maxPoints;
	float toolTip = 0f;
	float tttime = .5f;
	
	string terName;
	string resName;
	// Use this for initialization
	void Start () {
	
	}
	public void init(int x_, int y_, int terr, bool hasFont_, bool hasTower_, bool wiz_, int resource_, int resAmount, Grid world_){
		/*if (wiz_){
			Debug.Log ("we have a wizard tower");
		}*/
		resourceQuantity = resAmount;
		world = world_;
		x = x_;
		y = y_;
		oldTerrain = terrain = terr;
		pointsRequired = terr + 1;
		hasFont = hasFont_;
		hasTower = hasTower_;
		resource = resource_;
		wiz = wiz_;
		if (wiz){
//			Debug.Log ("we really do");
			resName = "no";
			return;
			//buildingModel = (GameObject)Instantiate (world.fontPrefab, transform.position, Quaternion.identity);
		}	
		else if (hasFont){
			buildingModel = (GameObject)Instantiate (world.fontPrefab, new Vector3(transform.position.x, transform.position.y + .085f, transform.position.z)+world.offset, Quaternion.identity);
			resName = "no";
		}
		else if (hasTower){
			buildingModel = (GameObject)Instantiate (world.towerPrefab, new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z)+world.offset, Quaternion.identity);
			resName = "no";
		}
		else if (resource == 0){
			buildingModel = (GameObject)Instantiate (world.arcanaResPrefab, transform.position+world.offset, Quaternion.identity);
			resName = "arcana";
		}
		else if (resource == 1){
			buildingModel = (GameObject)Instantiate (world.airResPrefab, new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z)+world.offset, Quaternion.identity);
			resName = "air";
		}
		else if (resource == 2){
			buildingModel = (GameObject)Instantiate (world.earthResPrefab, new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z)+world.offset, Quaternion.identity);
			resName = "earth";
		}
		else if (resource == 3){
			buildingModel = (GameObject)Instantiate (world.fireResPrefab, new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z)+world.offset, Quaternion.identity);
			resName = "fire";
		}
		
		else if (resource == 4){
			buildingModel = (GameObject)Instantiate (world.waterResPrefab, new Vector3(transform.position.x, transform.position.y + .085f, transform.position.z)+world.offset, Quaternion.identity);
			resName = "water";
		}
		else{
			resName = "no";
		}

		
			
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public Path movePoints (int available, Tile target, Unit mover, bool zoc, bool fly){
		bool zoc2 = false;
		System.Collections.Generic.List<Tile> temp = new System.Collections.Generic.List<Tile>();
		if(fly){
			available -=1;
		}
		else {
			available -= pointsRequired;
		}
		if (terrain == -1){
			
			return new Path(-1,temp, null);
		}
		if(target != this){
			if (available <= 0){
				
				return new Path(-1,temp, null);
			}
			if (this.occupyer != null && this.owner != world.activePlayer && this.occupyer != mover){
				
				return new Path(-1,temp, null);
			}
			
			
		}
		zoc2 = checkZOC (mover.owner);
		if (zoc2){
			//Debug.Log (x+", "+y);
			
		}
		if (zoc2 && zoc&&!fly){
			//Debug.Log ("zoc violation, available points setting to 0");
			available = 0;
		}
		
		if (target == this){
			if (this.occupyer != null && this.owner != world.activePlayer){
				temp.Add (this);
				
				return new Path(Mathf.Max(0, available), temp, this.occupyer);
			}
			if (this.occupyer != null){
				return new Path (-1, temp, null);
			}
			temp.Add (this);
			return new Path(Mathf.Max (0, available), temp, null);
		}
		
		
		Path max = world.map[x-1,y].movePoints (available,target, mover, zoc2, fly);
		Path current = world.map[x+1,y].movePoints (available,target, mover, zoc2, fly);
		if(current.pointsRemaining>max.pointsRemaining){
			max = current;
		}
		else if(current.pointsRemaining == max.pointsRemaining && current.path.Count < max.path.Count){
			max = current;
		}
		current = world.map[x-1,y+1].movePoints (available,target, mover, zoc2, fly);
		if(current.pointsRemaining>max.pointsRemaining){
			max = current;
		}
		else if(current.pointsRemaining == max.pointsRemaining && current.path.Count < max.path.Count){
			max = current;
		}
		current = world.map[x+1,y-1].movePoints (available,target, mover, zoc2, fly);
		if(current.pointsRemaining>max.pointsRemaining){
			max = current;
		}
		else if(current.pointsRemaining == max.pointsRemaining && current.path.Count < max.path.Count){
			max = current;
		}
		current = world.map[x,y-1].movePoints (available,target, mover, zoc2, fly);
		if(current.pointsRemaining>max.pointsRemaining){
			max = current;
		}
		else if(current.pointsRemaining == max.pointsRemaining && current.path.Count < max.path.Count){
			max = current;
		}
		current = world.map[x,y+1].movePoints (available,target, mover, zoc2, fly);
		if(current.pointsRemaining>max.pointsRemaining){
			max = current;
		}
		else if(current.pointsRemaining == max.pointsRemaining && current.path.Count < max.path.Count){
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
		//Debug.Log ("mouse is over" + x + ","+y);
		/*Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
		if (gameObject.layer >= 10){
			//occupyer tooltip
			GUI.Box (new Rect(screenPos.x, screenPos.y, 15,15), "blar");
		}*/
		toolTip += Time.deltaTime;
		if(!world.suspended&&terrain>=0){
			if (Input.GetMouseButton (0)){
				world.spells.spell = -1;
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
				world.highlight.transform.position = new Vector3(transform.position.x,transform.position.y + .2f, transform.position.z);
				//instantiat GUI
			}
			
			if (Input.GetMouseButtonUp (1)){
				if (world.spells.spell >=0){
					//Debug.Log ("clicking with a spell");
					outRange ();
					//Debug.Log ("outRangeComplete");
					world.spells.cast(this);
						
				}
				else if(this!=world.selected&&world.selected.occupyer!=null){
					Tile temp = world.selected.occupyer.goTo(this);
					world.selected.deselect();
					world.selected = temp;
					world.selected.choose();
					world.highlight.transform.position = new Vector3(temp.transform.position.x,temp.transform.position.y + .2f, temp.transform.position.z);
				}
			}
		}
	}
	public void OnMouseEnter(){
		toolTip = 0f;
		if(world.spells.spell >=0){
			inRange (world.spells.spellRanges[world.spells.spell], true, true, false);
		}
		
	}
	public void OnMouseExit(){
		toolTip = 0f;
		if(world.spells.spell >=0){
			outRange ();
		}
	}
	public void OnGUI(){
		if (toolTip > tttime){
			Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position+new Vector3(0f,0f, .578f));//the bottom point of the hex
			Vector3 screenPos2 = Camera.main.WorldToScreenPoint(transform.position+new Vector3(-.8f,0f, .289f));//the right side of the hex
			float width1 = 345; //terrain info box size
			float height1 = 70;
			float width2 = 125;//unit info box size
			float height2 = 100;
			
			if (terrain == 0){
				terName = "flatland";
			}
			else if (terrain == 1){
				terName = "hills";
			}
			else if (terrain == 2){
				terName = "mountain";
			}
			else {
				terName = "unkown terrain";
			}
			string message = terName+", "+resName+" resources";
			if (wiz){
				message+= "\nWizard tower: summoning font and focus tower combined\nhold both at the begining of a turn to win";
			}
			else if (hasFont){
				message += "\nsummoning font: use to create units";
			}
			else if (hasTower){
				message += "\nfocus tower: spells can only be cast within range of a focus tower";
			}
			if (gameObject.layer >= 10){
				if (owner == 0){
					GUI.contentColor = Color.yellow;
					if (occupyer != null){
						GUI.Box (new Rect(screenPos2.x, (Screen.height-screenPos2.y)-height2, width2,height2), occupyer.summary ());
					}
					GUI.Box (new Rect(screenPos.x - (width1/2), Screen.height-screenPos.y, width1,height1), message);
					//Debug.Log ("owner = 0");
				}
				else if (owner == 1){
					GUI.contentColor = Color.magenta;
					if (occupyer != null){
						GUI.Box (new Rect(screenPos2.x, (Screen.height-screenPos2.y)-height2, width2,height2), occupyer.summary ());
					}
					GUI.Box (new Rect(screenPos.x - (width1/2), Screen.height-screenPos.y, width1,height1), message);
					//Debug.Log ("owner = 1");
				}
				else{
					
					GUI.Box (new Rect(screenPos.x - (width1/2), Screen.height-screenPos.y, width1,height2), message);
				}
			}
				//GUI.Box (new Rect(screenPos.x - (width1/2), Screen.height-screenPos.y, width,50), "blar");

			// else if (seen previously but not currently visible){display with color but not unit);
			else{
			//GUI.contentColor = Color.blue;
				if (world.activePlayer ==0 && (gameObject.tag == "visBoth" || gameObject.tag == "vis0")){
					if (owner == 0){
						GUI.contentColor = Color.yellow;
						GUI.Box (new Rect(screenPos.x - (width1/2), Screen.height-screenPos.y, width1,height1), message);
						//Debug.Log ("owner = 0");
					}
					else if (owner == 1){
						GUI.contentColor = Color.magenta;
						GUI.Box (new Rect(screenPos.x - (width1/2), Screen.height-screenPos.y, width1,height1), message);
						//Debug.Log ("owner = 1");
					}
					
				}
				else if (world.activePlayer ==1 && (gameObject.tag == "visBoth" || gameObject.tag == "vis1")){
					if (owner == 0){
						GUI.contentColor = Color.yellow;
						GUI.Box (new Rect(screenPos.x - (width1/2), Screen.height-screenPos.y, width1,height1), message);
						//Debug.Log ("owner = 0");
						}
					else if (owner == 1){
						GUI.contentColor = Color.magenta;
						GUI.Box (new Rect(screenPos.x - (width1/2), Screen.height-screenPos.y, width1,height1), message);
						//Debug.Log ("owner = 1");
					}
					
				}
				else{
					GUI.Box (new Rect(screenPos.x - (width1/2), Screen.height-screenPos.y, width1,height1), message);
				}
			
			}
		}
	}
	public bool findTower(int range){
		for(int i = Mathf.Max (x-range,0); i<Mathf.Min (x+range, world.size); ++i){
			for(int j = Mathf.Max (y-range,0); j<Mathf.Min (7+range, world.size); ++j){
				if (world.map[i,j].hasTower&&world.map[i,j].owner == world.activePlayer &&Mathf.Abs (-i-j+x+y)<range){
					return true;
				}
			}	
		}
		return false;
	}
	public void inRange(int points, bool ignoreTerrain, bool fly, bool ZOC){
		//does not take into account enemy units or zone of control
		if ((points > maxPoints && gameObject.layer >=12)||(points>0&&gameObject.layer<12)){
			if (terrain < 0){
				return;
			}
			
			if (maxPoints != -1 && maxPoints > points){
				return;
			}
		
			maxPoints = points;
			//see (1);
			
			if (gameObject.layer == 11 || gameObject.layer == 10||gameObject.layer == 12){
				gameObject.layer = 12;
			}
			else{
				gameObject.layer = 13;
			}
			bool ZOC2 = checkZOC (world.activePlayer);
			if(ZOC && ZOC2 && !ignoreTerrain && !fly){
				//Debug.Log ("ZOC violation detected");
				return;
			}
			if(occupyer != null && occupyer.owner != world.activePlayer && !ignoreTerrain){
				return;
			}
			if(ignoreTerrain){
				points -=1;
			}
			else{
				points -= pointsRequired;
			}
			world.map[x+1,y].inRange (points, ignoreTerrain, fly, ZOC2);
			world.map[x-1,y].inRange (points, ignoreTerrain, fly, ZOC2);
			world.map[x+1,y-1].inRange (points, ignoreTerrain, fly, ZOC2);
			world.map[x-1,y+1].inRange (points, ignoreTerrain, fly, ZOC2);
			world.map[x,y+1].inRange (points, ignoreTerrain, fly, ZOC2);
			world.map[x,y-1].inRange (points, ignoreTerrain, fly, ZOC2);
		}
	}
	public void outRange(){
		//Debug.Log ("outrange function");
		if (terrain < 0){
				return;
		}
		maxPoints = -1;
		if (gameObject.layer == 12){
			gameObject.layer = 10 + world.activePlayer;
			world.map[x+1,y].outRange ();
			world.map[x-1,y].outRange ();
			world.map[x+1,y-1].outRange ();
			world.map[x-1,y+1].outRange ();
			world.map[x,y+1].outRange ();
			world.map[x,y-1].outRange ();
		}
		if (gameObject.layer == 13){
			gameObject.layer = 9;
			world.map[x+1,y].outRange ();
			world.map[x-1,y].outRange ();
			world.map[x+1,y-1].outRange ();
			world.map[x-1,y+1].outRange ();
			world.map[x,y+1].outRange ();
			world.map[x,y-1].outRange ();
		}
		
	}
	public bool checkZOC(int player){
		//checks to see if the players opponent has any units in adjacent tile
		return (world.map[x+1,y].occupyer!=null && world.map[x+1,y].owner!=player)
				||(world.map[x-1,y].occupyer!=null && world.map[x-1,y].owner!=player)
				||(world.map[x+1,y-1].occupyer!=null && world.map[x+1,y-1].owner!=player)
				||(world.map[x-1,y+1].occupyer!=null && world.map[x-1,y+1].owner!=player)
				||(world.map[x,y+1].occupyer!=null && world.map[x,y+1].owner!=player)
				||(world.map[x,y-1].occupyer!=null && world.map[x,y-1].owner!=player);
	}
	public void beginTurn(){
		//Debug.Log ("tile begining turn");
		
		scorch--;
		transformTime --;
		frost--;
		if (owner > 1){
			owner -= 2;
		}
		if (scorch >=0&&occupyer!= null&&owner != world.activePlayer){
			occupyer.hp -= 1;
		}
		if (transformTime == 0){
			terrain = oldTerrain;
			pointsRequired = terrain + 1;
			Destroy(mountainModel);
			renderer.enabled = true;
			
			//do model switch
		}
		if (frost == 0){
			pointsRequired -= 2;
		}
	}
	public void dispell(){
		//deal with visuals
		if(occupyer != null){
			occupyer.dispell();
		}
		scorch = 0;
		if (transformTime >0){
			terrain = oldTerrain;
			pointsRequired = terrain + 1;
			Destroy(mountainModel);
			renderer.enabled = true;
			transformTime = 0;
		}
		if (frost > 0){
			pointsRequired -= 2;
			frost =0;
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
		//else if (hasTower && owner == world.activePlayer){
		//	inRange (towerRange, true);
		//}
		//show GUI
	}
	public void capture(int player){
		if (player != owner){
			if(wiz){
				//do nothing
			}
			else if(hasFont){
				Destroy (buildingModel);
				if (player == 0){
					buildingModel = (GameObject)Instantiate (world.fontPrefab0, new Vector3(transform.position.x, transform.position.y + .085f, transform.position.z)+world.offset, Quaternion.identity);
				}
				else if (player == 1){
					buildingModel = (GameObject)Instantiate (world.fontPrefab1, new Vector3(transform.position.x, transform.position.y + .085f, transform.position.z)+world.offset, Quaternion.identity);
				}
			}
			else if(hasTower){
				Destroy (buildingModel);
				if (player == 0){
					buildingModel = (GameObject)Instantiate (world.towerPrefab0, new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z)+world.offset, Quaternion.identity);
				}
				else if (player == 1){
					buildingModel = (GameObject)Instantiate (world.towerPrefab1, new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z)+world.offset, Quaternion.identity);
				}
			}
			if(resource >=0){
				bool hasTech = false; //Check double resource techs
				
				if (owner >=0){
					world.players[owner].resources[resource]-=resourceQuantity;
					
					switch (resource)
					{
					case 0:
						if (world.players[owner].techAvailable[20] == 2) //Arcana
						{
							hasTech = true;
						}
						break;
						
					case 1:
						if (world.players[owner].techAvailable[8] == 2) //Air
						{
							hasTech = true;
						}
						break;
						
					case 2:
						if (world.players[owner].techAvailable[7] == 2) //Earth
						{
							hasTech = true;
						}
						break;
						
					case 3:
						if (world.players[owner].techAvailable[6] == 2) //Fire
						{
							hasTech = true;
						}
						break;
						
					case 4:
						if (world.players[owner].techAvailable[9] == 2) //Water
						{
							hasTech = true;
						}
						break;
						
					default:
						break;
					}
					
					if (hasTech)
					{
						world.players[owner].resources[resource] -= 2;
						hasTech = false;
					}
					
					world.players[owner].resSpots[resource] -= 1;
				}
			
				world.players[player].resources[resource]+=resourceQuantity;
				
				switch (resource)
				{
				case 0:
					if (world.players[player].techAvailable[20] == 2) //Arcana
					{
						hasTech = true;
					}
					break;
						
				case 1:
					if (world.players[player].techAvailable[8] == 2) //Air
					{
						hasTech = true;
					}
					break;
						
				case 2:
					if (world.players[player].techAvailable[7] == 2) //Earth
					{
						hasTech = true;
					}
					break;
						
				case 3:
					if (world.players[player].techAvailable[6] == 2) //Fire
					{
						hasTech = true;
					}
					break;
						
				case 4:
					if (world.players[player].techAvailable[9] == 2) //Water
					{
						hasTech = true;
					}
					break;
						
				default:
					break;
				}
				
				if (hasTech)
				{
					world.players[player].resources[resource] += 2;
				}
				world.players[player].resSpots[resource] += 1;
				//Debug.Log ("just added "+ resourceQuantity + " of resource " + resource + " to player " +player);
				Destroy (buildingModel);
				if (player == 0){
					if (resource == 0){
						buildingModel = (GameObject)Instantiate (world.arcanaResPrefab0, transform.position+world.offset, Quaternion.identity);
					}
					else if (resource == 1){
						buildingModel = (GameObject)Instantiate (world.airResPrefab0, new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z)+world.offset, Quaternion.identity);
					}
					else if (resource == 2){
						buildingModel = (GameObject)Instantiate (world.earthResPrefab0, new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z)+world.offset, Quaternion.identity);
					}
					else if (resource == 3){
						buildingModel = (GameObject)Instantiate (world.fireResPrefab0, new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z)+world.offset, Quaternion.identity);
					}
					else if (resource == 4){
						buildingModel = (GameObject)Instantiate (world.waterResPrefab0, new Vector3(transform.position.x, transform.position.y + .085f, transform.position.z)+world.offset, Quaternion.identity);
					}
				}
				else if (player == 1){
					if (resource == 0){
						buildingModel = (GameObject)Instantiate (world.arcanaResPrefab1, transform.position+world.offset, Quaternion.identity);
					}
					else if (resource == 1){
						buildingModel = (GameObject)Instantiate (world.airResPrefab1, new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z)+world.offset, Quaternion.identity);
					}
					else if (resource == 2){
						buildingModel = (GameObject)Instantiate (world.earthResPrefab1, new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z)+world.offset, Quaternion.identity);
					}
					else if (resource == 3){
						buildingModel = (GameObject)Instantiate (world.fireResPrefab1, new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z)+world.offset, Quaternion.identity);
					}
					else if (resource == 4){
						buildingModel = (GameObject)Instantiate (world.waterResPrefab0, new Vector3(transform.position.x, transform.position.y + .085f, transform.position.z)+world.offset, Quaternion.identity);
					}
				}
			}
				
			
			
			
		}
		owner = player;
	}
	public void see(int points){
		
		if (terrain<0){
			return;
		}
		if (points > 0){
			//Debug.Log (x+", "+y+"points = "+ points);
			show();
			gameObject.layer = 10+world.activePlayer;
			if(owner >=0){
				if (world.activePlayer == 0){
					if (gameObject.tag == "vis1" || gameObject.tag == "visBoth"){
						gameObject.tag = "visBoth";
						//Debug.Log("vis both active = 0");
					}
					else {
						gameObject.tag = "vis0";
						//Debug.Log("vis 0 active = 0");
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
				if (owner == 0){
					if(wiz){
						//do nothing
					}
					else if (hasFont){
						Destroy (buildingModel);
						buildingModel = (GameObject)Instantiate (world.fontPrefab0, new Vector3(transform.position.x, transform.position.y + .085f, transform.position.z)+world.offset, Quaternion.identity);
					}
					else if (hasTower){
						Destroy (buildingModel);
						buildingModel = (GameObject)Instantiate (world.towerPrefab0, new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z)+world.offset, Quaternion.identity);
					}
					else if (resource == 0){
						Destroy (buildingModel);
						buildingModel = (GameObject)Instantiate (world.arcanaResPrefab0, transform.position+world.offset, Quaternion.identity);
					}
					else if (resource == 1){
						Destroy (buildingModel);
						buildingModel = (GameObject)Instantiate (world.airResPrefab0, new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z)+world.offset, Quaternion.identity);
					}
					else if (resource == 2){
						Destroy (buildingModel);
						buildingModel = (GameObject)Instantiate (world.earthResPrefab0, new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z)+world.offset, Quaternion.identity);
					}
					else if (resource == 3){
						Destroy (buildingModel);
						buildingModel = (GameObject)Instantiate (world.fireResPrefab0, new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z)+world.offset, Quaternion.identity);
					}
					else if (resource == 4){
						Destroy (buildingModel);
						buildingModel = (GameObject)Instantiate (world.waterResPrefab0, new Vector3(transform.position.x, transform.position.y + .085f, transform.position.z)+world.offset, Quaternion.identity);
					}
				}
				if (owner == 1){
					if(wiz){
						//do nothing
					}
					else if (hasFont){
						Destroy (buildingModel);
						buildingModel = (GameObject)Instantiate (world.fontPrefab1, new Vector3(transform.position.x, transform.position.y + .085f, transform.position.z)+world.offset, Quaternion.identity);
					}
					else if (hasTower){
						Destroy (buildingModel);
						buildingModel = (GameObject)Instantiate (world.towerPrefab1, new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z)+world.offset, Quaternion.identity);
					}
					else if (resource == 0){
						Destroy (buildingModel);
						buildingModel = (GameObject)Instantiate (world.arcanaResPrefab1, transform.position+world.offset, Quaternion.identity);
					}
					else if (resource == 1){
						Destroy (buildingModel);
						buildingModel = (GameObject)Instantiate (world.airResPrefab1, new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z)+world.offset, Quaternion.identity);
					}
					else if (resource == 2){
						Destroy (buildingModel);
						buildingModel = (GameObject)Instantiate (world.earthResPrefab1, new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z)+world.offset, Quaternion.identity);
					}
					else if (resource == 3){
						Destroy (buildingModel);
						buildingModel = (GameObject)Instantiate (world.fireResPrefab1, new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z)+world.offset, Quaternion.identity);
					}
					else if (resource == 4){
						Destroy (buildingModel);
						buildingModel = (GameObject)Instantiate (world.waterResPrefab1, new Vector3(transform.position.x, transform.position.y + .085f, transform.position.z)+world.offset, Quaternion.identity);
					}
				}
			}
			
			points--;
			world.map[x+1,y].see(points);
			world.map[x-1,y].see(points);
			world.map[x+1,y-1].see(points);
			world.map[x-1,y+1].see(points);
			world.map[x,y+1].see(points);
			world.map[x,y-1].see(points);
		}
		
	}
	public void freeze(int range, int turns){
		if(terrain < 0 || range < 0 || frost == turns){
			return;
		}
		if(frost <=0){
			pointsRequired += 2;
		}
		frost = turns*2;
		range--;
		world.map[x+1,y].freeze (range, turns);
		world.map[x-1,y].freeze (range, turns);
		world.map[x+1,y-1].freeze (range, turns);
		world.map[x-1,y+1].freeze (range, turns);
		world.map[x,y+1].freeze (range, turns);
		world.map[x,y-1].freeze (range, turns);
	}
	public void hide(){
		if (occupyer != null){
			occupyer.hide();
		}
		if (wiz){
			//do nothing
		}
		else if (hasFont){
			Destroy (buildingModel);
			buildingModel = (GameObject)Instantiate (world.fontPrefab, new Vector3(transform.position.x, transform.position.y + .085f, transform.position.z)+world.offset, Quaternion.identity);
		}
		else if (hasTower){
			Destroy (buildingModel);
			buildingModel = (GameObject)Instantiate (world.towerPrefab, new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z)+world.offset, Quaternion.identity);
		}
		else if (resource == 0){
			Destroy (buildingModel);
			buildingModel = (GameObject)Instantiate (world.arcanaResPrefab, transform.position+world.offset, Quaternion.identity);
		}
		else if (resource == 1){
			Destroy (buildingModel);
			buildingModel = (GameObject)Instantiate (world.airResPrefab, new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z)+world.offset, Quaternion.identity);
		}
		else if (resource == 2){
			Destroy (buildingModel);
			buildingModel = (GameObject)Instantiate (world.earthResPrefab, new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z)+world.offset, Quaternion.identity);
		}
		else if (resource == 3){
			Destroy (buildingModel);
			buildingModel = (GameObject)Instantiate (world.fireResPrefab, new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z)+world.offset, Quaternion.identity);
		}
		else if (resource == 4){
			Destroy (buildingModel);
			buildingModel = (GameObject)Instantiate (world.waterResPrefab, new Vector3(transform.position.x, transform.position.y + .085f, transform.position.z)+world.offset, Quaternion.identity);
		}
		
	}
	public void hideUnit(){
		if (occupyer != null){
			occupyer.hide();
		}
	}
	public void show(){
		if (occupyer != null){
			occupyer.show ();
		}
		if (owner == 0){
			if(wiz){
				//do nothing
			}
			else if (hasFont){
				Destroy (buildingModel);
				buildingModel = (GameObject)Instantiate (world.fontPrefab0, new Vector3(transform.position.x, transform.position.y + .085f, transform.position.z)+world.offset, Quaternion.identity);
			}
			else if (hasTower){
				Destroy (buildingModel);
				buildingModel = (GameObject)Instantiate (world.towerPrefab0, new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z)+world.offset, Quaternion.identity);
			}
			else if (resource == 0){
				Destroy (buildingModel);
				buildingModel = (GameObject)Instantiate (world.arcanaResPrefab0, transform.position+world.offset, Quaternion.identity);
			}
			else if (resource == 1){
				Destroy (buildingModel);
				buildingModel = (GameObject)Instantiate (world.airResPrefab0, new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z)+world.offset, Quaternion.identity);
			}
			else if (resource == 2){
				Destroy (buildingModel);
				buildingModel = (GameObject)Instantiate (world.earthResPrefab0, new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z)+world.offset, Quaternion.identity);
			}
			else if (resource == 3){
				Destroy (buildingModel);
				buildingModel = (GameObject)Instantiate (world.fireResPrefab0, new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z)+world.offset, Quaternion.identity);
			}
			else if (resource == 4){
				Destroy (buildingModel);
				buildingModel = (GameObject)Instantiate (world.waterResPrefab0, new Vector3(transform.position.x, transform.position.y + .085f, transform.position.z)+world.offset, Quaternion.identity);
			}
		}
		if (owner == 1){
			if (wiz){
				//do nothing
			}
			else if (hasFont){
				Destroy (buildingModel);
				buildingModel = (GameObject)Instantiate (world.fontPrefab1, new Vector3(transform.position.x, transform.position.y + .085f, transform.position.z)+world.offset, Quaternion.identity);
			}
			else if (hasTower){
				Destroy (buildingModel);
				buildingModel = (GameObject)Instantiate (world.towerPrefab1, new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z)+world.offset, Quaternion.identity);
			}
			else if (resource == 0){
				Destroy (buildingModel);
				buildingModel = (GameObject)Instantiate (world.arcanaResPrefab1, transform.position+world.offset, Quaternion.identity);
			}
			else if (resource == 1){
				Destroy (buildingModel);
				buildingModel = (GameObject)Instantiate (world.airResPrefab1, new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z)+world.offset, Quaternion.identity);
			}
			else if (resource == 2){
				Destroy (buildingModel);
				buildingModel = (GameObject)Instantiate (world.earthResPrefab1, new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z)+world.offset, Quaternion.identity);
			}
			else if (resource == 3){
				Destroy (buildingModel);
				buildingModel = (GameObject)Instantiate (world.fireResPrefab1, new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z)+world.offset, Quaternion.identity);
			}
			else if (resource == 4){
				Destroy (buildingModel);
				buildingModel = (GameObject)Instantiate (world.waterResPrefab1, new Vector3(transform.position.x, transform.position.y + .085f, transform.position.z)+world.offset, Quaternion.identity);
			}
		}
	}
}