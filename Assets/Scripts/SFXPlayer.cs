using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour {

	public static void PlaySoundEffect(string soundEffectName, float volume) {
		AudioClip clip = Resources.Load("SFX/" + soundEffectName) as AudioClip;
		AudioSource.PlayClipAtPoint(clip, Vector3.zero, volume);
	}

}
