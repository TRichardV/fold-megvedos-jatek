using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider, _slider2;
    void Start()
    {
        SoundManager.Instance.ChangeMusicVolume(_slider.value);
        SoundManager.Instance.ChnageEffectsVolume(_slider2.value);
        _slider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMusicVolume(val));
        _slider2.onValueChanged.AddListener(val1 => SoundManager.Instance.ChnageEffectsVolume(val1));
    }


}
