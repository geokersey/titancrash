using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public Grid world;
	public float height = 10;
	public bool active = false;
	// Use this for initialization
	void Start () {
		transform.position = world.map[world.radius, world.radius].transform.position + new Vector3(0, height, 0);
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void activate (){
		active = true;
		//camera stuff
	}
}
