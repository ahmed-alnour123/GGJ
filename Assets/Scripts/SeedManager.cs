using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedManager : MonoBehaviour {

    public GameObject seed;
    public int maxSeeds;
    // public int seedsAtTime; to make more than 1 seed at same time
    Seed currentSeed;
    public float time;
    private float currentTime;
    Vector3 initPos;
    Quaternion initRot;

    private void Start() {
        initPos = seed.transform.position;
        initRot = seed.transform.rotation;
        currentSeed = Instantiate(seed, initPos, initRot).GetComponent<Seed>();
    }

    void Update() {
        if (currentSeed.isAwake) {
            Countdown();
        }
    }

    private void Countdown() {
        if (currentTime < time) {
            currentTime += Time.deltaTime;
        } else {
            Destroy(currentSeed.gameObject);
            currentSeed = Instantiate(seed, initPos, initRot).GetComponent<Seed>();
            currentTime = 0;
        }
    }
}