using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timer;
    public string textString;
    public float floaty;
    public int mcFloatFace;
    public static bool isGuest = false;

    // Start is called before the first frame update
    void Start()
    {
        if (isGuest == true)
        {
            timer.text = "LOG IN FOR TIMER";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGuest == false)
        {
            floaty = PlayerPrefs.GetFloat("Time") + Time.deltaTime;
            mcFloatFace = (int)floaty;
            textString = mcFloatFace.ToString();
            timer.text = textString;
            PlayerPrefs.SetFloat("Time", floaty);
        }
    }

    public void GuestTimer()
    {
        isGuest = true;
        timer.enabled = false; // Disable the timer UI element
    }

    public void GuestTimerBack()
    {
        isGuest = false;
        timer.enabled = true; // Enable the timer UI element
    }
}
