using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DisplayGameLog {

	public static List<string> loggedStrings = new List<string>();

	public static void ClearLog() {
		loggedStrings.Clear();
	}

	public static void LogString (string text) {
		loggedStrings.Add(text);
		Debug.Log(text);
	}

}
