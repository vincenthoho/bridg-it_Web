using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class csLogin : MonoBehaviour {

	public Text idText;
	public GameObject warningText;
	public Button loginBtn;

	public string debugID = "test";

	public Dictionary<string, int> dict = new Dictionary<string, int>();

	void Awake() {
		string jsonStr = Resources.Load<TextAsset>("teams").text;
		JSONObject json = new JSONObject(jsonStr);

		foreach(JSONObject team in json.list) {
			string numStr = team["Team #"].str;
			string id = team["Team ID"].str.ToLower();

			numStr = numStr.Replace("S", "");
			int num;

			if (!int.TryParse(numStr, out num)) {
				break;
			}

			this.dict.Add(id, num);
		}
	}

	int id2Num(string id) {
		int num;
		if (!dict.TryGetValue(id, out num)) {
			return 0;
		}
		return num;
	}

	void Start () {
	}

	public void Login() {
		this.warningText.SetActive(false);

		string id = this.idText.text.ToLower();
		string numStr;

		if (id != this.debugID) {
			int num = id2Num(id);
			if (num == 0) {
				this.warningText.SetActive(true);
				return;
			}
			numStr = num.ToString();
		}
		else {
			numStr = id;
		}

		if (id != PlayerPrefs.GetString("username")) {
			PlayerPrefs.DeleteAll (); // remove previous user's usrname, score and progress
		}

		PlayerPrefs.SetString("username", numStr);
		PlayerPrefs.Save();

		SceneManager.LoadScene("scene_menu");
	}
}