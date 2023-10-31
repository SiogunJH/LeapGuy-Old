using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiLogic : MonoBehaviour
{
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public static void LoadLatestLevel()
    {
        Debug.LogWarning("TODO: Load latest level");
    }
    public static void QuitApp()
    {
        Debug.Log("Quit Application");
        Application.Quit();
    }
}
