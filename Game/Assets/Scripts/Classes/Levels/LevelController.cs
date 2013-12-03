using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {
	
	
	public static List<Wave> level;
	
	//public static float maxPullSize = 1500;
	
	//public static List <GameObject> armorersPull = new List<GameObject> ();
	//public static List <GameObject> speedersPull = new List<GameObject> ();
	//public static List <GameObject> rangersPull = new List<GameObject> ();
	
	public static int currentWave = 0;
	public static int currentWaveEnemy = 0;
	public static float waveStartTime;
	
	private static float timeRadius = 0.1f; // to spawn the enemy
	
	private void SpawnEnemy (MonsterInfo enemy) {
		GameController.CreateSoldier( new Vector2( GameController.enter[0][0].first, GameController.enter[0][0].second ) );
	}
	
	void Start () {
		level = LevelCreator.CreateLevel(20);
		waveStartTime = Time.time + 1;
	}
	
	void Update () {
		//if (GameController.gameState != GameController.GameState.Playing) return;
		
		
		if (currentWave >= level.Count) {
			currentWave = 0;
			return;
		}
		int toSpawnCount = 0;
		//Debug.Log("asd " +  currentWave + " " + currentWaveEnemy + " " + level.Count);
		while (currentWaveEnemy+toSpawnCount < level[currentWave].monsters.Length) {
			
			if (Mathf.Abs ( Time.time - waveStartTime - level[currentWave].monsters[currentWaveEnemy+toSpawnCount].spawnTime) < timeRadius ) {
				toSpawnCount++;
			} else 
				break;
		}
		while (toSpawnCount>0) {
		 SpawnEnemy(level[currentWave].monsters[currentWaveEnemy]);
			toSpawnCount--;
			currentWaveEnemy++;
		}
		
		if (currentWaveEnemy >= level[currentWave].monsters.Length) {
			currentWave++;
			currentWaveEnemy = 0;
			waveStartTime = Time.time + LevelCreator.deltaWavesTime;
			GameController.difficulty +=1f/3;
		}
		
	}
}
