using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour 
{
	void ApplyEffect(Tile Target)
	{
		Destroy (gameObject,3.0f);
	}
}
