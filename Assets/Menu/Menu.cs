using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void NextLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        print(SceneManager.sceneCountInBuildSettings);
        print(sceneIndex);
        if (sceneIndex == SceneManager.sceneCountInBuildSettings - 1)
            SceneManager.LoadScene(1);
        else
            SceneManager.LoadScene(sceneIndex + 1);
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        print("ExitGame");

        Application.Quit();
    }
}
