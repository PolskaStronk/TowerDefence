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
	// Use this for initialization
	void Start () {
	}
	
	void ShowMainMenu (){
		if(GUI.Button (new Rect(50,50,200,50),"NEW MAP")){
			if (int.TryParse(mapWidthString,out mapWidth) && int.TryParse(mapHeightString, out mapHeight)){
				mapWidth = Mathf.Clamp(mapWidth,10,40);
				mapHeight = Mathf.Clamp(mapHeight,10,40);
				currentMenu = MenuType.RedactorMenu;
				for(int mapI=1;mapI<=mapWidth;mapI++) for(int mapJ=1;mapJ<=mapHeight;mapJ++){
					GameObject mapPlane = Resources.Load("Prefabs/Plane") as GameObject;
					GameObject mapPlane_ = Instantiate (mapPlane) as GameObject;
					mapPlane_.transform.position = new Vector3(mapI-mapWidth/2,mapJ-mapHeight/2,0);
				}
		} }
		mapWidthString = GUI.TextField(new Rect(300,50,100,25), mapWidthString);
		mapHeightString = GUI.TextField(new Rect(300,76,100,25), mapHeightString);
		if(GUI.Button (new Rect(50,101,200,50),"LOAD MAP")){}
		if(GUI.Button (new Rect(50,152,200,50),"EXIT")){}
	}
	
	void ShowRedactorMenu(){
		if(GUI.Button (new Rect(10,10,50,20),"Save")){}
		if(GUI.Button (new Rect(10,31,50,20),"Clear")){}
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
    if(Camera.current != null)
    {
        Camera.current.transform.Translate(new Vector3(xAxisValue, yAxisValue, 0.0f));
    }
	
	}
}
