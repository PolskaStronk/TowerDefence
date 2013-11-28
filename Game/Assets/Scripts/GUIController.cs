using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {
	
	public GameObject moneyText;
	public GameObject healthText;
	
	void SelectGunTower() {
		GameController.currentTowerType = TowerObject.TowerType.Gun;
	}
	
	void SelectRocketTower() {
		GameController.currentTowerType = TowerObject.TowerType.Missile;
	}
	
	void SelectSlowLaserTower() {
		GameController.currentTowerType = TowerObject.TowerType.SlowLaser;
	}
	
	void Update() {
		
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			SelectGunTower();	
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			SelectRocketTower();	
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			SelectSlowLaserTower();	
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha4)) {
			GameController.health = 0;	
		}
		
		moneyText.GetComponent<UILabel>().text = "You have " + GameController.money + " coins";
		healthText.GetComponent<UILabel>().text = "Your health:" + GameController.health;
	} 
}
