using System;
using System.Collections;
using System.Collections.Generic;
using PlatformGame;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private Scene[] levels;
    private static GameController _instance;

    public static GameController Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<GameController>();
            return _instance;
        }
    }

    private void Start()
    {
        MusicController.Instance.PlayInGameMusic();
    }

    public void LoadNextLevel()
    {
        var activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var nextSceneIndex = activeSceneIndex + 1;
        var sceneCountInBuildSettings = SceneManager.sceneCountInBuildSettings;
        if ((nextSceneIndex + 1) <= sceneCountInBuildSettings)
        {
            PlayerPrefsController.TrySetValue("current_level",activeSceneIndex);
            LoadLevelByBuildIndex(nextSceneIndex);
        }
        else
        {
            PlayerPrefsController.TrySetValue("current_level",0);
            LoadLevelByBuildIndex(0);
        }
    }

    private void LoadLevelByBuildIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void RestartLevel()
    {
        int currentLevelIndex = PlayerPrefsController.TryGetValue<int>("current_level");
        LoadLevelByBuildIndex(currentLevelIndex + 1);
    }
    
    public void ConvertStepsToCoin()
    {
        int stepAmount = StepController.Instance.GetStepAmount();
        CoinController.Instance.IncreaseCoinAmount(stepAmount);
        StepController.Instance.DecreaseStepAmount(stepAmount);
    }

    public void OnHomePageButtonClick()
    {
        LoadLevelByBuildIndex(0);
    }
    
}
