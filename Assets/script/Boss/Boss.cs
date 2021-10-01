using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

	Rigidbody2D rigid;
	SpriteRenderer spr;
	Animator animator;
	Color color;

	GameObject player;
	public GameObject clearPanel;

	//
	bool is_died = false;

	// hp
	float maxHealth = 200.0f;
	public float health = 0;

	public GameObject healthBar;
	Transform healthBarTrans;

	// skiil

	public bool infiniteMode = true;
	bool skilling = false;
	int actNum = 0;

	public GameObject SkillA; // 가시 3
	public GameObject SkillB; // 레이저 3
	public GameObject SkillC; // 창 2.5
	public GameObject SkillD; // 아이스 2.5
	public GameObject SkillE; // 메테오 5
	public GameObject SkillEMagic;

	float[] skillTime = { .0f,   .0f, .0f, .0f, .0f, .0f};
	float[] delay = { .0f,   4.0f,  4.0f, 5.0f, 12.0f, 20.0f}; // 실제 쿨타임

	WaitForSeconds wait005 = new WaitForSeconds (0.05f);
	WaitForSeconds wait025 = new WaitForSeconds (0.25f);
	WaitForSeconds wait100 = new WaitForSeconds (1.0f);
	WaitForSeconds wait250 = new WaitForSeconds (2.5f);
	WaitForSeconds wait300 = new WaitForSeconds (3.0f);

    string[] textList = new string[20];
    float[] textPoint = new float[20];
    float[] textTime = new float[20];
    int textSize = 0;
    public GameObject textPanel;
    public Text bossText;

    void Start () {
		rigid = GetComponent <Rigidbody2D> ();
		spr = GetComponent <SpriteRenderer> ();
		animator = GetComponent <Animator> ();
		color = spr.color;

		healthBarTrans = healthBar.GetComponent<Transform> ();

        textSize = 4;
        int temp = 0;

        textList[temp] = "또 너구나?";
        textPoint[temp] = 100.0f;
        textTime[temp] = 4.0f;
        temp++;

        textList[temp] = "제발 날 내버려둬.";
        textPoint[temp] =50.0f;
        textTime[temp] = 3.0f;
        temp++;

        textList[temp] = "날 죽인다고 달라지는건 없어";
        textPoint[temp] = 20.0f;
        textTime[temp] = 3.0f;
        temp++;

        textList[temp] = "이번에는 제발...";
        textPoint[temp] = 0.0f;
        textTime[temp] = 4.0f;
        temp++;

        textPoint[temp] = -100.0f;

        health = maxHealth;
		//healthBar.transform.localScale = new Vector3 ((3*health) / maxHealth, 2f ,1f);

		StartCoroutine ("CheckInfinite");
		StartCoroutine ("BossAction");
		StartCoroutine ("DecreaseCoolTime");
        StartCoroutine ("ScenarioPrint");

	}

	void Update()
	{
		//healthBar.transform.localScale = new Vector3 ((3*health) / maxHealth, 2f ,1f);
		healthBarTrans.localScale = new Vector3 ((1.7f*health) / maxHealth, 1.4f ,1f);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "playerAttack") {
			BeAttacked (other);
		}
	}

	//---------

	IEnumerator BossAction()
	{
		Vector3 pPos;
		int fall_stack = 0;
		while (true) {
			
			if (is_died == true) {
				break;
			}

			if (skilling == false) {
				fall_stack = 0;
				while (true) {
					if (infiniteMode == true) {
						actNum = Random.Range (0, 2); // skill 1, 3
						if (actNum == 0) { 
							actNum = 1;
						} else {
							actNum = 3;
						}
					} else if (infiniteMode == false) {
						actNum = Random.Range (1, 6); // skill 1 ~ 5 + walk
					}

					if (skillTime [actNum] <= 0.0f) {
						break;
					} else {
						fall_stack++;
					}

					if (fall_stack > 10) {
						actNum = 0;
						break;
					}
				}
			}
			skilling = true;
			player = GameObject.FindGameObjectWithTag ("Player");
			pPos = player.transform.position;
			if (infiniteMode == false) {
				animator.SetBool ("doSkill", true);
			}
			if (actNum == 0) {	// stop
				yield return wait100;
			} else if (actNum == 1) {
				Instantiate (SkillA, pPos + new Vector3(0,-1.0f, 0), Quaternion.identity);
				yield return wait300;
			} else if (actNum == 2) {
				if (player.transform.position.x < transform.position.x) {
					Instantiate (SkillB, this.transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
				} else {
					Instantiate (SkillB, this.transform.position + new Vector3(0, 1.5f, 0), Quaternion.Euler(0,180.0f,0));
				}
				yield return wait300;
			} else if (actNum == 3) {
				Instantiate (SkillC, pPos + new Vector3(0,+4.0f, 0), Quaternion.identity);
				yield return wait300;
			} else if (actNum == 4) {
				Instantiate (SkillD, pPos + new Vector3(0,+4.5f, 0),  Quaternion.identity);
				yield return wait300;
			} else if (actNum == 5) {
				Instantiate (SkillEMagic, transform.position + new Vector3(2,2,0), Quaternion.identity);
				Instantiate (SkillE, new Vector3(70, 40, 0), Quaternion.identity);
				yield return wait100;
				yield return wait100;
				if (health < maxHealth / 2) {
					Instantiate (SkillE, new Vector3 (100, 80, 0), Quaternion.identity);
				}
				yield return wait100;
			}
			skillTime [actNum] = delay [actNum];
			skilling = false; 
			if (infiniteMode == true) {
				yield return wait300;
			}
			if (infiniteMode == false) {
				animator.SetBool ("doSkill", false);
			}
			yield return wait100;
		}
	}

	void BeAttacked(Collider2D other)
	{
		if (infiniteMode == false) {			
			health -= playerAttackEffect.Skill2_damage;
			if (health < 0) {
				health = 0.0f;
			}
			//Transform HPT = healthBar.transform;


			if (health <= 0.0f && is_died == false) {
				StartCoroutine ("Die");
			}
		}
	}

	IEnumerator Die()
	{
		while (true) {
			GameObject tempPrefab;
			is_died = true;

			// destroy boss's attack effect
			while (true) {
				tempPrefab = GameObject.FindGameObjectWithTag ("MonsterAttack");
				if (tempPrefab) {
					Destroy (tempPrefab, 0.0f);
				} else {
					//WINNER WINNER CHICCKEN DINNER!
					break;
				}
				yield return wait005;
			}

			// transparency
			int i;
			color = spr.color;
			for (i = 0; i < 20; i++) {
				color.a -= 0.05f;
				spr.color = color;
				yield return new WaitForSeconds (0.25f);
			}

			// if there any ending, here
			yield return new WaitForSeconds (3.0f);
			clearPanel.SetActive(true);

			// destroy
			Destroy (gameObject, 0.0f);
			break;
		}
	}

	IEnumerator CheckInfinite()
	{
		GameObject tempBotPrefab;
		GameObject tempPlatPrefab;

		while (true) {
			tempBotPrefab = GameObject.FindGameObjectWithTag ("Monster");
			if (tempBotPrefab) {
				rigid.gravityScale = 0;
				rigid.velocity = Vector2.zero;
			} else {
				rigid.gravityScale = 1.0f;
				infiniteMode = false;
				animator.SetTrigger ("doOffInfinite");

				tempPlatPrefab = GameObject.FindGameObjectWithTag ("platform");
				while (true) {
					if (tempPlatPrefab) {
						tempPlatPrefab.SetActive (false);
						tempPlatPrefab = GameObject.FindGameObjectWithTag ("platform");

					} else {
						break;
					}
					yield return wait005;
				}

			}
				
			if (infiniteMode == false) {
				break;
			}

			yield return wait025;
		}
	}

	IEnumerator DecreaseCoolTime()
	{
		while (true) {
			for (int i = 0; i < skillTime.Length; i++) {
				if (skillTime [i] > .0f) {
					skillTime [i] -= 0.05f;
				}
			}
			yield return wait005;
		}
	}

    IEnumerator ScenarioPrint()
    {
        int turn = 0;
        int time;
        bool firstPrint = false;

        while (true)
        {
            if (textPoint[turn] >= 100 * health / maxHealth)
            {
                bossText.text = textList[turn];
                textPanel.SetActive(true);
                for (time = 0; time < textTime[turn] * 4; time++)
                {
                    yield return wait025;
                }
                textPanel.SetActive(false);
                turn++;
            }

            yield return wait025;
        }
    }
}
