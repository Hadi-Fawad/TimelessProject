using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishScreen : MonoBehaviour
{

    public void finish(int score)
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void continueButton()
    {
        Time.timeScale = 1f;
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the next scene by incrementing the current scene index
        SceneManager.LoadScene(currentSceneIndex + 1);


    }

    public void quitButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScreen");
    }
}
