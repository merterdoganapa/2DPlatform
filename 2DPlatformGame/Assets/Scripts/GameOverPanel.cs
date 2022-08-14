using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : PanelController
{
    private static GameOverPanel _instance;

    public static GameOverPanel Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<GameOverPanel>(true);
            return _instance;
        }
    }

    public void OnRestartButtonClick()
    {
        GameController.Instance.RestartLevel();
    }
}
