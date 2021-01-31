using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public float levelTime;
    private float currentTime; // we want to calculate time backwards (decreasing)

    public int neededSacks;

    private Player player;
    private SeedManager seedManager;

    public GameObject vehicle;
    private bool canRide = false;

    void Start() {
        player = GameObject.FindObjectOfType<Player>();
        seedManager = GameObject.FindObjectOfType<SeedManager>();
        currentTime = levelTime;
    }

    void Update() {
        // level timer
        if (currentTime > 0) {
            currentTime -= Time.deltaTime;
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
        Time.timeScale = 0; // TODO: remove
        print("Lost");
        player.GetComponentInChildren<Renderer>().material.color = Color.blue;
    }

    void WinLevel() {
        Time.timeScale = 0; // TODO: remove
        print("Won");
    }
}