using UnityEngine;
using System.Collections;

public class MenuSlider : MonoBehaviour {
	
	public GameObject menu;
	Vector3 defaultMenuPos;
	float screenWidth;
	float screenHeight;
	float scrollingSpeed = 10.0f;
	
	enum Action {CenterFromLeft, CenterFromRight, CenterFromUp, CenterFromDown, MoveLeft, MoveRight, MoveUp, MoveDown, None};
	Action currentAction = Action.None;
	
	void Start () {
		screenWidth = Camera.main.GetScreenWidth();
		screenHeight = Camera.main.GetScreenHeight();
		
		if (menu == null) Debug.Log("Menu is not found :(");
		else {
			defaultMenuPos = menu.transform.position;
		}
	}
	
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
		currentAction = Action.MoveRight;
	}
	
	void HideUpgrades() {
		Debug.Log("Hiding upgrades");
		currentAction = Action.CenterFromRight;
	}
	
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
