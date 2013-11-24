using UnityEngine;
using System.Collections;

public class MenuSlider : MonoBehaviour {
	
	public GameObject menu;
	public GameObject towerNameLabel;
	public GameObject rangeLabel;
	public GameObject fireRateLabel;
	public GameObject damageLabel;
	
	Vector3 defaultMenuPos;
	float screenWidth;
	float screenHeight;
	float scrollingSpeed = 10.0f;
	
	enum Action {
		CenterFromLeft, 
		CenterFromRight, 
		CenterFromUp, 
		CenterFromDown, 
		MoveLeft, 
		MoveRight, 
		MoveUp, 
		MoveDown, 
		None
	};
	Action currentAction = Action.None;
	
	void Start () {
		screenWidth = Camera.main.GetScreenWidth();
		screenHeight = Camera.main.GetScreenHeight();
		
		if (menu == null) Debug.Log("Menu is not found :(");
		else {
			defaultMenuPos = menu.transform.position;
		}
	}
	
	void Play() {
		Application.LoadLevel("Main_Game_Scene");
	}
	
	#region Showing and hiding menu
	void ShowOptions() {
		Debug.Log("Showing options");
		currentAction = Action.MoveLeft;
	}
	
	void HideOptions() {
		Debug.Log("Hiding options");
		currentAction = Action.CenterFromLeft;
	}
	
	void ShowUpgrades() {
		Debug.Log("Showing upgrades");
		SelectGunTower();
		currentAction = Action.MoveRight;
	}
	
	void HideUpgrades() {
		Debug.Log("Hiding upgrades");
		currentAction = Action.CenterFromRight;
	}
	
	#endregion
	#region Upgrading
	void UpgradeRange() {
		if (towerNameLabel.GetComponent<UILabel>().text == "Gun") {
			if (Settings.money > Mathf.Pow(2, Settings.gunTower.range)){
				Settings.money -= (int)Mathf.Pow(2, Settings.gunTower.range);
				Settings.gunTower.range++;
				rangeLabel.GetComponent<UILabel>().text = "Range: " + Settings.gunTower.range.ToString();
			}
		}
		if (towerNameLabel.GetComponent<UILabel>().text == "Rifle") {
			if (Settings.money > Mathf.Pow(2, Settings.rifleTower.range)){
				Settings.money -= (int)Mathf.Pow(2, Settings.rifleTower.range);
				Settings.rifleTower.range++;
				rangeLabel.GetComponent<UILabel>().text = "Range: " + Settings.rifleTower.range.ToString();
			}
		}
		if (towerNameLabel.GetComponent<UILabel>().text == "Minigun") {
			if (Settings.money > Mathf.Pow(2, Settings.minigunTower.range)){
				Settings.money -= (int)Mathf.Pow(2, Settings.minigunTower.range);
				Settings.minigunTower.range++;
				rangeLabel.GetComponent<UILabel>().text = "Range: " + Settings.minigunTower.range.ToString();
			}
		}
		Debug.Log ("Upgrading");
	}
	
	void UpgradeFireRate() {
		if (towerNameLabel.GetComponent<UILabel>().text == "Gun") {
			if (Settings.money > Mathf.Pow(2, Settings.gunTower.fireRate)){
				Settings.money -= (int)Mathf.Pow(2, Settings.gunTower.fireRate);
				Settings.gunTower.fireRate++;
				fireRateLabel.GetComponent<UILabel>().text = "Fire rate: " + Settings.gunTower.fireRate.ToString();
			}
		}
		if (towerNameLabel.GetComponent<UILabel>().text == "Rifle") {
			if (Settings.money > Mathf.Pow(2, Settings.rifleTower.fireRate)){
				Settings.money -= (int)Mathf.Pow(2, Settings.rifleTower.fireRate);
				Settings.rifleTower.fireRate++;
				fireRateLabel.GetComponent<UILabel>().text = "Fire rate: " + Settings.rifleTower.fireRate.ToString();
			}
		}
		if (towerNameLabel.GetComponent<UILabel>().text == "Minigun") {
			if (Settings.money > Mathf.Pow(2, Settings.minigunTower.fireRate)){
				Settings.money -= (int)Mathf.Pow(2, Settings.minigunTower.fireRate);
				Settings.minigunTower.fireRate++;
				fireRateLabel.GetComponent<UILabel>().text = "Fire rate: " + Settings.minigunTower.fireRate.ToString();
			}
		}
		Debug.Log ("Upgrading");
	}
	
