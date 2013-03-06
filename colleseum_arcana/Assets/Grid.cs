using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {
	
	public int size;
	public int numPlayers = 2;
	public int radius = 1;
	public Tile prefabWall;
	public Tile prefab0;
	public Tile prefab1;
	public Tile prefab2;
	
	public Unit tinyAirPrefab0;
	public Unit smallAirPrefab0;
	public Unit mediumAirPrefab0;
	public Unit largeAirPrefab0;
	public Unit tinyEarthPrefab0;
	public Unit smallEarthPrefab0;
	public Unit mediumEarthPrefab0;
	public Unit largeEarthPrefab0;
	public Unit tinyFirePrefab0;
	public Unit smallFirePrefab0;
	public Unit mediumFirePrefab0;
	public Unit largeFirePrefab0;
	public Unit tinyWaterPrefab0;
	public Unit smallWaterPrefab0;
	public Unit mediumWaterPrefab0;
	public Unit largeWaterPrefab0;
	
	public Unit tinyAirPrefab1;
	public Unit smallAirPrefab1;
	public Unit mediumAirPrefab1;
	public Unit largeAirPrefab1;
	public Unit tinyEarthPrefab1;
	public Unit smallEarthPrefab1;
	public Unit mediumEarthPrefab1;
	public Unit largeEarthPrefab1;
	public Unit tinyFirePrefab1;
	public Unit smallFirePrefab1;
	public Unit mediumFirePrefab1;
	public Unit largeFirePrefab1;
	public Unit tinyWaterPrefab1;
	public Unit smallWaterPrefab1;
	public Unit mediumWaterPrefab1;
	public Unit largeWaterPrefab1;
	
	public SummoningFont summoningFont;
	
	public Tile[,] map;
	public Tile selected;
	public ParticleSystem highlight;
	public Player playerPrefab;
	public Player[] players;
	public int activePlayer = 0;
	float minTurnTime;
	public CameraControl cam;
	// Use this for initialization
	void Awake () {
		players = new Player[numPlayers];
		for (int i = 0; i <numPlayers; ++i){
			players[i] = (Player)Instantiate(playerPrefab, new Vector3(radius, radius, 0), new Quaternion (0,0,0,0));
			//instantiate and initialize
		}
		activePlayer = 0;
		players[0].activate();
		size = (radius*2)+1;
		map = new Tile[size,size];
		for (int i = 0; i < size; ++i){
			for (int j = 0; j < size; ++j){
				int r = Random.Range (0,3);
				/*if (i == 0 || j == 0 || i == size-1 || j== size-1){
					map[i,j] = (Tile)Instantiate (prefabWall, new Vector3((float)(i+(.5*j)),0f,(float)j), new Quaternion (0,0,0,0));
					map[i,j].init (i,j,-1);
				}*/
				if (Mathf.Abs(radius - i) >= radius || Mathf.Abs(radius - j) >= radius || Mathf.Abs(radius*2 -(i+j)) >= radius){ 
					map[i,j] = (Tile)Instantiate (prefabWall, new Vector3((float)(i+(.5*j)),0f,(float)j), new Quaternion (0,0,0,0));
					map[i,j].init (i,j,-1, false, false);
				}
					
				else if (r == 0){
					map[i,j] = (Tile)Instantiate (prefab0, new Vector3((float)(i+(.5*j)),0f,(float)j), new Quaternion (0,0,0,0));
					map[i,j].init (i,j,0, false, false);
				}
				else if (r == 1){
					map[i,j] = (Tile)Instantiate (prefab1, new Vector3((float)(i+(.5*j)),0f,(float)j), new Quaternion (0,0,0,0));
					map[i,j].init (i,j,1, false, true);
				}
				else{
					map[i,j] = (Tile)Instantiate (prefab2, new Vector3((float)(i+(.5*j)),0f,(float)j), new Quaternion (0,0,0,0));
					map[i,j].init (i,j,2, true, false);
				}
			}
		}
	
	}
	
	public bool endTurn(){
		int p0 = 0;
		int p1 = 0;
		if (minTurnTime < 0){
			activePlayer = (activePlayer +1)% numPlayers;
			players[activePlayer].activate();
			minTurnTime = 2;
			cam.transform.position = players[activePlayer].transform.position;
			Debug.Log("turn ended");
			for (int i = 0; i<size; ++i){
				for (int j = 0; j<size; ++j){
					map[i,j].gameObject.layer = 8;
					
					if (map[i, j].owner == 0 && map[i, j].hasRes)
					{
						p0 += 2;
					}
					else if (map[i, j].owner == 1 && map[i, j].hasRes)
					{
						p1 += 2;
					}
				}
			}
			for (int i = 0; i<size; ++i){
				for (int j = 0; j<size; ++j){
					if (map[i,j].owner == activePlayer){
						//Debug.Log ("should see" +i.ToString()+", "+j.ToString());
						map[i,j].gameObject.layer = 9;
						if (map[i,j].occupyer != null){
							//Debug.Log ("occupied");
							//[i,j].occupyer.see(i,j);
						}
					}
					
						
				}
			}
			players[0].res = p0;
			players[1].res = p1;
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
		GUI.Box(new Rect(1250, 0, 100, 25), "Player 1: " + players[0].res);
		GUI.Box(new Rect(1250, 30, 100, 25), "Player 2: " + players[1].res);
	}
}