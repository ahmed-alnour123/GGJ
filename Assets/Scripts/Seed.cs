using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour {

    // TODO: start paricle system only when waking up
    private float speed, distance, awakeRadius;
    [HideInInspector]
    public bool isAwake = false, isDisappeard = false;
    private Transform playerBody;
    private Transform body;
    private Quaternion targetRot;
    private Quaternion newRot;

    public void Init() {
        SeedManager parent = GetComponentInParent<SeedManager>();
        speed = parent.speed;
        distance = parent.distance;
        awakeRadius = parent.awakeRadius;
    }

    private void Start() {
        Init();
        targetRot = transform.rotation * Random.rotation;
        playerBody = GameObject.FindObjectOfType<Player>().transform.GetChild(0);
        body = transform.GetChild(0);
        isAwake = false;
    }

    void Update() {
        if (!isAwake && Mathf.Abs((body.position - playerBody.position).magnitude) < awakeRadius) {
            isAwake = true;
            body.GetComponent<MeshRenderer>().material.color = Color.magenta;
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

    float currentTime = 0;
    public void Countdown(float time) {
        if (currentTime < time) {
            currentTime += Time.deltaTime;
        } else {
            isDisappeard = true;
        }
    }
}