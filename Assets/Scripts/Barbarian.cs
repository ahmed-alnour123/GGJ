using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbarian : MonoBehaviour {

    private float speed, searchRadius, height; // we will get it from parent
    private Transform player;
    private Rigidbody rb;
    private bool isAwake;

    void Init() {
        var parent = FindObjectOfType<BarbarianManager>();
        speed = parent.speed;
        searchRadius = parent.searchRadius;
        height = parent.height;
    }

    void Start() {
        Init();
        player = FindObjectOfType<Player>().transform;
        rb = GetComponent<Rigidbody>();
        isAwake = false;
    }

    void Update() {
        if (!isAwake && Vector3.Distance(rb.position, player.position) < searchRadius) {
            isAwake = true;
            GetComponentInChildren<Animator>().enabled = true; // to start the animation after awaking
        }
    }
    void FixedUpdate() {
        if (isAwake) {
            Move();
        }
    }

    void Move() {
        Vector3 newPos = Vector3.MoveTowards(rb.position, player.position, speed * Time.deltaTime);
        Vector3 originalPos = rb.position.normalized;

        // fix position
        Vector3 constrained = newPos.normalized * (ValuesManager.radius + height); // TODO: put it in variable
        rb.position = constrained;

        // fix rotation
        // rb.rotation = Quaternion.FromToRotation(transform.up, rb.position.normalized) * rb.rotation;
        rb.rotation = Quaternion.FromToRotation(transform.up, rb.position.normalized) *
            Quaternion.FromToRotation(transform.forward, rb.position.normalized - originalPos) *
            rb.rotation;
    }

    public void Stop() {
        speed = 0;
        GetComponentInChildren<Animator>().enabled = false;
        // play animatoin
    }
}