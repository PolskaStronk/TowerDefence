using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class MonsterObject : TDObject {

	public enum MonsterType {Soldier, EFV, Tank, Boss, MegaBoss, MegaMegaBoss};
	public MonsterType type;
	public float speed = 1;
	public bool isattackedNow = false;
	public int path;
	
	private List <Effect.EffectType> effects = new List<Effect.EffectType> ();
	
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
//		Debug.Log(health + " " + Time.time);
		if (health <= 0) 
			OnDeath();
	}
	private float last = 0.5f;
	public void MoveToNextCell() {
		
		if( Time.time - last >= 0.5 ) {
			last = Time.time;
			switch( GameController.path[ (int)this.position.x, (int)position.y, 0 ] ) {
				case( GameController.Direction.Left ): this.position.x--;
				break;
				
				case( GameController.Direction.Right ): this.position.x++;
				break;
				
				case( GameController.Direction.Up ): this.position.y--;
				break;
				
				case( GameController.Direction.Down ): this.position.y++;
				break;
			}
			gameObject.transform.position = new Vector3( this.position.x - GameController.width/2, this.position.y - GameController.height/2, -2 );
		}
	}
	
	public abstract void Update();
	
	public abstract void OnDeath();
	
}
