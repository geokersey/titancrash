using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SummoningFont : MonoBehaviour {
	
	public Grid world;
	public bool shown = false;
	
	void Start () {
		
	
	}
	public void show(){
		shown = true;
	}
	public void hide(){
		shown = false;
	}
	
	
	void Update () {
	
	}
	
	//public void init (Grid world_){
	//	world = world_;
	//}
	
	void OnGUI()
	
	{
		if(shown){
			
			
			if (GUI.Button(new Rect(0, 0, 99, 49), "Tiny Air"))
			{
				if (world.activePlayer == 0){
					if (world.players[0].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.tinyAirPrefab0, world.selected.transform.position, Quaternion.identity);
						world.players[0].res--;
					}
				}
				if (world.activePlayer == 1){
					if (world.players[1].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.tinyAirPrefab1, world.selected.transform.position, Quaternion.identity);
						world.players[1].res--;
					}
				}
				world.selected.occupyer.init(world.selected.x, world.selected.y, world);
				hide();
				
			}
			
			if (GUI.Button(new Rect(0, 50, 99, 49), "Small Air"))
			{
				if (world.activePlayer == 0){
					if (world.players[0].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.smallAirPrefab0, world.selected.transform.position, Quaternion.identity);
						world.players[0].res--;
					}
				}
				if (world.activePlayer == 1){
					if (world.players[1].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.smallAirPrefab1, world.selected.transform.position, Quaternion.identity);
						world.players[1].res--;
					}
				}
				world.selected.occupyer.init(world.selected.x, world.selected.y, world);
				hide();
			}
			
			if (GUI.Button(new Rect(0, 100, 99, 49), "Medium Air"))
			{
				if (world.activePlayer == 0){
					if (world.players[0].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.mediumAirPrefab0, world.selected.transform.position, Quaternion.identity);
						world.players[0].res--;
					}
				}
				if (world.activePlayer == 1){
					if (world.players[1].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.mediumAirPrefab1, world.selected.transform.position, Quaternion.identity);
						world.players[1].res--;
					}
				}
				world.selected.occupyer.init(world.selected.x, world.selected.y, world);
				hide();
			}
			
			if (GUI.Button(new Rect(0, 150, 99, 49), "Large Air"))
			{
				if (world.activePlayer == 0){
					if (world.players[0].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.largeAirPrefab0, world.selected.transform.position, Quaternion.identity);
						world.players[0].res--;
					}
				}
				if (world.activePlayer == 1){
					if (world.players[1].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.largeAirPrefab1, world.selected.transform.position, Quaternion.identity);
						world.players[1].res--;
					}
				}
				world.selected.occupyer.init(world.selected.x, world.selected.y, world);
				hide();
			}
			
			if (GUI.Button(new Rect(100, 0, 99, 49), "Tiny Earth"))
			{
				if (world.activePlayer == 0){
					if (world.players[0].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.tinyEarthPrefab0, world.selected.transform.position, Quaternion.identity);
						world.players[0].res--;
					}
				}
				if (world.activePlayer == 1){
					if (world.players[1].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.tinyEarthPrefab1, world.selected.transform.position, Quaternion.identity);
						world.players[1].res--;
					}
				}
				world.selected.occupyer.init(world.selected.x, world.selected.y, world);
				hide();
			}
			
			if (GUI.Button(new Rect(100, 50, 99, 49), "Small Earth"))
			{
				if (world.activePlayer == 0){
					if (world.players[0].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.smallEarthPrefab0, world.selected.transform.position, Quaternion.identity);
						world.players[0].res--;
					}
				}
				if (world.activePlayer == 1){
					if (world.players[1].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.smallEarthPrefab1, world.selected.transform.position, Quaternion.identity);
						world.players[1].res--;
					}
				}
				world.selected.occupyer.init(world.selected.x, world.selected.y, world);
				hide();
			}
			
			if (GUI.Button(new Rect(100, 100, 99, 49), "Medium Earth"))
			{
				if (world.activePlayer == 0){
					if (world.players[0].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.mediumEarthPrefab0, world.selected.transform.position, Quaternion.identity);
						world.players[0].res--;
					}
				}
				if (world.activePlayer == 1){
					if (world.players[1].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.mediumEarthPrefab1, world.selected.transform.position, Quaternion.identity);
						world.players[1].res--;
					}
				}
				world.selected.occupyer.init(world.selected.x, world.selected.y, world);
				hide();
			}
			
			if (GUI.Button(new Rect(100, 150, 99, 49), "Large Earth"))
			{
				if (world.activePlayer == 0){
					if (world.players[0].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.largeEarthPrefab0, world.selected.transform.position, Quaternion.identity);
						world.players[0].res--;
					}
				}
				if (world.activePlayer == 1){
					if (world.players[1].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.largeEarthPrefab1, world.selected.transform.position, Quaternion.identity);
						world.players[1].res--;
					}
				}
				world.selected.occupyer.init(world.selected.x, world.selected.y, world);
				hide();
			}
			
			if (GUI.Button(new Rect(200, 0, 99, 49), "Tiny Fire"))
			{
				
				if (world.activePlayer == 0){
					if (world.players[0].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.tinyFirePrefab0, world.selected.transform.position, Quaternion.identity);
						world.players[0].res--;
					}
				}
				if (world.activePlayer == 1){
					if (world.players[1].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.tinyFirePrefab1, world.selected.transform.position, Quaternion.identity);
						world.players[1].res--;
					}
				}
				world.selected.occupyer.init(world.selected.x, world.selected.y, world);
				hide();
			}
			
			if (GUI.Button(new Rect(200, 50, 99, 49), "Small Fire"))
			{
				
				if (world.activePlayer == 0){
					if (world.players[0].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.smallFirePrefab0, world.selected.transform.position, Quaternion.identity);
						world.players[0].res--;
					}
				}
				if (world.activePlayer == 1){
					if (world.players[1].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.smallFirePrefab1, world.selected.transform.position, Quaternion.identity);
						world.players[1].res--;
					}
				}
				world.selected.occupyer.init(world.selected.x, world.selected.y, world);
				hide();
			}
			
			if (GUI.Button(new Rect(200, 100, 99, 49), "Medium Fire"))
			{
				if (world.activePlayer == 0){
					if (world.players[0].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.mediumFirePrefab0, world.selected.transform.position, Quaternion.identity);
						world.players[0].res--;
					}
				}
				if (world.activePlayer == 1){
					if (world.players[1].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.mediumFirePrefab1, world.selected.transform.position, Quaternion.identity);
						world.players[1].res--;
					}
				}
				world.selected.occupyer.init(world.selected.x, world.selected.y, world);
				hide();
			}
			
			if (GUI.Button(new Rect(200, 150, 99, 49), "Large Fire"))
			{
				if (world.activePlayer == 0){
					if (world.players[0].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.largeFirePrefab0, world.selected.transform.position, Quaternion.identity);
						world.players[0].res--;
					}
				}
				if (world.activePlayer == 1){
					if (world.players[1].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.largeFirePrefab1, world.selected.transform.position, Quaternion.identity);
						world.players[1].res--;
					}
				}
				world.selected.occupyer.init(world.selected.x, world.selected.y, world);
				hide();
			}
			
			if (GUI.Button(new Rect(300, 0, 99, 49), "Tiny Water"))
			{
				if (world.activePlayer == 0){
					if (world.players[0].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.tinyWaterPrefab0, world.selected.transform.position, Quaternion.identity);
						world.players[0].res--;
					}
				}
				if (world.activePlayer == 1){
					if (world.players[1].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.tinyWaterPrefab1, world.selected.transform.position, Quaternion.identity);
						world.players[1].res--;
					}
				}
				world.selected.occupyer.init(world.selected.x, world.selected.y, world);
				hide();
			}
			
			if (GUI.Button(new Rect(300, 50, 99, 49), "Small Water"))
			{
				if (world.activePlayer == 0){
					if (world.players[0].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.smallWaterPrefab0, world.selected.transform.position, Quaternion.identity);
						world.players[0].res--;
					}
				}
				if (world.activePlayer == 1){
					if (world.players[1].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.smallWaterPrefab1, world.selected.transform.position, Quaternion.identity);
						world.players[1].res--;
					}
				}
				world.selected.occupyer.init(world.selected.x, world.selected.y, world);
				hide();
			}
			
			if (GUI.Button(new Rect(300, 100, 99, 49), "Medium Water"))
			{
				if (world.activePlayer == 0){
					if (world.players[0].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.mediumWaterPrefab0, world.selected.transform.position, Quaternion.identity);
						world.players[0].res--;
					}
				}
				if (world.activePlayer == 1){
					if (world.players[1].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.mediumWaterPrefab1, world.selected.transform.position, Quaternion.identity);
						world.players[1].res--;
					}
				}
				world.selected.occupyer.init(world.selected.x, world.selected.y, world);
				hide();
			}
			
			if (GUI.Button(new Rect(300, 150, 99, 49), "Large Water"))
			{
				if (world.activePlayer == 0){
					if (world.players[0].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.largeWaterPrefab0, world.selected.transform.position, Quaternion.identity);
						world.players[0].res--;
					}
				}
				if (world.activePlayer == 1){
					if (world.players[1].res < 1)
					{
						print("Not enough resources");
						return;
					}
					else
					{
						world.selected.occupyer = (Unit)Instantiate (world.largeWaterPrefab1, world.selected.transform.position, Quaternion.identity);
						world.players[1].res--;
					}
				}
				world.selected.occupyer.init(world.selected.x, world.selected.y, world);
				hide();
			}
		}
		
	}
}
