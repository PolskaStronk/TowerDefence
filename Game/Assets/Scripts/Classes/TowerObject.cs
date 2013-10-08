using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerObject : TDObject {

	public enum TowerType {Gun, Missile, Laser, SlowLaser, MegaLaser};
	public TowerType type;
	
	public GameObject gameObject;
	
	public List <int> groups = new List <int> ();
	
	#region AttackType
	public enum AttackType {Nearest, LowestHP, HighestHP, LowestMaxHP, 
		HighestMaxHP, WhoIsNotAttacked, WhoIsNotAttackedBuyThisTypeOfTower, ToSplashOnly}
	public bool isCatchTarget = false;
	
	
	#endregion
	
	public TowerObject (GameObject gameObject_) {
		classType = TDObject.TDType.Tower;
		gameObject = gameObject_;
	}
	
	public TowerObject (TowerType type, GameObject gameObject_) {
		classType = TDObject.TDType.Tower;
		this.type = type;
		gameObject = gameObject_;
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
