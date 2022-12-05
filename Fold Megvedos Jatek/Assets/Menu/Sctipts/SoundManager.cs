using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Istance;
    [SerializeField] private AudioSource musicSource, effectSource;
    public void Awake()
    {
        if (Istance == null)
        {
            Istance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    public void PlaySound(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
        musicSource.PlayOneShot(clip);
    }

    public void ChangeVolume(float value)
    {
        AudioListener.volume = value;
    }
}
