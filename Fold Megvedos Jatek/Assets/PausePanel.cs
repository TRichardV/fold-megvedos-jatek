using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    public void PanelOpener()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        GetComponent<Animator>().Play("PausePanelAppear");
        GameObject.Find("InGamePanel").GetComponent<Animator>().Play("InGamePanelDisAppear");
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        GetComponent<Animator>().Play("PausePanelDisAppear");
        GameObject.Find("InGamePanel").GetComponent<Animator>().Play("InGamePanelAppear");
        StartCoroutine(Scaler());
    }

    IEnumerator Scaler()
    {
        yield return new WaitForSeconds(0.3333333333f);
        transform.localScale = new Vector3(0f, 0f, 0f);
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
