using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    string Stage1_Scene = "GamePlayStage";

         public void stage1()
    {
        Debug.Log("Stage1");

        SceneManager.LoadScene(Stage1_Scene);
    }
}
