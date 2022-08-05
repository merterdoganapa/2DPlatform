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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            LoadNextLevel();
        }
    }
    
    public void LoadNextLevel()
    {
        var activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var nextSceneIndex = activeSceneIndex + 1;
        var sceneCountInBuildSettings = SceneManager.sceneCountInBuildSettings;
        if ((nextSceneIndex + 1) <= sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        
    }
    
    public void ConvertStepsToCoin()
    {
        int stepAmount = StepController.Instance.GetStepAmount();
        CoinController.Instance.IncreaseCoinAmount(stepAmount);
        StepController.Instance.DecreaseStepAmount(stepAmount);
    }
    
    public void OnPauseButtonClick()
    {
        SceneManager.LoadScene(0);
    }

}
