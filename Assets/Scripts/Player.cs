using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player {
	public int playerNumber;
	public int Position;// { get; set; }
	public int Health;// { get; set; }
	public bool Defending;// { get; set; }
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
		Position = Mathf.Clamp(Position + moveAmount, 0, 2);
		Debug.Log(string.Format("Player {0} Moves {1}", playerNumber, moveAmount > 0 ? "Right" : "Left"));
	}

	public void Block() {
		Defending = true;
		Debug.Log(string.Format("Player {0} Defends!", playerNumber));
	}

	public void Attack(Player enemyPlayer, int damage) {
		if(this.Position == enemyPlayer.Position && !enemyPlayer.Defending) {
			enemyPlayer.Health -= damage;
			Debug.Log(string.Format("Player {0} Attacks! Deals {1} Damage!", playerNumber, damage));
		}
		else {
			Debug.Log(string.Format("Player {0} Attacks, but it didn't work!", playerNumber));
		}
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

	public void AddActionToQueue(PlayerAction action) {
		actionQueue.Enqueue( action );
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