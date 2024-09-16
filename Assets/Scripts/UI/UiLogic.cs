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
    public static void LoadNextLevel()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Level 1":
                LoadScene("Level 2");
                break;
            case "Level 2":
                LoadScene("Level 3");
                break;
            case "Level 3":
                LoadScene("Main Menu");
                break;
        }
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
