using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedManager : MonoBehaviour {

    public GameObject seed;
    public int maxSeeds;
    private int createdSeeds;
    public int seedsAtTime = 1;
    Seed[] currentSeeds;
    public float lifeTime;
    public bool outOfSeeds = false; // for GameManager;
    private float currentTime;

    public float speed;
    public float distance;
    public float awakeRadius;

    public void Start() {
        currentSeeds = new Seed[seedsAtTime];
        for (int i = 0; i < currentSeeds.Length; i++) {
            currentSeeds[i] = CreateNew();
        }
    }

    public void Update() {
        for (int i = 0; i < currentSeeds.Length; i++) {
            if (currentSeeds[i] != null) {
                if (currentSeeds[i].isAwake) {
                    currentSeeds[i].Countdown(lifeTime);
                }
                if (currentSeeds[i].isDisappeard) {
                    Destroy(currentSeeds[i].gameObject);
                    if (createdSeeds < maxSeeds) {
                        currentSeeds[i] = CreateNew();
                    }
                }
            }
        }

        if (createdSeeds == maxSeeds) {
            outOfSeeds = true;
        }
    }

    private Seed CreateNew() {
        createdSeeds++;
        return Instantiate(seed, Vector3.zero, Random.rotation, transform).GetComponent<Seed>();
    }
}