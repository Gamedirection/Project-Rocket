using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
