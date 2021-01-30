using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {
    public Transform player;

    public Transform barbody;
    public Transform playerbody;
    [Range(1, 50)]
    public float speed;
    public float jump_power, jump_distance, jump_counter, jump_cooldown;
    public float searchRadius;
    public bool jumping;
    public bool cooldown;
    private float jump_start;
    private bool foundPlayer = false;
    private Quaternion direction;

    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Vector3.Distance(barbody.position, playerbody.position) < searchRadius) {
            foundPlayer = true;
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

        if (Vector3.Distance(barbody.position, playerbody.position) > jump_distance && !cooldown) {
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
        if (!cooldown)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, player.rotation, Time.deltaTime * speed);

    }
}