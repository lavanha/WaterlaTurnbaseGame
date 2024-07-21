using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI enemiesDeadAmount;
    [SerializeField] private TextMeshProUGUI enemiesDeadOnDemand;
    [SerializeField] private TextMeshProUGUI resultLevel;

    private int MaxEnemiesInThisLevel;

    private void Start()
    {
        WaterlaGameManager.Instance.OnStateChanged += WaterlaGameManager_OnStateChanged;

        Hide();
    }

    private void Update()
    {

    }

    private void WaterlaGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (WaterlaGameManager.Instance.IsGameOver())
        {
            Show();
        }
        else
        {
            if (MaxEnemiesInThisLevel < UnitManager.Instance.GetEnemyUnitList().Count)
            {
                MaxEnemiesInThisLevel = UnitManager.Instance.GetEnemyUnitList().Count;
            }
            Hide();
        }
        
    }

    private void Show()
    {
        int currentEmeniesLive = UnitManager.Instance.GetEnemyUnitList().Count;

        enemiesDeadAmount.text = (MaxEnemiesInThisLevel - currentEmeniesLive).ToString();
        enemiesDeadOnDemand.text = WaterlaGameManager.Instance.GetEnemiesDeadOnDemand().ToString();

        if (MaxEnemiesInThisLevel - currentEmeniesLive >= WaterlaGameManager.Instance.GetEnemiesDeadOnDemand())
        {
            resultLevel.text = "COMPLETED";
            LoadAndSaveLevelList.Save();
        }
        else
        {
            resultLevel.text = "FAILED";
        }

        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }


}
