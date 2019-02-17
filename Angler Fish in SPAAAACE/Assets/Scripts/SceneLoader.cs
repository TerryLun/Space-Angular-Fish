﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
