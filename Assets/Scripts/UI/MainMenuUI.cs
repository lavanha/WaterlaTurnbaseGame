using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button guideButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.LevelListScene);
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        guideButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.GuideScene);
        });
        Time.timeScale = 1.0f;
    }
}
