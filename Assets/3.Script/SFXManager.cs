using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance = null;
    public AudioSource[] audioSources;
    public AudioClip[] BGMs;
    public AudioClip[] Amiya;
    public AudioClip[] Wyvern;
    public AudioClip[] SFX;
    public AudioSource victory;
    public AudioClip victoryVoice;
    public AudioSource btnSound;
    public AudioClip btnSoundclip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

    }
    public void BtnClick()
    {
        if (btnSound.isPlaying)
        {
            btnSound.Stop();
        }
        btnSound.clip = btnSoundclip;
        btnSound.Play();
    }
    public void PlayVictory()
    {
        victory.clip = victoryVoice;
        victory.Play();
    }

}
