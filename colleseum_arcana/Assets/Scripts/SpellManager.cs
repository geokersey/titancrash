using UnityEngine;
using System.Collections;

public class SpellManager : MonoBehaviour 
{
	//temp variables to make script compile. Only used for variables accessable in the project not currently included
	private GameObject[,] GridTiles = new GameObject[99,99];
	public GameObject grid;
	Grid other;
	public GameObject Fireball;
	//other variables
	public bool SpellCasting = false;
	private bool found = false;
	private bool toggleTxt = false;
	
	void Start ()
	{
		other = grid.GetComponent<Grid>();
	}
	
	void Update () 
	{
		int x, y;
		RaycastHit hit;
   		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		if(toggleTxt && Input.GetMouseButtonDown(0) && Physics.Raycast(ray,out hit ,100))
		{ //check if the user is casting a spell, then if the mouse left button is pressed, then if the user is hovering over a tile to be selected
			
			//ApplySpell(hit.transform.position.x,hit.transform.position.y);
			for(int i = 0; i < other.size; i++)
			{
				if(found)
				{
					break;
				}
				for(int j = 0; j < other.size; j++)
				{
					if(found)
					{
						break;
					}
					//Debug.Log (found);
					//Debug.Log (hit.transform.gameObject);
					if(hit.transform == other.map[i,j].transform)
					{
						//found the tile selected
						
						/*if(IsInRange(i,j,k))
						{
							ApplySpell(i,j);
							found = true;
						}*/
						
						//Assuming infinite range
						ApplySpell(other.map[i,j]);
						found = true;
						//Debug.Log ("Fireball!");
					}
				}
			}
		}
		found = false;
		
		
		if(Input.GetKeyDown("b"))
		{
			if(!toggleTxt)
			{
				toggleTxt = true;
			}
			else
			{
				toggleTxt = false;
			}
		}
	}
	
	void OnGUI()
	{
		toggleTxt = GUI.Toggle(new Rect(10, 10, 100, 30), toggleTxt, "Fireball Selected");
	}
	
	void IsCasting(bool state)
	{
		//SpellCasting = state;	
	}
	
	void ApplySpell(Tile target)
	{
		GameObject spell;
		spell = (GameObject)Instantiate(Fireball,new Vector3(target.transform.position.x, 9, target.transform.position.z), Quaternion.identity);
		spell.SendMessage("ApplyEffect", target);
	}
	
	void IsInRange(int x, int y, int d)
	{
		//irrelevant function
	}
}
