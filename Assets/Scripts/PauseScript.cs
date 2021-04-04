using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    [SerializeField] GameObject pauseObj;

    public void PauseButton() {
        pauseObj.SetActive(true);
        GameManager.instance.pause = true;
    }

    public void ResumeButton() {
        GameManager.instance.pause = false;
        pauseObj.SetActive(false);
    }

    public void BackToMenuButton() {
        SceneManager.LoadScene(0);
    }
}
