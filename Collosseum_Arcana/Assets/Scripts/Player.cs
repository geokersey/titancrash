using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public Grid world;
	public float height = 10;
	public bool isActive = false;
	public int[] resources;
	// Use this for initialization
	void Start () {
//		transform.position = world.map[world.radius, world.radius].transform.position + new Vector3(0, height, 0);
		resources = new int[5];
		for (int i = 0; i <5; ++i){
			resources[i] = 0;
		}
		resources[0] = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void activate (){
		isActive = true;
		//camera stuff
	}
}
