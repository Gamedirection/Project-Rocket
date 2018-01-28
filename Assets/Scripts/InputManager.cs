using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	//Player 1/2
	//Accept/Attack
	//Decline/Shield
	//Left/Right
	//Start?

	public static bool PressedAcceptButton(int playerNumber = 0) {
		if(playerNumber == 0)
			return Input.GetKeyDown(KeyCode.UpArrow);
		else
			return Input.GetKeyDown(KeyCode.I);
	}

	public static bool PressedDeclineButton(int playerNumber = 0) {
		if(playerNumber == 0)
			return Input.GetKeyDown(KeyCode.DownArrow);
		else
			return Input.GetKeyDown(KeyCode.K);
	}

	public static bool PressedLeftButton(int playerNumber = 0) {
		if(playerNumber == 0)
			return Input.GetKeyDown(KeyCode.LeftArrow);
		else
			return Input.GetKeyDown(KeyCode.J);
	}

	public static bool PressedRightButton(int playerNumber = 0) {
		if(playerNumber == 0)
			return Input.GetKeyDown(KeyCode.RightArrow);
		else
			return Input.GetKeyDown(KeyCode.L);
	}

	public static bool PressedStartButton(int playerNumber = 0) {
		if(playerNumber == 0)
			return Input.GetKeyDown(KeyCode.Space);
		else
			return Input.GetKeyDown(KeyCode.Return);
	}

}
