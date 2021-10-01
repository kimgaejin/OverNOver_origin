using UnityEngine;
using System.Collections;

public class RageTransparency : MonoBehaviour {

	SpriteRenderer spr;
	private GameManager getGameManager;
	private float max= 1.0f;
	private Color color;

	void Start () {
		spr = GetComponent<SpriteRenderer> ();
		getGameManager = GameObject.Find("Managers").GetComponent ("GameManager") as GameManager;
		color = spr.color;
		StartCoroutine ("RageTransparencyControl");
	}
		
	//

	IEnumerator RageTransparencyControl()
	{
		while (true) {
			max = getGameManager.maxRage;
			color.a = (GameManager.rage / max);
			spr.color = color;
			yield return new WaitForSeconds (1.0f);
		}
	}


}
