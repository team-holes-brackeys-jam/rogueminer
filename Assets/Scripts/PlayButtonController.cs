﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonController : MonoBehaviour
{
    public void GoToGame()
    {
        SceneManager.LoadScene(2);
    }
}
