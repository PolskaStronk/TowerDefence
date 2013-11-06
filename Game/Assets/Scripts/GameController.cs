using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	
	public static int[,] map;
	public static TowerObject.TowerType [,] towersMap;
	
	public static List <TowerObject> towers = new List<TowerObject> ();
	public static List <MonsterObject> monsters = new List<MonsterObject> ();
	
	public static int height = 20, width = 20;
	//public static List <MonsterObject>[,] plane;
	
	private static TowerObject.TowerType currentTowerType = TowerObject.TowerType.Gun;
	
	private static GameObject planePrefab;
	
	void AddedTower(Vector2 position, TowerObject.TowerType type) {
		
		
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
		GameObject gunObject = Instantiate(Resources.Load("Prefabs/Towers/Gun") as GameObject) as GameObject;
		gunObject.name = "Gun" + towers.Count;
		GunTower result = new GunTower (gunObject);
		result.position = new Vector2 (parent.transform.position.x + width/2, parent.transform.position.y + height/2);
		gunObject.transform.position = parent.transform.position - new Vector3(0,0,1);
		towers.Add(result);
		towersMap[(int)result.position.y,(int)result.position.x] = TowerObject.TowerType.Gun;
		AddedTower(result.position,TowerObject.TowerType.Gun);
	}
	
	void CreateTower(GameObject parent) {
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
		towersMap = new TowerObject.TowerType [height, width];
		CreateLevel();
		
		CreateSoldier(new Vector2(Random.Range(0,width),Random.Range(0,height)));
		CreateSoldier(new Vector2(Random.Range(0,width),Random.Range(0,height)));
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
		
	}
	
	void Update () {
	
		AddTowersControll();
		
		//plane = new List <MonsterObject>[height, width];
		
		
		foreach (TowerObject tower_ in towers) {
			tower_.Update();	
		}
		
		foreach (MonsterObject monster_ in monsters) {
			monster_.Update();	
		}
		
	}
}
