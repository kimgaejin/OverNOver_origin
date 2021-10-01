using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnGameText : MonoBehaviour {

	public GameObject menuPanel;
	public Text myText;
	GameObject player;

	float playtime = 0f;

	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		StartCoroutine ("InPlaytime");
		StartCoroutine ("RenewText");
	}

	void Update () {
	
	}

	IEnumerator InPlaytime()
	{
		while (true) {
			playtime += 0.1f;
			yield return new WaitForSeconds (0.1f);
		}
	}
	IEnumerator RenewText()
	{
		while (true) {
			myText.GetComponent<Text> ().text = "playtime : " + playtime;
			yield return new WaitForSeconds (0.1f);
		}
	}

}
