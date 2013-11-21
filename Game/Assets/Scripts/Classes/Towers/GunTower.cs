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
		damage = 10;
		attackSpeed = 10;
		attackRange = 2;
	}
	
	public override void Update() {
		
		if (isDestroyed) return;
		
		MonsterObject target = FindEnemy();
		
		if (target != null && lastAttackTime + 1/attackSpeed <= Time.time) {
			lastAttackTime = Time.time;
			target.AddDamage(damage);
			target.AddEffect(new Effect (Effect.EffectType.Slow, 1f));
			if (Random.value <0.02f) 
				target.AddEffect(new Effect (Effect.EffectType.Stun, 0.2f));
			
		}
			
	}
	
	public override void OnDeath () {
		
	}
	
}
