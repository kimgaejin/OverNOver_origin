using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darkFadeIn : MonoBehaviour {

	SpriteRenderer spr;
	Color color;

	void Start () {
		spr = GetComponent<SpriteRenderer> ();
		StartCoroutine ("FadeIn");
	}

	IEnumerator FadeIn (){
		WaitForSeconds wait01 = new WaitForSeconds (0.1f);
		int i;

		while (true) {
			color = spr.color;
			for (i = 0; i < 20; i++) {
				color.a -= 0.025f;
				spr.color = color;
				yield return wait01;
			}
			yield return wait01;
		}
	}
}
