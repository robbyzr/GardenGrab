using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Audio Clip")]
    public AudioClip bgm;
    public AudioClip endgame;
    public AudioClip bomb;
    public AudioClip heart;
    public AudioClip scoreIn;
    public AudioClip click;

    public static AudioManager instance;
    private void Awake()
    {
        if(instance == null)
        {
           instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        musicSource.clip = bgm;
        musicSource.Play();
    }

    public void PlaySFX (AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
