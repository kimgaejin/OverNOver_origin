using UnityEngine;
using System.Collections;

public class WithPlayerScript : MonoBehaviour {

	GameObject player;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update () {
		if (player) {
			transform.position = player.transform.position;
		} else {
			transform.position = new Vector3 (-20, 0, 0);
			player = GameObject.FindGameObjectWithTag ("Player");
		}
	}
}
