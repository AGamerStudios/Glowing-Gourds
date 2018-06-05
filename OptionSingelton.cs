using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class OptionSingelton : MonoBehaviour
{
    public GameSettings gameSettings;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    
    void Update()
    {
        ConfigureSceneAudio();
    }

    public float GetVolumeLevel()
    {
        return gameSettings.masterVolume * 0.01f;
    }
    
    private void ConfigureSceneAudio()
    {
        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        foreach (var source in sources)
        {
            source.volume = GetVolumeLevel();
            source.mute = gameSettings.masterVolumeMute;
        }
		
    }
}
