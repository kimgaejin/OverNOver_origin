using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorSpecialWall : MonoBehaviour {

	GameObject Mob;

	void Start() {
		StartCoroutine ("CheckMonster");
	}

	IEnumerator CheckMonster(){
		WaitForSeconds wait01 = new WaitForSeconds (0.1f);
		Mob = GameObject.FindGameObjectWithTag ("Monster");
		while (true) {
			if (Mob.gameObject.activeInHierarchy == false) {
				gameObject.SetActive (false);
			}
			yield return wait01;
		}
	}
}
