using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public float levelTime;
    private float currentTime; // we want to calculate time backwards (decreasing)

    public int neededSacks;

    private Player player;
    private SeedManager seedManager;

    public GameObject vehicle;
    private bool canRide = false;
    public GameObject loseMenu;
    public GameObject winMenu;
    public Text timer;
    public bool justLost = false;
    private bool justWon = false;
    private float counter;

    void Start() {
        player = GameObject.FindObjectOfType<Player>();
        seedManager = GameObject.FindObjectOfType<SeedManager>();
        currentTime = levelTime;
    }

    void Update() {
        // level timer

        if (currentTime > 0) {
            currentTime -= Time.deltaTime;
            timer.text = ((int) currentTime / 60).ToString("D2") + ":" + ((int) currentTime % 60).ToString("D2");

        } else {
            LoseLevel();
        }

        // can player ride the vehicle or not?
        if (!canRide && player.gotSacks >= neededSacks) { // to make sure it called only once
            canRide = true;
            player.GetComponentInChildren<Renderer>().material.color = Color.yellow; // TODO: remove
            vehicle.GetComponentInChildren<Collider>().enabled = true; // TODO: remove
            vehicle.GetComponentInChildren<Renderer>().material.color = Color.cyan; // TODO: remove
        }

        //drive the vehcile
        if (canRide && player.onVehicle) {
            WinLevel();
        }

        // you dead or you there are no more sacks
        if (player.isDead ||
            (seedManager.outOfSacks && seedManager.sacksNow + player.gotSacks < neededSacks)) {
            LoseLevel();
        }
    }

    void LoseLevel() {
        if (!justLost) {
            counter = Time.time + 3f;
            player.GetComponentInChildren<Renderer>().material.color = Color.blue;
            justLost = true;
        } else if (Time.time >= counter) {
            Time.timeScale = 0f;
            loseMenu.SetActive(true);
            justLost = false;

        }

    }

    void WinLevel() {

        if (!justWon) {
            counter = Time.time + 3f;
            justWon = true;
        } else if (Time.time >= counter) {
            winMenu.SetActive(true);
            Time.timeScale = 0f;

            justWon = false;
        }

    }
}