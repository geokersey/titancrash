using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour 
{
	void ApplyEffect(Tile Target)
	{
		if (Target.occupyer != null){
			Target.occupyer.hp -= 1;
		}
		Destroy (gameObject,3.0f);
	}
}
