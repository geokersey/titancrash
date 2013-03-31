using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public Grid world;
	public int[] resources;
	// Use this for initialization
	void Start () {

		resources = new int[5];
		for (int i = 0; i <5; ++i){
			resources[i] = 0;
		}
		resources[0] = 3;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
}
