using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UglyExplosion : MonoBehaviour {

	public UglyExplosion explosionPrefab;
	public int subExplosionCount = 5;
	public float persistTime = 2f;
	public float spawnNextTime = 0.1f;
	public float scaleOfNext = 1.25f;

	private void Start() {
		Destroy(gameObject, persistTime);
		Invoke("MakeSubExplosion", spawnNextTime);
	}

	void MakeSubExplosion() {
		if(subExplosionCount > 0) {
			var subExplosion = Instantiate(explosionPrefab, transform.position, transform.rotation) as UglyExplosion;
			subExplosion.transform.localScale *= scaleOfNext;
			subExplosion.subExplosionCount = subExplosionCount-1;
		}
	}

	private void Update() {
		transform.LookAt(Camera.main.transform.position);
	}
}
