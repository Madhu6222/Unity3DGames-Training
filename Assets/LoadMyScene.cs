﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMyScene : MonoBehaviour
{
    public void LoadNewScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}