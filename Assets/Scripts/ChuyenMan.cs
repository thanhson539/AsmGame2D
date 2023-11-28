using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Input = UnityEngine.Windows.Input;

public class NewBehaviourScript : MonoBehaviour
{
    public string screenName;
    public InputField PlayerName;

    public void BtnChuyenMan()
    {
        string name = PlayerName.text;
        PlayerPrefs.SetString("PlayerName", name);
        SceneManager.LoadScene(screenName);
        Time.timeScale = 1;
    }

    private void Start()
    {
        String oldName = PlayerPrefs.GetString("PlayerName");
        if (oldName != null)
        {
            PlayerName.text = oldName;
        }
    }
}