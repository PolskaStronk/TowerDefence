using UnityEngine;
using System.Collections;

public class Wave {
	
	public int difficulty;
	public MonsterInfo[] monsters;
	
	public Wave () {
		
	}
	
	public Wave (int difficulty_, params MonsterInfo[] params_) {
		difficulty = difficulty_;
		monsters = params_;
	}
	
	public float WaveTime () {
		float result = 0;
		
		for (int i = 0; i <monsters.Length; i++) 
			result = Mathf.Max(result, monsters[i].spawnTime);
		
		return result;
	}
	
	public override string ToString () {
		string result = "";
		string nxt = ",";
		
		result += difficulty.ToString() + nxt;
		
		for (int i = 0; i < monsters.Length; i++) 
			result += monsters[i].ToString() + nxt;
		
		return result;
		
	}
	
	public static Wave Parse (string target) {
		Wave result = new Wave();
		int nxtPos = 0;
		char nxt = ',';
		int length = -1;
		string target_ = target;
		
		while (target_.Length>0) {
			nxtPos = target_.IndexOf(nxt);
			target_ = target_.Remove(0,nxtPos+1);	
			length++;
		}
		result.monsters = new MonsterInfo [length];
		
		nxtPos = target.IndexOf(nxt);
		result.difficulty = int.Parse(target.Substring(0,nxtPos));
		target = target.Remove(0,nxtPos+1);
		
		int i = 0;
		
		while (target.Length>0) {
			nxtPos = target.IndexOf(nxt);
			result.monsters[i] = MonsterInfo.Parse(target.Substring(0,nxtPos));
			target = target.Remove(0,nxtPos+1);	
			i++;
		}
		
		return result;
	}
}
