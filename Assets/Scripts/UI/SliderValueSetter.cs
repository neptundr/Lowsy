using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueSetter : MonoBehaviour
{
    public Mixer mixer;

    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
     
        Invoke(nameof(DelayedStart), 0.025f);
    }

    private void DelayedStart()
    {
        if (mixer == Mixer.Music)
        {
            _slider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
            Settings.This.SetMusicVolume(_slider.value);
        }
        else
        {
            _slider.value = PlayerPrefs.GetFloat("EffectsVolume", 0.85f);
            Settings.This.SetEffectsVolume(_slider.value);
        }
    }

    public void SetValue()
    {
        if (mixer == Mixer.Music)
        {
            Settings.This.SetMusicVolume(_slider.value);
            PlayerPrefs.SetFloat("MusicVolume", _slider.value);
        }
        else
        {
            Settings.This.SetEffectsVolume(_slider.value);
            PlayerPrefs.SetFloat("EffectsVolume", _slider.value);
        }
    }
}

public enum Mixer
{
    Music,
    Effects
} 