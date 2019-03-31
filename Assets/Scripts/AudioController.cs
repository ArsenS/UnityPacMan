using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    AudioClip startMusic;
    [SerializeField]
    AudioClip deathSoundEffect;
    [SerializeField]
    AudioClip eatingSoundEffect;
    [SerializeField]
    AudioSource source;

    public void PlayStartMusic()
    {
        source.PlayOneShot(startMusic, 1.0f);
    }

    public void PlayEatingSound()
    {
        if (!source.isPlaying)
        {
            source.clip = eatingSoundEffect;
            source.Play();
        }
        
    }

    public void PlayDeathSoundEffect()
    {
        source.PlayOneShot(deathSoundEffect, 1.0f);
    }
}
