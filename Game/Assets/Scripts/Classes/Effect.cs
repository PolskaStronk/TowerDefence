using UnityEngine;
using System.Collections;

public class Effect {
	
	public enum EffectType {Slow, Stun};
	EffectType type;
	
	public Effect ( EffectType type_) {
		type = type_;
	}
	
	
}
