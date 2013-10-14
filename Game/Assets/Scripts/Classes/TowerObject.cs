using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerObject : TDObject {

	public enum TowerType {Gun, Missile, Laser, SlowLaser, MegaLaser};
	public TowerType type;
	public int attackRange;
	
	public GameObject gameObject;
	
	public List <int> groups = new List <int> ();
	
	#region AttackType
	public enum AttackType {Nearest, LowestHP, HighestHP, LowestMaxHP, 
		HighestMaxHP, WhoIsNotAttacked, WhoIsNotAttackedBuyThisTypeOfTower, ToSplashOnly}
	public AttackType attackType = AttackType.Nearest;
	public bool isCatchTarget = false;
	
	
	#endregion
	
	public TowerObject () {
		
	}
	
	public TowerObject (GameObject gameObject_) {
		classType = TDObject.TDType.Tower;
		gameObject = gameObject_;
	}
	
	public TowerObject (TowerType type, GameObject gameObject_) {
		classType = TDObject.TDType.Tower;
		this.type = type;
		gameObject = gameObject_;
	}
	
	public MonsterObject FindEnemy() {
	
		if (GameController.monsters.Count == 0) 
			return null;
		
		switch (attackType) {
			case AttackType.Nearest: 
			foreach (MonsterObject monster in GameController.monsters) 
				if ( Mathf.Sqrt( Mathf.Pow(position.x - monster.position.x,2) + Mathf.Pow(position.y - monster.position.y,2) ) <= attackRange ) {
				
				return monster;
			}
			break;
		}
		
		return null;
	}
	
	
	public void AddDamage(int recievedDamage) {
		health -= recievedDamage;
	}
	
	public void MoveToNextCell() {
		
	}
	
	public void Update() {
		
	}
	
	public void OnDeath() {
		
	}
	
}
