using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour {

	public GameObject CheckPanel = null;
    public GameObject RevivePanel = null;

	public void ClickToMenu()
	{
		CheckPanel.SetActive (true);
	}

	public void ClickSureToMenu()
	{
        Time.timeScale = 1;
        SceneManager.LoadScene ("mainScene");
	}

	public void CleckNopeToMenu()
	{
		CheckPanel.SetActive (false);
	}

	public void ClickToRevive()
	{
        GameManager getGameManager;
        getGameManager = GameObject.Find("Managers").GetComponent("GameManager") as GameManager;
        getGameManager.stopToRevive = false;
        RevivePanel.SetActive(false);
	}

	public void ClickToEnding()
	{
        Time.timeScale = 1;
        SceneManager.LoadScene ("endingScene");
	}
}
