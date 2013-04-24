using UnityEngine;
using System.Collections;

public class ChangeTile : MonoBehaviour 
{
	public string[] Name_Library = new string[30];//Holds the names of all the tiles
	public GameObject[] Tile_Library = new GameObject[30];//Holds the prefabs of all the gameobjects
	//////All possible Tile names
	/*
	Blank
	
	T1_With_Nothing
	T1_With_Font;
	T1_With_Tower;
	T1_With_R1;
	T1_With_R2;
	T1_With_R3;
	T1_With_R4;
	T1_With_R5;
	
	T2_With_Nothing
	T2_With_Font;
	T2_With_Tower;
	T2_With_R1;
	T2_With_R2;
	T2_With_R3;
	T2_With_R4;
	T2_With_R5;
	
	T3_With_Nothing
	T3_With_Font;
	T3_With_Tower;
	T3_With_R1;
	T3_With_R2;
	T3_With_R3;
	T3_With_R4;
	T3_With_R5;
	*/
	
	//Changes a selected tile
	public void Replace(GameObject tile, string new_tile)
	{
		//Debug.Log (tile);
		int x = -1;
		int y = -1;
	
		for(int i = 0; i < 99; i++)
		{
			for(int j = 0; j < 99; j++)
			{
				if(tile == this.transform.gameObject.GetComponent<GenerateMap>().GridTiles[i,j])
				{
					//found the correct tile to replace
					x = i;
					y = j;
				}
				else if(new_tile == "Player1Start" && 
					this.transform.gameObject.GetComponent<GenerateMap>().GridTiles[i,j] != null && 
					this.transform.gameObject.GetComponent<GenerateMap>().GridTiles[i,j].name == "Player1Start(Clone)")
				{
					Replace(this.transform.gameObject.GetComponent<GenerateMap>().GridTiles[i,j], "Blank");
				}
				else if(new_tile == "Player2Start" && 
					this.transform.gameObject.GetComponent<GenerateMap>().GridTiles[i,j] != null && 
					this.transform.gameObject.GetComponent<GenerateMap>().GridTiles[i,j].name == "Player2Start(Clone)")
				{
					Replace(this.transform.gameObject.GetComponent<GenerateMap>().GridTiles[i,j], "Blank");
				}
				
				
				
				//if(new_tile == "Player1Start" && this.transform.gameObject.GetComponent<GenerateMap>().GridTiles[i,j] != null && this.transform.gameObject.GetComponent<GenerateMap>().GridTiles[i,j].name == "Player1Start(Clone)")
				//{
					//Destroy (this.transform.gameObject.GetComponent<GenerateMap>().GridTiles[i,j]);
					//this.transform.gameObject.GetComponent<GenerateMap>().GridTiles[i,j] = Instantiate (Tile_Library[0], tile.transform.position, Quaternion.Euler(0,30,0)) as GameObject;
				//}
			}
		}
		
		for(int i = 0; i < Name_Library.Length; i++)
		{
			if(x != -1 && new_tile == Name_Library[i])
			{
				Destroy (this.transform.gameObject.GetComponent<GenerateMap>().GridTiles[x,y]);
				this.transform.gameObject.GetComponent<GenerateMap>().GridTiles[x,y] = Instantiate (Tile_Library[i], tile.transform.position, Quaternion.Euler(0,30,0)) as GameObject;
				break;
			}
		}
	}
}
