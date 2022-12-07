using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof (Slider))]
public class VolumeSlider : MonoBehaviour
{

    Slider slider
    {
        get { return GetComponent<Slider>(); }

    [SerializeField] private Slider _slider, _slider2;
    void Start()
    {
        SoundManager.Instance.ChangeMusicVolume(_slider.value);
        SoundManager.Instance.ChnageEffectsVolume(_slider2.value);
        _slider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMusicVolume(val));
        _slider2.onValueChanged.AddListener(val1 => SoundManager.Instance.ChnageEffectsVolume(val1));

    }

    public AudioMixer mixer;

    [SerializeField]
    private string volumeName;

    [SerializeField]
    private TMPro.TextMeshProUGUI volumeLabel;

    User userScript;
    private void Start()
    {
        StartCoroutine(ValueLoader());
    }
    public void UpdateValueOnChange(float value)
    {
        if (mixer != null)
            mixer.SetFloat(volumeName, Mathf.Log(value) * 20f);

        if (volumeLabel != null)
            volumeLabel.text = Mathf.Round(value * 100.0f).ToString() + "%";

        if (gameObject.name == "MusicSlider")
        {
            userScript.volumePct[0] = value;
        }
        else
        {
            userScript.volumePct[1] = value;
        }

        userScript.SaveData();
    }

    private IEnumerator ValueLoader()
    {
        yield return new WaitForSeconds(0.0001f);
        GameObject.FindGameObjectWithTag("MainAudio").GetComponent<DontDestroy>().mainAudio.Play();
        userScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<User>();

        if (gameObject.name == "MusicSlider")
        {
            slider.value = userScript.volumePct[0];
        }
        else
        {
            slider.value = userScript.volumePct[1];
        }

        UpdateValueOnChange(slider.value);

        slider.onValueChanged.AddListener(delegate
        {
            UpdateValueOnChange(slider.value);
        });
    }
}
