using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillSpear_magic1 : MonoBehaviour {

	public GameObject Spear;
	public float magicHeight;
	public int howMuchSpear;
	public int spearRandPoints;

	public float waitTime = 0.0f;
	public float stayTime = 0.0f;
	public float endTime = 0.0f;

	SpriteRenderer spr;
	Color color;

	private Vector3 magicHeightVec;
	private float magicSize;

	void Start () {
		spr = GetComponent<SpriteRenderer> ();
		color = spr.color;
		color.a = 0.0f;
		magicHeightVec = transform.position + new Vector3 (0, magicHeight, 0);
		magicSize = spr.bounds.size.x;

		StartCoroutine("Anime");
	}

	IEnumerator Anime()
	{
		WaitForSeconds wait005 = new WaitForSeconds (0.05f);
		int temp;
		int temp2;

		float randX;
		Quaternion spearRotation = Quaternion.Euler (0.0f, 0.0f, 180.0f);

		while (true) 
		{
			for (temp = 0; temp < waitTime * 20; temp++) {
				if (color.a <= 1 - (1 / (endTime * 10))) {
					color.a += 1 / (endTime * 10);
					spr.color = color;
				}
				yield return wait005;
			}

			for (temp = 0; temp < stayTime * 20; temp++) {
				yield return wait005;
				if (temp % 5 * howMuchSpear == 0) {
					for (temp2 = 0; temp2 < howMuchSpear; temp2++) {
						randX = Random.Range (0, spearRandPoints);
						randX = (randX - (spearRandPoints/2)) * (  (magicSize * 0.9f) / spearRandPoints );
						Instantiate (Spear, magicHeightVec + new Vector3(randX, 0, 0) , spearRotation);
					}
				}
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
			Destroy (this.gameObject, 0.0f);
		}

	}

}
