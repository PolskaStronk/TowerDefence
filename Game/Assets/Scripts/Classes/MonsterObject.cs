using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class MonsterObject : TDObject {

	public enum MonsterType {Soldier, EFV, Tank, Boss, MegaBoss, MegaMegaBoss};
	public MonsterType type;
	public float speed = 1;
	public bool isattackedNow = false;
	
	private List <Effect.EffectType> effects = new List<Effect.EffectType> ();
	
	private enum Direction {Up, Down, Left, Right};
	private List <Direction> path = new List<Direction> ();
	private Vector2 targetPosition;
	private int currentPathCell = 0;
	
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
		Vector2 toAdd;
		
		switch (path[currentPathCell]) {
		case Direction.Up:
			toAdd = new Vector2 (0,1);
			break;
		case Direction.Down:
			toAdd = new Vector2 (0,-1);
			break;
		case Direction.Left:
			toAdd = new Vector2 (-1,0);
			break;
		case Direction.Right:
			toAdd = new Vector2 (1,1);
			break;
		}
		
		targetPosition = position + toAdd;
		currentPathCell++;
	}
	
	public abstract void Update();
	
	public abstract void OnDeath();
	
}
