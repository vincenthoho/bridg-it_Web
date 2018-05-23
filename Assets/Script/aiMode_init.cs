﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class aiMode_init : MonoBehaviour {
	public GameObject redNodePrefab;
	public GameObject redEdgePrefab, redEdgePrefab_ver;
	public GameObject blueNodePrefab;
	public GameObject blueEdgePrefab, blueEdgePrefab_ver;
	public GameObject ch;
	public GameObject en;
	public GameObject bonusChar;
	public GameObject endGameCanvas;
	public GameObject blueWinText;
	public GameObject TooManyMovesText;
	public GameObject redWinText;
	public GameObject spentTimeText;
	public GameObject scoreText;
	public GameObject levelText;
	public GameObject tmp_turnText;
	public GameObject tmp_bridgeText;
	public GameObject tmp_moveText;
	public GameObject bridgeTopic;
	public GameObject moveTopic;
	public GameObject homeButton;
	public GameObject instructionButton;
	public GameObject retryButton;
	public GameObject tmp_retryText;
	public GameObject replayButton;
	public GameObject nextButton;
	public GameObject UIbar;
	public Canvas canvas;
	public GameObject UIpanel;
	public AudioClip winClaps;
	public AudioClip failSE;

	private float verticalGap;
	private float horizonalGap;
	Camera cam;
	public Transform[,] redEdgeRowArray = new Transform[5,5];
	public Transform[,] redEdgeColArray = new Transform[4,6];
	public Transform[,] blueEdgeRowArray = new Transform[6,4];
	public Transform[,] blueEdgeColArray = new Transform[5,5];
	public Transform[,] blueNodeArray = new Transform[6, 7];
	public Transform[,] redNodeArray = new Transform[7, 6];

	public static int level = 1;
	public static int score = 0;
	public static int totalLength = 3;
	public static int maxRow = 2;
	public static int maxCol = 3;
	public static int retries = 3;
	public static bool limitBridges = false;
	public static bool mustConnect = false;
	public static bool limitMoves = false;
	private static bool blinking = false;
	private static bool blinkingText = false;
	public static int blinkSpeed = 50;
	private static int counter = 0, count = 0;
	public static int nodeNo = 0;
	public static int maxBridges = 10;
	public static int blueBridges = 10;
	public static int maxMoves = 10;
	public static bool aiFirst = true;
	public static GameObject turntext;
	public static GameObject retryText;
	public static GameObject bridgeText;
	public static GameObject moveText;
	public static GameObject redoButton;

	private static ArrayList aiMoves = new ArrayList();

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		cam = GetComponent<Camera> ();
		RectTransform CanvasRect = canvas.GetComponent<RectTransform> ();
		turntext = tmp_turnText;
		redoButton = retryButton;
		retryText = tmp_retryText;
		bridgeText = tmp_bridgeText;
		moveText = tmp_moveText;

		retries = 3;

		//set UI bar to the screen top
		float x = UIbar.transform.GetComponent<RectTransform> ().anchoredPosition.x;
		float y = (CanvasRect.rect.height / 2) - 34.8f;
		UIbar.transform.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (x,y);

		aiRespond.reset ();
		blueEdgeRespond.reset ();
		character.reset ();
		enemy.reset ();
		shannon.reset ();

		blueWinText.SetActive(false);
		redWinText.SetActive (false);
		UIpanel.SetActive (false);

		totalLength = 3+nodeNo+1;
		maxRow = 2+nodeNo+1;
		maxCol = 3+nodeNo+1;

		verticalGap = 200f;
		horizonalGap = 120f;

		blueBridges = maxBridges;

		levelText.GetComponent<Text>().text = level.ToString();
		checkRedoButton ();

		if (limitBridges)
			bridgeText.GetComponent<Text> ().text = blueBridges.ToString ();
		else {
			bridgeTopic.SetActive (false);
			bridgeText.SetActive (false);
		}

		if (limitMoves)
			moveText.GetComponent<Text> ().text = maxMoves.ToString();
		else {
			moveTopic.SetActive (false);
			moveText.SetActive (false);
		}

		int i,j;

		//create red and blue edges implicitly
		redEdgeRowArray [0, 0] = redEdgePrefab.transform;
		blueEdgeRowArray [0, 0] = blueEdgePrefab.transform;

		Vector3 redEdgePos = redEdgePrefab.GetComponent<RectTransform> ().anchoredPosition3D;
		redEdgePrefab.GetComponent<RectTransform> ().anchoredPosition3D = new Vector3 (redEdgePos.x - (nodeNo * 0.5f * verticalGap), redEdgePos.y + (nodeNo * 0.5f * horizonalGap), redEdgePos.z);
		redEdgePos = redEdgePrefab.GetComponent<RectTransform> ().anchoredPosition3D;
		Vector3 redEdgePos_ver = redEdgePrefab_ver.GetComponent<RectTransform> ().anchoredPosition3D;
		redEdgePrefab_ver.GetComponent<RectTransform> ().anchoredPosition3D = new Vector3(redEdgePos_ver.x - (nodeNo * 0.5f * verticalGap), redEdgePos_ver.y + (nodeNo * 0.5f * horizonalGap), 0);
		redEdgePos_ver = redEdgePrefab_ver.GetComponent<RectTransform> ().anchoredPosition3D;
		Vector3 blueEdgePos = blueEdgePrefab.GetComponent<RectTransform> ().anchoredPosition3D;
		blueEdgePrefab.GetComponent<RectTransform> ().anchoredPosition3D = new Vector3 (blueEdgePos.x - (nodeNo * 0.5f * verticalGap), blueEdgePos.y + (nodeNo * 0.5f * horizonalGap), blueEdgePos.z);
		blueEdgePos = blueEdgePrefab.GetComponent<RectTransform> ().anchoredPosition3D;
		Vector3 blueEdgePos_ver = blueEdgePrefab_ver.GetComponent<RectTransform> ().anchoredPosition3D;
		blueEdgePrefab_ver.GetComponent<RectTransform> ().anchoredPosition3D = new Vector3 (blueEdgePos_ver.x - (nodeNo * 0.5f * verticalGap), blueEdgePos_ver.y + (nodeNo * 0.5f * horizonalGap), 0);
		blueEdgePos_ver = blueEdgePrefab_ver.GetComponent<RectTransform> ().anchoredPosition3D;

		for (i=0; i<3+(nodeNo+1); i++) {
			for (j=0; j<3+(nodeNo+1); j++) {
				//if (!(j == 0 && i == 0)) {
					//Vector3 newRedPos = new Vector3 (redEdgePos.x + j * 90, redEdgePos.y - i * 50, redEdgePos.z);
					Vector3 newRedPos = new Vector3 (redEdgePos.x + j * verticalGap, redEdgePos.y - i * horizonalGap, redEdgePos.z);
					//redEdgeRowArray [i, j] = Instantiate (redEdgePrefab, new Vector2 (redEdgePrefab.transform.position.x + j * 12, redEdgePrefab.position.y - i * 10), Quaternion.identity) as Transform;
					GameObject newRedEdge = Instantiate(redEdgePrefab, newRedPos, redEdgePrefab.transform.rotation);
					newRedEdge.transform.SetParent (redEdgePrefab.transform.parent, false);
					redEdgeRowArray[i,j] = newRedEdge.transform;
					newRedEdge.GetComponent<redEdgeRespond> ().setMode (0);
				//}
				Vector3 newBluePos = new Vector3 (blueEdgePos_ver.x + j * verticalGap, blueEdgePos_ver.y - i * horizonalGap, 0);
				//blueEdgeColArray[i,j] = Instantiate (blueEdgePrefab, new Vector2 (-24 + j * 12, 20 - i * 10), Quaternion.Euler(0,0,90)) as Transform;
				GameObject newBlueEdge = Instantiate(blueEdgePrefab_ver, newBluePos, Quaternion.Euler(0,0,90));
				newBlueEdge.transform.SetParent (blueEdgePrefab.transform.parent, false);
				blueEdgeColArray[i,j] = newBlueEdge.transform.GetChild(0).transform;
				newBlueEdge.GetComponent<blueEdgeRespond> ().setMode (0);
				
				//blueEdgeColArray[i,j].GetComponent<Renderer>().enabled = false;
				//redEdgeRowArray[i,j].GetComponent<Renderer>().enabled = false;
				changeAlpha(0, blueEdgeColArray[i,j]);
				changeAlpha(0, redEdgeRowArray [i, j]);

				
				contactBlueEdge(blueEdgeColArray[i,j],i,j,"Col");
			}
		}

		for (i=0; i<2+(nodeNo+1); i++) {
			for (j=0; j<4+(nodeNo+1); j++) {
				//redEdgeColArray [i, j] = Instantiate (redEdgePrefab, new Vector2 (-30 + j * 12, 15 - i * 10), Quaternion.Euler(0,0,90)) as Transform;
				GameObject newRedEdge = Instantiate(redEdgePrefab_ver, new Vector3(redEdgePos_ver.x + j * verticalGap, redEdgePos_ver.y - i * horizonalGap, 0), Quaternion.Euler(0,0,90));
				newRedEdge.transform.SetParent (redEdgePrefab.transform.parent, false);
				redEdgeColArray[i,j] = newRedEdge.transform;
				newRedEdge.GetComponent<redEdgeRespond> ().setMode (0);
				//redEdgeColArray [i, j].GetComponent<Renderer>().enabled = false;

				//if (!(j==0 && i == 0)){
					//blueEdgeRowArray [j,i] = Instantiate (blueEdgePrefab, new Vector2 (blueEdgePrefab.position.x + i * 12, blueEdgePrefab.position.y - j * 10), Quaternion.identity) as Transform;
					Vector3 newBluePos = new Vector3(blueEdgePos.x + i * verticalGap, blueEdgePos.y - j * horizonalGap, blueEdgePos.z);
					GameObject newBlueEdge = Instantiate (blueEdgePrefab, newBluePos, Quaternion.identity);
					newBlueEdge.transform.SetParent (blueEdgePrefab.transform.parent, false);
					blueEdgeRowArray [j, i] = newBlueEdge.transform.GetChild(0).transform;
					newBlueEdge.GetComponent<blueEdgeRespond> ().setMode (0);
				//}
				//blueEdgeRowArray [j,i].GetComponent<Renderer>().enabled = false;

				//Connect the edges on two side
				if (j == 0 || j == 4 + nodeNo) {
					changeAlpha (255, blueEdgeRowArray [j, i]);
					changeAlpha (255, redEdgeColArray [i, j]);
				} else {
					changeAlpha (0, blueEdgeRowArray [j, i]);
					changeAlpha (0, redEdgeColArray [i, j]);
				}
				
				contactBlueEdge(blueEdgeRowArray[j,i],j,i,"Row");
			}
		}

		aiRespond.setColArray (redEdgeColArray);
		aiRespond.setRowArray (redEdgeRowArray);

		Vector3 redNodePos = redNodePrefab.GetComponent<RectTransform> ().anchoredPosition3D;
		redNodePrefab.GetComponent<RectTransform> ().anchoredPosition3D = new Vector3 (redNodePos.x - (nodeNo * 0.5f * verticalGap), redNodePos.y + (nodeNo * 0.5f * horizonalGap), redNodePos.z);
		redNodePos = redNodePrefab.GetComponent<RectTransform> ().anchoredPosition3D;
		redNodePrefab.transform.SetAsLastSibling ();
		redNodeArray [0, 0] = redNodePrefab.transform;
		Vector3 blueNodePos = blueNodePrefab.GetComponent<RectTransform> ().anchoredPosition3D;
		blueNodePrefab.GetComponent<RectTransform> ().anchoredPosition3D = new Vector3 (blueNodePos.x - (nodeNo * 0.5f * verticalGap), blueNodePos.y + (nodeNo * 0.5f * horizonalGap), blueNodePos.z);
		blueNodePos = blueNodePrefab.GetComponent<RectTransform> ().anchoredPosition3D;
		blueNodePrefab.transform.SetAsLastSibling ();
		blueNodeArray [0, 0] = blueNodePrefab.transform;

		//Debug.Log ("Original red node pos: " + redNodePrefab.transform.position.x + ", " + redNodePrefab.transform.position.y + ", " + redNodePrefab.transform.position.z);
		//Debug.Log ("Original red node rect pos: " + redNodePos.x + ", " + redNodePos.y + ", " + redNodePos.z);
		//create red nodes
		for (i = 0; i <= 2+(nodeNo+1); i++) {
			for (j = 0; j <=3+(nodeNo+1); j++) {
				if (!(j == 0 && i == 0)) {
					//Instantiate (redNodePrefab, new Vector2 (redNodePrefab.position.x + j * 12, redNodePrefab.position.y - i * 10), Quaternion.identity);
					Vector3 newPos = new Vector3 (redNodePos.x + j * verticalGap, redNodePos.y - i * horizonalGap, redNodePos.z);
					//Debug.Log ("New node " + i + "," + j + ": " + newPos.x + ", " + newPos.y + ", " + newPos.z);
					GameObject newRedNode = Instantiate (redNodePrefab, newPos, redNodePrefab.transform.rotation);
					newRedNode.transform.SetParent (redNodePrefab.transform.parent, false);
					newRedNode.GetComponent<RectTransform> ().anchoredPosition = newPos;
					redNodeArray [i, j] = newRedNode.transform;
				}
			}
		}
		//create blue nodes
		for (i = 0; i <= 3+(nodeNo+1);i++)
		{
			for (j = 0; j <=2+(nodeNo+1); j++)
			{
				if (!(j==0 && i == 0)){
					//Instantiate(blueNodePrefab,new Vector2(blueNodePrefab.transform.position.x + j*12,blueNodePrefab.transform.position.y - i*10),Quaternion.identity);
					Vector3 newPos = new Vector3 (blueNodePos.x + j * verticalGap, blueNodePos.y - (i * horizonalGap), blueNodePos.z);
					//Debug.Log ("New node " + i + "," + j + ": " + newPos.x + ", " + newPos.y + ", " + newPos.z);
					GameObject newBlueNode = Instantiate (blueNodePrefab, newPos, blueNodePrefab.transform.rotation);
					newBlueNode.transform.SetParent (blueNodePrefab.transform.parent, false);
					newBlueNode.GetComponent<RectTransform> ().anchoredPosition = newPos;
					blueNodeArray [i, j] = newBlueNode.transform;
				}	
			}
		}

		blueEdgeRespond.setNodeArray (blueNodeArray);
		aiRespond.setNodeArray (redNodeArray);

		//if bonus node needed
		if (mustConnect) {
			Debug.Log ("<color=red>mustConnect</color>");
			foreach(int nodeIndex in blueEdgeRespond.bonusNode){
				Debug.Log ("<color=red>"+nodeIndex+"</color>");
				int size = nodeNo + 4;
				int rowIndex = nodeIndex / size;
				int colIndex = nodeIndex % size;
				Vector3 charPos = blueNodeArray [rowIndex, colIndex].GetComponent<RectTransform>().anchoredPosition;
				Vector3 newCharPos = new Vector3 (charPos.x, charPos.y + 20, charPos.z);
				GameObject bc = Instantiate (bonusChar, newCharPos, bonusChar.transform.rotation);
				bc.transform.SetParent (bonusChar.transform.parent, false);
				bc.GetComponent<RectTransform> ().anchoredPosition = newCharPos;
			}
		}

		ch.transform.SetAsLastSibling ();
		en.transform.SetAsLastSibling ();

		if (aiFirst)
			aiRespond.raiseTurn (0, 0, "");
		else
			blueEdgeRespond.raiseTurn ();
	}

	void Update(){
		//Time.timeScale = 1;
		/*
		if (limitBridges && blueBridges < 2 && !blinking)
			StartCoroutine (blinkText());
		if (limitMoves && limitBridges && !blinkingText)
			StartCoroutine (switchText ());
		*/

		if (limitMoves && limitBridges) {
			if (counter == blinkSpeed) {
				blinkingText = !blinkingText;
				if (blinkingText) {
					moveTopic.SetActive (false);
					bridgeTopic.SetActive (true);
				} else {
					bridgeTopic.SetActive (false);
					moveTopic.SetActive (true);
				}
				counter = 0;
			}
		}

		counter++;
	}

	private IEnumerator blinkText(){
		blinking = true;
		Debug.Log ("<color=red>blink</color>");
		while (blueBridges < 2) {
			if (bridgeTopic.activeSelf) {
				bridgeText.SetActive (false);
				yield return new WaitForSeconds (0.5f);
				bridgeText.SetActive (true);
				yield return new WaitForSeconds (0.5f);
			}
		}
		blinking = false;
	}

	private IEnumerator switchText(){
		blinkingText = true;
		Debug.Log ("<color=red>blinking text</color>");
		while (true) {
			Debug.Log ("<color=red>blinking text1</color>");
			moveTopic.SetActive (false);
			bridgeTopic.SetActive (true);
			yield return new WaitForSeconds (1.5f);
			Debug.Log ("<color=red>blinking text2</color>");
			bridgeTopic.SetActive (false);
			moveTopic.SetActive (true);
			yield return new WaitForSeconds (1.5f);
		}
	}

	public static void updateMoves(){
		if((maxMoves - blueEdgeRespond.moveCount) >= 0)
			moveText.GetComponent<Text>().text = (maxMoves - blueEdgeRespond.moveCount).ToString();
		else
			moveText.GetComponent<Text>().text = "0";
	}

	public static void setLevel(int lv){
		level = lv; 
	}

	//set board size -> 5x4 as 0, 6x5 as 1
	public static void setBoardSize(int size){
		nodeNo = size;
	}

	public static void setLimitBridge(int bridgesNo){
		limitBridges = true;
		maxBridges = bridgesNo;
		aiRespond.setBrisgesNo (bridgesNo);
	}

	public static void setMustConnect(int mustNodesNo){
		Debug.Log ("<color=red>"+mustNodesNo+"</color>");
		mustConnect = true;
		int i = 0;
		int min = 4 + nodeNo;
		int max = (5 + nodeNo)*min-min;
		blueEdgeRespond.bonusNode.Clear ();
		while(i < mustNodesNo) {
			int randomNode = Random.Range (min, max);
			Debug.Log ("<color=red>"+randomNode+"</color>");
			if (!blueEdgeRespond.bonusNode.Contains (randomNode)) {
				blueEdgeRespond.bonusNode.Add (randomNode);
				i++;
			}
		}
	}

	public static void setLimitMoves(int moves){
		limitMoves = true;
		maxMoves = moves;
	}

	public static void checkRedoButton(){
		if (blueEdgeRespond.blueMoves.Count <= 0 || retries <= 0) {
			redoButton.GetComponent<Button> ().interactable = false;
		} else {
			redoButton.GetComponent<Button> ().interactable = true;
		}
	}

	private void changeAlpha(int value, Transform go){
		Color temp = go.GetComponent<Image> ().color;
		temp.a = value;
		go.GetComponent<Image> ().color = temp;
	}

	public static void useBlueBridge(){
		blueBridges--;
		bridgeText.GetComponent<Text> ().text = "" + blueBridges;
		if (blueBridges < 2){
			bridgeText.GetComponent<Text> ().color = new Color32(0xED, 0x1C, 0x24, 0xFF); //red
		}else
			bridgeText.GetComponent<Text> ().color = new Color32(0x82, 0x82, 0x82, 0xFF); //grey
	}

	public static void removeBlueBridge(){
		blueBridges++;
		bridgeText.GetComponent<Text> ().text = "" + blueBridges;
		if (blueBridges >= 2)
			bridgeText.GetComponent<Text> ().color = new Color32(0x82, 0x82, 0x82, 0xFF); //grey
	}

	public static void redo(){
		blueEdgeRespond.redo ();
		retries--;
		retryText.GetComponent<Text> ().text = "" + retries;

		if (blueEdgeRespond.blueMoves.Count <= 0 || retries <= 0) {
			redoButton.GetComponent<Button> ().interactable = false;
		}
	}

	public static void switchTurn(string s){
		switch (s) {
			case "ai":
				redoButton.GetComponent<Button> ().interactable = false;
				turntext.GetComponent<Text> ().text = "AI's turn...";
				turntext.GetComponent<Text> ().color = new Color32(0xED, 0x1C, 0x24, 0xFF); //ED1C24FF red
				break;
			case "player":
				checkRedoButton ();
				turntext.GetComponent<Text> ().text = "Your turn..."; 
				turntext.GetComponent<Text> ().color = new Color32(0x00, 0xA2, 0xE8, 0xFF); //00A2E8FF blue
				break;
		}
	}

	private void contactBlueEdge(Transform edge, int row, int col, string role){
		Debug.Log ("Contact edge " + row + ", " + col);
		blueEdgeRespond temp = (blueEdgeRespond)edge.GetComponent (typeof(blueEdgeRespond));
		temp.setRole (role);
		temp.setRow (row);
		temp.setCol (col);
	}

	public void disableButtons(){
		retryButton.GetComponent<Button> ().interactable = false;
		homeButton.GetComponent<Button> ().interactable = false;
		instructionButton.GetComponent<Button> ().interactable = false;
	}

	public int setScore(){
		int baseTimeScore = 100;
		int baseMoveScore = 30;

		float deductScoreByTime = 0.5f;
		float deductScoreByMove = 0.5f;

		int timeScore = (int)(baseTimeScore - (timer.getTime () * deductScoreByTime));
		Debug.Log ("<color=red> Time Score:" + timeScore + "</color>");

		int moveScore = (int)(baseMoveScore - (blueEdgeRespond.stepCount - totalLength) * deductScoreByMove);
		Debug.Log ("<color=red> Move Score:" + moveScore + "</color>");

		Debug.Log ("<color=red>Collected " + blueEdgeRespond.collectedBonusNode + " bonus nodes</color>");
		int bonusScore = 20 * (blueEdgeRespond.collectedBonusNode / 2);

		int retryScore = retries * 10;
		Debug.Log ("<color=red>Retry Score:" + retryScore + "</color>");

		int score = timeScore + moveScore + bonusScore + retryScore;
		if (score < 0)
			score = 0;

		return score;

		/*
		if (limitMoves) {
			if (timer.getTime () <= 30)
				baseScore = 1000 + 100 * maxMoves;
			else if (timer.getTime () <= 60)
				baseScore = 500 + 100 * maxMoves;
			else
				baseScore = 100 + 100 * maxMoves;
		} else {
			if (timer.getTime () <= 30)
				baseScore = 1000;
			else if (timer.getTime () <= 60)
				baseScore = 500;
			else
				baseScore = 100;
		}
		*/
		//score = baseScore - 100*(blueEdgeRespond.stepCount-totalLength) + 200*(blueEdgeRespond.collectedBonusNode/2);
	}

	public void endgame(string winner){
		if (winner.Equals ("blue")) {
			int s = PlayerPrefs.GetInt ("score");

			//update cleared level and score when playing new level
			if (level == PlayerPrefs.GetInt("clearedLevel")) {
				s += setScore ();
				PlayerPrefs.SetInt ("score", s);
				PlayerPrefs.SetInt ("clearedLevel", level + 1);
				PlayerPrefs.Save ();
			}

			scoreText.GetComponent<Text> ().text = "Score: " + s;
		}

		if (character.endMove || enemy.endMove) {
			UIpanel.SetActive (true);
		
			if (winner.Equals ("blue")) {
				if (limitMoves && blueEdgeRespond.moveCount > maxMoves) {
					AudioSource.PlayClipAtPoint (failSE, Camera.main.transform.position);
					blueWinText.SetActive (false);
					TooManyMovesText.SetActive (true);
					replayButton.SetActive (true);
					nextButton.SetActive (false);
				} else {
					AudioSource.PlayClipAtPoint (winClaps, Camera.main.transform.position);
					blueWinText.SetActive (true);
					TooManyMovesText.SetActive (false);
					replayButton.SetActive (false);
					nextButton.SetActive (true);
				}
				spentTimeText.SetActive (true);
				scoreText.SetActive (true);
				spentTimeText.GetComponent<Text> ().text = "Time used: " + timer.getTime() + " seconds";
			} else if (winner.Equals ("red")) {
				AudioSource.PlayClipAtPoint (failSE, Camera.main.transform.position);
				redWinText.SetActive (true);
				spentTimeText.SetActive (false);
				scoreText.SetActive (false);
				replayButton.SetActive (true);
				nextButton.SetActive (false);
			}
		}
	}

	public static void reset(){
		Time.timeScale = 1;
		nodeNo = 0;
		limitBridges = false;
		limitMoves = false;
		mustConnect = false;
		blinking = false;
		blinkingText = false;
		level = 1;
		score = 0;
		retries = 3;
	}
}
