using UnityEngine;
using System.Collections;

public class TowerObject : TDObject {

	public enum TowerType {Gun, Missile, Laser, SlowLaser, MegaLaser};
	public TowerType type;
	
	public GameObject gameObject;
	
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
