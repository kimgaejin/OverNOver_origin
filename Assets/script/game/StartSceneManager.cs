using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour {

	public void GameStartButton()
	{
		SceneManager.LoadScene ("tutorScene");
	}

	public void GameEndButton()
	{
		Application.Quit ();	
	}


}