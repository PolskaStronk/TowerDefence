using UnityEngine;
using System.Collections;

public class GunTower : TowerObject {
	LineRenderer lr;
	
	public GunTower () {
		type = TowerObject.TowerType.Gun;
		Start();
	}
	
	public GunTower (GameObject gameObject_) {
		classType = TDObject.TDType.Tower;
		gameObject = gameObject_;
		
		/*---*/
		lr = gameObject.AddComponent<LineRenderer>();
		/*---*/
		
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
			lr.SetVertexCount(2);
			lr.SetWidth( 0.1f, 0.1f );
			//lr.SetPosition( 0, new Vector3( 0, 0, 0 ) );
			//lr.SetPosition( 1, new Vector3( 100, 10, 10 ) );
			lr.SetPosition( 0, new Vector3( this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z - 1 ) );
			lr.SetPosition( 1, new Vector3( target.gameObject.transform.position.x, target.gameObject.transform.position.y, target.gameObject.transform.position.z - 1 ) );
		} else {
			lr.SetVertexCount( 0 );
		}
			
	}
	
	public override void OnDeath () {
		
	}
	
}
