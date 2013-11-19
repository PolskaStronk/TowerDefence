using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class MonsterObject : TDObject {

	public enum MonsterType {Soldier, EFV, Tank, Boss, MegaBoss, MegaMegaBoss};
	public MonsterType type;
	public float speed = 1;
	public bool isattackedNow = false;
	public int path;
	public float size = 1;
	protected float maxHealth;
	
	private List <Effect.EffectType> effects = new List<Effect.EffectType> ();
	
	private Vector2 targetPosition;
	private Vector2 direction;
	
	private int currentPathCell = 0;
	
	public GameObject gameObject;
	public GameObject healthBarObject;
	
	protected void UpdateHealthBar(float healthPercent) {
		
		healthBarObject.transform.localScale = new Vector3 (healthPercent,0.2f,1);
		healthBarObject.renderer.material.color = Color.Lerp(Color.green, 3*Color.red,1 - healthPercent);
	}
	
	protected void SetHealthBarPosition() {
		healthBarObject.transform.position = gameObject.transform.position - new Vector3 ( size/2f + 0.3f,0, 3);
	}
	
	protected void CreateHealthBar() {
	
		healthBarObject = GameObject.Instantiate (Resources.Load("Prefabs/HealthBar")) as GameObject;
		SetHealthBarPosition();
		UpdateHealthBar(1);
	}
	
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
		
		UpdateHealthBar(((float)health)/maxHealth);
		
		if (health <= 0) 
			OnDeath();
	}
	private float last = Time.time;
	public void MoveToNextCell() {
		
		if( Time.time - last >= 0.5 ) {
			last = Time.time;
			switch( GameController.path[ (int)this.position.x, (int)position.y, 0 ] ) {
				case( GameController.Direction.Left ): this.position.y--;
				break;
				
				case( GameController.Direction.Right ): this.position.y++;
				break;
				
				case( GameController.Direction.Up ): this.position.x--;
				break;
				
				case( GameController.Direction.Down ): this.position.x++;
				break;
			}
			//Debug.Log( this.position.x.ToString() + " " + this.position.y.ToString() );
			gameObject.transform.position = new Vector3( this.position.x /*- GameController.width/2*/, this.position.y /*- GameController.height/2*/, -2 );
		}
	}
	
	public abstract void Update();
	
	public abstract void OnDeath();
	
}
