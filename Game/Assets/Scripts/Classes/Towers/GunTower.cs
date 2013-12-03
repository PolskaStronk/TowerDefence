using UnityEngine;
using System.Collections;

public class GunTower : TowerObject {
	
	public GunTower () {
		type = TowerObject.TowerType.Gun;
		Start();
	}
	
	public GunTower (GameObject gameObject_) {
		classType = TDObject.TDType.Tower;
		gameObject = gameObject_;
		
		type = TowerObject.TowerType.Gun;
		Start();
	}
	
	private void Start() {
		health = 100;
		damage = 3;
		attackSpeed = 10;
		attackRange = 2;
	}
	
	public override void Update() {
		
		if (isDestroyed) return;
		
		MonsterObject target = FindEnemy();
		
		if (target != null && lastAttackTime + 1/attackSpeed <= Time.time) {
			lastAttackTime = Time.time;
			target.AddDamage(damage);
		}
			
	}
	
	public override void OnDeath () {
		
	}
	
	public override void OnUpgrade () {
		Debug.Log (position + " " + gameObject.name);
		splashRange = (upgradeSplash<0?-1:upgradeSplash)/2f;
		damage = 3 + upgradeDamage;
		attackSpeed = 10 + upgradeAtackSpeed;
		attackRange = 2 + upgradeRange;
		
	}
}
