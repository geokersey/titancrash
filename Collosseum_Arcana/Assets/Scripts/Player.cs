using UnityEngine;
using System.Collections;

public class ResOccupied{
	public int turns;
	public int resource;
	public int quantity;
	
	public ResOccupied(int t, int res, int quant){
		turns = t;
		resource = res;
		quantity = quant;
	}
	public ResOccupied(){
		turns = resource = quantity = 0;
	}
}

public class Player : MonoBehaviour {
	public Grid world;
	public int[] resources;
	public System.Collections.Generic.List<ResOccupied> resO;
	// Use this for initialization
	void Start () {
		//resO = new System.Collections.Generic.List<ResOccupied>();
		resources = new int[5];
		for (int i = 0; i <5; ++i){
			resources[i] = 0;
		}
		resources[0] = 3;
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	public void init(){
		resO = new System.Collections.Generic.List<ResOccupied>();
	}
	public void beginTurn(){
		int i = 1;
		while (resO.Count >0 && i <= resO.Count){
			resO[resO.Count-i].turns--;
			if (resO[resO.Count-i].turns<=0){
				resources[resO[resO.Count-i].resource]+=resO[resO.Count-i].quantity;
				resO.RemoveAt(resO.Count-i);
			}
			else{
				++i;
			}
		}
	}
	
	
}
