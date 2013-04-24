using UnityEngine;
using System.Collections;

public class SetHeight : MonoBehaviour {
	public float Height = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y != Height)
		{
			transform.Translate(0, Height - transform.position.y, 0);
		}
	}
}
