using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour {

//	int pOneX = 2; // x locations 1,2,3
//	int pTwoX = 2;

	//game object transform

	public GameObject player1;

//		player1.transform.Translate(1, 1, 1);


	public Button attack;
	public Button block;
	public Button left;
	public Button right;

//	String action = ""; // moveleft, moveright, attack, block

//	int move = 0; // moveLeft = -1, moveRight = +1
//
	void Start () {

		Button btn1 = attack.GetComponent<Button>();
		btn1.onClick.AddListener(Move);

		Button btn2 = block.GetComponent<Button>();
		btn2.onClick.AddListener(Move);

		Button btn3 = left.GetComponent<Button>();
		btn3.onClick.AddListener(Move);

		Button btn4 = right.GetComponent<Button>();
		btn4.onClick.AddListener(Move);
	}

	void Move(){
		Debug.Log(player1.transform.position);
		Debug.Log("You have clicked the button!");
	}

//	void GamePlay(){
//		
//	}
//
//	void Comparison(){
//
//	}
//
//	void Location(oneX, twoX){
//		if(){
//		
//		}
//	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
