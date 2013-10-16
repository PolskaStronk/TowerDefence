using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class MonsterObject : TDObject {

	public enum MonsterType {Soldier, EFV, Tank, Boss, MegaBoss, MegaMegaBoss};
	public MonsterType type;
	public float speed = 1;
	public bool isattackedNow = false;
	
	private List <Effect.EffectType> effects = new List<Effect.EffectType> ();
	
	public GameObject gameObject;
	
	
	public MonsterObject () {
		
	}
	
	public MonsterObject (GameObject gameObject_) {
		classType = TDObject.TDType.Monster;
		gameObject = gameObject_;
	}
	
	public MonsterObject (MonsterType type, GameObject gameObject_) {
		classType = TDObject.TDType.Monster;
		this.type = type;
		gameObject = gameObject_;
	}
	
	public void AddEffect(Effect.EffectType effect) {
		effects.Add(effect);	
	}
	
	public void AddDamage(int recievedDamage) {
		if (health <= 0) 
			return;
		
		health -= recievedDamage;
		Debug.Log(health + " " + Time.time);
		if (health <= 0) 
			OnDeath();
	}
	
	public void MoveToNextCell() {
		
	}
	
	public abstract void Update();
	
	public abstract void OnDeath();
	
}
