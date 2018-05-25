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
	public int maxID = 150;
	public string debugID = "test";
	//public string[] IDList = new string[150];
	private string [][] IDList;

	void Start () {
		//IDList = {"TCCS2", "jasonccf", "lamfy", "slcss", "sccwyh", "laiyiulam", "FUHOYI", "andy328", "lmc2@lkcss.edu.hk", "MFS", "TCCS", "LST161", "klleung88", "karenpo", "pluvia","wchau2", "Yty_Swk", "SPCS2", "csklsc2", "rmajithia", "wchau", "leungto", "Jenny Kelly", "SPCS", "rguo_gsis", "ndictss", "SEM_ICT", "wcwong", "King Ling College", "chkwok","Scgaw2018", "CPCYDSS1", "hcyict", "stone2", "ylmasscomputer", "SJC1875", "tssun", "gilbert2cschallenge", "MOSSJSS", "wcwong1", "TCKo", "sunnylui2004", "CPCYDSS", "BWFLC", "myclhw", "stone", "lhw", "HTYC", "lto", "frcss", "klleung", "CSCisit", "potszyan", "plk83"};
		loadIDList();
		for (int i = 0; i < IDList.Length - 1; i++) {
			int a = 0;
			int.TryParse (GetDataByRowAndCol (i, 0).Substring (2), out a);
			Debug.Log (a + ", " + GetDataByRowAndCol (i, 1) + ", " + GetDataByRowAndCol (i, 2));
		}
	}

	private void loadIDList(){
		TextAsset binAsset = Resources.Load ("IDlist", typeof(TextAsset)) as TextAsset;
		string[] lineArray = binAsset.text.Split ("\r" [0]);
		IDList = new string [lineArray.Length][];
		for (int i = 0; i < lineArray.Length; i++)
			IDList [i] = lineArray [i].Split (',');
	}

	private string GetDataByRowAndCol(int nRow, int nCol){
		if (IDList.Length <= 0 || nRow >= IDList.Length)
			return "";
		if (nCol >= IDList [0].Length)
			return "";

		return IDList [nRow] [nCol];
	}

	private string GetDataByIdAndName(int nID, string strName){
		if (IDList.Length <= 0)
			return "";
		int nRow = IDList.Length;
		int nCol = IDList [0].Length;
		for (int i = 1; i < nRow; ++i) {
			string strID = string.Format ("\n{0}", nID);
			if (IDList [i] [0] == strID) {
				for (int j = 0; j < nCol; ++j) {
					if (IDList [0] [j] == strName)
						return IDList [i] [j];
				}
			}
		}
		return "";
	}

	public void Login() {
		this.warningText.SetActive(false);

		//int iid;
		string id = this.idText.text;

		if (id != this.debugID) {
			for (int i = 0; i < IDList.Length - 1; i++) {
				string temp_id = GetDataByRowAndCol (i, 2);
				if (temp_id.Equals (id)) {
					int iid = 0;
					int.TryParse(GetDataByRowAndCol (i, 0).Substring (2), out iid);
					id = iid.ToString ();
					if (id != PlayerPrefs.GetString ("username")) 
						PlayerPrefs.DeleteAll ();
					PlayerPrefs.SetString("username", id);
					PlayerPrefs.Save();
					SceneManager.LoadScene("scene_menu");
					return;
				}
			}
			this.warningText.SetActive (true);
			return;
		}

		PlayerPrefs.SetString("username", id);
		PlayerPrefs.Save();

		SceneManager.LoadScene("scene_menu");
		/*
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
		*/
	}
}
