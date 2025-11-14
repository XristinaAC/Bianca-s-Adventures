using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    [System.Serializable]
    public enum backgroundAudio
    {
        backgroundMusic,
        levelMusic,
        bossRoomMusic
    }

    [System.Serializable]
    public enum effectsAudio
    {
        deathAudioEffect,
        buttonAudioEffect,
        jumpingAudioEffect,
        glidingAudioEffect,
        landAudioEffect,
        uiHoverAudioEffect,
        runningAudioEffect,
        metalAudioEffect,
        sizzlingPan,
        waterEffect
    }

    [System.Serializable]
    public struct BackgroundMusic
    {
        public backgroundAudio type;
        public AudioClip clip;
    }
    [System.Serializable]
    public struct SoundEffects
    {
        public effectsAudio type;
        public AudioClip clip;
    }

    [Header("_____________AudioSource_______________")]

    [SerializeField] private AudioSource MusicSource;
    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private AudioSource BackgroundSFXSource;
    //[SerializeField] private AudioSource MasSFXSource;
    [SerializeField] private AudioMixer  audioMixer;

    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider MasterSlider;


    [Header("_____________AudioClips_______________")]

    [SerializeField] List<BackgroundMusic> music;
    [SerializeField] public List<SoundEffects> effects;
   
    [SerializeField] public AudioClip mainMenuMusic;
    [SerializeField] public AudioClip inGameMusic;
 
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        SetMasterVolume();
        SetMusicVolume();
    }

    public void PlayMusic(AudioClip music)
    {
        MusicSource.clip = music;
        MusicSource.Play();
    }

    //public void PlayMusic(backgroundAudio musicT)
    //{
    //    for (int i = 0; i < music.Count; i++)
    //    {
    //        if (musicT == music[i].type)
    //        {
    //            MusicSource.clip = music[i].clip;
    //            MusicSource.Play();
    //        }
    //    }
    //}

    public void PlaySFX(AudioClip sfxEffect)
    {
        SFXSource.clip = sfxEffect;
        SFXSource.Play();
    }

    //public void PlaySFX(effectsAudio effectT)
    //{
    //    for (int i = 0; i < effects.Count; i++)
    //    {
    //        if (effectT == effects[i].type)
    //        {
    //            SFXSource.clip = effects[i].clip;
    //            SFXSource.Play();
    //        }
    //    }
    //}

    //public void PlayBackgroundSFX(effectsAudio effectT)
    //{
    //    for (int i = 0; i < effects.Count; i++)
    //    {
    //        if (effectT == effects[i].type)
    //        {
    //            BackgroundSFXSource.clip = effects[i].clip;
    //            BackgroundSFXSource.Play();
    //        }
    //    }
    //}

    //public void StopPlayingMusic(backgroundAudio musicT)
    //{
    //    for (int i = 0; i < music.Count; i++)
    //    {
    //        if (musicT == music[i].type)
    //        {
    //            MusicSource.clip = music[i].clip;
    //            MusicSource.Stop();
    //        }
    //    }
    //}

    public void SetMasterVolume()
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(MasterSlider.value) * 20);
    }

    public void SetMusicVolume()
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(MusicSlider.value) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }

    public void SetBackgroundSFXVolume(float volume)
    {
        //audioMixer.SetFloat("BackgroundSFX", Mathf.Log10(volume) * 20);
    }

}
