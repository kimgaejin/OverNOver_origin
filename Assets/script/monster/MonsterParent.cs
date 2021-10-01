using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// OverNOver, 2017년에 작성했던 몬스터 부모 클래스
/// 부족한 부분이 많지만, 일부러 주석 외 변경은 삼가했습니다.
/// </summary>
public class MonsterParent : MonoBehaviour {

    public GameObject AttackPrefab;
	public GameObject AshPrefab;
    protected GameObject player;

    protected Rigidbody2D rigid;
    protected Animator animator;
    protected SpriteRenderer spr;
    protected Color color;

    protected bool isDie;
    protected bool isFoundTarget;
    protected float findRange;
    
    protected float health;
    protected float maxHealth;

    protected float moveSpeed;
    protected Vector3 moveTempVector;

    protected float attackDelay;
    protected float attackTimer;
    protected float attackRange;

	protected Vector3 platformPos;
	protected float platformSizeX;

    protected void Walk(float direction)
    {
        if (direction <= 0) {
            direction = -1;
        } else {
            direction = 1;
        }
			
		if (direction == 1 && platformPos.x + (platformSizeX / 2) <= transform.position.x+ 0.5f) {
			return;
		} else if (direction == -1 && platformPos.x - (platformSizeX / 2) >= transform.position.x - 0.5f) {
			return;
		} else {
			moveTempVector = new Vector3(moveSpeed * direction, 0, 0);

			transform.position += moveTempVector * Time.deltaTime;
		}
    }

    protected void BeAttacked()
    {
        health -= playerAttackEffect.Skill2_damage;
    }
}
