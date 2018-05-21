using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class levelModifier : MonoBehaviour {

	public void callToLevel(int lv){
		switch (lv) {
		case 1:
			setBoardSize (0);
			buttonHandler.setAiFirst (true);
			break;
		case 2:
			setBoardSize (0);
			buttonHandler.setAiFirst (false);
			break;
		case 3:
			setBoardSize (0);
			setLimitMoves (7);
			buttonHandler.setAiFirst (true);
			break;
		case 4:
			setBoardSize (0);
			setLimitMoves (7);
			buttonHandler.setAiFirst (false);
			break;
		case 5:
			setBoardSize (1);
			buttonHandler.setAiFirst (true);
			break;
		case 6:
			setBoardSize (1);
			buttonHandler.setAiFirst (false);
			break;
		case 7:
			setBoardSize (1);
			setLimitMoves (15);
			buttonHandler.setAiFirst (true);
			break;
		case 8:
			setBoardSize (1);
			setLimitMoves (15);
			buttonHandler.setAiFirst (false);
			break;
		case 9:
			setBoardSize (1);
			setLimitBridge (17);
			buttonHandler.setAiFirst (true);
			break;
		case 10:
			setBoardSize (1);
			setLimitBridge (17);
			buttonHandler.setAiFirst (false);
			break;
		case 11:
			setBoardSize (1);
			setLimitBridge (17);
			setLimitMoves (20);
			buttonHandler.setAiFirst (true);
			break;
		case 12:
			setBoardSize (1);
			setLimitBridge (17);
			setLimitMoves (20);
			buttonHandler.setAiFirst (false);
			break;
		case 13:
			setBoardSize (1);
			setLimitBridge (13);
			buttonHandler.setAiFirst (true);
			break;
		case 14:
			setBoardSize (1);
			setLimitBridge (13);
			buttonHandler.setAiFirst (false);
			break;
		case 15:
			setBoardSize (1);
			setLimitBridge (13);
			setLimitMoves (20);
			buttonHandler.setAiFirst (true);
			break;
		case 16:
			setBoardSize (1);
			setLimitBridge (13);
			setLimitMoves (20);
			buttonHandler.setAiFirst (false);
			break;
		case 17:
			setBoardSize (1);
			setLimitBridge (10);
			buttonHandler.setAiFirst (true);
			break;
		case 18:
			setBoardSize (1);
			setLimitBridge (10);
			buttonHandler.setAiFirst (false);
			break;
		case 19:
			setBoardSize (1);
			setLimitBridge (10);
			setLimitMoves (20);
			buttonHandler.setAiFirst (true);
			break;
		case 20:
			setBoardSize (1);
			setLimitBridge (10);
			setLimitMoves (20);
			buttonHandler.setAiFirst (false);
			break;
		case 21:
			setBoardSize (1);
			setLimitBridge (13);
			setMustConnect (3);
			buttonHandler.setAiFirst (true);
			break;
		case 22:
			setBoardSize (1);
			setLimitBridge (13);
			setMustConnect (3);
			buttonHandler.setAiFirst (false);
			break;
		case 23:
			setBoardSize (1);
			setLimitBridge (13);
			setMustConnect (3);
			setLimitMoves (20);
			buttonHandler.setAiFirst (true);
			break;
		case 24:
			setBoardSize (1);
			setLimitBridge (13);
			setMustConnect (3);
			setLimitMoves (20);
			buttonHandler.setAiFirst (false);
			break;
		case 25:
			setBoardSize (1);
			setLimitBridge (10);
			setMustConnect (3);
			buttonHandler.setAiFirst (true);
			break;
		case 26:
			setBoardSize (1);
			setLimitBridge (10);
			setMustConnect (3);
			buttonHandler.setAiFirst (false);
			break;
		case 27:
			setBoardSize (1);
			setLimitBridge (10);
			setMustConnect (3);
			setLimitMoves (20);
			buttonHandler.setAiFirst (true);
			break;
		case 28:
			setBoardSize (1);
			setLimitBridge (10);
			setMustConnect (3);
			setLimitMoves (20);
			buttonHandler.setAiFirst (false);
			break;
		}
	}

	public void goToNextLevel(){
		int tmp_level = aiMode_init.level+1;
		aiMode_init.reset ();
		aiMode_init.setLevel (tmp_level);
		Application.LoadLevel ("scene_ai");
		callToLevel (tmp_level);
	}

	public void setLimitMoves(int move){
		aiMode_init.setLimitMoves (move);
	}
	
	public void setBoardSize(int size){
		aiMode_init.setBoardSize (size);
	}

	public void setLimitBridge(int bridgesNo){
		aiMode_init.setLimitBridge (bridgesNo);
	}

	public void setMustConnect(int mustNodesNo){
		aiMode_init.setMustConnect (mustNodesNo);
	}

}
