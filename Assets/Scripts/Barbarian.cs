using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbarian : MonoBehaviour {

    private Transform player;
    private Transform body;
    private Transform playerbody;

    private float speed;
    private float jump_power, jump_distance, jump_counter, jump_cooldown;
    private float searchRadius;
    public bool jumping;
    public bool cooldown;

    private float jump_start;
    private bool foundPlayer = false;
    private Quaternion direction;

    void Init() {
        var parent = FindObjectOfType<BarbarianManager>();
        speed = parent.speed;
        jump_power = parent.jump_power;
        jump_distance = parent.jump_distance;
        jump_counter = parent.jump_counter;
        jump_cooldown = parent.jump_cooldown;
        searchRadius = parent.searchRadius;
    }
    void Start() {
        Init();
        body = transform.GetChild(0);
        player = GameObject.FindObjectOfType<Player>().transform;
        playerbody = player.GetChild(0);
    }

    void Update() {
        if (!foundPlayer && Vector3.Distance(body.position, playerbody.position) < searchRadius) { // we need to check for this only once for performance
            foundPlayer = true;
            body.GetComponent<MeshRenderer>().material.color = Color.green;
        }

        if (foundPlayer) {
            FollowPlayer(speed);
            Jump(jump_power, jump_distance, jump_counter, jump_cooldown);
        }
    }

    private void Jump(float jump_power, float jump_distance, float jump_counter, float jump_cooldown) {
        if (Time.time >= jump_start + jump_counter + jump_cooldown) {
            cooldown = false;
        }

        if (Vector3.Distance(body.position, playerbody.position) > jump_distance && !cooldown) {
            if (!jumping) {
                jump_start = Time.time;
            }

            jumping = true;
            if (Time.time <= jump_start + jump_counter - 0.5f) {
                direction = player.rotation;
            } else if (Time.time >= jump_start + jump_counter) {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, direction, Time.deltaTime * jump_power);

                if (Time.time >= jump_start + jump_counter + 0.3f) {
                    cooldown = true;
                    if (Time.time >= jump_start + jump_counter + jump_cooldown) {
                        cooldown = false;
                    }
                }
            }

        } else {
            jumping = false;
        }
    }

    private void FollowPlayer(float speed) {
        if (!cooldown) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, player.rotation, Time.deltaTime * speed);
        }
    }
}