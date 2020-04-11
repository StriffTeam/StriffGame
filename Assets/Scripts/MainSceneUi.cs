using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneUi : MonoBehaviour
{
    public void StartGameButtonPressed()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGameButtonPressed()
    {
        Application.Quit();
    }
}
