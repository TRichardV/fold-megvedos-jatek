using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    Transform cameraPosition;
    public AudioSource mainAudio;

    private void Awake()
    {
        mainAudio = gameObject.GetComponent<AudioSource>();

        GameObject[] objs = GameObject.FindGameObjectsWithTag("MainAudio");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        cameraPosition = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();

        if (!mainAudio.isPlaying)
        {
            mainAudio.Play();
        }

    }
    private void Update()
    {
        gameObject.transform.position = new Vector3(cameraPosition.position.x, cameraPosition.position.y, cameraPosition.position.z);
    }
}
