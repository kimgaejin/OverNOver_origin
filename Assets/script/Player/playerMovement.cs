using UnityEngine;
using System.Collections;

// should tune up attackDelay with animation

public class playerMovement : MonoBehaviour {

	private Rigidbody2D rigid;
	private SpriteRenderer renderImage;
	private Animator animator;

	// Move
	public float moveSpeed = 10f;
	public float jumpPower = 15f;
	private bool canMoving = true;
	private bool canJumping = true;
	private bool is_ground = false;
	private bool Jumping = false;
	private bool FaceLeft = true;

	private float jumpTimer = 0.0f;

	// Attack
	private bool canAttacking = true;
	public GameObject AttackPrefab;
	public float attackDelay = .4f;
	private float attackTimer = .0f;

	private float shieldDelay = 1.0f;
	private float shieldTimer = .0f;
	private bool skilling = false;

	// HP
	private bool infinite = false;
	public static int maxHealth = 5;
	public static int health = 1;

	// BeAttacked
	private float beAttackedTimer = .0f;
	private float beAttackedDelay = 1.0f;

	WaitForSeconds wait02 = new WaitForSeconds(.2f);
	WaitForSeconds wait03 = new WaitForSeconds(.3f);

	//-----------------[Override function]
	// Initialize
	void Start()
	{
		rigid = gameObject.GetComponent<Rigidbody2D> ();
		renderImage = gameObject.GetComponent<SpriteRenderer> ();
		animator = gameObject.GetComponent<Animator> ();

		health = maxHealth;
	}

	// Graphic & Input Updates & Timer
	void Update ()
	{
		if (GameManager.playerDie == false) {

			// Key and animation
			if (Input.GetAxisRaw ("Horizontal") != 0) {
				if (canMoving) {
					animator.SetBool ("isWalk", true);
				}
			} else {
				animator.SetBool ("isWalk", false);
			}

			if (Input.GetButtonDown ("Jump")) {
				animator.SetBool ("isJump", true);
			}

			if (Input.GetButtonDown ("SkillZ")) {
				if (canAttacking) {
					if (attackTimer <= 0)
                    {
                        animator.SetTrigger("doAttack");
                        StartCoroutine("Attack");

                        attackTimer = attackDelay;
					}
				}
			}
			if (Input.GetButtonDown ("SkillX")) {
				if (canAttacking) {
                    if (shieldTimer <= 0)
                    {
                        animator.SetTrigger("doBlock");
                        shieldTimer = shieldDelay;
                    }
				}
			}
			
			// Timer
            beAttackedTimer -= Time.deltaTime;
			shieldTimer -= Time.deltaTime;
			attackTimer -= Time.deltaTime;
			jumpTimer -= Time.deltaTime;

		}
	}
		
	//Phsics engine Updates
	void FixedUpdate()
	{
		if (GameManager.playerDie == false) {
			Move ();
			Jump ();
		}
	}

	void OnTriggerStay2D (Collider2D other)
	{
		if (jumpTimer <= 0) {
			if (other.tag == "Ground") {
				is_ground = true;
				animator.SetBool ("isJump", false);
			}
			if (other.tag == "platform") {
				if (other.transform.position.y < transform.position.y) {
					is_ground = true;
					animator.SetBool ("isJump", false);
				}
			}
		}
		if (other.tag == "MonsterAttack") {
			BeAttacked ();
		}
	}

	//-----------------[Movement function]
	public void ActManage (int actType, bool toggleType)
	// 0canMoving, 1canAttacking, 2infinite, 3jumping
	{
		if (actType == 0) {
			if (toggleType) {
				canMoving = true;
			} else {
				canMoving = false;
			}
		}

		else if (actType == 1) {
			if (toggleType) {
				canAttacking = true;
			} else {
				canAttacking = false;
			}
		}

		else if (actType == 2) {
			if (toggleType) {
				infinite = true;
			} else {
				infinite = false;
			}
		} 

		else if (actType == 3) {
			if (toggleType) {
				canJumping = true;
			} else {
				canJumping = false;
			}
		}

		else {
			print ("error: MonsterStruct1.cs; void ActManage; couldn't find accurate actType");
		}
	}

	public Vector3 getPlayerPosition()
	{
		return rigid.position;
	}

	void Move()
	{
		if (canMoving == false) {
			return;
		}
		if (skilling == true) {
			return;
		}

		Vector3 moveVector = Vector3.zero;

		if (Input.GetAxisRaw ("Horizontal") < 0){	// left
			moveVector = Vector3.left;
			renderImage.flipX = false;
			FaceLeft = true;
		} else if (Input.GetAxisRaw ("Horizontal") > 0) {	// right
			moveVector = Vector3.right;
			renderImage.flipX = true;
			FaceLeft = false;
		}

		transform.position += moveVector * moveSpeed * Time.deltaTime;
	}

	void Jump ()
	{
		if (canJumping == false) {
			return;
		}
			
		if (Input.GetButton("Jump")) {
			if (!is_ground) {
				return;
			}

			Vector2 jumpVelocity = new Vector2 (0, jumpPower);
			rigid.AddForce (jumpVelocity, ForceMode2D.Impulse);

			is_ground = false;
			jumpTimer = 0.5f;
		}
	}

	IEnumerator Attack ()
	{
        
        while (true)
        {
			skilling = true;
            yield return wait03;
            if (FaceLeft == true) {
                Instantiate(AttackPrefab, this.transform.position, Quaternion.identity);
            } else {
                Instantiate(AttackPrefab, this.transform.position, Quaternion.Euler(0, 180.0f, 0));
            }
			yield return wait02;
			skilling = false;
            break;
        }
	}

	void BeAttacked()
	{
		if (infinite == true
            || shieldTimer > 0.5
            || beAttackedTimer > 0) {
			return;
		}

		health--;
        beAttackedTimer = beAttackedDelay;
	}

}
