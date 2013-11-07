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
	
	/*----Radomir---*/
	private struct pair {
		public int first, second;
	}
	private static pair make_pair( int x, int y ) {
		pair a;
		a.first = x;
		a.second = y;
		return a;
	}
	
	public enum Direction {Up, Down, Left, Right, None};
	public static int number_of_paths = 1;
	public static Direction[,,] path, new_path; //[height, width, path]
	private static List <pair>[] enter; //[path]
	private static List <pair>[] exit; //[path]
	
	void find_path() {
		for( int x = 0; x < height; x++ )
			for( int y = 0; y < width; y++ )
				for( int i = 0; i < number_of_paths; i++ ) new_path[x, y, i] = Direction.None;
		
		//Debug.Log(number_of_paths.ToString());
		for( int i = 0; i < number_of_paths; i++ ) {
			Queue <pair> queue_for_bfs = new Queue <pair>();
			foreach (pair exit_ in exit[i]) {
				queue_for_bfs.Enqueue( exit_ );
				//new_path[exit_.first, exit_.second, i] = ( exit_.second != 0 )? Direction.Up: Direction.Down;
				//Debug.Log (exit_.first.ToString() + " " + exit_.second.ToString());
			}
			
			while( queue_for_bfs.Count != 0 )
			{
				int kol_on_that_step = queue_for_bfs.Count;
				for( int j = 0; j < kol_on_that_step; j++ ) {
					pair pos = queue_for_bfs.Dequeue();
					
					if( 0 < pos.first && new_path[pos.first - 1, pos.second, i] == Direction.None && towersMap[pos.second, pos.first - 1] == TowerObject.TowerType.None ) {
						new_path[pos.first - 1, pos.second, i] = Direction.Right; 
						queue_for_bfs.Enqueue( make_pair( pos.first - 1, pos.second ) );
					}
					if( pos.first + 1 < height && new_path[pos.first + 1, pos.second, i] == Direction.None && towersMap[pos.second, pos.first + 1] == TowerObject.TowerType.None ) {
						new_path[pos.first + 1, pos.second, i] = Direction.Left; 
						queue_for_bfs.Enqueue( make_pair( pos.first + 1, pos.second ) );
					}
					if( 0 < pos.second && new_path[pos.first, pos.second - 1, i] == Direction.None && towersMap[pos.second - 1, pos.first] == TowerObject.TowerType.None ) {
						new_path[pos.first, pos.second - 1, i] = Direction.Down; 
						queue_for_bfs.Enqueue( make_pair( pos.first, pos.second - 1 ) );
					}
					if( pos.second + 1 < width && new_path[pos.first, pos.second + 1, i] == Direction.None && towersMap[pos.second + 1, pos.first] == TowerObject.TowerType.None ) {
						new_path[pos.first, pos.second + 1, i] = Direction.Up; 
						queue_for_bfs.Enqueue( make_pair( pos.first, pos.second + 1 ) );
					}
				}
			}
		}
	}
	
	bool all_paths_are_correct() {
		foreach(MonsterObject monster_ in monsters) {
			if( monster_.path != -1 && new_path[(int)monster_.position.x, (int)monster_.position.y, monster_.path] == Direction.None ) return false;
		}
		int numberOfIsolated = 0;
		for( int i = 0; i < number_of_paths; i++ ) {
			foreach(pair enter_ in enter[i]) {
				if( new_path[enter_.first, enter_.second, i] == Direction.None ) numberOfIsolated++;
			}
			if( numberOfIsolated == enter[i].Count ) return false;
			numberOfIsolated = 0;
			foreach(pair exit_ in exit[i]) {
				if( new_path[exit_.first, exit_.second, i] == Direction.None ) numberOfIsolated++;
			}
			if( numberOfIsolated == exit[i].Count ) return false;
		}
		return true;
	}
	/*--------------*/
	
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
		
		find_path();
		if( all_paths_are_correct() ) {
			GameObject gunObject = Instantiate(Resources.Load("Prefabs/Towers/Gun") as GameObject) as GameObject;
			gunObject.name = "GunTower" + towers.Count;
			GunTower result = new GunTower (gunObject);
			result.position = new Vector2 (parent.transform.position.x + width/2, parent.transform.position.y + height/2);
			towers.Add(result);
			gunObject.transform.position = parent.transform.position - new Vector3(0,0,1);
			
			towersLinkMap[(int)result.position.y,(int)result.position.x] = result; 
			
			for( int x = 0; x < height; x++ )
				for( int y = 0; y < width; y++ )
					for( int i = 0; i < number_of_paths; i++ )
						path[x, y, i] = new_path[x, y, i];
		} else {
			towersMap[(int)(parent.transform.position.y + height/2),(int)(parent.transform.position.x + width/2)] = TowerObject.TowerType.None;
		}
	}
	
	void CreateTower(GameObject parent) {
		pair positionInTowersMap = make_pair( (int)(parent.transform.position.y + height/2),(int)(parent.transform.position.x + width/2) );
		
		/*-------We can't create tower on enter/exit------*/
		for( int i = 0; i < number_of_paths; i++ ) {
			foreach(pair enter_ in enter[i]) {
				if( enter_.second == positionInTowersMap.first && enter_.first == positionInTowersMap.second ) return;
			}
			foreach(pair exit_ in exit[i]) {
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
		exit = new List <pair>[number_of_paths];
		enter = new List <pair>[number_of_paths];
		exit[0]= new List <pair>();
		enter[0] = new List <pair>();
		enter[0].Add( make_pair( 0, 0 ) );
		exit[0].Add( make_pair( height - 1, width - 1 ) );
		new_path = new Direction[height, width, number_of_paths];
		path = new Direction[height, width, number_of_paths];
		
		find_path();
		for( int x = 0; x < height; x++ )
			for( int y = 0; y < width; y++ )
				for( int i = 0; i < number_of_paths; i++ )
					path[x, y, i] = new_path[x, y, i];
		
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
					find_path();
					for( int x = 0; x < height; x++ )
						for( int y = 0; y < width; y++ )
							for( int i = 0; i < number_of_paths; i++ )
								path[x, y, i] = new_path[x, y, i];
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
