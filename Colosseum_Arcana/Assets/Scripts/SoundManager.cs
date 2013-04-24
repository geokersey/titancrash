using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	//public AudioClip thing;
	public AudioClip[] Effects = new AudioClip[1];
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("h"))
		{
			Play (2);	
		}
	}
	
	public void Play(int num)
	{
		 AudioSource.PlayClipAtPoint(Effects[num], transform.position);
	}
}
