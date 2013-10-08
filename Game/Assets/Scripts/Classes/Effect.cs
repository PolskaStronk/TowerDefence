using UnityEngine;
using System.Collections;

public class Effect {
	
	public enum EffectType {Slow, Stun};
	public EffectType type;
	public float timeIsActive;
	
	public Effect ( EffectType type_) {
		type = type_;
	}
	
	public Effect ( EffectType type_, float timeIsActive_) {
		type = type_;
		timeIsActive = timeIsActive_;
	}
	
}
