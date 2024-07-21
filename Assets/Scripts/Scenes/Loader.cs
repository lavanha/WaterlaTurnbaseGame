using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        MainMenuScene,
        GuideScene,
        LevelListScene,
        GameSceneLevel1,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Level7,
        Level8,
        Level9,
        Level10,
        LoaderScene
    }

    public static Scene targetScene;

    public static void Load(Scene targetScene)
    {
        Loader.targetScene = targetScene; 
        SceneManager.LoadScene(Scene.LoaderScene.ToString());
    }
    public static void CallBackLoaderScene()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
