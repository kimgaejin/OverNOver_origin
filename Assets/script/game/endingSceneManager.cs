using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class endingSceneManager : MonoBehaviour {

	public GameObject camera;
	public GameObject player;
	SpriteRenderer playerSpr;
	Animator playerAni;
	public GameObject cat;
	SpriteRenderer catSpr;
	public GameObject door;
    public GameObject door2;
    GameObject door2Clone;
	public GameObject dark;

	bool canMoveCamera;
	bool canMoveCat;
	bool canMovePlayer;
    bool door2Move;

    public Text myText;
	int textSize;

	string[] textList = new string [20];
	float[] timeList = new float[20];

	Vector3 speed = new Vector3 (1, 0, 0);
    Vector3 verticalSpeed = new Vector3(0, -1, 0);

	void Start() {
		playerSpr = player.GetComponent<SpriteRenderer>();
		playerSpr.flipX = true;
		playerAni = player.GetComponent<Animator> ();
		playerAni.SetBool ("isWalk", true);
		catSpr = cat.GetComponent<SpriteRenderer>();
		catSpr.flipX = true;

		canMoveCamera = true;
		canMoveCat = true;
		canMovePlayer = true;
        door2Move = false;

		textSize = 7;
		int temp = 0;

		textList [temp] = "축하드려요 용사님. \n드디어 저 나쁜 마녀를 물리치셨네요";
		timeList [temp] = 6.0f;
		temp++;

		textList [temp] = "뭔가 익숙한 느낌이라고요?\n에이 기분 탓이에요.";
		timeList [temp] = 5.0f;
		temp++;

		textList [temp] = "나쁜 마녀를 쓰러뜨리셨는데 뭐가 더 중요한 게 있겠어요?";
		timeList [temp] = 5.0f;
		temp++;

		textList [temp] = "용사님은 이제 저기 있는 문으로 나가면 돼요.";
		timeList [temp] = 5.0f;
		temp++;

		textList [temp] = "전 할 일이 있으니 용사님 먼저 가세요.";
		timeList [temp] = 3.0f;
		temp++;

		textList [temp] = "용사님이 빨리 나가셔야 제가 일을 할 수 있답니다.";
		timeList [temp] = 4.0f;
		temp++;

		textList [temp] = "다음 분이 오실지도 모르잖아요?";
		timeList [temp] = 5.0f;
		temp++;

		StartCoroutine ("CheckScenario");
	}

	void Update()
	{
		if (canMovePlayer == true) {
			player.transform.position += speed * Time.deltaTime;
		}
		if (canMoveCamera == true) {
			camera.transform.position += speed * Time.deltaTime;
		}
		if (canMoveCat == true) {
			cat.transform.position += speed * Time.deltaTime;
		}

        if (door2Clone)
        {
            if (door2Move == true)
            {
                door2Clone.transform.position += verticalSpeed * Time.deltaTime;
            }
        }
        else
        {
            door2Clone = GameObject.FindGameObjectWithTag("door");
        }
	}

	IEnumerator CheckScenario()
	{
		WaitForSeconds wait01 = new WaitForSeconds(0.1f);
		WaitForSeconds wait10 = new WaitForSeconds(1.0f);

		int progress = 0;
		int i;

		while (true) {

			if (progress < textSize) {
				myText.text = textList [progress];
				for (i = 0; i < timeList [progress] * 10; i++) {
					yield return wait01;
				}
				progress++;
			}
			if (progress == 3) {
                Instantiate(door, camera.transform.position + new Vector3(10.0f, -2.5f, 20), Quaternion.identity);
                Instantiate(door2, camera.transform.position + new Vector3(10.0f, -2.5f, 20), Quaternion.identity);
            }
			if (progress == 4) {
				canMoveCat = false;
				canMoveCamera = false;

			}
			if (progress == 5) {
				canMovePlayer = false;
				playerAni.SetBool ("isWalk", false);
				playerSpr.flipX = false;
			}
			if (progress == 6) {
				Instantiate (dark, camera.transform.position + new Vector3 (0, 0, 20), Quaternion.identity);
				canMovePlayer = true;
				playerAni.SetBool ("isWalk", true);
				playerSpr.flipX = true;
                door2Move = true;

            }
			if (progress == 7) {

				SceneManager.LoadScene ("mainScene");
			}

			yield return wait01;
		}

	}
}
