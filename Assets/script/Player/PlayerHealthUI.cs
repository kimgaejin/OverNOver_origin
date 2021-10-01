using UnityEngine;
using System.Collections;

public class PlayerHealthUI : MonoBehaviour {

	Transform transform;
	private float maxHp;
	private float hp;

	void Start()
	{
		transform = GetComponent<Transform> ();
		maxHp = (float)playerMovement.maxHealth;
		hp = (float)playerMovement.health;
	}

	void Update()
	{
		hp = (float)playerMovement.health;
		if (hp >= 0) {
			transform.localScale = new Vector3 (hp / maxHp, 1f, 1f);
		} else {
			transform.localScale = new Vector3 (hp / ( maxHp * maxHp ), 1f, 1f);
		}
	}
}
