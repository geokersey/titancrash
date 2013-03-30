using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {
	
	public int size;
	public int numPlayers = 2;
	public int radius = 1;
	public int x0, y0;
	public int x1, y1;
	
	public Tile prefabWall;
	public Tile prefab0;
	public Tile prefab1;
	public Tile prefab2;
	
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
	public int activePlayer = 0;
	float minTurnTime = -1;
	public GameObject cam;
	bool unitsAlive = true;
	int turn = 0;
	// Use this for initialization
	void Awake () {
		players = new Player[numPlayers];
		for (int i = 0; i <numPlayers; ++i){
			players[i] = (Player)Instantiate(playerPrefab, new Vector3(-10, 10, 6), Quaternion.Euler(0,i*180,0));
			//instantiate and initialize
		}
		activePlayer = 0;
		players[0].activate();
		size = (radius*2)+1;
		map = new Tile[size,size];
		for (int i = 0; i < size; ++i){
			for (int j = 0; j < size; ++j){
				if(i == x0 && j == y0){
					map[i,j] = (Tile)Instantiate (prefab2, new Vector3((float)(i+(.5*j)),0f,(float)j), Quaternion.Euler(0,30,0));
					map[i,j].init (i,j,2, true,-1, this);
					map[i,j].owner = 0;
//					map[i,j].occupyer = (Unit)Instantiate (tinyAirPrefab0,map[i,j].transform.position, Quaternion.Euler(0,30,0));
//					map[i,j].occupyer.init(i,j,this);
					//map[i,j].occupyer.availableMovePoints=map[i,j].occupyer.startingMovePoints;
					
				}
				else if(i == x1 && j == y1){
					map[i,j] = (Tile)Instantiate (prefab2, new Vector3((float)(i+(.5*j)),0f,(float)j), Quaternion.Euler(0,30,0));
					map[i,j].init (i,j,2, true, -1, this);
					map[i,j].owner = 1;
					//map[i,j].occupyer = (Unit)Instantiate (tinyAirPrefab1,map[i,j].transform.position, Quaternion.Euler(0,30,0));
//					map[i,j].occupyer.init(i,j,this);
					//map[i,j].occupyer.availableMovePoints=map[i,j].occupyer.startingMovePoints;
				}
					else{
					int r = Random.Range (0,3);
					/*if (i == 0 || j == 0 || i == size-1 || j== size-1){
						map[i,j] = (Tile)Instantiate (prefabWall, new Vector3((float)(i+(.5*j)),0f,(float)j), new Quaternion (0,0,0,0));
						map[i,j].init (i,j,-1);
					}*/
					if (Mathf.Abs(radius - i) >= radius || Mathf.Abs(radius - j) >= radius || Mathf.Abs(radius*2 -(i+j)) >= radius){ 
						map[i,j] = (Tile)Instantiate (prefabWall, new Vector3((float)(i+(.5*j)),0f,(float)j), Quaternion.Euler(0,30,0));
						map[i,j].init (i,j,-1, false, -1, this);
					}
						
					else if (r == 0){
						map[i,j] = (Tile)Instantiate (prefab0, new Vector3((float)(i+(.5*j)),0f,(float)j), Quaternion.Euler(0,30,0));
						map[i,j].init (i,j,0, false, 2, this);
					}
					else if (r == 1){
						map[i,j] = (Tile)Instantiate (prefab1, new Vector3((float)(i+(.5*j)),0f,(float)j), Quaternion.Euler(0,30,0));
						map[i,j].init (i,j,1, false, 3, this);
					}
					else{
						map[i,j] = (Tile)Instantiate (prefab2, new Vector3((float)(i+(.5*j)),0f,(float)j), Quaternion.Euler(0,30,0));
						map[i,j].init (i,j,2, true, 4, this);
					}
				}
			}
		}
		endTurn ();
	}
	
	public bool endTurn(){
		//Debug.Log ("spacebar "+minTurnTime.ToString ());
		if (minTurnTime < 0){
			unitsAlive = false;
			if (selected!= null){
				selected.deselect ();
			}
			activePlayer = (activePlayer +1)% numPlayers;
			players[activePlayer].activate();
			minTurnTime = 1;
			cam.transform.position = players[activePlayer].transform.position;
			Debug.Log("turn ended "+turn.ToString ());
			turn++;
			summoningFont.hide ();
			
			/*for (int i = 0; i<size; ++i){
				for (int j = 0; j<size; ++j){
					map[i,j].gameObject.layer = 8;
				}
			}*/
			if(activePlayer == 0)
			{
				for (int i = 0; i<size; ++i){
					for (int j = 0; j<size; ++j){
						if(map[i,j].occupyer!=null&&map[i,j].owner==1){
							unitsAlive = true;
							map[i,j].occupyer.beginTurn();
						}
						/*if(map[i,j].occupyer!=null&&map[i,j].owner==0){
							unitsAlive = true;
							map[i,j].occupyer.beginTurn();
							Debug.Log("things be backwards");
						}*/
						if (map[i,j].gameObject.tag == "visible1"||map[i,j].gameObject.tag == "range"){
							map[i,j].gameObject.tag = "Untagged";
							map[i,j].gameObject.layer = 8;
						}
						if (map[i,j].gameObject.tag == "visible0"||map[i,j].owner ==activePlayer){
							map[i,j].gameObject.layer = 9;
							map[i,j].gameObject.tag = "visible0";
							if (map[i,j].occupyer != null){
								map[i,j].occupyer.see(i,j);
								//Debug.Log(activePlayer);
							}
						}	
							
					}
				}
			}
			else if (activePlayer == 1)
			{
				for (int i = 0; i<size; ++i){
					for (int j = 0; j<size; ++j){
						if(map[i,j].occupyer!=null&&map[i,j].owner==0){
							unitsAlive = true;
							map[i,j].occupyer.beginTurn();
						}
						/*if(map[i,j].occupyer!=null&&map[i,j].owner==1){
							unitsAlive = true;
							map[i,j].occupyer.beginTurn();
							Debug.Log ("things be backwards");
						}*/
						if (map[i,j].gameObject.tag == "visible0"||map[i,j].gameObject.tag == "range"){
							map[i,j].gameObject.tag = "Untagged";
							map[i,j].gameObject.layer = 8;
						}
						if (map[i,j].gameObject.tag == "visible1"||map[i,j].owner ==activePlayer){
							map[i,j].gameObject.layer = 9;
							map[i,j].gameObject.tag = "visible1";
							if (map[i,j].occupyer != null){
								map[i,j].occupyer.see(i,j);
								//Debug.Log(activePlayer);
							}
						}
						
							
					}
				}
			}
			if (!unitsAlive){
				Debug.Log ("gameOver, player "+(1-activePlayer).ToString ()+" has no more units");
			}
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
		//needs fix
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
	}
}