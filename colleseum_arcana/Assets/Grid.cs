using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {
	
	int size;
	public int numPlayers = 2;
	public int radius = 1;
	public Tile prefabWall;
	public Tile prefab0;
	public Tile prefab1;
	public Tile prefab2;
	public Tile[,] map;
	public Tile selected;
	public ParticleSystem highlight;
	public Player playerPrefab;
	public Player[] players;
	public int activePlayer = 0;
	float minTurnTime;
	public Cameracontrol camera;
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
					map[i,j].init (i,j,-1);
				}
					
				else if (r == 0){
					map[i,j] = (Tile)Instantiate (prefab0, new Vector3((float)(i+(.5*j)),0f,(float)j), new Quaternion (0,0,0,0));
					map[i,j].init (i,j,0);
				}
				else if (r == 1){
					map[i,j] = (Tile)Instantiate (prefab1, new Vector3((float)(i+(.5*j)),0f,(float)j), new Quaternion (0,0,0,0));
					map[i,j].init (i,j,1);
				}
				else{
					map[i,j] = (Tile)Instantiate (prefab2, new Vector3((float)(i+(.5*j)),0f,(float)j), new Quaternion (0,0,0,0));
					map[i,j].init (i,j,2);
				}
			}
		}
	
	}
	
	public bool endTurn(){
		if (minTurnTime < 0){
			activePlayer = (activePlayer +1)% numPlayers;
			players[activePlayer].activate();
			minTurnTime = 2;
			camera.transform.position = players[activePlayer].transform.position;
			Debug.Log("turn ended");
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
	
}
