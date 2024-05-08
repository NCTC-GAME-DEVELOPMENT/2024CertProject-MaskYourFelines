using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;
    string Stage1_Scene = "GamePlayStage";
    string MainMenu_Scene = "MainMenu";

    private void Awake()
    {
        instance = this;
    }

    public void stage1()
    {
        Debug.Log("Stage1");

        SceneManager.LoadScene(Stage1_Scene);
    }

    public void MainMenuScene()
    {
        Debug.Log("menu is loaded");
        SceneManager.LoadScene(MainMenu_Scene);
    }
}
