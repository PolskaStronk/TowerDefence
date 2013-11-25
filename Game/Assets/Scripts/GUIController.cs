using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {
	
	public GameObject moneyText;
	
	void SelectGundTower() {
		GameController.currentTowerType = TowerObject.TowerType.Gun;
	}
	
	void SelectRocketTower() {
		GameController.currentTowerType = TowerObject.TowerType.Missile;
	}
	
	void Update() {
		moneyText.GetComponent<UILabel>().text = "You have " + GameController.money + " coins";
	} 
}
