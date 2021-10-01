using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorUI : MonoBehaviour {

	GameObject player;

	string[] textList = new string [20];
	int[] positionList = new int[20];
    float[] timeList = new float[20];
	int textSize = 0;

	Text myText;

	void Start()
	{
		myText = GetComponent<Text> ();
		player = GameObject.FindGameObjectWithTag ("Player");

		// scenario text

		textSize = 9;
		int temp = 0;

		textList [temp] = "드디어 오셨군요 용사님! 여기가 어디냐고요?";
		positionList [temp] = -1;
        timeList [temp] = 7.0f;
		temp++;

		textList [temp] = "여기는 나쁜 마녀의 성이예요.\n저는 나쁜 마녀의 저주로 이렇게 고양이가 되어 버렸답니다.";
		positionList [temp] = -1;
		timeList[temp] = 3.0f;
        temp++;

		textList [temp] = "용사님은 왜 이곳에 있냐고요?\n당연히 마녀를 물리치기 위해 여기에 온 거죠.";
		positionList [temp] = -1;
		timeList[temp] = 3.0f;
		temp++;

		textList [temp] = "방향키로 몸을 움직일 수 있어요!";
		positionList [temp] = 15;
        timeList[temp] = -1;
        temp++;

		textList [temp] = "'C' 로 점프해서 넘어가세요!";
		positionList [temp] = 20;
        timeList[temp] = -1;
        temp++;

		textList [temp] = "앞에 스켈레톤이 있는거같아요. 조심하세요.";
		positionList [temp] = -1;
        timeList[temp] = 2.0F;
        temp++;

        textList[temp] = "'Z'로 공격, 'X'로 막아낼 수 있어요!";
        positionList[temp] = 40;
        timeList[temp] = -1;
        temp++;

        textList[temp] = "이 문으로 들어가시면 마녀가 있을 거예요.\n 마녀는 모든 해골들을 처치하면 만날 수 있어요.";
        positionList[temp] = -1;
        timeList[temp] = 3.0f;
        temp++;

        textList[temp] = "이번에는 제발 잘 좀 하자구요!";
        positionList[temp] = -1;
        timeList[temp] = -1;
        temp++;

        StartCoroutine ("CheckScenario");
	}

	IEnumerator CheckScenario()
	{
        WaitForSeconds wait01 = new WaitForSeconds(0.1f);
		int progress = 0;
        int i;

		while (true) {

			if (progress < textSize) {
				myText.text =textList [progress];

				if (positionList[progress] >= 0) {
					while (true) {
						if (player.transform.position.x >= positionList [progress]) {
							progress++;
							break;
						} else {
							yield return wait01;
						}
					}
                }
                else if (timeList[progress] >= 0) {
					for (i = 0; i < timeList [progress] * 10; i++) {
						yield return wait01;
					}
					progress++;
                }
			} else {
                yield return wait01;
            }
			yield return wait01;
		}

	}
}
