using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {

	void Start () {
	
	}
	
	
	void OnGUI() {
		
		if (GUI.Button ( new Rect (0,40,150,20) , "You have " + GameController.money + " coins")) 
			GameController.money +=100;
		
		
	}
}
