using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorSkull : MonoBehaviour {

	void Update () {
        if (transform.position.x > 42 || transform.position.x < 26 || transform.position.y < -3)
        {
            transform.position = new Vector3(32, 0, 0);
        }
	}
}
