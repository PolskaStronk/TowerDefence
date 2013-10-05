using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	List <TowerObject> towers = new List<TowerObject> ();
	List <MonsterObject> monsters = new List<MonsterObject> ();
	
	
	void Start () {
	
	}
	
	void Update () {
	
		foreach (TowerObject tower_ in towers) {
			tower_.Update();	
		}
		
		foreach (MonsterObject monster_ in monsters) {
			monster_.Update();	
		}
		
	}
}
