using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public float levelTime;
    private float currentTime; // we want to calculate time backwards (decreasing)

    public int neededSacks;

    private Player player;
    private SackManager sackManager;

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
        sackManager = GameObject.FindObjectOfType<SackManager>();
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
            vehicle.GetComponentInChildren<Collider>().enabled = true; // TODO: remove
            vehicle.GetComponentInChildren<Renderer>().material.color = Color.cyan; // TODO: remove
        }

        //drive the vehcile
        if (canRide && player.onVehicle) {
            WinLevel();
        }

        // you dead or you there are no more sacks
        if (player.isDead ||
            (sackManager.outOfSacks && sackManager.sacksNow + player.gotSacks < neededSacks)) {
            LoseLevel();
        }
    }

    void LoseLevel() {
        foreach (var barbarian in FindObjectsOfType<Barbarian>()) {
            barbarian.Stop();
        }
        if (!justLost) {
            counter = Time.time + 0.1f;
            justLost = true;
        } else if (Time.time >= counter) {
            Time.timeScale = 0f;
            loseMenu.SetActive(true);
            justLost = false;
            Time.timeScale = 0;
        }
    }

    void WinLevel() {
        if (!justWon) {
            justWon = true;
            counter = Time.time + 0.1f;
        } else if (Time.time >= counter) {
            winMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}