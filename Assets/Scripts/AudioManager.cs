using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set;}
    public AudioSource bgmSource;
    public List<AudioClip> bgmList = new ();
    public AudioSource sfxSource;
    public List<AudioClip> sfxList = new ();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySfxOneShot(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlaySfxOneShot(int index)
    {
        sfxSource.PlayOneShot(sfxList[index]);
    }

    public void PlayBgm(AudioClip clip)
    {
        if(bgmSource.clip == clip && bgmSource.isPlaying)
            return;

        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void PlayBgm(int index)
    {
        AudioClip clip = sfxList[index];
        if(bgmSource.clip == clip && bgmSource.isPlaying)
            return;

        bgmSource.clip = clip;
        bgmSource.Play();
    }   

    public void StopBgm()
    {
        bgmSource.Stop();
    }
}
