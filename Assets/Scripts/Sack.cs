using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sack : MonoBehaviour {

    private float speed, awakeRadius; // we will get it from parent
    [HideInInspector]
    public bool isAwake = false, isDisappeard = false;
    private AudioSource source;
    private Rigidbody rb;
    private Vector3 target; // random position on planet
    private Transform player;

    public void Init() {
        SackManager parent = GetComponentInParent<SackManager>();
        speed = parent.speed;
        awakeRadius = parent.awakeRadius;
    }

    private void Start() {
        source = GetComponent<AudioSource>();
        Init();
        isAwake = false;
        rb = GetComponent<Rigidbody>();
        target = Random.onUnitSphere * (ValuesManager.radius + 0.5f); // TODO: put it in variable
        player = FindObjectOfType<Player>().transform;
    }

    void Update() {
        if (!isAwake && Vector3.Distance(rb.position, player.position) < awakeRadius) {
            isAwake = true;
            source.Play();
        }
    }

    void FixedUpdate() {
        if (isAwake) {
            Move();
        }
    }

    void Move() {
        if (Vector3.Distance(rb.position, target) <= 1f) { // TODO: put it in variable
            target = Random.onUnitSphere * (ValuesManager.radius + 0.5f); // TODO: put it in variable
        }

        Vector3 newPos = Vector3.MoveTowards(rb.position, target, speed * Time.deltaTime);
        Vector3 originalVel = rb.velocity;

        // fix position
        Vector3 constrained = newPos.normalized * (ValuesManager.radius + 0.5f);
        rb.position = constrained;

        // fix rotation
        rb.rotation = Quaternion.FromToRotation(transform.up, rb.position.normalized) * rb.rotation;
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