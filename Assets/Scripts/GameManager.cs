using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public enum GamePlayState { Selecting, Executing, Ended }
	public GamePlayState gameMode = GamePlayState.Selecting;

	public GamePlay gameplay;
	public Player player1;
	public Player player2;

	void Start() {
		gameplay = new GamePlay();
		player1 = new Player();
		player2 = new Player();

		player1.playerNumber = 1;
		player2.playerNumber = 2;

		gameplay.player1 = player1;
		gameplay.player2 = player2;
	}

	void Update() {
		if(gameMode == GamePlayState.Selecting) {
			//Each player selects 3 actions each.
			ScanPlayer1Inputs(player1);
			ScanPlayer2Inputs(player2);

			//We are done picking actions
			if(Input.GetKeyDown(KeyCode.Return)) {
				gameMode = GamePlayState.Executing;
				StartCoroutine(ExecuteMoves());
			}
		}
	}

	public void ScanPlayer1Inputs(Player player) {
		//Move Left
		if(Input.GetKeyDown(KeyCode.Alpha1)) {
			player.AddActionToQueue(new PlayerAction(Player.ActionType.Move, -1));
			Debug.Log("Player 1 Selects Move Left");
		}
		//Move Right
		else if(Input.GetKeyDown(KeyCode.Alpha2)) {
			player.AddActionToQueue(new PlayerAction(Player.ActionType.Move, 1));
			Debug.Log("Player 1 Selects Move Right");
		}
		//Attack
		else if(Input.GetKeyDown(KeyCode.Alpha3)) {
			player.AddActionToQueue(new PlayerAction(Player.ActionType.Attack, 1));
			Debug.Log("Player 1 Selects Attack");
		}
		//Defend
		else if(Input.GetKeyDown(KeyCode.Alpha4)) {
			player.AddActionToQueue(new PlayerAction(Player.ActionType.Block));
			Debug.Log("Player 1 Selects Defend");
		}
	}

	public void ScanPlayer2Inputs(Player player) {
		//Move Left
		if(Input.GetKeyDown(KeyCode.Q)) {
			player.AddActionToQueue(new PlayerAction(Player.ActionType.Move, -1));
			Debug.Log("Player 2 Selects Move Left");
		}
		//Move Right
		else if(Input.GetKeyDown(KeyCode.W)) {
			player.AddActionToQueue(new PlayerAction(Player.ActionType.Move, 1));
			Debug.Log("Player 2 Selects Move Right");
		}
		//Attack
		else if(Input.GetKeyDown(KeyCode.E)) {
			player.AddActionToQueue(new PlayerAction(Player.ActionType.Attack, 1));
			Debug.Log("Player 2 Selects Attack");
		}
		//Defend
		else if(Input.GetKeyDown(KeyCode.R)) {
			player.AddActionToQueue(new PlayerAction(Player.ActionType.Block));
			Debug.Log("Player 2 Selects Defend");
		}
	}

	IEnumerator ExecuteMoves() {
		Debug.Log("Match Start!\n");

		//Execute the actions until someone dies or we run out of moves
		while (!gameplay.AllMovesCompleted()) {
			gameplay.ExecuteTurn();

			if(gameplay.gameOver) {
				gameMode = GamePlayState.Ended;
				yield break;
			}
			else
				yield return new WaitForSeconds(1f);
		}
		gameMode = GamePlayState.Selecting;
	}
}
