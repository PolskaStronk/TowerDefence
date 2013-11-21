using UnityEngine;
using System.Collections;

public class Effect {
	
	public enum EffectType {Slow, Stun};
	public EffectType type;
	public float timeIsActive = 0;
	
	public Effect ( EffectType type_) {
		type = type_;
	}
	
	public Effect ( EffectType type_, float timeIsActive_) {
		type = type_;
		timeIsActive = timeIsActive_;
	}
	
	public void Update() {
		if (timeIsActive == 0) 
			return;
		timeIsActive -= Time.deltaTime;
		if (timeIsActive < 0) 
			timeIsActive = 0;
	}
	
}
