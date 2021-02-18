using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;
    public float rotSpeed;
    public float height;
    public int health = 2;

    [HideInInspector]
    public bool isDead;
    [HideInInspector]
    public bool onVehicle = false;
    [HideInInspector]
    public int gotSacks = 0;

    private AudioSource source;
    private Rigidbody rb;
    private float h, v;

    private void Start() {
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }
    void Update() {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (health == 0) {
            isDead = true;
        }
    }

    void FixedUpdate() => Move();

    void Move() {
        Vector3 originalVel = rb.velocity;

        Vector3 newPos = rb.position + (transform.forward * v * speed * Time.deltaTime);
        Quaternion newRot = rb.rotation * Quaternion.Euler(0, h * rotSpeed * Time.deltaTime, 0);

        // fix position
        Vector3 constrained = newPos.normalized * (ValuesManager.radius + height);
        rb.position = constrained;

        // fix rotation
        rb.rotation = Quaternion.FromToRotation(transform.up, rb.position.normalized) * newRot;
    }

    private void OnTriggerEnter(Collider other) {
        switch (other.tag) {
            case "Vehicle":
                onVehicle = true;
                break;
            case "Sack":
                Destroy(other.gameObject);
                gotSacks++;
                source.Play();
                break;
            case "Barbarian":
                health--;
                break;
        }
    }

    private void OnTriggerExit(Collider other) {
        onVehicle = false;
    }
}