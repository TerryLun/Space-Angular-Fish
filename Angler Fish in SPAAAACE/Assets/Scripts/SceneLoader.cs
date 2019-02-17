using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(2);
    }

    public void Starting()
    {
        SceneManager.LoadScene(0);
    }

    public void Instructions()
    {
        SceneManager.LoadScene(1);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(3);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
