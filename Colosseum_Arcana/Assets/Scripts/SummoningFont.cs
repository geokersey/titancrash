using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SummoningFont : MonoBehaviour {
	
	public Grid world;
	public bool shown = false;
	
	public Unit defaultUnit0;
	
	public Unit smallAirPrefab0;
	//public Unit mediumAirPrefab0;
	public Unit largeAirPrefab0;
	
	public Unit smallEarthPrefab0;
	//public Unit mediumEarthPrefab0;
	public Unit largeEarthPrefab0;
	
	public Unit smallFirePrefab0;
	//public Unit mediumFirePrefab0;
	public Unit largeFirePrefab0;
	
	public Unit smallWaterPrefab0;
	//public Unit mediumWaterPrefab0;
	public Unit largeWaterPrefab0;
	
	
	
	public Unit defaultUnit1;
	
	public Unit smallAirPrefab1;
	//public Unit mediumAirPrefab1;
	public Unit largeAirPrefab1;
	
	public Unit smallEarthPrefab1;
	//public Unit mediumEarthPrefab1;
	public Unit largeEarthPrefab1;
	
	public Unit smallFirePrefab1;
	//public Unit mediumFirePrefab1;
	public Unit largeFirePrefab1;
	
	public Unit smallWaterPrefab1;
	//public Unit mediumWaterPrefab1;
	public Unit largeWaterPrefab1;
	
	void Start () {
		
	
	}
	public void show(){
		world.GUIstate = 1;
	}
	public void hide(){
		world.GUIstate = 0;
	}
	
	
	void Update () {
	
	}
	
	//public void init (Grid world_){
	//	world = world_;
	//}
	
	void OnGUI()
	
	{
		if(world.GUIstate == 1){
			
			
			if (GUI.Button(new Rect(100, Screen.height - 150, 99, 49), "basic unit", world.GUIfunstuff.button))
			{
				if (world.activePlayer == 0){
					if (world.players[0].resources[0] < 1)
					{
						print("Not enough resources");
						return;
					}
					else if (world.players[world.activePlayer].researched && world.players[world.activePlayer].techAvailable[27] != 2)
					{
						print("Researched this turn, cannot summon");
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (defaultUnit0, new Vector3(world.selected.transform.position.x, world.selected.transform.position.y + .3f, world.selected.transform.position.z), Quaternion.identity);
						world.players[0].resources[0]--;
						world.players[world.activePlayer].summoned = true;
					}
				}
				if (world.activePlayer == 1){
					if (world.players[1].resources[0] < 1)
					{
						print("Not enough resources");
						return;
					}
					else if (world.players[world.activePlayer].researched && world.players[world.activePlayer].techAvailable[27] != 2)
					{
						print("Researched this turn, cannot summon");
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (defaultUnit1, new Vector3(world.selected.transform.position.x, world.selected.transform.position.y + .3f, world.selected.transform.position.z), Quaternion.identity);
						world.players[1].resources[0]--;
						world.players[world.activePlayer].summoned = true;
					}
				}
				if (world.players[world.activePlayer].summoned)
				{
					world.selected.occupyer.init(world.selected.x, world.selected.y, world);
				}
				hide();
				
			}
			
			if (GUI.Button(new Rect(100, Screen.height - 105, 99, 49), "Small Air", world.GUIfunstuff.button))
			{
				if (world.activePlayer == 0){
					if (world.players[0].resources[1] < 1)
					{
						print("Not enough resources");
						return;
					}
					else if (world.players[world.activePlayer].researched && world.players[world.activePlayer].techAvailable[27] != 2)
					{
						print("Researched this turn, cannot summon");
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (smallAirPrefab0, world.selected.transform.position, Quaternion.identity);
						world.players[0].resources[1]--;
						world.players[world.activePlayer].summoned = true;
					}
				}
				if (world.activePlayer == 1){
					if (world.players[1].resources[1] < 1)
					{
						print("Not enough resources");
						return;
					}
					else if (world.players[world.activePlayer].researched && world.players[world.activePlayer].techAvailable[27] != 2)
					{
						print("Researched this turn, cannot summon");
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (smallAirPrefab1, world.selected.transform.position, Quaternion.identity);
						world.players[1].resources[1]--;
						world.players[world.activePlayer].summoned = true;
					}
				}
				if (world.players[world.activePlayer].summoned)
				{
					world.selected.occupyer.init(world.selected.x, world.selected.y, world);
				}
				hide();
			}
			
			if (GUI.Button(new Rect(100, Screen.height - 60, 99, 49), "Large Air", world.GUIfunstuff.button))
			{
				if (world.activePlayer == 0){
					if (world.players[0].resources[1] < 1)
					{
						print("Not enough resources");
						return;
					}
					else if (world.players[world.activePlayer].researched && world.players[world.activePlayer].techAvailable[27] != 2)
					{
						print("Researched this turn, cannot summon");
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (largeAirPrefab0, world.selected.transform.position, Quaternion.identity);
						world.players[0].resources[1]--;
						world.players[world.activePlayer].summoned = true;
					}
				}
				if (world.activePlayer == 1){
					if (world.players[1].resources[1] < 1)
					{
						print("Not enough resources");
						return;
					}
					else if (world.players[world.activePlayer].researched && world.players[world.activePlayer].techAvailable[27] != 2)
					{
						print("Researched this turn, cannot summon");
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (largeAirPrefab1, world.selected.transform.position, Quaternion.identity);
						world.players[1].resources[1]--;
						world.players[world.activePlayer].summoned = true;
					}
				}
				if (world.players[world.activePlayer].summoned)
				{
					world.selected.occupyer.init(world.selected.x, world.selected.y, world);
				}
				hide();
			}
			
			if (GUI.Button(new Rect(195, Screen.height - 105, 99, 49), "Small Earth", world.GUIfunstuff.button))
			{
				if (world.activePlayer == 0){
					if (world.players[0].resources[2] < 1)
					{
						print("Not enough resources");
						return;
					}
					else if (world.players[world.activePlayer].researched && world.players[world.activePlayer].techAvailable[27] != 2)
					{
						print("Researched this turn, cannot summon");
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (smallEarthPrefab0, world.selected.transform.position, Quaternion.identity);
						world.players[0].resources[2]--;
						world.players[world.activePlayer].summoned = true;
					}
				}
				if (world.activePlayer == 1){
					if (world.players[1].resources[2] < 1)
					{
						print("Not enough resources");
						return;
					}
					else if (world.players[world.activePlayer].researched && world.players[world.activePlayer].techAvailable[27] != 2)
					{
						print("Researched this turn, cannot summon");
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (smallEarthPrefab1, world.selected.transform.position, Quaternion.identity);
						world.players[1].resources[2]--;
						world.players[world.activePlayer].summoned = true;
					}
				}
				if (world.players[world.activePlayer].summoned)
				{
					world.selected.occupyer.init(world.selected.x, world.selected.y, world);
				}
				hide();
			}
			
			if (GUI.Button(new Rect(195, Screen.height - 60, 99, 49), "Large Earth", world.GUIfunstuff.button))
			{
				if (world.activePlayer == 0){
					if (world.players[0].resources[2] < 1)
					{
						print("Not enough resources");
						return;
					}
					else if (world.players[world.activePlayer].researched && world.players[world.activePlayer].techAvailable[27] != 2)
					{
						print("Researched this turn, cannot summon");
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (largeEarthPrefab0, world.selected.transform.position, Quaternion.identity);
						world.players[0].resources[2]--;
						world.players[world.activePlayer].summoned = true;
					}
				}
				if (world.activePlayer == 1){
					if (world.players[1].resources[2] < 1)
					{
						print("Not enough resources");
						return;
					}
					else if (world.players[world.activePlayer].researched && world.players[world.activePlayer].techAvailable[27] != 2)
					{
						print("Researched this turn, cannot summon");
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (largeEarthPrefab1, world.selected.transform.position, Quaternion.identity);
						world.players[1].resources[2]--;
						world.players[world.activePlayer].summoned = true;
					}
				}
				if (world.players[world.activePlayer].summoned)
				{
					world.selected.occupyer.init(world.selected.x, world.selected.y, world);
				}
				hide();
			}
			
			if (GUI.Button(new Rect(290, Screen.height - 105, 99, 49), "Small Fire", world.GUIfunstuff.button))
			{
				
				if (world.activePlayer == 0){
					if (world.players[0].resources[3] < 1)
					{
						print("Not enough resources");
						return;
					}
					else if (world.players[world.activePlayer].researched && world.players[world.activePlayer].techAvailable[27] != 2)
					{
						print("Researched this turn, cannot summon");
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (smallFirePrefab0, world.selected.transform.position, Quaternion.identity);
						world.players[0].resources[3]--;
						world.players[world.activePlayer].summoned = true;
					}
				}
				if (world.activePlayer == 1){
					if (world.players[1].resources[3] < 1)
					{
						print("Not enough resources");
						return;
					}
					else if (world.players[world.activePlayer].researched && world.players[world.activePlayer].techAvailable[27] != 2)
					{
						print("Researched this turn, cannot summon");
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (smallFirePrefab1, world.selected.transform.position, Quaternion.identity);
						world.players[1].resources[3]--;
						world.players[world.activePlayer].summoned = true;
					}
				}
				if (world.players[world.activePlayer].summoned)
				{
					world.selected.occupyer.init(world.selected.x, world.selected.y, world);
				}
				hide();
			}
			
			if (GUI.Button(new Rect(290, Screen.height - 60, 99, 49), "Large Fire", world.GUIfunstuff.button))
			{
				if (world.activePlayer == 0){
					if (world.players[0].resources[3] < 1)
					{
						print("Not enough resources");
						return;
					}
					else if (world.players[world.activePlayer].researched && world.players[world.activePlayer].techAvailable[27] != 2)
					{
						print("Researched this turn, cannot summon");
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (largeFirePrefab0, world.selected.transform.position, Quaternion.identity);
						world.players[0].resources[3]--;
						world.players[world.activePlayer].summoned = true;
					}
				}
				if (world.activePlayer == 1){
					if (world.players[1].resources[3] < 1)
					{
						print("Not enough resources");
						return;
					}
					else if (world.players[world.activePlayer].researched && world.players[world.activePlayer].techAvailable[27] != 2)
					{
						print("Researched this turn, cannot summon");
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (largeFirePrefab1, world.selected.transform.position, Quaternion.identity);
						world.players[1].resources[3]--;
						world.players[world.activePlayer].summoned = true;
					}
				}
				if (world.players[world.activePlayer].summoned)
				{
					world.selected.occupyer.init(world.selected.x, world.selected.y, world);
				}
				hide();
			}
			
			if (GUI.Button(new Rect(385, Screen.height - 105, 99, 49), "Small Water", world.GUIfunstuff.button))
			{
				if (world.activePlayer == 0){
					if (world.players[0].resources[4] < 1)
					{
						print("Not enough resources");
						return;
					}
					else if (world.players[world.activePlayer].researched && world.players[world.activePlayer].techAvailable[27] != 2)
					{
						print("Researched this turn, cannot summon");
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (smallWaterPrefab0, world.selected.transform.position, Quaternion.identity);
						world.players[0].resources[4]--;
						world.players[world.activePlayer].summoned = true;
					}
				}
				if (world.activePlayer == 1){
					if (world.players[1].resources[4] < 1)
					{
						print("Not enough resources");
						return;
					}
					else if (world.players[world.activePlayer].researched && world.players[world.activePlayer].techAvailable[27] != 2)
					{
						print("Researched this turn, cannot summon");
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (smallWaterPrefab1, world.selected.transform.position, Quaternion.identity);
						world.players[1].resources[4]--;
						world.players[world.activePlayer].summoned = true;
					}
				}
				if (world.players[world.activePlayer].summoned)
				{
					world.selected.occupyer.init(world.selected.x, world.selected.y, world);
				}
				hide();
			}
			
			if (GUI.Button(new Rect(385, Screen.height - 60, 99, 49), "Large Water", world.GUIfunstuff.button))
			{
				if (world.activePlayer == 0){
					if (world.players[0].resources[4] < 1)
					{
						print("Not enough resources");
						return;
					}
					else if (world.players[world.activePlayer].researched && world.players[world.activePlayer].techAvailable[27] != 2)
					{
						print("Researched this turn, cannot summon");
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (largeWaterPrefab0, world.selected.transform.position, Quaternion.identity);
						world.players[0].resources[4]--;
						world.players[world.activePlayer].summoned = true;
					}
				}
				if (world.activePlayer == 1){
					if (world.players[1].resources[4] < 1)
					{
						print("Not enough resources");
						return;
					}
					else if (world.players[world.activePlayer].researched && world.players[world.activePlayer].techAvailable[27] != 2)
					{
						print("Researched this turn, cannot summon");
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (largeWaterPrefab1, world.selected.transform.position, Quaternion.identity);
						world.players[1].resources[4]--;
						world.players[world.activePlayer].summoned = true;
					}
				}
				if (world.players[world.activePlayer].summoned)
				{
					world.selected.occupyer.init(world.selected.x, world.selected.y, world);
				}
				hide();
			}
		}
		
	}
}
