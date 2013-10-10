using UnityEngine;
using System.Collections;

public class EditorMenu : MonoBehaviour {
	
	public enum MenuType{MainMenu = 0, RedactorMenu = 1};
	public MenuType currentMenu = MenuType.MainMenu;
	// Use this for initialization
	void Start () {
	
	}
	
	void ShowMainMenu (){
		if(GUI.Button (new Rect(50,50,200,50),"NEW MAP")){
			currentMenu = MenuType.RedactorMenu; }
	}
	
	void OnGUI (){
		if(currentMenu == MenuType.MainMenu) {ShowMainMenu();}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
