using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private AudioClip clip;

    void Start()
    {
        SoundManager.Istance.PlaySound(clip);    
    }

  
}
