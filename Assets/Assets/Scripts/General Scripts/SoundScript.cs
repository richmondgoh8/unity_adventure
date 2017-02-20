using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour {

    public AudioClip typeSound;
    private AudioSource[] source;

	public AudioSource noise1;
	public AudioSource noise0;
    void OnEnable()
	{
        source = GetComponents<AudioSource>();
		noise0 = source [2];
		//noise1 = source [1];
		//Debug.Log (source.Length);
    }

    //Gets the music audio from type sound and places it into audiosource and plays it once.
    public void PlaySoundEffect()
    {
		noise0.PlayOneShot(typeSound, 1.0f);
        //source.PlayOneShot(typeSound, 1.0f);
    }

    public void StopSoundEffectIfPlaying()
    {
		if(noise0.isPlaying)
        {
			noise0.Stop();
        }
    }

	public void PlayIndicatedSoundEffect()
	{
	}

}
