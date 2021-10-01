using UnityEngine;
using System.Collections;

public class ForPlatform : MonoBehaviour {

	Boss getBoss;

	void Start () {
		getBoss = GameObject.Find("Boss").GetComponent ("Boss") as Boss;

		//StartCoroutine ("Check");
	}

	IEnumerator Check()
	{
		while (true) {
			if (getBoss.infiniteMode == false) {
				Destroy (this, .0f);
			} else {
				yield return new WaitForSeconds (1.0f);
			}
		}
	}

}
