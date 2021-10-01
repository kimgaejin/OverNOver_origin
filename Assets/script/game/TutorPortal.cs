using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TutorPortal : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") {
			Invoke ("StartGame", 0);
		}
	}

	void StartGame()
	{
		SceneManager.LoadScene ("ongameScene");
	}

}
