using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissileTower : TowerObject {
	
	
	private float splashRange;
	
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
		splashRange = 3;
	}
	
	public override void Update() {
		
		if (isDestroyed) return;
		
		MonsterObject target_ = FindEnemy();
		if (target_ == null)
			return;
		if (lastAttackTime + 1/attackSpeed > Time.time) 
			return;
		
		lastAttackTime = Time.time;
		List < MonsterObject > targets = FindEnemiesToSplash ( target_.position , splashRange) ;
		
				
		
		foreach (MonsterObject target in targets) {	
			target.AddDamage(damage);
			target.AddEffect(new Effect (Effect.EffectType.Stun, 0.1f));
		}
			
	}
	
	public override void OnDeath () {
		
	}
	
}
