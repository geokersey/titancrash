using UnityEngine;
using System.Collections;

public class SpellTargeting : MonoBehaviour 
{
	//temp variables to make script compile. Only used for variables accessable in the project not currently included
	private GameObject[,] GridTiles = new GameObject[99,99];
	//other variables
	public bool SpellCasting = false;
	private bool found = false;
	
	void Update () 
	{
		//int x, y;
		RaycastHit hit;
   		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		if(SpellCasting && Input.GetMouseButtonDown(0) && Physics.Raycast(ray,out hit ,100))
		{ //check if the user is casting a spell, then if the mouse left button is pressed, then if the user is hovering over a tile to be selected
			for(int i = 0; i < 99; i++)
			{
				if(found)
				{
					break;
				}
				for(int j = 0; j < 99; j++)
				{
					if(found)
					{
						break;
					}
					if(hit.transform.gameObject == GridTiles[i,j])
					{
						//found the tile selected
						
						/*if(IsInRange(i,j,k))
						{
							ApplySpell(i,j);
							found = true;
						}*/
						
						//Assuming infinite range
						ApplySpell(i,j);
						found = true;
					}
				}
			}
		}
		found = false;
	}
	
	void IsCasting()
	{
		SpellCasting = true;	
	}
	
	void ApplySpell(int x, int y)
	{
		//Instantiate spell at tile coordinates (x,y)
	}
	
	void IsInRange(int x, int y, int d)
	{
		//irrelevant function
	}
}
