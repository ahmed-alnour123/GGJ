using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedManager : MonoBehaviour {

    public GameObject seed;
    public int maxSeeds;
    private int numberOfSeeds;
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
        numberOfSeeds = 1;
    }

    void Update() {
        if (currentSeed != null && currentSeed.isAwake) {
            Countdown();
        }
    }

    private void Countdown() {
        if (currentTime < time) {
            currentTime += Time.deltaTime;
        } else {
            Destroy(currentSeed.gameObject);
            if (numberOfSeeds < maxSeeds) {
                currentSeed = Instantiate(seed, initPos, initRot * Random.rotation).GetComponent<Seed>();
                currentTime = 0;
                numberOfSeeds++;
            }
        }
    }
}