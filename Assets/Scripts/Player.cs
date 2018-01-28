﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player {
	public int playerNumber;
	public int Position = 0;// { get; set; }
	public int Health = 1;// { get; set; }
	public bool Defending = false;// { get; set; }
	public int maxQueueActions = 3;
	public Queue<PlayerAction> actionQueue = new Queue<PlayerAction>();

	public Player() {
		this.Health = 1;
		this.Position = 1;
		this.Defending = false;
	}

	public Player(int health, int position) {
		this.Health = health;
		this.Position = position;
		this.Defending = false;
	}

	public enum ActionType { Move, Block, Attack, Nothing }

	public void MovePlayer(int moveAmount) {
		if(Position + moveAmount > 2 || Position + moveAmount < 0)
			DisplayGameLog.LogString(string.Format("Player {0} moves {1}, but didn't go anywhere.", playerNumber, moveAmount > 0 ? "Right" : "Left"));
		else
			DisplayGameLog.LogString(string.Format("Player {0} moves {1}.", playerNumber, moveAmount > 0 ? "Right" : "Left"));

		Position = Mathf.Clamp(Position + moveAmount, 0, 2);
	}

	public void Block() {
		Defending = true;
		DisplayGameLog.LogString(string.Format("Player {0} <color=cyan>defends</color>!", playerNumber));
	}

	public void Attack(Player enemyPlayer, int damage) {
		if(this.Position == enemyPlayer.Position && !enemyPlayer.Defending) {
			enemyPlayer.Health -= damage;
			DisplayGameLog.LogString(string.Format("Player {0} attacks! <color=orange>Deals {1} Damage</color>!", playerNumber, damage));
			SpawnBullet(enemyPlayer);
			SpawnExplosion(enemyPlayer);
		}
		else if(this.Position == enemyPlayer.Position && enemyPlayer.Defending) {
			DisplayGameLog.LogString(string.Format("Player {0} attack was <color=cyan>blocked</color>!", playerNumber));
			SpawnReverseExplosion(enemyPlayer);
		}
		else {
			DisplayGameLog.LogString(string.Format("Player {0} missed!", playerNumber));
			SpawnBullet(enemyPlayer);
		}
	}

	public void SpawnBullet(Player enemyPlayer) {
		//Spawn Bullet
		var prefab = Resources.Load("Missed Shot");
		var bulletObj = Object.Instantiate(prefab) as GameObject;
		var bullet = bulletObj.transform;
		var viewObj = Object.FindObjectOfType<GameManagerView>();
		GameManagerView view = viewObj.GetComponent<GameManagerView>();
		bullet.SetParent(view.transform);
		if(enemyPlayer.playerNumber == 1) {
			bullet.position = view.transform.TransformPoint(view.player2Positions[Position]);
			bullet.LookAt(view.transform.TransformPoint(view.player1Positions[Position]));
		}
		else {
			bullet.position = view.transform.TransformPoint(view.player1Positions[Position]);
			bullet.LookAt(view.transform.TransformPoint(view.player2Positions[Position]));
		}
	}

	public void SpawnExplosion(Player enemyPlayer) {
		//Spawn explosion
		var prefab = Resources.Load("Ugly Programmer Explosion");
		var explosionObj = Object.Instantiate(prefab) as GameObject;
		var explosion = explosionObj.transform;
		var viewObj = Object.FindObjectOfType<GameManagerView>();
		GameManagerView view = viewObj.GetComponent<GameManagerView>();
		explosion.SetParent(view.transform);
		if(enemyPlayer.playerNumber == 1)
			explosion.position = view.transform.TransformPoint(view.player1Positions[enemyPlayer.Position]);
		else
			explosion.position = view.transform.TransformPoint(view.player2Positions[enemyPlayer.Position]);
	}

	public void SpawnReverseExplosion(Player enemyPlayer) {
		//Spawn explosion
		var prefab = Resources.Load("Ugly Programmer Explosion Reverse");
		var explosionObj = Object.Instantiate(prefab) as GameObject;
		var explosion = explosionObj.transform;
		var viewObj = Object.FindObjectOfType<GameManagerView>();
		GameManagerView view = viewObj.GetComponent<GameManagerView>();
		explosion.SetParent(view.transform);
		if(enemyPlayer.playerNumber == 1)
			explosion.position = view.transform.TransformPoint(view.player1Positions[enemyPlayer.Position]);
		else
			explosion.position = view.transform.TransformPoint(view.player2Positions[enemyPlayer.Position]);
	}

	public void ExecuteAction(PlayerAction action, Player enemy) {
		switch(action.actionType) {
			case ActionType.Block:
				Block();
				break;

			case ActionType.Move:
				MovePlayer(action.values[0]);
				break;

			case ActionType.Attack:
				Attack(enemy, action.values[0]);
				break;

			default:
				//Do nothing.
				break;
		}
	}

	public bool AddActionToQueue(PlayerAction action) {
		if(actionQueue.Count < maxQueueActions) {
			actionQueue.Enqueue( action );
			return true;
		}
		else {
			DisplayGameLog.LogString("Player "+ playerNumber +" cannot enter more actions.");
			return false;
		}
	}

}

[System.Serializable]
public class PlayerAction {
	public Player.ActionType actionType;
	public int[] values;

	public PlayerAction(Player.ActionType actionType, params int[] values) {
		this.actionType = actionType;
		this.values = values;
	}

	public static PlayerAction Nothing() {
		return new PlayerAction(Player.ActionType.Nothing);
	}
}

/**
public class PlayerAction {
	public delegate void PlayerWrappedCommand();
	public int priority;
	public PlayerWrappedCommand command;

	public PlayerAction(int priority, PlayerWrappedCommand command) {
		this.priority = priority;
		this.command = command;
	}

	//TODO: Create the Nothing Action once.
	public static PlayerAction Nothing() {
		return new PlayerAction(int.MinValue, delegate() { }); //Nothing.
	}

}
**/