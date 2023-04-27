using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour
{

    public InputField usernameInput;
    public InputField passwordInput;
    public Button registerButton;
    public GameObject UsernameTaken;
    public GameObject AccountRegistered;



    ArrayList credentials;

    // Start is called before the first frame update
    void Start()
    {
        UsernameTaken.gameObject.SetActive(false);
        AccountRegistered.gameObject.SetActive(false);

        registerButton.onClick.AddListener(writeToFile);

        if (File.Exists(Application.dataPath + "/credentials.txt"))
        {
            credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));
        }
        else
        {
            File.WriteAllText(Application.dataPath + "/credentials.txt", "");
        }

    }

     void Update()
    {
        bool anyInputFieldEmpty = false;

        if (string.IsNullOrEmpty(usernameInput.text))
        {
            anyInputFieldEmpty = true;
        }

        if (string.IsNullOrEmpty(passwordInput.text))
        {
            anyInputFieldEmpty = true;
        }

        registerButton.gameObject.SetActive(!anyInputFieldEmpty);

    }

    void writeToFile()
    {
        bool isExists = false;

        credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));
        foreach (var i in credentials)
        {
            if (i.ToString().Contains(usernameInput.text))
            {
                isExists = true;
                break;
            }
        }

        if (isExists)
        {
            Debug.Log($"Username '{usernameInput.text}' already exists");
            UsernameTaken.gameObject.SetActive(true);
            AccountRegistered.gameObject.SetActive(false);

        }
        else
        {
            credentials.Add(usernameInput.text + ":" + passwordInput.text);
            File.WriteAllLines(Application.dataPath + "/credentials.txt", (String[])credentials.ToArray(typeof(string)));
            Debug.Log("Account Registered");
            UsernameTaken.gameObject.SetActive(false);
            AccountRegistered.gameObject.SetActive(true);
            PlayerPrefs.SetString("Username", usernameInput.text);

        }
    }


}
