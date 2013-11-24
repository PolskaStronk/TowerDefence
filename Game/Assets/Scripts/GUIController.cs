using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {
	
	public GameObject moneyText;
	
	void SelectGundTower() {
		Debug.Log("GUN!!!!");
		//Ilya??
	}
	
	void SelectRocketTower() {
		Debug.Log("ROCKET!!!!");
		//Ilya??
	}
	
	void Update() {
		moneyText.GetComponent<UILabel>().text = "You have " + GameController.money + " coins";
	} 
}
