using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlowLaserTower : TowerObject {
	LineRenderer lineRenderer;
	
	public SlowLaserTower () {
		type = TowerObject.TowerType.Missile;
		Start();
	}
	
	public SlowLaserTower (GameObject gameObject_) {
		classType = TDObject.TDType.Tower;
		gameObject = gameObject_;
		
		/**/
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.SetWidth( 0.1f, 0.1f );
		/**/
		
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
		
		if (lastAttackTime + 1/attackSpeed > Time.time) 
			return;
		
		lastAttackTime = Time.time;
		List < MonsterObject > targets = FindEnemiesToSplash ( position , attackRange) ;
		
		//----!!!!!!!!!!!!!!!!!---
		if( targets.Count != 0 ) {
			lineRenderer.SetVertexCount((targets.Count << 1) + 1);
			int q = 1;
			lineRenderer.SetPosition( 0, new Vector3( this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z - 1 ) );
			foreach (MonsterObject target in targets) {	
				target.AddEffect(new Effect (Effect.EffectType.Slow, 1f));
				lineRenderer.SetPosition( q, new Vector3( target.gameObject.transform.position.x, target.gameObject.transform.position.y, target.gameObject.transform.position.z - 1 ) );
				lineRenderer.SetPosition( q + 1, new Vector3( this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z - 1 ) );
				q+=2;
			}
		} else {
			lineRenderer.SetVertexCount( 0 );
		}
		//-----------------------
	}
	
	public override void OnDeath () {
		
	}
	
}
