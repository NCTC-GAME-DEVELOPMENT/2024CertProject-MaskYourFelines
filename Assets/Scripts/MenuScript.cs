using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public string sceneName;

   public void QuitGame()
   {
        Application.Quit();
        Debug.Log("Quit!");
   }

    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

}
