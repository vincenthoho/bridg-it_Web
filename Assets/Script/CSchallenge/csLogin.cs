using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class csLogin : MonoBehaviour {

	public Text idText;
	public GameObject warningText;
	public Button loginBtn;

	public int minID = 1;
	public int maxID = 106;
	public string debugID = "test";

	void Start () {
	}

	public void Login() {
		this.warningText.SetActive(false);

		int iid;
		string id = this.idText.text.ToLower();

		if (id.ToLower () != this.debugID) {
			if (!int.TryParse (id, out iid)) {
				this.warningText.SetActive (true);
				return;
			}
			if (iid < minID || iid > maxID) {
				this.warningText.SetActive (true);
				return;
			}

			if (id != PlayerPrefs.GetString ("username")) {
				PlayerPrefs.DeleteAll (); // remove previous user's usrname, score and progress
			}
		}

		PlayerPrefs.SetString("username", id);
		PlayerPrefs.Save();

		SceneManager.LoadScene("scene_menu");
	}
}
