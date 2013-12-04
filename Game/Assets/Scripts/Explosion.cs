using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
		if (gameObject.particleSystem.time > 1f ) {
			GameController.explosionsPull.Add(gameObject);	
			particleSystem.Stop();
			particleSystem.time = 0f;
		
		}
	}
}
