using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {

	public static MainMenuUI MainMenuUISingleton;

	public MainMenuGUIElements guiElements;

	public enum MainMenuScreen { MainMenu, Instructions, Options, Credits, Play, Game }
	public MainMenuScreen curScreen = MainMenuScreen.MainMenu;

	public RectTransform MainMenuPanel;
	public RectTransform MainMenuBar;
	public int mainMenuItem = 0;

	public RectTransform PlayPanel;
	public Image[] gameModes;
	public Image[] playerModes;
	public int choosenGameMode = 0;
	public int choosenPlayerMode = 0;
	public Color highlightedColor;
	public int curPlayMenuSubMenu = 0;
	public Image[] playMenuArrows;

	public RectTransform InstructionsPanel;
	public Image[] instructionPages;
	public int instructionPage;

	public RectTransform OptionsPanel;
	public RectTransform OptionsBar;
	public int optionsMenuItem = 0;
	public AudioMixer audioMixer;
	public Text[] optionTexts;

	public RectTransform CreditsPanel;

	private void Awake() {
		MainMenuUISingleton = this;
	}

	private void Start() {
		GoToMenu(MainMenuScreen.MainMenu);
		UpdateOptionTexts();
	}

	void Update() {
		switch(curScreen) {
			case MainMenuScreen.MainMenu:
				MainMenuActions();
				break;

			case MainMenuScreen.Play:
				PlayMenuActions();
				break;

			case MainMenuScreen.Instructions:
				InstructionMenuActions();
				break;

			case MainMenuScreen.Options:
				OptionsMenuActions();
				break;

			case MainMenuScreen.Credits:
				CreditsMenuActions();
				break;

			default:
				break;
		}
	}

	#region Helpers
	public void GoToMenu(MainMenuScreen menu) {
		curScreen = menu;
		ClearMenus();
		ShowMenu();
	}

	void ClearMenus() {
		MainMenuPanel.gameObject.SetActive(false);
		PlayPanel.gameObject.SetActive(false);
		InstructionsPanel.gameObject.SetActive(false);
		OptionsPanel.gameObject.SetActive(false);
		CreditsPanel.gameObject.SetActive(false);
	}

	void ShowMenu() {
		switch(curScreen) {
			case MainMenuScreen.MainMenu:
				MainMenuPanel.gameObject.SetActive(true);
				break;

			case MainMenuScreen.Play:
				PlayPanel.gameObject.SetActive(true);
				break;

			case MainMenuScreen.Instructions:
				InstructionsPanel.gameObject.SetActive(true);
				break;

			case MainMenuScreen.Options:
				OptionsPanel.gameObject.SetActive(true);
				break;

			case MainMenuScreen.Credits:
				CreditsPanel.gameObject.SetActive(true);
				break;

			default:
				break;
		}
	}
	#endregion

	#region Main Menu
	void MainMenuActions() {
		if(Input.GetKeyDown(KeyCode.LeftArrow)) {
			mainMenuItem = Mathf.Max(mainMenuItem - 1, 0);
			guiElements.pressJoystickLeft();
		}
		if(Input.GetKeyDown(KeyCode.RightArrow)) {
			mainMenuItem = Mathf.Min(mainMenuItem + 1, 4);
			guiElements.pressJoystickRight();
		}

		float startPoint = MainMenuBar.sizeDelta.x / 5f / 2f;
		float offset = MainMenuBar.sizeDelta.x / 5f;
		Vector2 goalPosition = new Vector2( -startPoint - offset * (mainMenuItem), 0);
		MainMenuBar.anchoredPosition = Vector2.Lerp( MainMenuBar.anchoredPosition, goalPosition, Time.deltaTime * 3f);

		if(Input.GetKeyDown(KeyCode.UpArrow)) {
			AcceptMainMenuItem();
			guiElements.pressAttackButton();
		}
	}

	void AcceptMainMenuItem() {
		switch(mainMenuItem) {
			//Play
			case 0:
				GoToMenu(MainMenuScreen.Play);
				Debug.Log("Going to play menu");
				break;
			//Instruction
			case 1:
				GoToMenu(MainMenuScreen.Instructions);
				Debug.Log("Going to instructions");
				break;
			//Options
			case 2:
				GoToMenu(MainMenuScreen.Options);
				Debug.Log("Going to options");
				break;
			//Credits
			case 3:
				GoToMenu(MainMenuScreen.Credits);
				Debug.Log("Going to credits");
				break;
			//Quit
			case 4:
				#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
				#endif
				Application.Quit();
				break;
			default:break;
		}
	}
	#endregion

	#region Options Menu
	void AcceptOptionsMenuItem() {
		switch(optionsMenuItem) {
			//Resolution
			case 0:
				break;
			//Window/Fullscreen
			case 1:
				Screen.fullScreen = !Screen.fullScreen;
				break;
			//Master Volume
			case 2:
				float masterVolume;
				audioMixer.GetFloat("Master Volume", out masterVolume);
				masterVolume = (masterVolume - 5f) % -60f;
				audioMixer.SetFloat("Master Volume", masterVolume);
				break;
			//Music Volume
			case 3:
				float musicVolume;
				audioMixer.GetFloat("Music Volume", out musicVolume);
				musicVolume = (musicVolume - 5f) % -60f;
				audioMixer.SetFloat("Music Volume", musicVolume);
				break;
			//SFX Volume
			case 4:
				float sfxVolume;
				audioMixer.GetFloat("SFX Volume", out sfxVolume);
				sfxVolume = (sfxVolume - 5f) % -60f;
				audioMixer.SetFloat("SFX Volume", sfxVolume);
				break;
			default:
				break;
		}
		UpdateOptionTexts();
	}

	void UpdateOptionTexts() {
		float masterVolume, musicVolume, sfxVolume;
		audioMixer.GetFloat("Master Volume", out masterVolume);
		audioMixer.GetFloat("Music Volume", out musicVolume);
		audioMixer.GetFloat("SFX Volume", out sfxVolume);

		optionTexts[0].text = string.Format(Screen.currentResolution.width + "x" + Screen.currentResolution.height, 0);
		optionTexts[1].text = Screen.fullScreen ? "Fullscreen" : "Windowed";
		optionTexts[2].text = string.Format("Master Vol\n{0} dB", masterVolume);
		optionTexts[3].text = string.Format("Music Vol\n{0} dB", musicVolume);
		optionTexts[4].text = string.Format("SFX Vol\n{0} dB", sfxVolume);
	}
	#endregion

	void OptionsMenuActions() {
		if(Input.GetKeyDown(KeyCode.LeftArrow)) {
			optionsMenuItem = Mathf.Max(optionsMenuItem - 1, 0);
			guiElements.pressJoystickLeft();
		}
		if(Input.GetKeyDown(KeyCode.RightArrow)) {
			optionsMenuItem = Mathf.Min(optionsMenuItem + 1, 4);
			guiElements.pressJoystickRight();
		}

		float startPoint = OptionsBar.sizeDelta.x / 5f / 2f;
		float offset = OptionsBar.sizeDelta.x / 5f;
		Vector2 goalPosition = new Vector2( -startPoint - offset * (optionsMenuItem), 0);
		OptionsBar.anchoredPosition = Vector2.Lerp( OptionsBar.anchoredPosition, goalPosition, Time.deltaTime * 3f);

		if(Input.GetKeyDown(KeyCode.UpArrow)) {
			AcceptOptionsMenuItem();
			guiElements.pressAttackButton();
		}

		if(Input.GetKeyDown(KeyCode.DownArrow)) {
			GoToMenu(MainMenuScreen.MainMenu);
			guiElements.pressShieldButton();
		}
	}

	void InstructionMenuActions() {
		if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) {
			instructionPage = instructionPage == 0 ? 1 : 0;
			instructionPages[instructionPage].enabled = true;
			instructionPages[(instructionPage + 1) % 2].enabled = false;
		}


		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			GoToMenu(MainMenuScreen.MainMenu);
			guiElements.pressAttackButton();
		}
	}

	void CreditsMenuActions() {
		if(Input.GetKeyDown(KeyCode.UpArrow)) {
			GoToMenu(MainMenuScreen.MainMenu);
			guiElements.pressAttackButton();
		}
	}

	void PlayMenuActions() {
		playMenuArrows[0].enabled = curPlayMenuSubMenu == 0;
		playMenuArrows[1].enabled = curPlayMenuSubMenu == 1;

		if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) {
			switch(curPlayMenuSubMenu) {
				case 0:
					choosenGameMode = choosenGameMode == 0 ? 1 : 0;
					gameModes[choosenGameMode].color = Color.white;
					gameModes[(choosenGameMode + 1) % 2].color = Color.red;
					break;

				case 1:
					choosenPlayerMode = choosenPlayerMode == 0 ? 1 : 0;
					playerModes[choosenPlayerMode].color = Color.white;
					playerModes[(choosenPlayerMode + 1) % 2].color = Color.red;
					break;

				case 2:
					//choosenGameMode = choosenGameMode == 0 ? 1 : 0;
					//gameModes[choosenGameMode].color = Color.white;
					//gameModes[(choosenGameMode + 1) % 2].color = Color.red;
					break;
			}
		}

		if(Input.GetKeyDown(KeyCode.UpArrow)) {
			switch(curPlayMenuSubMenu) {
				case 0:
					curPlayMenuSubMenu = 1;
					break;

				case 1:
					curPlayMenuSubMenu = 2;
					break;

				case 2:
					curPlayMenuSubMenu = 0;
					GoToMenu(MainMenuScreen.Game);
					StartTheGame();
					break;
			}
		}

		if(Input.GetKeyDown(KeyCode.DownArrow)) {
			switch(curPlayMenuSubMenu) {
				case 0:
					GoToMenu(MainMenuScreen.MainMenu);
					break;

				case 1:
					curPlayMenuSubMenu = 0;
					break;

				case 2:
					curPlayMenuSubMenu = 1;
					break;
			}
		}
	}

	void StartTheGame() {
		var gameManagerObject = Resources.Load("GameManager");
		var manager = Instantiate(gameManagerObject) as GameManager;

		manager.gameType = (GameManager.GameType)choosenGameMode;
		manager.isPlayer2AI = choosenPlayerMode == 0;

		guiElements.pressAttackButton();
	}
}
