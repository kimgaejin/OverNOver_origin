using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullSpearman : MonsterParent {

    WaitForSeconds wait005 = new WaitForSeconds(0.05f);
    WaitForSeconds wait01 = new WaitForSeconds(0.1f);
    WaitForSeconds wait05 = new WaitForSeconds(0.5f);

	float rageInc;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        color = spr.color;

        isDie = false;
        isFoundTarget = false;
        findRange = 10.0f;
		rageInc = 0.4f;

        maxHealth = 7.0f;
        health = maxHealth;

        moveSpeed = 6.0f;

        attackDelay = 2.0f;
        attackTimer = 0.0f;
        attackRange = 5.0f;
        StartCoroutine("Action");
    }

    private void Update()
    {
        attackTimer -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "playerAttack")
        {
            BeAttacked();
            if (health <= 0)
            {
                Die();
            }
        }

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
            if (isDie == false)
            {
                if (player)
                {
                    if (Mathf.Abs(transform.position.x - player.transform.position.x) <= findRange)
                    {
                        if (attackTimer <= 0)
                        {
                            if (Mathf.Abs(transform.position.x - player.transform.position.x) <= attackRange)
                            {
                                animator.SetTrigger("doAttack");
                                if (transform.position.x - player.transform.position.x > 0) {
                                    spr.flipX = false;
                                }
                                else {
                                    spr.flipX = true;
                                }
                                yield return wait05;
                                if (transform.position.x - player.transform.position.x > 0)
                                {
                                    Instantiate(AttackPrefab, transform.position + new Vector3(0,0.5f,0), Quaternion.identity);
                                }
                                else
                                {
                                    Instantiate(AttackPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.Euler(0, 180.0f, 0));
                                }
                                yield return wait05;
                                attackTimer = attackDelay;
                            }
                            else
                            {
                                frame = 20;

                                animator.SetBool("isWalk", true);
                                if (transform.position.x - player.transform.position.x > 0) {
                                    spr.flipX = true;
                                } else  {
                                    spr.flipX = false;
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
                            frame = 20;
                            animator.SetBool("isWalk", true);

                            if (transform.position.x - player.transform.position.x > 0)
                            {
                                spr.flipX = true;
                            }
                            else
                            {
                                spr.flipX = false;
                            }
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
                        frame = 20;
                        animator.SetBool("isWalk", true);

                        tempVec = Random.Range(0, 2);
                        if (tempVec == 1)
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
                else
                {
                    player = GameObject.FindGameObjectWithTag("Player");
                    yield return wait01;
                }
            }
            else
            {
                yield return wait01;
            }
        }
    }

    void Die()
    {
        if (isDie == false)
        {
			if (AshPrefab) {
				Instantiate (AshPrefab, this.transform.position, Quaternion.identity);
			}
			isDie = true;
			GameManager.rage += rageInc;
			gameObject.SetActive (false);
        }
    }
}
