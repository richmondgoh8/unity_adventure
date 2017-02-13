using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonSoundScript : MonoBehaviour {

    public AudioSource buttonSound;

	void OnEnable()
	{
        buttonSound = GetComponent<AudioSource>();
	}

    public void PlayButtonSound()
    {
        buttonSound.Play();
    }

}
