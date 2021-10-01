using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSKillMeteo : MonoBehaviour {
 
	Rigidbody2D rigid;
	GameObject player;
	float distanceX;
	float distanceY;
	Vector2 distanceVec;
	Animator animator;
	SpriteRenderer spr;

	public float hitTimeOnGround;
	public float endTime;

	bool onGround = false;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player");

		rigid = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		spr = GetComponent<SpriteRenderer> ();

		distanceX = player.transform.position.x - transform.position.x;
		distanceY = player.transform.position.y - transform.position.y;
		distanceVec = new Vector2 (distanceX, distanceY);
		rigid.AddForce ( distanceVec / 3, ForceMode2D.Impulse);
		StartCoroutine("CheckNoneGround");
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Ground") {
			animator.SetTrigger ("doTrans");
			StartCoroutine ("ActionOnGround");
			onGround = true;
			rigid.velocity = Vector3.zero;
			rigid.position += new Vector2 (-2, -2.2f);
		}
	}

	IEnumerator ActionOnGround()
	{
		WaitForSeconds wait01 = new WaitForSeconds (0.1f);
		int temp;
		Color color = spr.color;

		if (onGround == false) {

			while (true) {
				for (temp = 0; temp < hitTimeOnGround * 10; temp++) {
					yield return wait01;
				}
				transform.gameObject.tag = "endedSkill";
				animator.SetTrigger ("doEnd");
				for (temp = 0; temp < endTime * 10; temp++) {
					if (color.a >= 1 / (endTime * 10)) {
						color.a -= 1 / (endTime * 10);
						spr.color = color;
					}
					yield return wait01;
				} 

				animator.SetTrigger ("doExit");
				Destroy (gameObject, 0.0f);
			}
		}

	}

	IEnumerator CheckNoneGround()
	{
		WaitForSeconds wait01 = new WaitForSeconds (0.1f);
		float Timer = 0.0f;

		while (true) {
			Timer += 0.1f;
			yield return wait01;

			if (Timer > 45.0f) {
				Destroy (gameObject, 0.0f);
			}
		}
	}
}
