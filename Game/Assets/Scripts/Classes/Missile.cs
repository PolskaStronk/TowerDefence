using UnityEngine;
using System.Collections;

public class Missile {
	
	bool isActive = true;
	private GameObject gameObject;
	private MissileTower parentObject;
	private MonsterObject target;
	private Vector2 targetPosition;
	private float speed;
	private bool isTargetIsDead = false;
	
	
	public static Missile FindMissile () {
		
		foreach (Missile missile in GameController.missiles) {
			if (!missile.isActive) 	
				return missile;
		}
		return null;
	}
	
	public Missile (MissileTower parent, MonsterObject target, float speed = 3f) {
		parentObject = parent;	
		this.target = target;
		this.speed = speed;
		gameObject = GameObject.Instantiate (Resources.Load("Prefabs/Missile")) as GameObject;
		gameObject.transform.position = new Vector3 (parent.position.x,parent.position.y, -5);
		isActive = true;
	}
	
	public void Activate (MissileTower parent, MonsterObject target, float speed = 3f) {
		this.target = target;
		this.speed = speed;
		gameObject.transform.position = new Vector3 (parent.position.x,parent.position.y, -5);
		isActive = true;
	}
	
	public void Update () {
		if (!isActive) 
			return;
		if (!isTargetIsDead) {
			
			if (target.position.x < -1) {
				isTargetIsDead = true;
				targetPosition = target.deathPosition;
			} else
				targetPosition = target.position;
			
		}
		Vector2 add_ = (new Vector2(gameObject.transform.position.x,gameObject.transform.position.y) - targetPosition);
		add_ /= (Mathf.Sqrt(add_.x*add_.x +add_.y*add_.y));
		Vector3 toAdd = new Vector3 (add_.x,add_.y,0)*speed*Time.deltaTime;
		gameObject.transform.position -= toAdd;
		
		if (Vector2.Distance ( targetPosition, new Vector2(gameObject.transform.position.x,gameObject.transform.position.y)) < 0.1f) {
			parentObject.BlowUp(targetPosition);
			isActive = false;
			gameObject.transform.position = new Vector3 (0,0,-100);
			isTargetIsDead = false;
		}
	}
}
