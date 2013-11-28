using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	
	public static int[,] map;
	public static TowerObject.TowerType [,] towersMap;
	public static TowerObject [,] towersLinkMap;
	
	public static int money = 100;
	public static float difficulty = 1;
	public static int health = 10;
	
	public static List <TowerObject> towers = new List<TowerObject> ();
	public static List <MonsterObject> monsters = new List<MonsterObject> ();
	
	public static int height = 15, width = 30;
	
	public static TowerObject.TowerType currentTowerType = TowerObject.TowerType.Gun; //???
	
	private static GameObject planePrefab; //???
	
	private float lastTimeKilledMonsterByHand=-1;
	private float coolDownToKillMonsterByHand = 0.3f;
	
	public struct Pair {
		public int first, second;
	}
	public static Pair makePair( int x, int y ) {
		Pair a;
		a.first = x;
		a.second = y;
		return a;
	}
	
	
	
	public enum Direction {Up, Down, Left, Right, None};
	public static int numberOfPaths = 1;
	public static Direction[,,] path, newPath; //[height, width, path]
	public static List <Pair>[] enter; //[path]
	public static List <Pair>[] exit; //[path]
	
	void FindPath() {
		for( int x = 0; x < height; x++ )
			for( int y = 0; y < width; y++ )
				for( int i = 0; i < numberOfPaths; i++ ) newPath[x, y, i] = Direction.None;
		
		for( int i = 0; i < numberOfPaths; i++ ) {
			Queue <Pair> queueForBfs = new Queue <Pair>();
			foreach (Pair exit_ in exit[i]) {
				queueForBfs.Enqueue( exit_ );
			}
			
			while( queueForBfs.Count != 0 )
			{
				for( int j = queueForBfs.Count; j > 0; j-- ) {
					Pair pos = queueForBfs.Dequeue();
					
					if( 0 < pos.first && newPath[pos.first - 1, pos.second, i] == Direction.None && towersMap[pos.first - 1, pos.second] == TowerObject.TowerType.None ) {
						newPath[pos.first - 1, pos.second, i] = Direction.Down; 
						queueForBfs.Enqueue( makePair( pos.first - 1, pos.second ) );
					}
					if( pos.first + 1 < height && newPath[pos.first + 1, pos.second, i] == Direction.None && towersMap[pos.first + 1, pos.second] == TowerObject.TowerType.None ) {
						newPath[pos.first + 1, pos.second, i] = Direction.Up; 
						queueForBfs.Enqueue( makePair( pos.first + 1, pos.second ) );
					}
					if( 0 < pos.second && newPath[pos.first, pos.second - 1, i] == Direction.None && towersMap[pos.first, pos.second - 1] == TowerObject.TowerType.None ) {
						newPath[pos.first, pos.second - 1, i] = Direction.Right; 
						queueForBfs.Enqueue( makePair( pos.first, pos.second - 1 ) );
					}
					if( pos.second + 1 < width && newPath[pos.first, pos.second + 1, i] == Direction.None && towersMap[pos.first, pos.second + 1] == TowerObject.TowerType.None ) {
						newPath[pos.first, pos.second + 1, i] = Direction.Left; 
						queueForBfs.Enqueue( makePair( pos.first, pos.second + 1 ) );
					}
				}
			}
		}
	}
	
	bool AllPathsAreCorrect() {
		foreach(MonsterObject monster_ in monsters) {
			if( monster_.path != -1 && newPath[(int)monster_.position.x, (int)monster_.position.y, monster_.path] == Direction.None ) return false;
		}
		int numberOfIsolated = 0;
		for( int i = 0; i < numberOfPaths; i++ ) {
			foreach(Pair enter_ in enter[i]) {
				if( newPath[enter_.first, enter_.second, i] == Direction.None ) numberOfIsolated++;
			}
			if( numberOfIsolated == enter[i].Count ) return false;
		}
		
		/*--------!!!!!!!!!---------*/
		
		foreach(MonsterObject monster_ in monsters) {
			if( monster_.path != -1 && newPath[(int)monster_.target.x, (int)monster_.target.y, monster_.path] == Direction.None ) return false;
		}
		
		/*--------------------*/
		
		return true;
	}
	
	public static void CreateSoldier(Vector2 position) {
		GameObject soldierObject = Instantiate(Resources.Load("Prefabs/Monsters/Soldier") as GameObject) as GameObject;
		soldierObject.name = "Soldier" + monsters.Count;
		Soldier result = new Soldier (soldierObject);
		result.position = position;
		soldierObject.transform.position = new Vector3 (result.position.x, result.position.y, -2);
		monsters.Add(result);
		
	}
	
	void CreateGunTower(GameObject parent) {
		towersMap[(int)(parent.transform.position.x ),(int)(parent.transform.position.y )] = TowerObject.TowerType.Gun;
		
		FindPath();
		if( AllPathsAreCorrect() ) {
			GameObject gunObject = Instantiate(Resources.Load("Prefabs/Towers/Gun") as GameObject) as GameObject;
			gunObject.name = "GunTower" + towers.Count;
			GunTower result = new GunTower (gunObject);
			result.position = new Vector2 (parent.transform.position.x , parent.transform.position.y );
			towers.Add(result);
			gunObject.transform.position = parent.transform.position - new Vector3(0,0,1);
			
			towersLinkMap[(int)result.position.x,(int)result.position.y] = result; 
			
			for( int x = 0; x < height; x++ )
				for( int y = 0; y < width; y++ )
					for( int i = 0; i < numberOfPaths; i++ )
						path[x, y, i] = newPath[x, y, i];
		} else {
			towersMap[(int)(parent.transform.position.x ),(int)(parent.transform.position.y )] = TowerObject.TowerType.None;
		}
	}
	
	void CreateMissileTower(GameObject parent) {
		towersMap[(int)(parent.transform.position.x ),(int)(parent.transform.position.y )] = TowerObject.TowerType.Missile;
		
		FindPath();
		if( AllPathsAreCorrect() ) {
			GameObject missileObject = Instantiate(Resources.Load("Prefabs/Towers/Missile") as GameObject) as GameObject;
			missileObject.name = "MissileTower" + towers.Count;
			MissileTower result = new MissileTower (missileObject);
			result.position = new Vector2 (parent.transform.position.x , parent.transform.position.y );
			towers.Add(result);
			missileObject.transform.position = parent.transform.position - new Vector3(0,0,1);
			
			towersLinkMap[(int)result.position.x,(int)result.position.y] = result; 
			
			for( int x = 0; x < height; x++ )
				for( int y = 0; y < width; y++ )
					for( int i = 0; i < numberOfPaths; i++ )
						path[x, y, i] = newPath[x, y, i];
		} else {
			towersMap[(int)(parent.transform.position.x ),(int)(parent.transform.position.y )] = TowerObject.TowerType.None;
		}
	}
	
	void CreateSlowLaserTower(GameObject parent) {
		towersMap[(int)(parent.transform.position.x ),(int)(parent.transform.position.y )] = TowerObject.TowerType.SlowLaser;
		
		FindPath();
		if( AllPathsAreCorrect() ) {
			GameObject slowLaserObject = Instantiate(Resources.Load("Prefabs/Towers/SlowLaser") as GameObject) as GameObject;
			slowLaserObject.name = "SlowLasereTower" + towers.Count;
			SlowLaserTower result = new SlowLaserTower (slowLaserObject);
			result.position = new Vector2 (parent.transform.position.x , parent.transform.position.y );
			towers.Add(result);
			slowLaserObject.transform.position = parent.transform.position - new Vector3(0,0,1);
			
			towersLinkMap[(int)result.position.x,(int)result.position.y] = result; 
			
			for( int x = 0; x < height; x++ )
				for( int y = 0; y < width; y++ )
					for( int i = 0; i < numberOfPaths; i++ )
						path[x, y, i] = newPath[x, y, i];
		} else {
			towersMap[(int)(parent.transform.position.x ),(int)(parent.transform.position.y )] = TowerObject.TowerType.None;
		}
	}
	
	void CreateTower(GameObject parent) {
		
		int prise = TowerObject.prises[(int)currentTowerType];
		
		if (money < prise) return;
		money -= prise;
		
		Pair positionInTowersMap = makePair( (int)(parent.transform.position.x ),(int)(parent.transform.position.y ) );
		
		/*-------We can't create tower on enter/exit------*/
		for( int i = 0; i < numberOfPaths; i++ ) {
			foreach(Pair enter_ in enter[i]) {
				if( enter_.second == positionInTowersMap.second && enter_.first == positionInTowersMap.first ) return;
			}
			foreach(Pair exit_ in exit[i]) {
				if( exit_.second == positionInTowersMap.second && exit_.first == positionInTowersMap.first ) return;
			}
		}
		/*------------------------------------------------*/
		
		switch (currentTowerType) {
			
		case  TowerObject.TowerType.Gun:
				CreateGunTower(parent);
			break;
		case  TowerObject.TowerType.Missile:
				CreateMissileTower(parent);
			break;
		case  TowerObject.TowerType.SlowLaser:
				CreateSlowLaserTower(parent);
			break;
			
			
			
		}
	}
	
	void CreateLevel() {
		for (int i = 0; i < height; i++) 
			for (int q = 0; q < width; q++) {
				map[i,q] = 10; //Towers+|Monsters+
				towersMap[i,q] = TowerObject.TowerType.None;
				
		}
		map[0,0] = 1; //!!!
		map[height-1,width-1] = -1; //!!!
		
		planePrefab = Resources.Load("Prefabs/Plane") as GameObject;
		Texture grass = Resources.Load("Textures/Grass") as Texture;
		
		for (int i = 0 ; i < height; i++) 
			for (int q = 0; q < width; q++) {
				GameObject plane_ = Instantiate(planePrefab) as GameObject;
				plane_.name = "Plane"+i+"|"+q;
				plane_.transform.position = new Vector3 (i, q, -1);
				plane_.renderer.material.mainTexture = grass;
			
		}
		
	}
	
	void Start () {
		
		map = new int [height, width];
		towersMap = new TowerObject.TowerType[height, width];
		for( int x = 0; x < height; x++ )
			for( int y = 0; y < width; y++ )
				towersMap[x, y] = TowerObject.TowerType.None;
		/*------only now------*/
		for( int i = 0; i < height; i++ )
			for( int j = 0; j < width; j++ ) map[i, j] = 10; //!!!
		exit = new List <Pair>[numberOfPaths];
		enter = new List <Pair>[numberOfPaths];
		exit[0]= new List <Pair>();
		enter[0] = new List <Pair>();
		enter[0].Add( makePair( 0, 0 ) );
		exit[0].Add( makePair( height - 1, width - 1 ) );
		newPath = new Direction[height, width, numberOfPaths];
		path = new Direction[height, width, numberOfPaths];
		
		FindPath();
		for( int x = 0; x < height; x++ )
			for( int y = 0; y < width; y++ )
				for( int i = 0; i < numberOfPaths; i++ )
					path[x, y, i] = newPath[x, y, i];
		
		/*--------------------*/
		
		towersMap = new TowerObject.TowerType [height, width];
		towersLinkMap = new TowerObject [height,width];
		CreateLevel();
		
		/*!!!!!!!!!!!*/
		GameObject camera = GameObject.Find("Main Camera");
		camera.transform.position = new Vector3( (float)(height - 1) / 2, (float)(width - 1) / 2, -10 );
		camera.camera.orthographicSize = Mathf.Max( ( height >> 1 ) + ( height & 1 ), ( width >> 1 ) + ( width & 1 ) ) + 1; // int x; x >> 1 == x / 2, x & 1 == x % 2;
		/*!!!!!!!!!!!*/
		
	}
	
	
	void MouseController() {
		
		if(Input.GetMouseButton(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100)) {
				if (hit.collider.gameObject.name.Contains ("Plane")) {
					CreateTower(hit.collider.gameObject);
				}
				//hit.collider.gameObject.renderer.material.mainTexture = Resources.Load("Textures/texture" + nowTextureNumber.ToString()) as Texture;
			}
		}
		
		if(Input.GetMouseButton(1)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100)) {
				if (hit.collider.gameObject.name.Contains ("Tower")) {
					
					towersLinkMap[(int)hit.collider.gameObject.transform.position.x, 
						(int)hit.collider.gameObject.transform.position.y].DestroyTower();
					//towersMap[(int)hit.collider.gameObject.transform.position.x, (int)hit.collider.gameObject.transform.position.y] = TowerObject.TowerType.None;
					FindPath();
					for( int x = 0; x < height; x++ )
						for( int y = 0; y < width; y++ )
							for( int i = 0; i < numberOfPaths; i++ )
								path[x, y, i] = newPath[x, y, i];
				}
				
				if (hit.collider.gameObject.name.Contains("Soldier")) {
					if (lastTimeKilledMonsterByHand + coolDownToKillMonsterByHand < Time.time) {
						lastTimeKilledMonsterByHand = Time.time;
						monsters[int.Parse(hit.collider.gameObject.name.Substring(7))].AddDamage(1000);
					}
					
				}
				
				//hit.collider.gameObject.renderer.material.mainTexture = Resources.Load("Textures/texture" + nowTextureNumber.ToString()) as Texture;
			}
		}
		
		
	}
	
	void Update () {
		
		//if( Random.value < 0.02f ) CreateSoldier( new Vector2( enter[0][0].first, enter[0][0].second ) );
		
		MouseController();
		
		/*
		List<Vector3> vertexList = new List<Vector3>();
		LineRenderer lr = gameObject.AddComponent<LineRenderer>();
		
		lr.SetColors(Color.red, Color.red);
		vertexList.Add(new Vector3(0, 0, -5));
		vertexList.Add(new Vector3(2, 4, -5));
		vertexList.Add(new Vector3(4, 0, -5));
		lr.SetVertexCount(vertexList.Count);
		for(int i=0; i<vertexList.Count; i++){
		lr.SetPosition(i, vertexList[i]);
		}
		
		*/
		
		foreach (TowerObject tower_ in towers) {
			tower_.Update();	
		}
		
		foreach (MonsterObject monster_ in monsters) {
			//if( monster_.path == -1 ) monsters.Remove( monster_ );
			monster_.Update();	
		}
		
		if (health <= 0) {
		
			
			
		}
		
	}
}
