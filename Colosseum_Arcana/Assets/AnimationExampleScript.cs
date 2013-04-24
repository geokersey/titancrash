using UnityEngine;
using System.Collections;

public class AnimationExampleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A))
		{
			animation.Play("jump");	
			animation.CrossFadeQueued("fly",.1F);
		}
		
		if (Input.GetKeyDown (KeyCode.S))
		{
			animation.CrossFade ("attack",.25F);
			
		}
	}
}
