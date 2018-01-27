using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour {

	public enum MainMenuScreen { MainMenu, Instructions, Options, Credits }
	public MainMenuScreen curScreen = MainMenuScreen.MainMenu;

	public RectTransform MainMenuBar;
	public int mainMenuItem = 0;

	public RectTransform OptionsBar;

	void Update() {
		if(Input.GetKeyDown(KeyCode.LeftArrow))
			mainMenuItem = (mainMenuItem + 5 - 1) % 5;
		if(Input.GetKeyDown(KeyCode.RightArrow))
			mainMenuItem = (mainMenuItem + 1) % 5;

		//-150, -475
		float startPoint = MainMenuBar.sizeDelta.x / 5f / 2f;
		float offset = MainMenuBar.sizeDelta.x / 5f;
		Vector2 goalPosition = new Vector2( -startPoint - offset * (mainMenuItem), 0);
		MainMenuBar.anchoredPosition = Vector2.Lerp( MainMenuBar.anchoredPosition, goalPosition, Time.deltaTime * 3f);

			
	}



}
