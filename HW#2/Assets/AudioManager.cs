using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    
    public AudioSource AudioSource;


    public void Awake()
    {
        if (PlayerPrefs.GetInt("SoundVolume", 1) == 0)
        {
            AudioSource.volume = 0;
        }
    }

    public void SoundController()
    {
        AudioSource.volume = PlayerPrefs.GetInt("SoundVolume", 1) == 0 ? 1 : 0;
        
        PlayerPrefs.SetInt("SoundVolume", (int)AudioSource.volume);
        
        EventSystem.current.SetSelectedGameObject(null);
    }
    
    
    
}
