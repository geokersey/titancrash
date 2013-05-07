using UnityEngine;
//using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class Grid : MonoBehaviour {
	
	public int size;
	public int defaultQuant = 2;
	public int numPlayers = 2;
	public int radius = 1;
	public int x0, y0;
	public int x1, y1;
	public float jMult;
	
	public Tile prefabWall;
	public Tile prefab0;
	public Tile prefab1;
	public Tile prefab2;
	public GameObject mountainModelPrefab;
	
	public GameObject wizTowerPrefab0;
	public GameObject wizTowerPrefab1;
	
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
	
	public Vector3 offset;
	public GameObject TileOutline;
	private List<GameObject> OutlineTiles = new List<GameObject>();
	
	public GUISkin GUIfunstuff;
	
	public SummoningFont summoningFont;
	
	public Tile[,] map = new Tile[0,0];
	public Tile selected;
	public GameObject highlight;
	public GameObject hiModel;
	//public GameObject id0prefab;
	//public GameObject id1prefab;
	
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
	public int GUIstate = 0;
	int winner = -1;
	//bool gameOver = false;
	
	private string MapName = StartScreen.LevelName;
	
	public AudioClip EarthMusic;
	public AudioClip WindMusic;
	public AudioClip WaterMusic;
	public AudioClip FireMusic;
	public int song = 3;
	
	
	// Use this for initialization
	void Awake () {
		offset = new Vector3 (0f, 0f, -.25f);
		highlight = (GameObject) Instantiate (hiModel);
		//highlight.transform.
		players = new Player[numPlayers];
		for (int i = 0; i <numPlayers; ++i){
			players[i] = (Player)Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0,i*180,0));
			players[i].init ();
			//instantiate and initialize
		}
		techs.init();
		activePlayer = 0;
