using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// OverNOver, 2017년에 작성했던 몬스터 클래스
/// 부족한 부분이 많지만, 일부러 주석 외 변경은 삼가했습니다.
/// </summary>
public class SkullWarrior : MonsterParent {

    WaitForSeconds wait005 = new WaitForSeconds(0.05f);
    WaitForSeconds wait01 = new WaitForSeconds(0.1f);
    WaitForSeconds wait05 = new WaitForSeconds(0.5f);

	float rageInc;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        color = spr.color;

        isDie = false;
        isFoundTarget = false;
        findRange = 7.0f;
		rageInc = 0.3f;

        maxHealth = 15.0f;
        health = maxHealth;

        moveSpeed = 12.0f;

        attackDelay = 0.0f;
        attackTimer = 0.0f;
        attackRange = 1.8f;

        // 모든 몬스터가 코루틴으로 동작
        StartCoroutine("Action");
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        // 플레이어의 공격을 받음
        if (other.tag == "playerAttack")
        {
            BeAttacked();
            // 공격을 받고 체력이 0 이하가 될 때, Die() 호출
            if (health <= 0)
            {
                Die();
            }
        }
		
        // 지면에 접촉함. 지면 밖으로 떨어지진 않기 위해 지면의 정보 읽음.
		if ((other.tag == "platform" || other.tag == "Ground")
			&& other.transform.position.y < transform.position.y) {
			platformPos = other.transform.position;
			platformSizeX = other.bounds.size.x;
		}
    }

    IEnumerator Action()
    {
        int frame = 0;
        int tempVec = 0;
        int i = 0;
        while (true)
        {
            // 몬스터가 죽었다면 동작 중지
            if (isDie)
                break;

            // 플레이어 객체가 없으면 플레이어 0.1초마다 반복해서 플레이어 객체를 찾음
            if (!player)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                yield return wait01;
                continue;
            }

            // 몬스터의 인식 범위 내에 플레이어가 존재하는가
            bool isInFindRange = Mathf.Abs(transform.position.x - player.transform.position.x) <= findRange;
            if (isInFindRange)
            {
                // 공격 쿨타임이 돌아 왔는가
                if (attackTimer <= 0)
                {
                    // 공격 범위 내에 들어왔는가
                    bool isInAttackRange = Mathf.Abs(transform.position.x - player.transform.position.x) <= attackRange
                                                        && Mathf.Abs(transform.position.y - player.transform.position.y) < 2.0f;
                    if (isInAttackRange)
                    {
                        // 공격 범위 내라면 공격 실행
                        animator.SetTrigger("doAttack");
                        yield return wait05;    // 선 딜레이

                        // 닿으면 데미지를 주는 프리팹 소환...
                        if (transform.position.x - player.transform.position.x > 0)
                            Instantiate(AttackPrefab, transform.position, Quaternion.identity);
                        else
                            Instantiate(AttackPrefab, transform.position, Quaternion.Euler(0, 180.0f, 0));

                        yield return wait05;    // 후 딜레이
                    }
                    else
                    {
                        // 공격 범위 안에 없는 경우, 플레이어에게 접근
                        frame = 20;

                        animator.SetBool("isWalk", true);
                        if (transform.position.x - player.transform.position.x > 0)
                        {
                            spr.flipX = false;
                        }
                        else
                        {
                            spr.flipX = true;
                        }
                        for (i = 0; i < frame; i++)
                        {
                            Walk(-(transform.position.x - player.transform.position.x));
                            yield return wait005;
                        }
                        animator.SetBool("isWalk", false);
                    }
                }
                else
                {
                    // 공격 쿨타임이 아직 남아있는 경우, 플레이어로부터 멀어짐
                    frame = 20;

                    animator.SetBool("isWalk", true);
                    bool isPlayerLeft = transform.position.x - player.transform.position.x > 0;
                    if (isPlayerLeft)
                        spr.flipX = true;
                    else
                        spr.flipX = false;

                    for (i = 0; i < frame; i++)
                    {
                        Walk(transform.position.x - player.transform.position.x);
                        yield return wait005;
                    }
                    animator.SetBool("isWalk", false);
                }
            }
            else
            {
                // 인식 범위 내에 플레이어가 존재하지 않는 경우, 랜덤으로 현재 자리 근처로 이동
                frame = 20;
                tempVec = Random.Range(0, 2);

                animator.SetBool("isWalk", true);
                if (tempVec == 0)
                {
                    spr.flipX = false;
                }
                else
                {
                    spr.flipX = true;
                }
                for (i = 0; i < frame; i++)
                {
                    Walk(tempVec);
                    yield return wait005;
                }
                animator.SetBool("isWalk", false);
            }
        }
        yield break;
    }

    void Die()
    {
        if (isDie == false)
        {
            Instantiate(AshPrefab, this.transform.position, Quaternion.identity);
            isDie = true;
			GameManager.rage += rageInc; 
			gameObject.SetActive (false);
			//Destroy (this, 0.0f);
        }
    }
}
