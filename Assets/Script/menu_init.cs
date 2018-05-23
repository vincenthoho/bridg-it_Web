using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menu_init : MonoBehaviour {

	public GameObject instructionPanel;
	public GameObject[] levelButtons = new GameObject[2];
	public Sprite[] disabledButtons = new Sprite[2];
	public Sprite[] instructionPage = new Sprite[2];
	public GameObject aiToogle;
	public GameObject playerToogle;
	public static bool aiFirst = true;

	void Awake(){
	}

	// Use this for initialization
	void Start () {
		loadLevelButtons ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void loadLevelButtons(){
		//int currentLevels = playerData.Singleton.clearedLevel;
		int currentLevels = 1;
		if (PlayerPrefs.HasKey ("clearedLevel"))
			currentLevels = PlayerPrefs.GetInt ("clearedLevel");
		else {
			PlayerPrefs.SetInt ("clearedLevel", 1);
			PlayerPrefs.Save ();
		}

		for (int i = 0; i < levelButtons.Length; i++) {
			if (i < currentLevels) {
				levelButtons [i].GetComponent<Button> ().interactable = true;
			} else {
				levelButtons [i].GetComponent<Image> ().sprite = disabledButtons [i];
				levelButtons [i].GetComponent<Button> ().interactable = false;
			}
		}
	}
}
