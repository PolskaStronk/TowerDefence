using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	
	public static int[,] map;
	public static TowerObject.TowerType [,] towersMap;
	public static TowerObject [,] towersLinkMap;
	
	public static List <TowerObject> towers = new List<TowerObject> ();
	public static List <MonsterObject> monsters = new List<MonsterObject> ();
	
	public static int height = 20, width = 20;
	
	private static TowerObject.TowerType currentTowerType = TowerObject.TowerType.Gun; //???
	
	private static GameObject planePrefab; //???
	
	private struct Pair {
		public int first, second;
	}
	private static Pair makePair( int x, int y ) {
		Pair a;
		a.first = x;
		a.second = y;
		return a;
	}
	
	public enum Direction {Up, Down, Left, Right, None};
	public static int numberOfPaths = 1;
	public static Direction[,,] path, newPath; //[height, width, path]
	private static List <Pair>[] enter; //[path]
	private static List <Pair>[] exit; //[path]
	
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
					
					if( 0 < pos.first && newPath[pos.first - 1, pos.second, i] == Direction.None && towersMap[pos.second, pos.first - 1] == TowerObject.TowerType.None ) {
						newPath[pos.first - 1, pos.second, i] = Direction.Right; 
						queueForBfs.Enqueue( makePair( pos.first - 1, pos.second ) );
					}
					if( pos.first + 1 < height && newPath[pos.first + 1, pos.second, i] == Direction.None && towersMap[pos.second, pos.first + 1] == TowerObject.TowerType.None ) {
						newPath[pos.first + 1, pos.second, i] = Direction.Left; 
						queueForBfs.Enqueue( makePair( pos.first + 1, pos.second ) );
					}
					if( 0 < pos.second && newPath[pos.first, pos.second - 1, i] == Direction.None && towersMap[pos.second - 1, pos.first] == TowerObject.TowerType.None ) {
						newPath[pos.first, pos.second - 1, i] = Direction.Down; 
						queueForBfs.Enqueue( makePair( pos.first, pos.second - 1 ) );
					}
					if( pos.second + 1 < width && newPath[pos.first, pos.second + 1, i] == Direction.None && towersMap[pos.second + 1, pos.first] == TowerObject.TowerType.None ) {
						newPath[pos.first, pos.second + 1, i] = Direction.Up; 
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
		return true;
	}
	
	void CreateSoldier(Vector2 position) {
		GameObject soldierObject = Instantiate(Resources.Load("Prefabs/Monsters/Soldier") as GameObject) as GameObject;
		soldierObject.name = "Soldier" + monsters.Count;
		Soldier result = new Soldier (soldierObject);
		result.position = position;
		soldierObject.transform.position = new Vector3 (result.position.x - width/2, result.position.y - height/2, -2);
		monsters.Add(result);
		
	}
	
	void CreateGunTower(GameObject parent) {
		towersMap[(int)(parent.transform.position.y + height/2),(int)(parent.transform.position.x + width/2)] = TowerObject.TowerType.Gun;
		
		FindPath();
		if( AllPathsAreCorrect() ) {
			GameObject gunObject = Instantiate(Resources.Load("Prefabs/Towers/Gun") as GameObject) as GameObject;
			gunObject.name = "GunTower" + towers.Count;
			GunTower result = new GunTower (gunObject);
			result.position = new Vector2 (parent.transform.position.x + width/2, parent.transform.position.y + height/2);
			towers.Add(result);
			gunObject.transform.position = parent.transform.position - new Vector3(0,0,1);
			
			towersLinkMap[(int)result.position.y,(int)result.position.x] = result; 
			
			for( int x = 0; x < height; x++ )
				for( int y = 0; y < width; y++ )
					for( int i = 0; i < numberOfPaths; i++ )
						path[x, y, i] = newPath[x, y, i];
		} else {
			towersMap[(int)(parent.transform.position.y + height/2),(int)(parent.transform.position.x + width/2)] = TowerObject.TowerType.None;
		}
	}
	
	void CreateTower(GameObject parent) {
		Pair positionInTowersMap = makePair( (int)(parent.transform.position.y + height/2),(int)(parent.transform.position.x + width/2) );
		
		/*-------We can't create tower on enter/exit------*/
		for( int i = 0; i < numberOfPaths; i++ ) {
			foreach(Pair enter_ in enter[i]) {
				if( enter_.second == positionInTowersMap.first && enter_.first == positionInTowersMap.second ) return;
			}
			foreach(Pair exit_ in exit[i]) {
				if( exit_.second == positionInTowersMap.first && exit_.first == positionInTowersMap.second ) return;
			}
		}
		/*------------------------------------------------*/
		
		CreateGunTower(parent);
	}
	
	void CreateLevel() {
		for (int i = 0; i < height; i++) 
			for (int q = 0; q < width; q++) {
				map[i,q] = 10; //Towers+|Monsters+
				towersMap[i,q] = TowerObject.TowerType.None;
				
		}
		map[0,0] = 1;
		map[height-1,width-1] = -1;
		
		planePrefab = Resources.Load("Prefabs/Plane") as GameObject;
		Texture grass = Resources.Load("Textures/Grass") as Texture;
		
		for (int i = 0 ; i < height; i++) 
			for (int q = 0; q < width; q++) {
				GameObject plane_ = Instantiate(planePrefab) as GameObject;
				plane_.name = "Plane"+i+"|"+q;
				plane_.transform.position = new Vector3 (q - width/2, i - height/2, -1);
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
			for( int j = 0; j < width; j++ ) map[i, j] = 10;
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
		
		for( int x = 0; x < height; x++ ) {
			string s = "";
			for( int y = 0; y < width; y++ ) s += path[x, y, 0] + " ";
//			Debug.Log(s);
		}
		
		/*--------------------*/
		
		towersMap = new TowerObject.TowerType [height, width];
		towersLinkMap = new TowerObject [height,width];
		CreateLevel();
		
		/*Queue q = new Queue();
		q*/
		
		//CreateSoldier(new Vector2(0,0));
		/*CreateSoldier(new Vector2(Random.Range(0,width),Random.Range(0,height)));
		CreateSoldier(new Vector2(Random.Range(0,width),Random.Range(0,height)));*/
	}
	
	
	void AddTowersControll() {
		
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
					
					towersLinkMap[(int)hit.collider.gameObject.transform.position.y + height/2, 
						(int)hit.collider.gameObject.transform.position.x + width/2].DestroyTower();
					FindPath();
					for( int x = 0; x < height; x++ )
						for( int y = 0; y < width; y++ )
							for( int i = 0; i < numberOfPaths; i++ )
								path[x, y, i] = newPath[x, y, i];
				}
				//hit.collider.gameObject.renderer.material.mainTexture = Resources.Load("Textures/texture" + nowTextureNumber.ToString()) as Texture;
			}
		}
		
		
	}
	
	void Update () {
		
		if( Random.value < 0.03f ) CreateSoldier( new Vector2( enter[0][0].first, enter[0][0].second ) );
		//Debug.Log(monsters.Count.ToString());
		AddTowersControll();
		
		foreach (TowerObject tower_ in towers) {
			tower_.Update();	
		}
		
		foreach (MonsterObject monster_ in monsters) {
			//if( monster_.path == -1 ) monsters.Remove( monster_ );
			monster_.Update();	
		}
		
	}
}
