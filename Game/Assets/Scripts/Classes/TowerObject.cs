using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class TowerObject : TDObject {

	public enum TowerType {Gun, Missile, Laser, SlowLaser, MegaLaser, None};
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
	
	protected float lastAttackTime = -100;
	
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
				if ( Mathf.Pow(position.x - monster.position.x,2) + Mathf.Pow(position.y - monster.position.y,2) <= Mathf.Pow(attackRange, 2) ) {
				
				return monster;
			}
			break;
		}
		
		return null;
	}
	
	
	public void AddDamage(int recievedDamage) {
		health -= recievedDamage;
		if (health <= 0) OnDeath();
	}
	
	public void MoveToNextCell() {
		
	}
	
	public abstract void Update();
	
	public abstract void OnDeath();
	
}
