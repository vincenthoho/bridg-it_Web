using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class csAPIController : MonoBehaviour {

	public static IEnumerator Post(int steps, int level, int duration, int redos, int movesAllowed, string question) {
		Debug.Log ("<color=white>Upload Score</color>");
		WWWForm form = new WWWForm();

		string name = PlayerPrefs.GetString("username");
		if (name != null) { form.AddField("name", "CSC2018-S" + name.ToLower()); }
		form.AddField("steps", steps);
		form.AddField("level", level);
		form.AddField("duration", duration);
		form.AddField("redos", redos);
		form.AddField("moves_allowed", movesAllowed);
		form.AddField("question", question);
		form.AddField("score", PlayerPrefs.GetInt("score"));
		form.AddField ("udid", "CSC2018-BridgIt");

		UnityWebRequest request = UnityWebRequest.Post("https://www.algebragamification.com/api/v3/csc/scores", form);
		yield return request.Send();

		/*
		if (request.isNetworkError) {
			Debug.Log("Error posting score: " + request.error);
		}
		*/
	}

}