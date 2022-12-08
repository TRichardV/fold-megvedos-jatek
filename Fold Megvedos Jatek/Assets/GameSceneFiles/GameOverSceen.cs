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
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("MenuScene");
    }

}

