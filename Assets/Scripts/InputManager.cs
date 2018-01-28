using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	//Player 1/2
	//Accept/Attack
	//Decline/Shield
	//Left/Right
	//Start?

	#region Main Player Inputs
	public static bool PressedAcceptButton(int playerNumber = 0) {
		bool pressed = PressedAcceptButtonController(playerNumber);
		if(pressed) return true;
		return PressedAcceptButtonKeyboard(playerNumber);
	}

	public static bool PressedDeclineButton(int playerNumber = 0) {
		bool pressed = PressedDeclineButtonController(playerNumber);
		if(pressed) return true;
		return PressedDeclineButtonKeyboard(playerNumber);
	}

	public static bool PressedLeftButton(int playerNumber = 0) {
		bool pressed = PressedLeftButtonController(playerNumber);
		if(pressed) return true;
		return PressedLeftButtonKeyboard(playerNumber);
	}

	public static bool PressedRightButton(int playerNumber = 0) {
		bool pressed = PressedRightButtonController(playerNumber);
		if(pressed) return true;
		return PressedRightButtonKeyboard(playerNumber);
	}

	public static bool PressedStartButton(int playerNumber = 0) {
		bool pressed = PressedStartButtonController(playerNumber);
		if(pressed) return true;
		return PressedStartButtonKeyboard(playerNumber);
	}
	#endregion

	#region Controller Inputs
	private static bool PressedAcceptButtonController(int playerNumber = 0) {
		if(playerNumber == 0)
			return Input.GetKeyDown(KeyCode.UpArrow);
		else
			return Input.GetKeyDown(KeyCode.I);
	}
	private static bool PressedDeclineButtonController(int playerNumber = 0) {
		if(playerNumber == 0)
			return Input.GetKeyDown(KeyCode.DownArrow);
		else
			return Input.GetKeyDown(KeyCode.K);
	}
	private static bool PressedLeftButtonController(int playerNumber = 0) {
		if(playerNumber == 0)
			return Input.GetKeyDown(KeyCode.LeftArrow);
		else
			return Input.GetKeyDown(KeyCode.J);
	}
	private static bool PressedRightButtonController(int playerNumber = 0) {
		if(playerNumber == 0)
			return Input.GetKeyDown(KeyCode.RightArrow);
		else
			return Input.GetKeyDown(KeyCode.L);
	}
	private static bool PressedStartButtonController(int playerNumber = 0) {
		if(playerNumber == 0)
			return Input.GetKeyDown(KeyCode.Space);
		else
			return Input.GetKeyDown(KeyCode.Return);
	}
	#endregion

	#region Keyboard Fallbacks
	private static bool PressedAcceptButtonKeyboard(int playerNumber = 0) {
		if(playerNumber == 0)
			return Input.GetKeyDown(KeyCode.UpArrow);
		else
			return Input.GetKeyDown(KeyCode.W);
	}
	private static bool PressedDeclineButtonKeyboard(int playerNumber = 0) {
		if(playerNumber == 0)
			return Input.GetKeyDown(KeyCode.DownArrow);
		else
			return Input.GetKeyDown(KeyCode.S);
	}
	private static bool PressedLeftButtonKeyboard(int playerNumber = 0) {
		if(playerNumber == 0)
			return Input.GetKeyDown(KeyCode.LeftArrow);
		else
			return Input.GetKeyDown(KeyCode.A);
	}
	private static bool PressedRightButtonKeyboard(int playerNumber = 0) {
		if(playerNumber == 0)
			return Input.GetKeyDown(KeyCode.RightArrow);
		else
			return Input.GetKeyDown(KeyCode.D);
	}
	private static bool PressedStartButtonKeyboard(int playerNumber = 0) {
		if(playerNumber == 0)
			return Input.GetKeyDown(KeyCode.Space);
		else
			return Input.GetKeyDown(KeyCode.Return);
	}
	#endregion


}
