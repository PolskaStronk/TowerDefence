using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlowLaserTower : TowerObject {
	
	
	public SlowLaserTower () {
		type = TowerObject.TowerType.Missile;
		Start();
	}
	
	public SlowLaserTower (GameObject gameObject_) {
		classType = TDObject.TDType.Tower;
		gameObject = gameObject_;
		type = TowerObject.TowerType.Missile;
		Start();
	}
	
	private void Start() {
		health = 200;
		damage = 0;
		attackSpeed = 200f;
		attackRange = 3;
	}
	
	public override void Update() {
		
		if (isDestroyed) return;
		
		MonsterObject target_ = FindEnemy();
		if (target_ == null)
			return;
		if (lastAttackTime + 1/attackSpeed > Time.time) 
			return;
		
		lastAttackTime = Time.time;
		List < MonsterObject > targets = FindEnemiesToSplash ( position , attackRange) ;
		
				
		
		foreach (MonsterObject target in targets) {	
			target.AddEffect(new Effect (Effect.EffectType.Slow, 1f));
		}
			
	}
	
	public override void OnDeath () {
		
	}
	
}
