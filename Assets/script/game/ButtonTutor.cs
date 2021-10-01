using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonTutor : MonoBehaviour {

	public GameObject skipPanel;
	public GameObject textPanel;

	void Start()
	{
		GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>().ActManage(0,false);
		GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>().ActManage(1,false);
		GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>().ActManage(3,false);
	}

	public void StartTutor()
	{
		GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>().ActManage(0,true);
		GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>().ActManage(1,true);
		GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>().ActManage(3,true);
		textPanel.SetActive (true);
		skipPanel.SetActive (false);
	}

	public void SkipTutor()
	{
		SceneManager.LoadScene ("ongameScene");
	}


}
