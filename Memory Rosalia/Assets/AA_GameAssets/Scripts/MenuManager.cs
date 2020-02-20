using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Play(string play)
    {
        SceneManager.LoadScene(play);
    }

    public void Tutorial(string tutorial)
    {
        SceneManager.LoadScene(tutorial);
    }



    public void Exit()
    {
        Application.Quit();
    }
}
