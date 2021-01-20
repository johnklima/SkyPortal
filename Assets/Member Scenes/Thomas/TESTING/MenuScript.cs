using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void buttonPlay()
    {
        SceneManager.LoadScene(0);
    }

    public void buttonCredits()
    {
        SceneManager.LoadScene(1);
    }

    public void buttonQuit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
