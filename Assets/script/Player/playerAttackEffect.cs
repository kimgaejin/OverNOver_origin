using UnityEngine;
using System.Collections;

public class playerAttackEffect : MonoBehaviour {

	public static float Skill2_damage = 1.0f;

	void Start()
	{
		Skill2_damage = GameManager.rage/10 + 1.0f;
		Destroy (gameObject, .2f);
	}
		
}
