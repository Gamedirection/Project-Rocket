using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickBlip : MonoBehaviour {

	public float scale = 100f;
	public float timer = 0f;
	public float duration = 1f;

	private void Awake() {
		Destroy(gameObject, duration);
	}

	private void Update() {
		timer += Time.deltaTime;
		transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * scale, timer/duration);
	}

}
