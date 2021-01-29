using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour {

    public float speed;
    public float distance;
    public float awakeRadius;
    private Transform player;
    public bool isAwake = false;
    private Quaternion targetRot;
    private Quaternion newRot;
    private Transform clone;
    private Vector3 initPos;
    private Quaternion initRot;

    private void Start() {
        targetRot = transform.rotation * Random.rotation;
        player = GameObject.FindObjectOfType<Player>().transform;
        clone = transform;
        initPos = transform.position;
        initRot = transform.rotation;
        isAwake = false;
    }

    void Update() {
        if (Quaternion.Angle(transform.rotation, player.rotation) < awakeRadius) {
            isAwake = true;
        }

        if (isAwake) {
            Run();
        }
    }

    private void Run() {
        if (Quaternion.Angle(transform.rotation, targetRot) > 20) {
            newRot = Quaternion.RotateTowards(transform.rotation, targetRot, speed * Time.deltaTime); // because we need very small values
            transform.rotation = newRot;
        } else {
            targetRot = transform.rotation * Random.rotation;
        }
    }
}