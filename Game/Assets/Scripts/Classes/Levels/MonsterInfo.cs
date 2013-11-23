using UnityEngine;
using System.Collections;

public class MonsterInfo {
	
	public float spawnTime;
	public int enter = 1;
	public MonsterObject.MonsterType type;
	
	public MonsterInfo () {
		
	}
	public MonsterInfo (float spawnTime_, float enter_, MonsterObject.MonsterType type_) {
		spawnTime = spawnTime_;
		enter_ = enter;
		type = type_;
	}
	
	public override string ToString () {
		string result = "";
		string nxt = "|";
		result += spawnTime.ToString() + nxt;
		result += enter.ToString() + nxt;
		result += type.ToString() + nxt;
		
		return result;
	}
	public static MonsterInfo Parse(string target) {
		MonsterInfo result = new MonsterInfo();
		int nxtPos = 0;
		char nxt = '|';
		
		nxtPos = target.IndexOf(nxt);
		result.spawnTime = float.Parse(target.Substring(0,nxtPos));
		target = target.Remove(0,nxtPos+1);
		
		nxtPos = target.IndexOf(nxt);
		result.enter = int.Parse(target.Substring(0,nxtPos));
		target = target.Remove(0,nxtPos+1);
		
		nxtPos = target.IndexOf(nxt);
		result.type = (MonsterObject.MonsterType) System.Enum.Parse(typeof(MonsterObject.MonsterType),target.Substring(0,nxtPos));
		target = target.Remove(0,nxtPos+1);
		return result;
	}
	
	
}
