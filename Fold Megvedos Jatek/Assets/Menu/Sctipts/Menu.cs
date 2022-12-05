using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void QuitGame()
    {
        Debug.Log("asd");
        Application.Quit();
        
    }
}
