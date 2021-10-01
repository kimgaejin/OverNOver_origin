using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btStop : MonoBehaviour {

    bool stop = false;
    public GameObject stopPanel;

    public void ClickStopButton() {
        stop = !stop;
        if (stop == true) {
            Time.timeScale = 0;
            stopPanel.SetActive(true);
        } else {
            Time.timeScale = 1;
            stopPanel.SetActive(false);
        }
    }
}