	void UpgradeDamage() {
		if (towerNameLabel.GetComponent<UILabel>().text == "Gun") {
			if (Settings.money > Mathf.Pow(2, Settings.gunTower.damage)){
				Settings.money -= (int)Mathf.Pow(2, Settings.gunTower.damage);
				Settings.gunTower.damage++;
				damageLabel.GetComponent<UILabel>().text = "Damage: " + Settings.gunTower.damage.ToString();
			}
		}
		if (towerNameLabel.GetComponent<UILabel>().text == "Rifle") {
			if (Settings.money > Mathf.Pow(2, Settings.rifleTower.damage)){
				Settings.money -= (int)Mathf.Pow(2, Settings.rifleTower.damage);
				Settings.rifleTower.damage++;
				damageLabel.GetComponent<UILabel>().text = "Damage: " + Settings.rifleTower.damage.ToString();
			}
		}
		if (towerNameLabel.GetComponent<UILabel>().text == "Minigun") {
			if (Settings.money > Mathf.Pow(2, Settings.minigunTower.damage)){
				Settings.money -= (int)Mathf.Pow(2, Settings.minigunTower.damage);
				Settings.minigunTower.damage++;
				damageLabel.GetComponent<UILabel>().text = "Damage: " + Settings.minigunTower.damage.ToString();
			}
		}
		Debug.Log ("Upgrading");
	}
	#endregion
	#region Tower selection
	void SelectGunTower() {
		
		towerNameLabel.GetComponent<UILabel>().text = "Gun";
		
		rangeLabel.GetComponent<UILabel>().text = "Range: " + Settings.gunTower.range.ToString();
		fireRateLabel.GetComponent<UILabel>().text = "Fire rate: " + Settings.gunTower.fireRate.ToString();
		damageLabel.GetComponent<UILabel>().text = "Damage: " + Settings.gunTower.damage.ToString();
		
	}
	
	void SelectRifleTower() {
		
		towerNameLabel.GetComponent<UILabel>().text = "Rifle";
		
		rangeLabel.GetComponent<UILabel>().text = "Range: " + Settings.rifleTower.range.ToString();
		fireRateLabel.GetComponent<UILabel>().text = "Fire rate: " + Settings.rifleTower.fireRate.ToString();
		damageLabel.GetComponent<UILabel>().text = "Damage: " + Settings.rifleTower.damage.ToString();
		
	}
	
	void SelectMinigunTower() {
		
		towerNameLabel.GetComponent<UILabel>().text = "Minigun";
		
		rangeLabel.GetComponent<UILabel>().text = "Range: " + Settings.minigunTower.range.ToString();
		fireRateLabel.GetComponent<UILabel>().text = "Fire rate: " + Settings.minigunTower.fireRate.ToString();
		damageLabel.GetComponent<UILabel>().text = "Damage: " + Settings.minigunTower.damage.ToString();
		
	}
	#endregion
	void Update () {
		#region Left(Options)
		if (currentAction == Action.MoveLeft){
			
			if (menu.transform.localPosition.x > -screenWidth ) 
				menu.transform.Translate(Vector3.left * scrollingSpeed * Time.deltaTime);
			else {
				menu.transform.localPosition = new Vector3(-screenWidth, 0, 0);
				currentAction = Action.None;
			}
			
		}
		
		if (currentAction == Action.CenterFromLeft && menu.transform.localPosition != Vector3.zero) {
			
			if (menu.transform.localPosition.x < 0) 
				menu.transform.Translate(Vector3.right * scrollingSpeed * Time.deltaTime);
			else {
				menu.transform.localPosition = Vector3.zero;
				currentAction = Action.None;
			}
		}
		#endregion
		#region Right(Upgrades)
		if (currentAction == Action.MoveRight){
			
			if (menu.transform.localPosition.x < screenWidth ) 
				menu.transform.Translate(Vector3.right * scrollingSpeed * Time.deltaTime);
			else {
				menu.transform.localPosition = new Vector3(screenWidth, 0, 0);
				currentAction = Action.None;
			}
			
		}
		
		if (currentAction == Action.CenterFromRight && menu.transform.localPosition != Vector3.zero) {
			
			if (menu.transform.localPosition.x > 0) 
				menu.transform.Translate(Vector3.left * scrollingSpeed * Time.deltaTime);
			else {
				menu.transform.localPosition = Vector3.zero;
				currentAction = Action.None;
			}
		}
		#endregion
	}
}
