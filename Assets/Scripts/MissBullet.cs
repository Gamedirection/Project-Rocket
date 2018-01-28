using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissBullet : MonoBehaviour {

	public float speed;

	private void Awake() {
		Destroy(gameObject, 2f);
	}

	void Update () {
		transform.position += transform.forward * speed * Time.deltaTime;
	}
}
