using UnityEngine;
using System.Collections;

public abstract class TDObject {
	
	public enum TDType {Monster, Tower};
	public TDType classType;
	public int health;
	public int damage;
	public int attackSpeed;
	public Vector2 position;
}
