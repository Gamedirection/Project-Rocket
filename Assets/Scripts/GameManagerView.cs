using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerView : MonoBehaviour {

	public GameManager manager;
	public Transform player1Model;
	public Transform player2Model;

	public Transform shieldModel;
	private Transform p1Shield, p2Shield;

	public Vector3[] player1Positions;
	public Vector3[] player2Positions;

	public Text player1MovesText, player2MovesText, timeLeft, textLog;

	private void Awake() {
		p1Shield = Instantiate(shieldModel, player1Model.position, Quaternion.identity);
		p2Shield = Instantiate(shieldModel, player2Model.position, Quaternion.identity);
		p1Shield.gameObject.SetActive(false);
		p2Shield.gameObject.SetActive(false);
		p1Shield.SetParent(player1Model);
		p2Shield.SetParent(player2Model);
	}

	void Update () {
		player1Model.position = Vector3.Lerp(player1Model.position, player1Positions[manager.player1.Position], Time.deltaTime * 5f);
		player2Model.position = Vector3.Lerp(player2Model.position, player2Positions[manager.player2.Position], Time.deltaTime * 5f);

		player1MovesText.text = "Phases:" + new string('▣', manager.player1.actionQueue.Count);
		player2MovesText.text = new string('▣', manager.player2.actionQueue.Count) + ":Phases";

		p1Shield.gameObject.SetActive(manager.player1.Defending);
		p2Shield.gameObject.SetActive(manager.player2.Defending);

		timeLeft.text = manager.gameMode == 
				GameManager.GamePlayState.Executing && manager.gameType == GameManager.GameType.Speed 
				? (manager.storedTimestamp - Time.realtimeSinceStartup).ToString("F2") : string.Empty;

		//Console Log. Display the last 3 lines.
		string logLines = "";
		for(int i = 0; i < Mathf.Min(2,DisplayGameLog.loggedStrings.Count); i++)
			logLines += DisplayGameLog.loggedStrings[DisplayGameLog.loggedStrings.Count-1-i] + "\n";
		textLog.text = logLines;
	}
}
