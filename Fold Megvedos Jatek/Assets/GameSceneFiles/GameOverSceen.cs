using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverSceen : MonoBehaviour
{

    public void RestartButton()
    {
        SceneManager.LoadScene("GraphicsScene");
        Time.timeScale = 1f;
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("MenuScene");
        Time.timeScale = 1f;
    }

}

