using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] public AudioClip _mainMenuMusic;
    [SerializeField] public AudioClip _inGameMusic;
    [SerializeField] public AudioSource _audio;
    private AudioClip _currentClip;
    private static MusicController _instance;

    public static MusicController Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<MusicController>();
            return _instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        PlayerPrefsController.TryGenerateKey("music_muted", 0);
    }

    public bool IsMusicMuted() => PlayerPrefsController.TryGetValue<int>("music_muted") == 1;

    private void PlayMusic()
    {
        _audio.clip = _currentClip;
        _audio.Play();
    }

    public void PlayMainMenuMusic()
    {
        _currentClip = _mainMenuMusic;
        PlayMusic();
    }

    public void PlayInGameMusic()
    {
        _currentClip = _inGameMusic;
        PlayMusic();
    }

    public bool ToggleMute()
    {
        _audio.mute = _audio.mute == true ? false : true;
        int value = _audio.mute == true ? 1 : 0;
        PlayerPrefsController.TrySetValue("music_muted", value);
        return _audio.mute;
    }

    public void Mute(bool mute)
    {
        int value = mute == true ? 1 : 0;
        _audio.mute = mute;
        PlayerPrefsController.TrySetValue("music_muted", value);
    }

    

}
