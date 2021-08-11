using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppMethods : MonoBehaviour
{

    public void ExitApplication()

    {
        Application.Quit();
        Debug.Log(">>>> ExitApplication <<<<");
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
