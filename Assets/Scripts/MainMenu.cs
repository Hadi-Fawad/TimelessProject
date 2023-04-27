using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider slider;
    public AudioSource backgroundMusic;
    public float timeLevel1 = 0;
    public float timeLevel2 = 0;
    public float timeLevel3 = 0;
    public float timeLevel4 = 0;

    void Start()
    {
        // Set the starting value of the Slider to the current volume of the background music
        backgroundMusic.volume = PlayerPrefs.GetFloat("Volume", 1.0f); // Use 1.0f as default value
        slider.value = backgroundMusic.volume;
        PlayerPrefs.SetString("UserName", "");

        if (SceneManager.GetActiveScene().name == "Keeton") // Check if the current scene is named "Keeton"
        {
            PlayerPrefs.SetFloat("Time", 0); // Reset the timer value to 0 in player preferences
        }

    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Level1()
    {
        SceneManager.LoadScene("Keeton");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Hadi's");
    }

    public void Level3()
    {
        SceneManager.LoadScene("JohnLevel3");
    }

    public void Level4()
    {
        SceneManager.LoadScene("Base Level");
    }


    // Callback method for handling Slider value changes
    public void OnValueChanged()
    {
        // Set the volume of the background music to the Slider value
        backgroundMusic.volume = slider.value;
        PlayerPrefs.SetFloat("Volume", backgroundMusic.volume);
    }

   
}
