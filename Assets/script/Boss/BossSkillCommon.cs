using UnityEngine;
using System.Collections;

public class BossSkillCommon : MonoBehaviour {

	public float waitTime = 0.0f;
	public float hitTime = 0.0f;
	public float endTime = 0.0f;

	Animator animator;
	SpriteRenderer spr;
	Color color;

	void Start () {
		animator = GetComponent<Animator>();
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
			animator.SetTrigger ("doHit");
			transform.gameObject.tag = "MonsterAttack";
			for (temp = 0; temp < hitTime * 20; temp++) {
				yield return wait005;
			}
			animator.SetTrigger ("doEnd");
			gameObject.tag = "endedSkill";
			for (temp = 0; temp < endTime * 20; temp++) {
				if (temp >= endTime * 10) {
					if (color.a >= 1 / (endTime * 10)) {
						color.a -= 1 / (endTime * 10);
						spr.color = color;
					}
				}
				yield return wait005;
			}
			animator.SetTrigger ("doExit");
			Destroy (gameObject, 0.0f);
		}

	}
}
