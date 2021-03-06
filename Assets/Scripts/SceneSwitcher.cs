﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{  
    public void LoadTutorialScene()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void LoadPlayScene()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("SceneLoader");
    }

    public void LoadModeSelectionScene()
    {
        SceneManager.LoadScene("ModeSelection");
    }

    public void LoadDrinkingModeScene()
    {
        SceneManager.LoadScene("DrinkingModeScene");
    }

    public void LoadExplodeModeScene()
    {
        SceneManager.LoadScene("ExplodeModeScene");
    }
}
