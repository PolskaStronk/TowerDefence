using UnityEngine;
using System.Collections;

public class EditorMenu : MonoBehaviour {
	
	public enum MenuType{MainMenu = 0, RedactorMenu = 1};
	public MenuType currentMenu = MenuType.MainMenu;
	public int nowTextureNumber = 1;
	const int maxTexture = 6;
	string mapWidthString = "20";
	string mapHeightString = "20";
	int mapWidth;
	int mapHeight;
	int i,j,k; 
	string reader, numbers; 
	int[,] mapArray = new int[100,100];
	Texture[] textures;
	GameObject plane;

	
	void SaveMap() {
		
	}
	
	void CreateMap(int width, int heigth) {
		mapWidth = width;
		mapHeight = heigth;
		for(int mapI=1; mapI<=mapWidth; mapI++) for(int mapJ=1;mapJ<=mapHeight;mapJ++){
			GameObject mapPlane = Resources.Load("Prefabs/Plane") as GameObject;
			GameObject mapPlane_ = Instantiate (mapPlane) as GameObject;
			mapPlane_.transform.position = new Vector3(mapI-mapWidth/2,mapJ-mapHeight/2,0);
			mapPlane_.name = "Plane_" + mapI.ToString() + "x" + mapJ.ToString();
		}
	}
	
	void LoadMap(/*string path, int width, int heigth*/) {
	numbers="0123456789";
		
	TextAsset map = Resources.Load ("map") as TextAsset;
		while (map.text[i]==numbers[0]||map.text[i]==numbers[1]||map.text[i]==numbers[2]||map.text[i]==numbers[3]||map.text[i]==numbers[4]||map.text[i]==numbers[5]||map.text[i]==numbers[6]||map.text[i]==numbers[7]||map.text[i]==numbers[8]||map.text[i]==numbers[9])
		{
		     reader=reader+map.text[i]; i=i+1;
		}
		int n = int.Parse(reader);
		reader="";i=i+1;
		while (map.text[i]==numbers[0]||map.text[i]==numbers[1]||map.text[i]==numbers[2]||map.text[i]==numbers[3]||map.text[i]==numbers[4]||map.text[i]==numbers[5]||map.text[i]==numbers[6]||map.text[i]==numbers[7]||map.text[i]==numbers[8]||map.text[i]==numbers[9])
		{
		     reader=reader+map.text[i]; i=i+1;
		}
		int m = int.Parse(reader);
		reader="";i=i+2;
		   for (k=0;k<n;k++){
		   for (j=0;j<m;j++)
		       {
			   while (map.text[i]==numbers[0]||map.text[i]==numbers[1]||map.text[i]==numbers[2]||map.text[i]==numbers[3]||map.text[i]==numbers[4]||map.text[i]==numbers[5]||map.text[i]==numbers[6]||map.text[i]==numbers[7]||map.text[i]==numbers[8]||map.text[i]==numbers[9])
		{
		     reader=reader+map.text[i]; i=i+1;
		} i++;mapArray[k,j]=int.Parse(reader);reader="";}
		      i++; } 
		
		plane = Resources.Load("Prefabs/Plane") as GameObject;
			textures = new Texture[7];
		textures[1] = Resources.Load("Textures/texture1")as Texture;
		textures[2] = Resources.Load("Textures/texture2")as Texture;
		textures[3] = Resources.Load("Textures/texture3")as Texture;
		textures[4] = Resources.Load("Textures/texture4")as Texture;
		textures[5] = Resources.Load("Textures/texture5")as Texture;
		textures[6] = Resources.Load("Textures/texture6")as Texture;
		for (k=1;k<=n;k++)
		   for (j=1;j<=m;j++){
			GameObject plane_ = Instantiate(plane) as GameObject;
		plane_.transform.position = new Vector3 (k,j,0);
			plane_.renderer.material.mainTexture = textures[mapArray[j-1,k-1]];
			plane_.name="Plane_"+j.ToString()+"x"+k.ToString();
		}
	}
	
	// Use this for initialization
	void Start () {
	}
	
	void ShowMainMenu (){
		if(GUI.Button (new Rect(50,50,200,50),"NEW MAP")){
			if (int.TryParse(mapWidthString,out mapWidth) && int.TryParse(mapHeightString, out mapHeight)){
				CreateMap(Mathf.Clamp(mapWidth,10,40),Mathf.Clamp(mapHeight,10,40));
				currentMenu = MenuType.RedactorMenu;
				
		} }
		mapWidthString = GUI.TextField(new Rect(300,50,100,25), mapWidthString);
		mapHeightString = GUI.TextField(new Rect(300,76,100,25), mapHeightString);
		if(GUI.Button (new Rect(50,101,200,50),"LOAD MAP")){LoadMap();currentMenu = MenuType.RedactorMenu;}
		if(GUI.Button (new Rect(50,152,200,50),"EXIT")){}
	}
	
	void ShowRedactorMenu(){
		if(GUI.Button (new Rect(10,10,50,20),"Save")){}
		if(GUI.Button (new Rect(10,31,50,20),"Clear")){
			for(int clearI = 0;clearI<=mapWidth; clearI++)
				for(int clearJ = 0;clearJ<=mapWidth; clearJ++) {
				GameObject.Find("Plane_"+(clearI+1)+"x" + (clearJ+1)).renderer.material.mainTexture = Resources.Load("Textures/texture" + nowTextureNumber.ToString()) as Texture;
		
				}
		}
		if(GUI.Button (new Rect(10,52,20,20),"+")){ nowTextureNumber++;}
		if(GUI.Button (new Rect(31,52,20,20),"-")){ nowTextureNumber--;}
		nowTextureNumber = Mathf.Clamp(nowTextureNumber,1,6);
		GUI.Box(new Rect(10,73,50,20),nowTextureNumber.ToString());
	}
	
	void OnGUI (){
		if (currentMenu == MenuType.RedactorMenu) { ShowRedactorMenu(); }
		if(currentMenu == MenuType.MainMenu) {ShowMainMenu();}
	}
	
	// Update is called once per frame
	void Update () {
		float xAxisValue = Input.GetAxis("Horizontal");
    	float yAxisValue = Input.GetAxis("Vertical");
		float zAxisValue = Input.GetAxis("Perspective");
	    if(Camera.current != null){
	        Camera.current.transform.Translate(new Vector3(xAxisValue, yAxisValue,/* zAxisValue*/ 0));
			Camera.current.orthographicSize += zAxisValue;
			Camera.current.orthographicSize = Mathf.Clamp(Camera.current.orthographicSize,0.5f,25);
	    }
		if(Input.GetMouseButton(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				//Debug.Log(hit.collider.gameObject.name);
				hit.collider.gameObject.renderer.material.mainTexture = Resources.Load("Textures/texture" + nowTextureNumber.ToString()) as Texture;
			}
		}
	}
}
