using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicForSight : MonoBehaviour {

	public float waitTime = 0.0f;
	public float hitTime = 0.0f;
	public float endTime = 0.0f;

	SpriteRenderer spr;
	Color color;

	void Start () {
		spr = GetComponent<SpriteRenderer> ();
		color = spr.color;
		color.a = 0.0f;

		StartCoroutine("Anime");
	}

	IEnumerator Anime()
	{
		WaitForSeconds wait005 = new WaitForSeconds (0.05f);
		int temp;

		while (true) 
		{
			for (temp = 0; temp < waitTime * 20; temp++) {
				if (color.a <= 1 - (1 / (endTime * 10))) {
					color.a += 1 / (endTime * 10);
					spr.color = color;
				}
				yield return wait005;
			}

			for (temp = 0; temp < hitTime * 20; temp++) {
				yield return wait005;
			}

			for (temp = 0; temp < endTime * 20; temp++) {
				if (temp >= endTime * 10) {
					if (color.a >= 1 / (endTime * 10)) {
						color.a -= 1 / (endTime * 10);
						spr.color = color;
					}
				}
				yield return wait005;
			}
			Destroy (gameObject, 0.0f);
		}

	}
}
