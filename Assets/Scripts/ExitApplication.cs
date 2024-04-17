using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitApplication : MonoBehaviour
{
    public static string webplayerQuitURL = "http://www.google.com";
    public void Quit()
    {
        Debug.Log("Quit Application");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

#if UNITY_WEBPLAYER
     Application.OpenURL(webplayerQuitURL)
#endif
        Application.Quit();
    }


}
