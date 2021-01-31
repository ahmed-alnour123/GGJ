using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject pauseButton;
    
    public static bool GamePaused = false;
    private GameObject MainObject;
    private Scene scene;


    private void Start() 
    {
        scene = SceneManager.GetActiveScene();
        Time.timeScale = 1f;

    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GamePaused)
            {
                Resume();

            } else 
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
        pauseButton.SetActive(true);

    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
        pauseButton.SetActive(false);
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void retry()
    {
        SceneManager.LoadScene(scene.name);
    }
}
