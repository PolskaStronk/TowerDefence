using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class MonsterObject : TDObject {

	public enum MonsterType {Soldier, EFV, Tank, Boss, MegaBoss, MegaMegaBoss};
	public MonsterType type;
	public float speed = 10;
	public bool isattackedNow = false;
	public int path;
	public float size = 1;
	protected int goldForDeath;
	protected float maxHealth;
	
	
	protected List <Effect> effects = new List<Effect> ();
	
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
	
	public void AddEffect(Effect effect) {
		
		foreach (Effect effect_ in effects) {
			if (effect_.type == effect.type) {
				effect_.timeIsActive = effect.timeIsActive;
				return;
			}
		}
		
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
	public Vector2 target;
	public void MoveToNextCell() {
		
		float distance = speed * Time.deltaTime;
		float dist, len;
		
		while( distance >= 0.0000001f ) {
			if( Mathf.Abs( this.position.x - target.x ) + Mathf.Abs( this.position.y - target.y ) <= 0.000001f ) {
				this.position.x = target.x;
				this.position.y = target.y;
				
				/*---don't delete
				if( Random.value < 0.05 ) {
					List< Vector2 > possible_way = new List<Vector2>();
					if( (int)this.position.x - 1 >= 0 ) {
						if( GameController.path[ (int)this.position.x - 1, (int)position.y, 0 ] != GameController.Direction.None ) possible_way.Add( new Vector2( (int)this.position.x - 1, (int)position.y ) );
					}
					if( (int)this.position.y - 1 >= 0 ) {
						if( GameController.path[ (int)this.position.x, (int)position.y - 1, 0 ] != GameController.Direction.None ) possible_way.Add( new Vector2( (int)this.position.x, (int)position.y - 1 ) );
					}
					if( (int)this.position.x + 1 < GameController.width ) {
						if( GameController.path[ (int)this.position.x + 1, (int)position.y, 0 ] != GameController.Direction.None ) possible_way.Add( new Vector2( (int)this.position.x + 1, (int)position.y ) );
					}
					if( (int)this.position.y + 1 < GameController.height ) {
						if( GameController.path[ (int)this.position.x, (int)position.y + 1, 0 ] != GameController.Direction.None ) possible_way.Add( new Vector2( (int)this.position.x, (int)position.y + 1 ) );
					}
					target = possible_way[ (int)(Random.value * possible_way.Count) ];
				} else { 
				*/
				if (GameController.map[(int)this.position.x, (int)position.y] == -1) {
					GameController.health -- ;
					health = 0;
					OnDeath();
					return;
				}
					switch( GameController.path[ (int)this.position.x, (int)position.y, 0 ] ) {
						case( GameController.Direction.Left ): target.y--;
						break;
				
						case( GameController.Direction.Right ): target.y++;
						break;
				
						case( GameController.Direction.Up ): target.x--;
						break;
				
						case( GameController.Direction.Down ): target.x++;
						break;
					}
				//}
			}
			len = Mathf.Abs( this.position.x - target.x ) + Mathf.Abs( this.position.y - target.y );
			dist = Mathf.Min( distance, len );
			distance -= dist;
			this.position.x += ( target.x - this.position.x ) * dist / len;
			this.position.y += ( target.y - this.position.y ) * dist / len;
			gameObject.transform.position = new Vector3( this.position.x , this.position.y , -2 );
		}
	}
	
	public abstract void Update();
	
	public abstract void OnDeath();
	
}
