using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuGUIElements : MonoBehaviour {

	public Image attackButton, shieldButton, joystick, onOffButton;
	public Sprite[] attackButtonFrames, shieldButtonFrames, joystickButtonLeftFrames, joystickButtonRightFrames, onOffButtonFrames;

	public void guiInteraction(Image element, Sprite[] buttonFrames, float time) {
		StartCoroutine(guiInteractionAnimation(element, buttonFrames, time));
	}

	public void pressAttackButton() {
		guiInteraction(attackButton, attackButtonFrames, 0.05f);
	}

	public void pressShieldButton() {
		guiInteraction(shieldButton, shieldButtonFrames, 0.05f);
	}

	public void pressJoystickLeft() {
		guiInteraction(joystick, joystickButtonLeftFrames, 0.05f);
	}

	public void pressJoystickRight() {
		guiInteraction(joystick, joystickButtonRightFrames, 0.05f);
	}


	IEnumerator guiInteractionAnimation(Image element, Sprite[] buttonFrames, float time) {
		element.sprite = buttonFrames[0];
		yield return new WaitForSeconds(time);
		element.sprite = buttonFrames[1];
	}
}
