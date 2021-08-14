using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject levelsMenu;
    public GameObject mainMenu;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadLevels()
    {
        levelsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void Play()
    {
        SceneManager.LoadScene("Main");

    }
    public void Quit()
    {
Application.Quit();
    }
    public void Return()
    {
        levelsMenu.SetActive(false);
        mainMenu.SetActive(true); 
    }
}
