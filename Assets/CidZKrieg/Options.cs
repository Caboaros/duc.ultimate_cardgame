
using System;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Slider SL_Volume;

    private void Start()
    {
        LoadPreferences();
    }

    public void LoadPreferences()
    {
        SL_Volume.value = PlayerPrefs.GetFloat("VolumeSlider", 0.9f);
    }

    public void SavePreferences()
    {
        PlayerPrefs.SetFloat("VolumeSlider", SL_Volume.value);
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        LoadPreferences();
    }
}
