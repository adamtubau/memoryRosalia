﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelSelector : MonoBehaviour
{
   public void Play(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    public void Exit()
    {
        Application.Quit();
    }
}
