using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class revivNumText : MonoBehaviour {

	public Text myText;
	GameManager getGameManager;

	WaitForSeconds wait050 = new WaitForSeconds(0.5f);

	void Start() {
		getGameManager = GameObject.Find ("Managers").GetComponent ("GameManager") as GameManager;
	}

	void Update()
	{
		myText.text = getGameManager.reviveNum.ToString();
	}
}
