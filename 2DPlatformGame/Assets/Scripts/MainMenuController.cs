using System;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Image playButton;
    [SerializeField] private Image musicButton;
    [SerializeField] private GameObject musicToggleObject;

    private void Start()
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.ACTIVITY_RECOGNITION"))
        {
            Permission.RequestUserPermission("android.permission.ACTIVITY_RECOGNITION");
        }
        PlayerPrefsController.TryGenerateKey("current_level",0);
        MusicController.Instance.PlayMainMenuMusic();
        playButton.rectTransform.DOScale(1.2f, 1f).SetLoops(-1, LoopType.Yoyo);
        CheckMusic();
    }

    private void CheckMusic()
    {
        bool isMuted = MusicController.Instance.IsMusicMuted();
        musicToggleObject.SetActive(isMuted);
        MusicController.Instance.Mute(isMuted);
    }
    
    public void OnPlayButtonClick()
    {
        int levelIndex = PlayerPrefsController.TryGetValue<int>("current_level");
        SceneManager.LoadScene(levelIndex + 1);
    }

    public void OnMusicButtonClick()
    {
        bool isMuted = MusicController.Instance.ToggleMute();
        musicToggleObject.SetActive(isMuted);
        Debug.Log("IsMuted : " + isMuted);
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }
    
}
