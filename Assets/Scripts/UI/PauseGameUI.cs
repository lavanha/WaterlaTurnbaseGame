using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button continueButton;

    private void Start()
    {
        WaterlaGameManager.Instance.OnGamePaused += WaterlaGameManager_OnGamePaused;
        WaterlaGameManager.Instance.OnGameUnPaused += WaterlaGameManager_OnGameUnPaused;

        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        continueButton.onClick.AddListener(() =>
        {
            Hide();
            WaterlaGameManager.Instance.TogglePauseGame();
        });

        Hide();

    }

    private void WaterlaGameManager_OnGameUnPaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void WaterlaGameManager_OnGamePaused(object sender, EventArgs e)
    {
        Show();
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);

    }
}
