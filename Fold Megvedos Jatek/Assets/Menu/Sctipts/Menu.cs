using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    bool isOpened = false;
    private void Start()
    {
        if (gameObject.name =="SettingsPart")
        {
            gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
        }
        
    }

    public void panelOpener()
    {
        if (isOpened)
        {
            gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
            isOpened = false;
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            isOpened = true;
        }
    }
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
