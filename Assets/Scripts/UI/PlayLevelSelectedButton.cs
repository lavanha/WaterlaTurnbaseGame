using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayLevelSelectedButton : MonoBehaviour
{
    public static event EventHandler onSpawnButton;

    [SerializeField] private Button playLevelButton;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private Loader.Scene scene;

    [SerializeField] private bool canOpenLevel;

    private void Start()
    {
        onSpawnButton?.Invoke(this, EventArgs.Empty);
        textMeshProUGUI.text = scene.ToString();
        playLevelButton.onClick.AddListener(() =>
        {
            LoadAndSaveLevelList.SetCurrentScene(scene);
            LoadAndSaveLevelList.SetPlayLevelSelectedButtons(LevelsManager.Instance.GetPlayLevelSelectedButtonsList());
            LoadAndSaveLevelList.SetLevelValues(LevelsManager.Instance.GetLevelValues());
            Loader.Load(scene);
        });

        LevelsManager.Instance.Load();
    }

    private void Update()
    {
        if (canOpenLevel)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public bool CanOpenLevel()
    {
        return canOpenLevel;
    }
    public Loader.Scene GetNameLevel()
    {
        return scene;
    }

    public void SetCanOpenLevel(bool canOpenLevel)
    {
        this.canOpenLevel = canOpenLevel;
        Debug.Log(GetNameLevel().ToString() + " " + canOpenLevel);
    }
}
