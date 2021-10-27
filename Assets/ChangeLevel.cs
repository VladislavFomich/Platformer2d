using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    public void NextLevelHandler()
    {
        int scenesCount = SceneManager.sceneCountInBuildSettings;
        Scene scene = SceneManager.GetActiveScene();
        int indNextScene = scene.buildIndex + 1;
        if (indNextScene != scenesCount)
        {
        SceneManager.LoadScene(indNextScene);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
        
 
        Time.timeScale = 1;
    }
}
