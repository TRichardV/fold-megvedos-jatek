using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverSceen : MonoBehaviour
{
    public TextMeshProUGUI Points;

    public void GameOverScore()
    {
        Points.text = "Score:" + GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().score;
    }


    public void RestartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("MenuScene");
    }





}

