using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public static class LoadAndSaveLevelList
{
    public static Loader.Scene currentScene;

    private static List<PlayLevelSelectedButton> playLevelSelectedButtons;

    private static string[] LevelNames =
        {
            "Level1","Level2","Level3","Level4","Level5","Level6","Level7","Level8","Level9","Level10",
        };
    private static bool[] LevelValues = new bool[10];


    public static void SetCurrentScene(Loader.Scene currentScene)
    {
        LoadAndSaveLevelList.currentScene = currentScene;
    } 

    public static Loader.Scene GetNextLevelScene()
    {
        switch(currentScene)
        {
            case Loader.Scene.Level1:
                return Loader.Scene.Level2;

            case Loader.Scene.Level2:
                return Loader.Scene.Level3;

            case Loader.Scene.Level3:
                return Loader.Scene.Level4;
            
            case Loader.Scene.Level4:
                return Loader.Scene.Level5;
              
            case Loader.Scene.Level5:
                return Loader.Scene.Level6;
               
            case Loader.Scene.Level6:
                return Loader.Scene.Level7;
        
            case Loader.Scene.Level7:
                return Loader.Scene.Level8;

            case Loader.Scene.Level8:
                return Loader.Scene.Level9;
               
            case Loader.Scene.Level9:
                return Loader.Scene.Level10;
        

        }
        return Loader.Scene.Level1;
    }

    public static void Save()
    {
        for (int y = 0; y < playLevelSelectedButtons.Count; y++)
        {
            if (playLevelSelectedButtons[y].GetNameLevel().ToString() == GetNextLevelScene().ToString())
            {
                playLevelSelectedButtons[y].SetCanOpenLevel(true);
            }
        }

        int i = 0;
        foreach (string Level in LevelNames)
        {
            foreach (PlayLevelSelectedButton button in playLevelSelectedButtons)
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
        Debug.Log(json);
    }
    public static void Load()
    {
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/save.txt");
            Debug.Log("Load" + saveString);
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);


          
                foreach (PlayLevelSelectedButton button in playLevelSelectedButtons)
                {
                    if (button.GetNameLevel().ToString() == currentScene.ToString())
                    {
                        button.SetCanOpenLevel(true);
                        break;
                    }
                }
            

        }
    }

    public class SaveObject
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

    public static void SetPlayLevelSelectedButtons(List<PlayLevelSelectedButton> playLevelSelectedButton)
    {
        LoadAndSaveLevelList.playLevelSelectedButtons = playLevelSelectedButton;
    }

    public static void SetLevelValues(bool[] levelValues)
    {
        LoadAndSaveLevelList.LevelValues = levelValues;
    }

}
