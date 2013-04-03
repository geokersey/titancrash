using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {
	
	public int size;
	public int defaultQuant = 2;
	public int numPlayers = 2;
	public int radius = 1;
	public int x0, y0;
	public int x1, y1;
	
	public Tile prefabWall;
	public Tile prefab0;
	public Tile prefab1;
	public Tile prefab2;
	
	public GameObject fontPrefab;
	public GameObject fontPrefab0;
	public GameObject fontPrefab1;
	
	public GameObject towerPrefab;
	public GameObject towerPrefab0;
	public GameObject towerPrefab1;
	
	public GameObject arcanaResPrefab;
	public GameObject airResPrefab;
	public GameObject earthResPrefab;
	public GameObject fireResPrefab;
	public GameObject waterResPrefab;
	
	public GameObject arcanaResPrefab0;
	public GameObject airResPrefab0;
	public GameObject earthResPrefab0;
	public GameObject fireResPrefab0;
	public GameObject waterResPrefab0;
	
	public GameObject arcanaResPrefab1;
	public GameObject airResPrefab1;
	public GameObject earthResPrefab1;
	public GameObject fireResPrefab1;
	public GameObject waterResPrefab1;
	
	
	
	
	public SummoningFont summoningFont;
	
	public Tile[,] map;
	public Tile selected;
	public ParticleSystem highlight;
	public Player playerPrefab;
	public Player[] players;
	public SpellManager spells;
	public int activePlayer = 0;
	float minTurnTime = -1;
	public GameObject cam;
	bool unitsAlive = true;
	public bool suspended;
	public techstuff techs;
	int turn = 0;
	// Use this for initialization
	void Awake () {
		players = new Player[numPlayers];
		for (int i = 0; i <numPlayers; ++i){
			players[i] = (Player)Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0,i*180,0));
			players[i].init ();
			//instantiate and initialize
		}
		activePlayer = 0;
