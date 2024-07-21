using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    private float countdownTimer = 0;
    private bool isActive;

    private void Start()
    {
        Hide();
        isActive = false;
        fillImage.fillAmount = 0;
        countdownTimer =  WaterlaGameManager.Instance.GetGamePlayingTimer();

        WaterlaGameManager.Instance.OnStateChanged += WaterlaGameManager_OnStateChanged;
    }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        fillImage.fillAmount = WaterlaGameManager.Instance.GetGamePlayingTimer() / countdownTimer;
    }

    private void WaterlaGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (WaterlaGameManager.Instance.IsGamePlaying())
        {
            Show();
            isActive = true;
        }
        else
        {
            if (WaterlaGameManager.Instance.IsGameOver())
            {
                Hide();
            }
        }
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
