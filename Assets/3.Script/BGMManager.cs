using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance = null;
    public static string currentSceneName = "Home";
    public AudioSource introSource;
    public AudioSource loopSource;
    public AudioClip[] BGMintros;
    public AudioClip[] BGMLoops;
    public bool isIntro = false;
    public int index;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        PlayBGMintro(0);

    }
    private void Update()
    {
        if (currentSceneName == "Home")
        {
            if (isIntro && !introSource.isPlaying)
            {
                PlayBGMLoop();
            }

        }
        else if(currentSceneName == "0-1")
        {
            if (isIntro && !introSource.isPlaying)
            {
                PlayBGMLoop();
            }
        }
    }
    public void PlayBGMintro(int index)
    {
        introSource.Stop();
        loopSource.Stop();
        this.index = index;
        isIntro = true;
        introSource.clip = BGMintros[index];
        introSource.Play();
        introSource.loop = false;
    }
    public void PlayBGMLoop()
    {
        isIntro = false;
        loopSource.clip = BGMLoops[index];
        loopSource.Play();
        loopSource.loop = true;
    }
    public void StopAllBGM()
    {
        isIntro = false;
        introSource.Stop();
        loopSource.Stop();
    }
}
