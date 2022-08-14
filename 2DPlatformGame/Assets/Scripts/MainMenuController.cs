using System;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Image playButton;
    private void Start()
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.ACTIVITY_RECOGNITION"))
        {
            Permission.RequestUserPermission("android.permission.ACTIVITY_RECOGNITION");
        }
        PlayerPrefsController.TryGenerateKey("current_level",0);
        playButton.rectTransform.DOScale(1.2f, 1f).SetLoops(-1, LoopType.Yoyo);
    }
    
    public void OnPlayButtonClick()
    {
        int levelIndex = PlayerPrefsController.TryGetValue<int>("current_level");
        SceneManager.LoadScene(levelIndex + 1);
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }
    
}
