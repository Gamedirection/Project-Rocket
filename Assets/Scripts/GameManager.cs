using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public enum GamePlayState { Selecting, Executing, Ended }
	public GamePlayState gameMode = GamePlayState.Selecting;

	public GamePlay gameplay;
	public Player player1;
	public Player player2;

	public float storedTimestamp;

	public enum GameType { Strategy, Speed }
	public GameType gameType = GameType.Strategy;


	void Start() {
		gameplay = new GamePlay();
		player1 = new Player();
		player2 = new Player();

		player1.playerNumber = 1;
		player2.playerNumber = 2;

		gameplay.player1 = player1;
		gameplay.player2 = player2;

		Debug.Log("=========Game Start=========\n");
	}

	void Update() {
		#region Faster Real-time mode
		if (gameType == GameType.Speed) {
			if(gameMode == GamePlayState.Selecting) {
				//Each player selects 3 actions each.
				ScanPlayerInputs(player1, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4);
				ScanPlayerInputs(player2, KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R);

				//Process once each player has 3 actions.
				if(Input.GetKeyDown(KeyCode.Return) && player1.actionQueue.Count >= 3 && player2.actionQueue.Count >= 3) {
					gameMode = GamePlayState.Executing;
					StartCoroutine(ExecuteFirstMovesForever());
				}
			}
			else if(gameMode == GamePlayState.Executing) {
				//Player can add commands during the game.
				ScanPlayerInputs(player1, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4);
				ScanPlayerInputs(player2, KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R);
			}
			else {
				if(Input.GetKeyDown(KeyCode.Space))
					EndGame();
			}
		}
		#endregion
		#region Turn-based mode
		else if (gameType == GameType.Strategy) {
			if(gameMode == GamePlayState.Selecting) {
				//Each player selects 3 actions each.
				ScanPlayerInputs(player1, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4);
				ScanPlayerInputs(player2, KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R);

				//Process once each player has 3 actions.
				if(Input.GetKeyDown(KeyCode.Return) && player1.actionQueue.Count >= 3 && player2.actionQueue.Count >= 3) {
					gameMode = GamePlayState.Executing;
					StartCoroutine(ExecuteMoves());
				}
			}
			else {
				if(Input.GetKeyDown(KeyCode.Space))
					EndGame();
			}
		}
		#endregion

	}

	public void ScanPlayerInputs(Player player, params KeyCode[] keys) {
		//Move Left
		if(Input.GetKeyDown(keys[0])) {
			if(player.AddActionToQueue(new PlayerAction(Player.ActionType.Move, -1)))
				Debug.Log("Player "+ player.playerNumber +" Selects Move Left");
		}
		//Move Right
		else if(Input.GetKeyDown(keys[1])) {
			if(player.AddActionToQueue(new PlayerAction(Player.ActionType.Move, 1)))
				Debug.Log("Player "+ player.playerNumber +" Selects Move Right");
		}
		//Attack
		else if(Input.GetKeyDown(keys[2])) {
			var attackingAction = player.actionQueue.Where(item => item.actionType == Player.ActionType.Attack);
			if(attackingAction.Count() <= 0) {
				if (player.AddActionToQueue(new PlayerAction(Player.ActionType.Attack, 1)))
					Debug.Log("Player "+ player.playerNumber +" Selects Attack");
			}
			else {
				Debug.Log("Player "+ player.playerNumber +" cannot use Attack again this turn.");
			}
		}
		//Defend
		else if(Input.GetKeyDown(keys[3])) {
			var blockingAction = player.actionQueue.Where(item => item.actionType == Player.ActionType.Block);
			if(blockingAction.Count() <= 0) {
				if(player.AddActionToQueue(new PlayerAction(Player.ActionType.Block)))
					Debug.Log("Player "+ player.playerNumber +" Selects Defend");
			}
			else {
				Debug.Log("Player "+ player.playerNumber +" cannot use Defend again this turn.");
			}
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

	IEnumerator ExecuteFirstMovesForever() {
		Debug.Log("Match Start!\n");

		//Execute the actions one at a time forever.
		while (!gameplay.gameOver) {
			gameplay.ExecuteTurn();

			if(gameplay.gameOver) {
				gameMode = GamePlayState.Ended;
				yield break;
			}
			else {
				storedTimestamp = Time.realtimeSinceStartup + 3f;
				yield return new WaitForSeconds(3f);
			}
		}
		//gameMode = GamePlayState.Selecting;
	}

	public void EndGame() {
		Destroy(this.gameObject);
		MainMenuUI.MainMenuUISingleton.GoToMenu(MainMenuUI.MainMenuScreen.MainMenu);
	}
}
