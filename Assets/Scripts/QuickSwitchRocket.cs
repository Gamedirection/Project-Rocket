using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSwitchRocket : MonoBehaviour {

	public GameObject[] shipModels;

	void Awake() {
		int randomShip = Random.Range(0,2);
		if(Random.value > 0.95f)
			randomShip = 2;
		for(int i = 0; i < shipModels.Length; i++) {
			shipModels[i].SetActive(randomShip == i);
		}
	}

}
