using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountdownToStartUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownToStartText;

    private void Start()
    {
        WaterlaGameManager.Instance.OnStateChanged += WaterlaGameManager_OnStateChanged;
        Hide();
    }

    private void Update()
    {
        countdownToStartText.text = Mathf.Ceil( WaterlaGameManager.Instance.GetCountdownToStartAmount()).ToString();
    }

    private void WaterlaGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (WaterlaGameManager.Instance.IsCountdownToStart())
        {
            Show();
        }
        else
        {
            Hide();
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
