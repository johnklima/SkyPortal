using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{

    //By default, the game is running
    public static bool GameIsPaused = false;

    //TitleScreenUI is on "Menu" panel
    public GameObject TitleScreenUI;

    //By default, not in credits
    public bool InCredits = false;


    private GameObject[] TitleScreenObjs;

    public void Start()
    {

        GameObject[] TitleScreenOBJs = GameObject.FindGameObjectsWithTag("TitleScreen");

        if (TitleScreenOBJs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    //Press Play button to start game
    public void buttonPlay()
    {
        Resume();
    }

    //Press Credits button to load scene(1) (credits scene)
    public void buttonCredits()
    {
        SceneManager.LoadScene(1);
        InCredits = true;
        Credits();
    }

    //Press Quit button to quit application
    public void buttonQuit()
    {
        Debug.Log("Quitting Game!");
        Application.Quit();
    }

    //Press Escape to bring menu back up when in game
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }

            else
            {
                Pause();
            }

            if (InCredits && Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(2);
                Pause();
            }
        }
    }


    //Resumes game
    public void Resume ()
    {
        TitleScreenUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    //Pauses game
    public void Pause ()
    {
        TitleScreenUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    //In credits scene
    public void Credits ()
    {
        TitleScreenUI.SetActive(false);
        Time.timeScale = 0f;
    }

}