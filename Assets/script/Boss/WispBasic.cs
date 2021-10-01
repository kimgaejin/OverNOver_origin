using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispBasic : MonoBehaviour {

	public GameObject skill;
	public float rotationZ =0.0f;
	public float height =0.0f;

	private Quaternion magicRotation;

	void Start() {
		magicRotation = Quaternion.Euler (0.0f, 0.0f, rotationZ);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Ground" || other.tag == "platform") {
			Instantiate (skill, transform.position + new Vector3(0,-0.5f + height,0), magicRotation);
			Destroy (this.gameObject, .0f);
		}
	}
}
