using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundTrigger: MonoBehaviour
{
    public AudioSource s_Audio;
    public AudioClip s_Clip;
    public bool s_Trigger;
    // Use this for initialization
    void Awake()
    {
        s_Audio = s_Audio.GetComponent<AudioSource>();
        s_Audio.clip = s_Clip;
    }

    void Update()
    {
        PlayAudio();
    }
    void PlayAudio()
    {
        if (s_Trigger == true) {

            s_Audio.Play();
            s_Trigger = false;
        }

    }
}
