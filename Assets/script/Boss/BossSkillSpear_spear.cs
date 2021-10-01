using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillSpear_spear : MonoBehaviour {

	void Start () {
		StartCoroutine ("CheckWhere");
	}
	
	IEnumerator CheckWhere() {
		WaitForSeconds wait05 = new WaitForSeconds (0.5f);

		while (true) {

			if (this.transform.position.y < -10) {
				Destroy (gameObject, .0f);
			}

			yield return wait05;
		}
	}
}
