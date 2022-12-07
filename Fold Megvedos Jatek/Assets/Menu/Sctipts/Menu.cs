using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    bool isOpened = false;
    private void Start()
    {

        
    }

    public void panelOpener()
    {
        if (isOpened)
        {
            GetComponent<Animator>().Play("CloseSettingPart", 0, 0);
            GameObject.Find("Main Menu").GetComponent<Animator>().Play("MainMenuAnimationClose", 0,0);
            isOpened = false;
        }
        else
        {
            GetComponent<Animator>().Play("OpenSettingPart",0,0);
            GameObject.Find("Main Menu").GetComponent<Animator>().Play("MainMenuAnimation",0,0);
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
