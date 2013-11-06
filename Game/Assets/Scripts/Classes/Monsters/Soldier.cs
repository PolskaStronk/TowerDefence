using UnityEngine;
using System.Collections;

public class Soldier : MonsterObject {
	
	public Soldier () {
		type = MonsterObject.MonsterType.Soldier;
		Start();
	}
	
	public Soldier (GameObject gameObject_) {
		classType = TDObject.TDType.Monster;
		gameObject = gameObject_;
		type = MonsterObject.MonsterType.Soldier;
		Start ();
	}
	
	private void Start() {
		health = 10;
	}
	
	public override void Update() {
		if (health <= 0) return;
		
		//GameController.plane[(int)this.position.x, (int)this.position.y].Add(this);
	}
	
	public override void OnDeath () {
		Debug.Log("DEAD");
		GameObject.Destroy(gameObject);
	}
}