//		players[0].activate();
		//size = (radius*2)+1;
		
		
		
		/////
		if(!Directory.Exists("C:/ElementalFury/Maps"))
		{
			Debug.Log ("Created a directory!");
			Directory.CreateDirectory("C:/ElementalFury/Maps");
		}
		if(!File.Exists("C:/ElementalFury/Maps/DefaultMatch.xml"))
		{
			Debug.Log ("Couldn't find default map. Copying from install");
			File.Copy("Assets/DefaultMatch.xml", "C:/ElementalFury/Maps/DefaultMatch.xml");
			//FileUtil.CopyFileOrDirectory ("Assets/DefaultMatch.xml", "C:/ElementalFury/Maps/DefaultMatch.xml");
		}
		//Debug.Log ("Normal: " + Application.dataPath);
		//Debug.Log ("Persistent: " + Application.persistentDataPath);
		TileContainer LoadedMap = LoadMap(MapName + ".xml");
		/////
		
		radius = (size-1)/2;
		map = new Tile[size,size];
		jMult = Mathf.Sqrt (.75f);
		/*
		x0 = 2;
		y0 = (size+1)/2 - 1;
		x1 = 16;
		y1 = (size+1)/2 - 1;*/
		
		//size = 19;
		int z = 0;
		for (int i = 0; i < size; ++i){
			for (int j = 0; j < size; ++j){
				Setup(LoadedMap.Tiles[z], i, j);
				z++;
				/*
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
				}*/
			}
			
		}
		map[x0, y0].capture (0);
		map[x0, y0].gameObject.tag = "visBoth";
		map[x1, y1].capture (1);
		map[x1, y1].gameObject.tag = "visBoth";
		
		endTurn ();
	}
	
	public bool endTurn(){
		
		if (minTurnTime < 0 && !suspended){
			//unitsAlive = false;
			if (selected!= null){
				selected.deselect ();
			}
			activePlayer = (activePlayer +1)% numPlayers;
			if(activePlayer == map[x1,y1].owner&&activePlayer == map[x0,y0].owner){
				Debug.Log ("player "+activePlayer+" is victorious");
				winner = activePlayer;
			}
			/*if(activePlayer == 0 && map[x1,y1].owner==0 && map[x0,y0].owner ==0){
				Debug.Log ("player 0 has captured player both bases");
				GUIstate = 2;
				
			}
			else if(activePlayer == 1 && map[x0,y0].owner==1 && map[x1,y1].owner == 1){
				Debug.Log ("player 1 has captured both bases");
				GUIstate = 2;
			}*/	
			//Debug.Log ("1");
			techs.checkAvailability();
			players[activePlayer].beginTurn ();
//			players[activePlayer].activate();
			minTurnTime = 1;
			
//			Debug.Log("turn ended "+turn.ToString ());
			turn++;
			summoningFont.hide ();
			
			
			//Debug.Log ("2");
			for (int i = 0; i<size; i++){
				for (int j = 0; j<size; j++){
					map[i,j].beginTurn ();
					if (activePlayer == 0){
						if (map[i,j].gameObject.tag == "vis1" || map[i,j].gameObject.tag == "visNone"){
							//map[i,j].gameObject.tag = "Untagged";
							map[i,j].gameObject.layer = 9;
							map[i,j].hide ();
							map[i,j].hideUnit ();
							
							//object under complete fog
						}
						if (map[i,j].gameObject.layer != 10 && (map[i,j].gameObject.tag == "vis0" || map[i,j].gameObject.tag == "visBoth")){
							map[i,j].gameObject.layer = 9;
							map[i,j].show();
							map[i,j].hideUnit();
							//object under partial fog	
							}
					}
					else if (activePlayer == 1){
						if (map[i,j].gameObject.tag == "vis0" || map[i,j].gameObject.tag == "visNone"){
							//map[i,j].gameObject.tag = "Untagged";
							map[i,j].gameObject.layer = 9;
							map[i,j].hide ();
							map[i,j].hideUnit ();
							//object under complete fog
						}
						if (map[i,j].gameObject.layer != 11 && (map[i,j].gameObject.tag == "vis1" || map[i,j].gameObject.tag == "visBoth")){
							map[i,j].gameObject.layer = 9;
							map[i,j].show();
							map[i,j].hideUnit ();
							//object under partial fog	
							}
					}
					if (map[i, j].owner == activePlayer){
						map[i,j].gameObject.layer = 10+activePlayer;
						map[i,j].show ();
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
	
		//if old song ends, start new song
		if (audio.isPlaying == false)
		{
			if (song == 3)
				song = 0;
			else
				song++;
			switch(song)
			{
			case 0:
				audio.clip = EarthMusic;
				break;
			case 1:
				audio.clip = WindMusic;
				break;
			case 2:
				audio.clip = WaterMusic;
				break;
			case 3:
				audio.clip = FireMusic;
				break;
			}
			audio.Play();
		}
	}
	
	void OnGUI()
	{
		if (winner == 0){
			if (GUI.Button (new Rect((Screen.width/2)-250, (Screen.height/2)-100, 500, 200),"congratulations player 2, you are victorious")){
				Application.LoadLevel("StartingScreen");
			}
			
		}
		else if (winner == 1){
			if (GUI.Button (new Rect((Screen.width/2)-250, (Screen.height/2)-100, 500, 200),"congratulations player 1, you are victorious")){
				Application.LoadLevel("StartingScreen");
			}
			
		}
		if (suspended){
			//GUI.Box (new Rect(200, 0, 250, 100), "suspended = true", GUIfunstuff.box);
		}
		GUI.Box(new Rect(Screen.width - 150, 0, 100, 25), "Player "+activePlayer, GUIfunstuff.box);
		GUI.Box(new Rect(0, 500, 100, 25), "Resources", GUIfunstuff.box);
		GUI.Box(new Rect(0, 525, 100, 25), "Arcana: " + players[activePlayer].resources[0], GUIfunstuff.box);
		GUI.Box(new Rect(0, 550, 100, 25), "Air: " + players[activePlayer].resources[1], GUIfunstuff.box);
		GUI.Box(new Rect(0, 575, 100, 25), "Earth: " + players[activePlayer].resources[2], GUIfunstuff.box);
		GUI.Box(new Rect(0, 600, 100, 25), "Fire: " + players[activePlayer].resources[3], GUIfunstuff.box);
		GUI.Box(new Rect(0, 625, 100, 25), "Water: " + players[activePlayer].resources[4], GUIfunstuff.box);
		
		/*GUI.Box(new Rect(1250, 150, 100, 25), "Player 2: " + players[1].resources[0]);
		GUI.Box(new Rect(1250, 180, 100, 25), "Player 2: " + players[1].resources[1]);
		GUI.Box(new Rect(1250, 210, 100, 25), "Player 2: " + players[1].resources[2]);
		GUI.Box(new Rect(1250, 240, 100, 25), "Player 2: " + players[1].resources[3]);
		GUI.Box(new Rect(1250, 270, 100, 25), "Player 2: " + players[1].resources[4]);*/
		
		if (minTurnTime < 0 && !suspended){
			if (GUI.Button (new Rect(Screen.width * 0.85f, Screen.height * 0.85f, 125, 40), "End turn", GUIfunstuff.button)){
			endTurn();
			}
		}
	}
	public TileContainer LoadMap(string name)
	{
		TileContainer temp = TileContainer.Load("C:/ElementalFury/Maps/" + name);
		//Debug.Log ("C:/ProgramFiles/ElementalFury/Maps/" + name);
		int ms = temp.Tiles.Count;
		int EdgeSize = 0;
		if(ms == 361)
		{
			//Small Map: Map edge of 19
			EdgeSize = 19;
		}
		else if(ms == 1521)
		{
			//Medium Map: Map edge of 39
			EdgeSize = 39;
		}
		else if(ms == 3481)
		{
			//Large Map: Map edge of 59
			EdgeSize = 59;
		}
		size = EdgeSize;
		return temp;
	}
	public void Setup(TileBlock tile, int i, int j)
	{
		if(tile.Name == "P1" || tile.Name == "Player1Start(Clone)")
		{
			//Debug.Log ("P1");
			//player0
			x0 = i;
			y0 = j;
			map[i,j] = (Tile)Instantiate (prefab0, new Vector3((float)(i+(.5*j) - (1.5f*radius)),0f,(float)j*jMult - radius*jMult), Quaternion.Euler(0,30 + (60*Random.Range (0,10)),0));
			map[i,j].init (i,j,2, true,true,true,-1,defaultQuant, this);
			map[i,j].buildingModel = (GameObject)Instantiate(wizTowerPrefab0, map[i,j].transform.position+offset, Quaternion.Euler(0,90,0));
			GameObject Outline = (GameObject)Instantiate (TileOutline, new Vector3((float)(i+(.5*j) - (1.5f*radius)),0f,(float)j*jMult - radius*jMult), Quaternion.Euler(0,30,0));
			OutlineTiles.Add (Outline);
		}
		else if(tile.Name == "P2" || tile.Name == "Player2Start(Clone)")
		{
			//player1
			//Debug.Log ("player 1 start");
			x1 = i;
			y1 = j;
			map[i,j] = (Tile)Instantiate (prefab0, new Vector3((float)(i+(.5*j) - (1.5f*radius)),0f,(float)j*jMult - radius*jMult), Quaternion.Euler(0,30 + (60*Random.Range (0,10)),0));
			map[i,j].init (i,j,2, true,true,true, -1,defaultQuant, this);
			map[i,j].buildingModel = (GameObject)Instantiate(wizTowerPrefab1, map[i,j].transform.position+offset, Quaternion.Euler(0,90,0));
			GameObject Outline = (GameObject)Instantiate (TileOutline, new Vector3((float)(i+(.5*j) - (1.5f*radius)),0f,(float)j*jMult - radius*jMult), Quaternion.Euler(0,30,0));
			OutlineTiles.Add (Outline);
		}
		else
		{
			bool HasFont = false;
			bool HasTower = false;
			int ResourceType = -1;
			if(tile.HasFont)
			{
				HasFont = true;
			}
			if(tile.HasTower)
			{
				HasTower = true;
			}
			if(tile.Resource == "Arcana")
			{
				ResourceType = 0;
			}
			else if(tile.Resource == "Air")
			{
				ResourceType = 1;	
			}
			else if(tile.Resource == "Earth")
			{
				ResourceType = 2;	
			}
			else if(tile.Resource == "Fire")
			{
				ResourceType = 3;	
			}
			else if(tile.Resource == "Water")
			{
				ResourceType = 4;	
			}
			
			if(tile.Name == "T1")
			{
				map[i,j] = (Tile)Instantiate (prefab0, new Vector3((float)(i+(.5*j) - (1.5f*radius)),0f,(float)j*jMult - radius*jMult), Quaternion.Euler(0,30 + (60*Random.Range (0,10)),0));
				map[i,j].init (i,j,0, HasFont, HasTower,false, ResourceType, defaultQuant, this);
				GameObject Outline = (GameObject)Instantiate (TileOutline, new Vector3((float)(i+(.5*j) - (1.5f*radius)),0f,(float)j*jMult - radius*jMult), Quaternion.Euler(0,30,0));
				OutlineTiles.Add (Outline);
			}
			else if(tile.Name == "T2")
			{
				map[i,j] = (Tile)Instantiate (prefab1, new Vector3((float)(i+(.5*j) - (1.5f*radius)),.1f,(float)j*jMult - radius*jMult), Quaternion.Euler(0,30 + (60*Random.Range (0,10)),0));
				map[i,j].init (i,j,1, HasFont, HasTower,false, ResourceType, defaultQuant, this);
				GameObject Outline = (GameObject)Instantiate (TileOutline, new Vector3((float)(i+(.5*j) - (1.5f*radius)),0f,(float)j*jMult - radius*jMult), Quaternion.Euler(0,30,0));
				OutlineTiles.Add (Outline);
			}
			else if(tile.Name == "T3")
			{
				map[i,j] = (Tile)Instantiate (prefab2, new Vector3((float)(i+(.5*j) - (1.5f*radius)),.1f,(float)j*jMult - radius*jMult), Quaternion.Euler(0,30 + (60*Random.Range (0,10)),0));
				map[i,j].init (i,j,2, HasFont, HasTower,false, ResourceType, defaultQuant, this);
				GameObject Outline = (GameObject)Instantiate (TileOutline, new Vector3((float)(i+(.5*j) - (1.5f*radius)),0f,(float)j*jMult - radius*jMult), Quaternion.Euler(0,30,0));
				OutlineTiles.Add (Outline);
			}
			else
			{
				map[i,j] = (Tile)Instantiate (prefabWall, new Vector3((float)(i+(.5*j) - (1.5f*radius) - (1.5f*radius)),0f,(float)j*jMult - radius*jMult), Quaternion.Euler(0,30,0));
				map[i,j].init (i,j,-1, false, false, false, -1, defaultQuant, this);
			}
			Debug.Log ("startup loop");
		}
		Debug.Log ("end of startup");
	}
	
}

public struct TileBlock
{
	public string Name;
	public bool HasFont;
	public bool HasTower;
	public string Resource;
}

[XmlRoot("Tiles")]
public class TileContainer
{
	[XmlArray("Tiles"),XmlArrayItem("Tile")]
	public List<TileBlock> Tiles = new List<TileBlock>();
	
	public void Save(string path)
	{
		var serializer = new XmlSerializer(typeof(TileContainer));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}

	public static TileContainer Load(string path)
	{
		var serializer = new XmlSerializer(typeof(TileContainer));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as TileContainer;
		}
	}

    //Loads the xml directly from the given string. Useful in combination with www.text.
    public static TileContainer LoadFromText(string text) 
	{
		var serializer = new XmlSerializer(typeof(TileContainer));
		return serializer.Deserialize(new StringReader(text)) as TileContainer;
	}
}