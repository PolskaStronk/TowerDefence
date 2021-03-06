using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissileTower : TowerObject {
	
	
	
	public MissileTower () {
		type = TowerObject.TowerType.Missile;
		Start();
	}
	
	public MissileTower (GameObject gameObject_) {
		classType = TDObject.TDType.Tower;
		gameObject = gameObject_;
		type = TowerObject.TowerType.Missile;
		Start();
	}
	
	private void Start() {
		health = 200;
		damage = 10;
		attackSpeed = 0.5f;
		attackRange = 5;
		splashRange = 1.5f;
	}
	
	public void BlowUp (Vector2 position) {
		
		
		List < MonsterObject > targets = FindEnemiesToSplash ( position , splashRange) ;
		GameController.CreateExplosion(splashRange, position);
		
		bool isStun = Random.value < 0.1f;
		foreach (MonsterObject target in targets) {	
			target.AddDamage(damage);
			if (isStun)  target.AddEffect(new Effect (Effect.EffectType.Stun, 1f));
		}
		
	}
	public override void Update() {
		
		if (isDestroyed) return;
		
		MonsterObject target_ = FindEnemy();
		if (target_ == null)
			return;
		if (lastAttackTime + 1/attackSpeed > Time.time) 
			return;
		
		lastAttackTime = Time.time;
		
		Missile missile = Missile.FindMissile();
		if (missile == null) {
			missile = new Missile (this,target_);
			GameController.missiles.Add(missile);
		} else {
			missile.Activate(this,target_);
		}
				
			
	}
	
	public override void OnDeath () {
		
	}
	
	
	public override void OnUpgrade () {
		splashRange =  3 +(upgradeSplash)/2f;
		damage = 10 + upgradeDamage*3;
		attackSpeed = 0.5f + upgradeAtackSpeed/2f;
		attackRange = 5 + upgradeRange;
		
	}
	
}
