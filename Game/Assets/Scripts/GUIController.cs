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
			GameController.UpgradeSelectedTower(TowerObject.UpgradeType.Range);
		}
		if (Input.GetKeyDown(KeyCode.Alpha5)) {
			GameController.UpgradeSelectedTower(TowerObject.UpgradeType.Damage);
		}
		if (Input.GetKeyDown(KeyCode.Alpha6)) {
			GameController.UpgradeSelectedTower(TowerObject.UpgradeType.Splash);
		}
		if (Input.GetKeyDown(KeyCode.Alpha7)) {
			GameController.UpgradeSelectedTower(TowerObject.UpgradeType.AtackSpeed);
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha0)) {
			GameController.health = 0;	
		}
		
		moneyText.GetComponent<UILabel>().text = "You have " + GameController.money + " coins";
		healthText.GetComponent<UILabel>().text = "Your health:" + GameController.health;
	} 
}
