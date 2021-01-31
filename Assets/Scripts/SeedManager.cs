using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedManager : MonoBehaviour {

    public GameObject seed;
    public int maxSeeds;
    private int createdSeeds;
    public int seedsAtTime = 1;
    [HideInInspector] public int sacksNow = 0;
    Seed[] currentSeeds;
    public float lifeTime;
    public bool outOfSacks = false; // for GameManager;
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
                    sacksNow--;
                    if (createdSeeds < maxSeeds) {
                        currentSeeds[i] = CreateNew();
                    }
                }
            }
        }

        if (createdSeeds == maxSeeds) {
            outOfSacks = true;
        }
    }

    private Seed CreateNew() {
        createdSeeds++;
        sacksNow++;
        return Instantiate(seed, Vector3.zero, Random.rotation, transform).GetComponent<Seed>();
    }
}