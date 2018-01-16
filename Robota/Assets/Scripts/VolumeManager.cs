using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour {

    public AudioMixerGroup soundEffects;
    public AudioMixerGroup backgroundMusic;

    public void SetVolumeSounds(float s){
        soundEffects.audioMixer.SetFloat("soundEffects", s);

    }

    public void SetBackgroundSounds(float s)
    {
        soundEffects.audioMixer.SetFloat("background", s);

    }
   
}
