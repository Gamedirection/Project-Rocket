using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerView : MonoBehaviour {

	public GameManager manager;
	public Transform player1Model;
	public Transform player2Model;

	public Vector3[] player1Positions;
	public Vector3[] player2Positions;

	public Text player1MovesText, player2MovesText;

	void Update () {
		player1Model.position = Vector3.Lerp(player1Model.position, player1Positions[manager.player1.Position], Time.deltaTime * 5f);
		player2Model.position = Vector3.Lerp(player2Model.position, player2Positions[manager.player2.Position], Time.deltaTime * 5f);

		player1MovesText.text = "Moves:" + new string('▣', manager.player1.actionQueue.Count);
		player2MovesText.text = new string('▣', manager.player2.actionQueue.Count) + ":Moves";
	}
}
