using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class clearText : MonoBehaviour {

	public Text myText;
	GameManager getGameManager;

	string[] textList = new string [8];
	int textSize;

	void Start() {
		textSize = 4;

		int temp = 0;

		textList[temp] = "해골들보다 조금 더 나은 수준이야... 기대는 하지마";
		temp++;

		textList[temp] = "이정도면 그럭저럭 쓸만한걸.";
		temp++;

		textList[temp] = "드디어 괜찮은게 나왔어. 이게 얼마만이야?";
		temp++;

		textList[temp] = "완벽해.";
		temp++;

		getGameManager = GameObject.Find ("Managers").GetComponent ("GameManager") as GameManager;
		if (getGameManager.reviveNum > 9) {
			temp = 0;
		} else if (getGameManager.reviveNum > 4) {
			temp = 1;
		} else if (getGameManager.reviveNum > 1) {
			temp = 2;
		} else {
			temp = 3;
		}

		myText.text = textList [temp];
	}
}
