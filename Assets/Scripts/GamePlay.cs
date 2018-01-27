using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
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
=======

[System.Serializable]
public class GamePlay {

	public Player player1;
	public Player player2;
	public bool gameOver = false;

	public void ExecuteTurn() {
		//Undefend player each turn
		player1.Defending = false;
		player2.Defending = false;

		//Get each player's action from their queues
		PlayerAction player1Action = (player1.actionQueue.Count > 0) ? player1.actionQueue.Dequeue() : PlayerAction.Nothing();
		PlayerAction player2Action = (player2.actionQueue.Count > 0) ? player2.actionQueue.Dequeue() : PlayerAction.Nothing();

		//Execute the action with the highest priority, then the other action
		if(player1Action.actionType <= player2Action.actionType) {
			player1.ExecuteAction(player1Action, player2);
			player2.ExecuteAction(player2Action, player1);
		}
		else {
			player2.ExecuteAction(player2Action, player1);
			player1.ExecuteAction(player1Action, player2);
		}

		//Did anyone die?
		EvaluateEndGame();
	}

	public void EvaluateEndGame() {
		if(player1.Health <= 0 || player2.Health <= 0) {
			gameOver = true;
			Debug.Log("\n=========Game Over!==============");
		}
	}

	public bool AllMovesCompleted() {
		if(player1.actionQueue.Count == 0 && player2.actionQueue.Count == 0) {
			Debug.Log("\nAll Moves Finished.");
			return true;
		}
		return false;
	}

>>>>>>> e03736d77f2180784e11ad7e5fe8ffd8d8a471da
}
