using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class MapSaveLoad : MonoBehaviour 
{
	TileContainer Grid;// = new TileContainer();
	TileContainer TestRead;
	public GameObject[,] GridTiles;
	
	void Start () 
	{
		GridTiles = this.transform.gameObject.GetComponent<GenerateMap>().GridTiles;
	}
	
	void Update () 
	{
		//Debugging use
		/*
		if(Input.GetKeyDown("g"))
		{
			Save ("TestMap");
		}
		if(Input.GetKeyDown("h"))
		{
			Load ("TestMap");
		}*/
	}
	
	public void Save(string name)
	{
		Grid = new TileContainer();
		for(int i = 0; i < 100; i++)
		{
			for(int j = 0; j < 100; j++)
			{
				if(GridTiles[i,j])
				{
					TileBlock temp;
					temp.Name = GridTiles[i,j].name;
					if(temp.Name == "T1_With_Font(Clone)" || temp.Name == "T2_With_Font(Clone)" || temp.Name == "T3_With_Font(Clone)")
					{
						temp.HasFont = true;
					}
					else
					{
						temp.HasFont = false;	
					}
					
					if(temp.Name == "T1_With_Tower(Clone)" || temp.Name == "T2_With_Tower(Clone)" || temp.Name == "T3_With_Tower(Clone)")
					{
						temp.HasTower = true;
					}
					else
					{
						temp.HasTower = false;	
					}
					if(temp.Name == "T1_With_R1(Clone)" || temp.Name == "T2_With_R1(Clone)" || temp.Name == "T3_With_R1(Clone)")
					{
						temp.Resource = "Arcana";
					}
					else if(temp.Name == "T1_With_R2(Clone)" || temp.Name == "T2_With_R2(Clone)" || temp.Name == "T3_With_R2(Clone)")
					{
						temp.Resource = "Air";
					}
					else if(temp.Name == "T1_With_R3(Clone)" || temp.Name == "T2_With_R3(Clone)" || temp.Name == "T3_With_R3(Clone)")
					{
						temp.Resource = "Earth";
					}
					else if(temp.Name == "T1_With_R4(Clone)" || temp.Name == "T2_With_R4(Clone)" || temp.Name == "T3_With_R5(Clone)")
					{
						temp.Resource = "Fire";
					}
					else if(temp.Name == "T1_With_R5(Clone)" || temp.Name == "T2_With_R5(Clone)" || temp.Name == "T3_With_R5(Clone)")
					{
						temp.Resource = "Water";
					}
					else
					{
						temp.Resource = "Empty";	
					}
					if(temp.Name == "T1_With_Nothing(Clone)" || temp.Name == "T1_With_Font(Clone)" || temp.Name == "T1_With_Tower(Clone)" ||
					   temp.Name == "T1_With_R1(Clone)" || temp.Name == "T1_With_R2(Clone)" || temp.Name == "T1_With_R3(Clone)" ||
					   temp.Name == "T1_With_R4(Clone)" || temp.Name == "T1_With_R5(Clone)")
					{
						temp.Name = "T1";
					}
					else if(temp.Name == "T2_With_Nothing(Clone)" || temp.Name == "T2_With_Font(Clone)" || temp.Name == "T2_With_Tower(Clone)" ||
					   temp.Name == "T2_With_R1(Clone)" || temp.Name == "T2_With_R2(Clone)" || temp.Name == "T2_With_R3(Clone)" ||
					   temp.Name == "T2_With_R4(Clone)" || temp.Name == "T2_With_R5(Clone)")
					{
						temp.Name = "T2";
					}
					else if(temp.Name == "T3_With_Nothing(Clone)" || temp.Name == "T3_With_Font(Clone)" || temp.Name == "T3_With_Tower(Clone)" ||
					   temp.Name == "T3_With_R1(Clone)" || temp.Name == "T3_With_R2(Clone)" || temp.Name == "T3_With_R3(Clone)" ||
					   temp.Name == "T3_With_R4(Clone)" || temp.Name == "T3_With_R5(Clone)")
					{
						temp.Name = "T3";
					}
					else if(temp.Name == "Player1Start(Clone)")
					{
						temp.Name = "P1";
					}
					else if(temp.Name == "Player2Start(Clone)")
					{
						temp.Name = "P2";
					}
					Grid.Tiles.Add(temp);
					//Debug.Log(GridTiles[i,j].name);
				}
			}
		}
		if(!Directory.Exists("C:/ElementalFury/Maps"))
		{
			Debug.Log ("Created a directory!");
			Directory.CreateDirectory("C:/ElementalFury/Maps");
		}
		Grid.Save("C:/ElementalFury/Maps/" + name + ".xml");
		//Debug.Log ("C:/ProgramFiles/ElementalFury/Maps/" + name + ".xml");
	}
	
	public void Load(string name)
	{
		TestRead = TileContainer.Load("C:/ElementalFury/Maps/" + name + ".xml");
		this.transform.gameObject.GetComponent<GenerateMap>().LoadMap(TestRead);
		//Debug.Log ("Loaded " + Application.persistentDataPath);
		//Debug.Log (TestRead.Tiles.Count);
	}
}
/*
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
}*/