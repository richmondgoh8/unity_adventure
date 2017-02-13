using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour {

    public AudioClip typeSound;
    private AudioSource source;

    void OnEnable()
	{
        source = GetComponent<AudioSource>();
    }

    //Gets the music audio from type sound and places it into audiosource and plays it once.
    public void PlaySoundEffect()
    {
        source.PlayOneShot(typeSound, 1.0f);
    }

    public void StopSoundEffectIfPlaying()
    {
        if(source.isPlaying)
        {
            source.Stop();
        }
    }

}
