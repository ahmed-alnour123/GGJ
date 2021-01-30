using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour {

    public float speed;
    public float distance;
    public float awakeRadius;
    public bool isAwake = false;
    private Transform playerBody;
    private Transform body;
    private Quaternion targetRot;
    private Quaternion newRot;

    private void Start() {
        targetRot = transform.rotation * Random.rotation;
        playerBody = GameObject.FindObjectOfType<Player>().transform.GetChild(0);
        body = transform.GetChild(0);
        isAwake = false;
    }

    void Update() {
        if (Mathf.Abs((body.position - playerBody.position).magnitude) < awakeRadius) {
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