using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;

    public void Start()
    {
        transform.GetComponent<Slider>().value = PlayerPrefs.GetFloat("OptionVolume");
    }
    public void Update()
    {
        float value = transform.GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("OptionVolume", value);
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }
    
    public void SetSliderValue(float value)
    {
        transform.GetComponent<Slider>().value = value;
    }
}
