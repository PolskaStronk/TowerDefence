using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public static List <TowerObject> towers = new List<TowerObject> ();
	public static List <MonsterObject> monsters = new List<MonsterObject> ();
	
	public static int height = 40, width = 40;
	public static List <MonsterObject>[,] plane;
	
	void Start () {
	
		GunTower tower_ = new GunTower ();
		towers.Add(tower_);
		tower_.position = new Vector2 (0,0);
		
		Soldier soldier = new Soldier ();
		monsters.Add(soldier);
		soldier.position = new Vector2 (0,0);
		
	}
	
	void Update () {
	
		plane = new List <MonsterObject>[height, width];
		
		
		foreach (TowerObject tower_ in towers) {
			tower_.Update();	
		}
		
		foreach (MonsterObject monster_ in monsters) {
			monster_.Update();	
		}
		
	}
}
