using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void buttonStart()
    {
        SceneManager.LoadScene(0);
    }

    public void buttonCredits()
    {
        SceneManager.LoadScene(1);
    }

    public void buttonQuit()
    {
        Application.Quit();
    }
}
