using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public void OnPauseButtonClick()
    {
        SceneManager.LoadScene(0);
    }
}
