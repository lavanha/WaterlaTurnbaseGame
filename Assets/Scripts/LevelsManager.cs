using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    public static LevelsManager Instance { get; private set; }

    private List<PlayLevelSelectedButton> playLevelSelectedButtons;

    private string[] LevelNames =
        {
            "Level1","Level2","Level3","Level4","Level5","Level6","Level7","Level8","Level9","Level10",
        };
    private bool[] LevelValues = new bool[10];

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("There's more than one UnitActionSystem" + transform + "-" + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;


        playLevelSelectedButtons = new List<PlayLevelSelectedButton>();

        PlayLevelSelectedButton.onSpawnButton += PlayLevelSelectedButton_onSpawnButton;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            Save();
        }
        if (Input.GetKey(KeyCode.F))
        {
            Load();
        }
    }

    public List<PlayLevelSelectedButton> GetPlayLevelSelectedButtonsList()
    {
        return playLevelSelectedButtons;
    } 
    public bool[] GetLevelValues()
    {
        return LevelValues;
    }

    private void PlayLevelSelectedButton_onSpawnButton(object sender, System.EventArgs e)
    {
        
        PlayLevelSelectedButton button = sender as PlayLevelSelectedButton;
        playLevelSelectedButtons.Add(button);
    }

    private void Save()
    {
        int i = 0;
        foreach (string Level in LevelNames)
        {
            foreach(PlayLevelSelectedButton button in playLevelSelectedButtons)
            {
                if (button.GetNameLevel().ToString() == Level)
                {
                    LevelValues[i] = button.CanOpenLevel();
                    i++;
                    break;
                }
            }
        }

        SaveObject saveObject = new SaveObject
        {
            Level1 = LevelValues[0],
            Level2 = LevelValues[1],
            Level3 = LevelValues[2],
            Level4 = LevelValues[3],
            Level5 = LevelValues[4],
            Level6 = LevelValues[5],
            Level7 = LevelValues[6],
            Level8 = LevelValues[7],
            Level9 = LevelValues[8],
            Level10 = LevelValues[9]
        };

        string json = JsonUtility.ToJson(saveObject);
        File.WriteAllText(Application.dataPath + "/save.txt", json);
    }

    public void Load()
    {
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/save.txt");
            Debug.Log("Load" + saveString);
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);


            foreach (string Level in LevelNames)
            {
                foreach (PlayLevelSelectedButton button in playLevelSelectedButtons)
                {
                    if (button.GetNameLevel().ToString() == Level)
                    {
                        switch (Level)
                        {
                            case "Level1":
                                button.SetCanOpenLevel(saveObject.Level1);
                                break;
                            case "Level2":
                                button.SetCanOpenLevel(saveObject.Level2);
                                break;
                            case "Level3":
                                button.SetCanOpenLevel(saveObject.Level3);
                                break;
                            case "Level4":
                                button.SetCanOpenLevel(saveObject.Level4);
                                break;
                            case "Level5":
                                button.SetCanOpenLevel(saveObject.Level5);
                                break;
                            case "Level6":
                                button.SetCanOpenLevel(saveObject.Level6);
                                break;
                            case "Level7":
                                button.SetCanOpenLevel(saveObject.Level7);
                                break;
                            case "Level8":
                                button.SetCanOpenLevel(saveObject.Level8);
                                break;
                            case "Level9":
                                button.SetCanOpenLevel(saveObject.Level9);
                                break;
                            case "Level10":
                                button.SetCanOpenLevel(saveObject.Level10);
                                break;
                        }
                        break;
                    }
                }
            }

        }
    }


    private class SaveObject
    {
        public bool Level1;
        public bool Level2;
        public bool Level3;
        public bool Level4;
        public bool Level5;
        public bool Level6;
        public bool Level7;
        public bool Level8;
        public bool Level9;
        public bool Level10;
    }

}
