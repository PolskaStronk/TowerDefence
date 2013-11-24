using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class LevelCreator : MonoBehaviour {
	
	// TODO: refactor to one file
	private static int count = 3;
	private static int difficultyRadius = 300;
	public static float deltaWavesTime = 3f;
	
	private static bool isStarted = false;
	
	public static List<Wave> waves = new List<Wave>();
	
	static void Start () {
		isStarted = true;
		for (int i = 1 ; i <= count; i++) {
			TextAsset file = Resources.Load("Waves/"+i) as TextAsset;
			waves.Add(Wave.Parse(file.text));
		}
		
	}
	
	public static List<Wave> CreateLevel(float maxTime) {
			
		if (!isStarted) Start();
		
		int lastDifficulty = 0;
		float currentTime = 0;
		bool unfoundable = false;
		
		List<Wave> result = new List<Wave>();
		
		while (currentTime<maxTime && !unfoundable) {
			unfoundable = true;
			
			for (int i = 0; i< waves.Count; i++) {
				if (Mathf.Abs(waves[i].difficulty - lastDifficulty) <= difficultyRadius)
					unfoundable = false;
				if (Mathf.Abs(waves[i].difficulty - lastDifficulty) <= difficultyRadius && Random.value <1f/waves.Count) {
					result.Add(waves[i]);
					currentTime += waves[i].WaveTime()+deltaWavesTime;
					lastDifficulty = waves[i].difficulty;
				}
			}
		}
		//Debug.Log(currentTime + " " + waves.Count);
		return result;
	}
}

