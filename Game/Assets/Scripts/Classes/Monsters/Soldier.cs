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
		speed = 1;
		type = MonsterObject.MonsterType.Soldier;
		Start ();
	}
	
	private void Start() {
		health = 45 + 20* ((int)GameController.difficulty - 1);
		goldForDeath = 15;
		maxHealth = health;
		CreateHealthBar();
		target.x = this.position.x;
		target.y = this.position.y;
	}
	
	public override void Update() {
		if (health <= 0) return;
		
		float startSpeed = speed;
		
		foreach (Effect effect in effects) {
		effect.Update();
		if (effect.timeIsActive > 0)	
			switch (effect.type) {
				
			case Effect.EffectType.Slow: 
				speed = startSpeed/2;
				break;
			case Effect.EffectType.Stun:
				speed = 0;
				break;
				
			}
		}
		if (speed>0)
			MoveToNextCell();
		speed = startSpeed;
		SetHealthBarPosition();
		//Debug.Log ( this.position.x.ToString() + " " + this.position.y.ToString() );
		//GameController.plane[(int)this.position.x, (int)this.position.y].Add(this);
	}
	
	public override void OnDeath () {
		//Debug.Log("DEAD");
		position = new Vector2 (-100,-100);
		GameObject.Destroy(gameObject);
		GameObject.Destroy(healthBarObject);
		path = -1;
		GameController.money +=goldForDeath;
		
	}
}
