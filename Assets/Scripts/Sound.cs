using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public Slider slider;


    public void OnValueChanged()
    {
        // Set the volume of the background music to the Slider value

        PlayerPrefs.SetFloat("Volume", backgroundMusic.volume);
        backgroundMusic.volume = slider.value;
    }
}
