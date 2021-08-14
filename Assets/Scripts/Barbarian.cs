using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbarian : MonoBehaviour {

    private float speed, searchRadius, height; // we will get it from parent
    private Transform player;
    private Rigidbody rb;
    private bool isAwake;
    private Vector3 target;
    private AudioSource sound;

    void Init() {
        var parent = FindObjectOfType<BarbarianManager>();
        speed = parent.speed / 2f;
        searchRadius = parent.searchRadius;
        height = parent.height;
    }

    void Start() {
        Init();
        player = FindObjectOfType<Player>().transform;
        rb = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();
        isAwake = false;
        target = Random.onUnitSphere * (ValuesManager.radius + height); // TODO: put it in variable
    }

    void Update() {
        if (!isAwake && Vector3.Distance(rb.position, player.position) < searchRadius) {
            isAwake = true;
            sound.Play();
            speed *= 2;
            // GetComponentInChildren<Animator>().enabled = true; // to start the animation after awaking
        }
        Move();
    }
    void FixedUpdate() {
        // if (isAwake)
        // Move();
    }

    void Move() {
        if (Vector3.Distance(rb.position, target) <= 5f) { // TODO: put it in variable
            target = Random.onUnitSphere * (ValuesManager.radius + height); // TODO: put it in variable
        }

        Vector3 newPos = Vector3.MoveTowards(rb.position, isAwake?player.position : target, speed * Time.deltaTime);
        Vector3 originalPos = rb.position.normalized;

        // fix position
        Vector3 constrained = newPos.normalized * (ValuesManager.radius + height);
        rb.position = constrained;

        // fix rotation
        transform.rotation = Quaternion.FromToRotation(transform.up, rb.position.normalized) *
            Quaternion.FromToRotation(transform.forward, rb.position.normalized - originalPos) *
            transform.rotation;
    }

    public void Stop() {
        speed = 0;
        // GetComponentInChildren<Animator>().enabled = false;
        // play animatoin
    }
}