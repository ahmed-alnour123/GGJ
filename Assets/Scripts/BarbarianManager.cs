using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbarianManager : MonoBehaviour {

    public GameObject barbarian;
    public int numberOfBarbarians;

    [Range(1, 50)]
    public float speed;
    public float jump_power, jump_distance, jump_counter, jump_cooldown;
    public float searchRadius;

    void Start() {
        var player = GameObject.FindObjectOfType<Player>();
        for (int i = 0; i < numberOfBarbarians; i++) {
            // player.barbarians.Add(Instantiate(barbarian, Vector3.zero, Random.rotation, transform).transform);
            Instantiate(barbarian, Vector3.zero, Random.rotation, transform);
        }
    }
}