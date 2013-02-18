using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {
	
	public int size;
	public Tile prefabWall;
	public Tile prefab0;
	public Tile prefab1;
	public Tile prefab2;
	public Tile[,] map;
	

	// Use this for initialization
	void Start () {
		map = new Tile[size,size];
		for (int i = 0; i < size; ++i){
			for (int j = 0; j < size; ++j){
				int r = Random.Range (0,3);
				if (i == 0 || j == 0 || i == size-1 || j== size-1){
					map[i,j] = (Tile)Instantiate (prefabWall, new Vector3((float)(i+(.5*(j%2))),0f,(float)j), new Quaternion (0,0,0,0));
					map[i,j].init (i,j,-1);
				}
					
				else if (r == 0){
					map[i,j] = (Tile)Instantiate (prefab0, new Vector3((float)(i+(.5*(j%2))),0f,(float)j), new Quaternion (0,0,0,0));
					map[i,j].init (i,j,0);
				}
				else if (r == 1){
					map[i,j] = (Tile)Instantiate (prefab1, new Vector3((float)(i+(.5*(j%2))),0f,(float)j), new Quaternion (0,0,0,0));
					map[i,j].init (i,j,1);
				}
				else{
					map[i,j] = (Tile)Instantiate (prefab2, new Vector3((float)(i+(.5*(j%2))),0f,(float)j), new Quaternion (0,0,0,0));
					map[i,j].init (i,j,2);
				}
			}
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
