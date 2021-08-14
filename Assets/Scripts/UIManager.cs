using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    // Start is called before the first frame update  
    public Text timer;
    public GameObject loseMenu;
    public GameObject winMenu;
    public float time = 300;
    private bool justLost = false;
    private bool justWon = false;
    private float counter;

    private void Start() {

    }
    // Update is called once per frame
    void Update() {

        time -= Time.deltaTime;
        timer.text = ((int) time / 60).ToString() + ":" + ((int) time % 60).ToString();

        if (justLost && Time.time >= counter) LoseGame();
        if (justWon && Time.time >= counter) WinGame();

    }
    public void WinGame() {
        if (!justWon) {
            counter = Time.time + 3f;
            justWon = true;
        } else {
            winMenu.SetActive(true);
            Time.timeScale = 0f;

            justWon = false;
        }

    }
    public void LoseGame() {
        if (!justLost) {
            counter = Time.time + 3f;
            justLost = true;
        } else {
            Time.timeScale = 0f;
            loseMenu.SetActive(true);
            justLost = false;
        }

    }
}