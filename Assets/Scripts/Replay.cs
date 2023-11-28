using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Replay : MonoBehaviour
{
    public String ScreenName;
    
    public void btnReplay()
    {
        SceneManager.LoadScene(ScreenName);
        Time.timeScale = 1;
    }
    public void btnChoiLai()
    {
        PlayerPrefs.SetInt("prefCoin", 0);
        PlayerPrefs.SetString("PlayerName", null);
        PlayerPrefs.SetInt("prefHP", 0);
        SceneManager.LoadScene("ChonMan");
    }
}
