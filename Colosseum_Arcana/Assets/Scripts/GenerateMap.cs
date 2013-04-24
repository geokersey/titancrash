using UnityEngine;
using System.Collections;

public class GenerateMap : MonoBehaviour 
{
	public GameObject[,] GridTiles = new GameObject[100,100]; //Stores the actual tiles
	public GameObject Blank;
	public GameObject Edge;
	public GameObject Empty;
	
	public string Size = "";

	public int EdgeSize;
	private int radius;
	
	public void Generate(int EdgeSize)
	{
		if(EdgeSize == 10)
		{
			Size = "Small";
		}
		else if(EdgeSize == 20)
		{
			Size = "Medium";
		}
		else if(EdgeSize == 30)
		{
			Size = "Large";
		}
		
		
		
		int Map_size = EdgeSize*2 - 1;
		radius = (Map_size - 1)/2;
		for(int i = 0; i < Map_size; i++)
		{
			for(int j = 0; j < Map_size; j++)
			{
				if(i < Map_size - EdgeSize - j || i > EdgeSize - 2 + Map_size - j)
				{
					GridTiles[i,j] = Instantiate (Empty, new Vector3((i * 1.1f + .55f * j) - radius*1.65f,0,(j * 1.0f) - radius*1.0f), Quaternion.Euler(0,30,0)) as GameObject;
				}
				else if(i < Map_size - EdgeSize - j  + 1 || i > EdgeSize - 3 + Map_size - j ||
					    i == 0 || i == Map_size - 1 || j == 0 || j == Map_size - 1)
				{
					GridTiles[i,j] = Instantiate (Edge, new Vector3((i * 1.1f + .55f * j) - radius*1.65f,0,(j * 1.0f) - radius*1.0f), Quaternion.Euler(0,30,0)) as GameObject;
				}
				else
				{
					GridTiles[i,j] = Instantiate (Blank, new Vector3((i * 1.1f + .55f * j) - radius*1.65f,0,(j * 1.0f) - radius*1.0f), Quaternion.Euler(0,30,0)) as GameObject;
				}
			}
		}
	}
	public void LoadMap(TileContainer container)
	{
		DestroyMap();
		int ms = container.Tiles.Count;
		int EdgeSize = 0;
		int z = 0;
		if(ms == 361)
		{
			//Small Map: Map edge of 19
			EdgeSize = 19;
			Size = "Small";
			//radius = 9;
		}
		else if(ms == 1521)
		{
			//Medium Map: Map edge of 39
			EdgeSize = 39;
			Size = "Medium";
			//radius = 19;
		}
		else if(ms == 3481)
		{
			//Large Map: Map edge of 59
			EdgeSize = 59;
			//radius = 29;
			Size = "Large";
		}
		else
		{
			//Error!
			return;
		}
		radius = (EdgeSize - 1)/2;
		for(int i = 0; i < EdgeSize; i++)
		{
			for(int j = 0; j < EdgeSize; j++)
			{
				if(z < container.Tiles.Count)
				{
					Initialize (container.Tiles[z], i, j);
				}
				z++;
			}
		}
	}
	
	void Initialize(TileBlock tile, int x, int y)
	{
		string TileName = "";
		string[] Name_Library = this.transform.gameObject.GetComponent<ChangeTile>().Name_Library;
		GameObject[] Tile_Library = this.transform.gameObject.GetComponent<ChangeTile>().Tile_Library;
		
		if(tile.Name == "EmptyTile(Clone)")
		{
			TileName = "Empty";
		}
		else if(tile.Name == "EdgeTile(Clone)")
		{
			TileName = "Edge";
		}
		else if(tile.Name == "BlankTile(Clone)")
		{
			TileName = "Blank";
		}
		else if(tile.Name == "P1")
		{
			TileName = "Player1Start";
		}
		else if(tile.Name == "P2")
		{
			TileName = "Player2Start";
		}
		else if(tile.Name == "T1")
		{
			if(tile.HasFont)
			{
				TileName = "T1_With_Font";
			}
			else if(tile.HasTower)
			{
				TileName = "T1_With_Tower";
			}
			else if(tile.Resource == "Empty")
			{
				TileName = "T1_With_None";
			}
			else if(tile.Resource == "Arcana")
			{
				TileName = "T1_With_R1";
			}
			else if(tile.Resource == "Air")
			{
				TileName = "T1_With_R2";
			}
			else if(tile.Resource == "Earth")
			{
				TileName = "T1_With_R3";
			}
			else if(tile.Resource == "Fire")
			{
				TileName = "T1_With_R4";
			}
			else if(tile.Resource == "Water")
			{
				TileName = "T1_With_R5";
			}
		}
		else if(tile.Name == "T2")
		{
			if(tile.HasFont)
			{
				TileName = "T2_With_Font";
			}
			else if(tile.HasTower)
			{
				TileName = "T2_With_Tower";
			}
			else if(tile.Resource == "Empty")
			{
				TileName = "T2_With_None";
			}
			else if(tile.Resource == "Arcana")
			{
				TileName = "T2_With_R1";
			}
			else if(tile.Resource == "Air")
			{
				TileName = "T2_With_R2";
			}
			else if(tile.Resource == "Earth")
			{
				TileName = "T2_With_R3";
			}
			else if(tile.Resource == "Fire")
			{
				TileName = "T2_With_R4";
			}
			else if(tile.Resource == "Water")
			{
				TileName = "T2_With_R5";
			}
		}
		else if(tile.Name == "T3")
		{
			if(tile.HasFont)
			{
				TileName = "T3_With_Font";
			}
			else if(tile.HasTower)
			{
				TileName = "T3_With_Tower";
			}
			else if(tile.Resource == "Empty")
			{
				TileName = "T3_With_None";
			}
			else if(tile.Resource == "Arcana")
			{
				TileName = "T3_With_R1";
			}
			else if(tile.Resource == "Air")
			{
				TileName = "T3_With_R2";
			}
			else if(tile.Resource == "Earth")
			{
				TileName = "T3_With_R3";
			}
			else if(tile.Resource == "Fire")
			{
				TileName = "T3_With_R4";
			}
			else if(tile.Resource == "Water")
			{
				TileName = "T3_With_R5";
			}
		}
		
		for(int i = 0; i < Name_Library.Length; i++)
		{
			if(x != -1 && TileName == Name_Library[i])
			{
				GridTiles[x,y] = Instantiate (Tile_Library[i], new Vector3((x * 1.1f + .55f * y) - 1.65f*radius,0,(y * 1.0f) - radius*1.0f), Quaternion.Euler(0,30,0)) as GameObject;
			}
		}
	}
	
	public void DestroyMap()
	{
		for(int i = 0; i < 100; i++)
		{
			for(int j = 0; j < 100; j++)
			{
				if(GridTiles[i,j])
				{
					Destroy (GridTiles[i,j]);
				}
			}
		}
	}
}