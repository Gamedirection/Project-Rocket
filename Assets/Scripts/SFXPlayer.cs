using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXPlayer : MonoBehaviour {

	public static void PlaySoundEffect(string soundEffectName, float volume) {
		PlaySoundEffect(soundEffectName, volume, false);
	}

	public static void PlaySoundEffect(string soundEffectName, float volume, bool looped) {
		AudioClip clip = Resources.Load("SFX/" + soundEffectName) as AudioClip;

		//AudioSource.PlayClipAtPoint(clip, Vector3.zero, volume);

		GameObject oneShotAudio = new GameObject();
		var audioSource = oneShotAudio.AddComponent<AudioSource>();
		audioSource.clip = clip;
		audioSource.volume = volume;
		audioSource.Play();

		AudioMixer mixer = Resources.Load("Master Mix") as AudioMixer;
		audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];

		if(looped) {
			audioSource.loop = true;
			var GameManagerObject = FindObjectOfType<GameManager>();
			if(GameManagerObject != null) {
				audioSource.transform.SetParent(GameManagerObject.transform);
			}
		}
		else {
			Destroy(oneShotAudio, clip.length);
		}
	}
}
