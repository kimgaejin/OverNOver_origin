using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class reviveText : MonoBehaviour {

	public Text myText;

	string[] textList = new string [8];
	int textSize;
	
	void Start() {
		textSize = 4;

		int temp = 0;

		textList[temp] = "이번에도 실패네. 너희들 요즘 일 제대로 안하지?";
		temp++;

		textList[temp] = "또 저거 치울 생각하니 치가 떨리네.";
		temp++;

		textList[temp] = "이러다 성 안에 해골만 가득 차겠어.";
		temp++;

		textList[temp] = "차라리 해골들에게 기회를 더 주는게 나을까?";
		temp++;

		temp = Random.Range (0, textSize);
		myText.text = textList [temp];
	}
}
