using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public Grid world;
	public float height = 10;
	// Use this for initialization
	void Start () {
		transform.position = world.map[world.radius, world.radius].transform.position + new Vector3(0, height, 0);
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Input.GetAxis ("Horizontal"),0,Input.GetAxis ("Vertical"));
	
	}
}