//		players[0].activate();
		size = (radius*2)+1;
		map = new Tile[size,size];
		float jMult = Mathf.Sqrt (.75f);
		for (int i = 0; i < size; ++i){
			for (int j = 0; j < size; ++j){
				if(i == x0 && j == y0){
					map[i,j] = (Tile)Instantiate (prefab2, new Vector3((float)(i+(.5*j)),0f,(float)j*jMult), Quaternion.Euler(0,30,0));
					map[i,j].init (i,j,2, true,false,-1,defaultQuant, this);
					
				}
				else if(i == x1 && j == y1){
					map[i,j] = (Tile)Instantiate (prefab2, new Vector3((float)(i+(.5*j)),0f,(float)j*jMult), Quaternion.Euler(0,30,0));
					map[i,j].init (i,j,2, true,false, -1,defaultQuant, this);
					
				}
					else{
					int r = Random.Range (0,3);
					
					if (Mathf.Abs(radius - i) >= radius || Mathf.Abs(radius - j) >= radius || Mathf.Abs(radius*2 -(i+j)) >= radius){ 
						map[i,j] = (Tile)Instantiate (prefabWall, new Vector3((float)(i+(.5*j)),0f,(float)j*jMult), Quaternion.Euler(0,30,0));
						map[i,j].init (i,j,-1, false, false, -1, defaultQuant, this);
					}
						
					else if (r == 0){
						map[i,j] = (Tile)Instantiate (prefab0, new Vector3((float)(i+(.5*j)),0f,(float)j*jMult), Quaternion.Euler(0,30,0));
						map[i,j].init (i,j,0, false, true, -1,defaultQuant, this);
					}
					else if (r == 1){
						map[i,j] = (Tile)Instantiate (prefab1, new Vector3((float)(i+(.5*j)),0f,(float)j*jMult), Quaternion.Euler(0,30,0));
						map[i,j].init (i,j,1, false, false, 3,defaultQuant, this);
					}
					else{
						map[i,j] = (Tile)Instantiate (prefab2, new Vector3((float)(i+(.5*j)),0f,(float)j*jMult), Quaternion.Euler(0,30,0));
						map[i,j].init (i,j,2, true, false, -1,defaultQuant, this);
					}
				}
			}
			
		}
		map[x0, y0].capture (0);
		map[x1, y1].capture (1);
		
		endTurn ();
	}
	
	public bool endTurn(){
		
		if (minTurnTime < 0 && !suspended){
			unitsAlive = false;
			if (selected!= null){
				selected.deselect ();
			}
			activePlayer = (activePlayer +1)% numPlayers;
			techs.checkAvailability();
			players[activePlayer].beginTurn ();
//			players[activePlayer].activate();
			minTurnTime = 1;
			
//			Debug.Log("turn ended "+turn.ToString ());
			turn++;
			summoningFont.hide ();
			
			
			
			for (int i = 0; i<size; ++i){
				for (int j = 0; j<size; ++j){
					map[i,j].beginTurn ();
					if (activePlayer == 0){
						if (map[i,j].gameObject.tag == "vis1" || map[i,j].gameObject.tag == "visNone"){
							//map[i,j].gameObject.tag = "Untagged";
							map[i,j].gameObject.layer = 8;
							map[i,j].hide ();
							
							//object under complete fog
						}
						if (map[i,j].gameObject.layer != 10 && (map[i,j].gameObject.tag == "vis0" || map[i,j].gameObject.tag == "visBoth")){
							map[i,j].gameObject.layer = 9;
							map[i,j].show();
							//object under partial fog	
							}
					}
					else if (activePlayer == 1){
						if (map[i,j].gameObject.tag == "vis0" || map[i,j].gameObject.tag == "visNone"){
							//map[i,j].gameObject.tag = "Untagged";
							map[i,j].gameObject.layer = 8;
							map[i,j].hide ();
							//object under complete fog
						}
						if (map[i,j].gameObject.layer != 11 && (map[i,j].gameObject.tag == "vis1" || map[i,j].gameObject.tag == "visBoth")){
							map[i,j].gameObject.layer = 9;
							map[i,j].show();
							//object under partial fog	
							}
					}
					if (map[i, j].owner == activePlayer){
						map[i,j].gameObject.layer = 10+activePlayer;
						if (map[i, j].occupyer != null){
							map[i,j].occupyer.see (i,j);
							map[i,j].occupyer.beginTurn ();
						}
						if (map[i,j].hasTower){
							map[i,j].see (3);
							}
					}	
		
				}
			
			}
			//if (!unitsAlive){
			//	Debug.Log ("player "+(1-activePlayer).ToString ()+" has no more units");
			//}
			return true;	
		}
		return false;
	}
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		minTurnTime -= Time.deltaTime;
		if (Input.GetKey ("space")){
			endTurn();
		}
	
	}
	
	void OnGUI()
	{
		if (suspended){
			GUI.Box (new Rect(200, 0, 250, 100), "suspended = true");
		}
		
		GUI.Box(new Rect(1250, 0, 100, 25), "Player 1: " + players[0].resources[0]);
		GUI.Box(new Rect(1250, 30, 100, 25), "Player 1: " + players[0].resources[1]);
		GUI.Box(new Rect(1250, 60, 100, 25), "Player 1: " + players[0].resources[2]);
		GUI.Box(new Rect(1250, 90, 100, 25), "Player 1: " + players[0].resources[3]);
		GUI.Box(new Rect(1250, 120, 100, 25), "Player 1: " + players[0].resources[4]);
		
		GUI.Box(new Rect(1250, 150, 100, 25), "Player 2: " + players[1].resources[0]);
		GUI.Box(new Rect(1250, 180, 100, 25), "Player 2: " + players[1].resources[1]);
		GUI.Box(new Rect(1250, 210, 100, 25), "Player 2: " + players[1].resources[2]);
		GUI.Box(new Rect(1250, 240, 100, 25), "Player 2: " + players[1].resources[3]);
		GUI.Box(new Rect(1250, 270, 100, 25), "Player 2: " + players[1].resources[4]);
	
	
		if (minTurnTime < 0 && !suspended){
			if (GUI.Button (new Rect(1250, 500, 125, 40), "end turn")){
			endTurn();
			}
		}
	}
	
}