using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	// Die
	public static bool playerDie = false;

	private GameObject player;
	private playerMovement getPlayerMovement;
    private Animator getPlayerAnimator;
	public int reviveNum =0;

	public bool stopToRevive = false;
    public GameObject RevivePanel;

	// Rage
	public static float rage = 1.0f;
	public float maxRage = 10.0f;
	public float minRage = 0.0f;
	private float rageTimer = 6.0f;
	private float rageDecrease = 0.1f;

	// revive text
	public Text myText;

	string[] textList = new string [8];
	int textSize;

    WaitForSeconds wait01 = new WaitForSeconds(0.1f);

    void Start()
	{
		rageTimer = 6.0f;
		rageDecrease = 0.1f;

		textSize = 4;
		int temp = 0;

		textList[temp] = "이번에도 실패네. 너희들 요즘 일 제대로 안하지?";
		temp++;

		textList[temp] = "또 저거 치울 생각하니 치가 떨리네.";
		temp++;

		textList[temp] = "이러다 성 안에 해골만 가득 차겠어.";
		temp++;

		textList[temp] = "차라리 해골들에게 기회를 더 주는게 나을까?";
		temp++;

		temp = Random.Range (0, textSize);
		myText.text = textList [temp];
		reviveNum = 0;

		player = GameObject.FindGameObjectWithTag ("Player");
        getPlayerMovement = player.GetComponent ("playerMovement") as playerMovement;
        getPlayerAnimator = player.GetComponent <Animator> ();
        StartCoroutine ("CheckPlayer");
		StartCoroutine ("AreYouDead");
		StartCoroutine("RageDecrease");
	}

	// 

	IEnumerator AreYouDead()
	{
		while (true) {
			if (playerMovement.health <= 0) {
				Die ();
				stopToRevive = true;
                RevivePanel.SetActive(true);
				while (stopToRevive == true){
					yield return wait01;
				}
				Revive ();
			} else {
				yield return wait01;
			}
		}
	}

	IEnumerator RageDecrease()
    {
        int timeStack = 0;
		while (true) {
            yield return wait01;

			if (rage > maxRage) {
				rage = maxRage;
			}
			else if (rage > minRage) {
                timeStack++;
			} else {
				rage = minRage;
			}

            if ((float)timeStack > rageTimer * 10)
            {
                rage -= rageDecrease;
                timeStack = 0;
            }

		}
	}

	IEnumerator CheckPlayer()
	{
		while (true) {
			if (player) {
				yield return new WaitForSeconds (1.0f);
			}else {
				player = GameObject.FindGameObjectWithTag ("Player");
                getPlayerMovement = player.GetComponent("playerMovement") as playerMovement;
                getPlayerAnimator = player.GetComponent<Animator>();
                yield return new WaitForSeconds (1.0f);
			}
		}
	}

	void Die()
	{
		playerDie = true;
        getPlayerAnimator.SetBool("isDie", true);
        AddConcreteRage();

		int temp;
		temp = Random.Range (0, textSize);
		myText.text = textList [temp];
	}

	void AddConcreteRage()
	{
		minRage += 0.1f * maxRage;
	}

	void Revive()
	{
		player.SetActive (true);
		player.transform.position = Vector3.zero;
        getPlayerAnimator.SetBool("isDie", false);
		playerMovement.health = playerMovement.maxHealth;

		getPlayerMovement.ActManage (0, true);
		getPlayerMovement.ActManage (1, true);
		getPlayerMovement.ActManage (3, true);
		playerDie = false;
		reviveNum++;
	}
}
