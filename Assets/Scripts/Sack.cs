using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sack : MonoBehaviour {

    private float speed, awakeRadius, height; // we will get it from parent
    [HideInInspector]
    public bool isAwake = false, isDisappeard = false;
    private AudioSource source;
    private Rigidbody rb;
    private Vector3 target; // random position on planet
    private Transform player;
    private PauseMenu pausemenu;

    public void Init() {
        SackManager parent = GetComponentInParent<SackManager>();
        speed = parent.speed;
        awakeRadius = parent.awakeRadius;
        height = parent.height;
    }

    private void Start() {
        pausemenu = GameObject.Find("Canvas").GetComponent<PauseMenu>();
        source = GetComponent<AudioSource>();
        Init();
        isAwake = false;
        rb = GetComponent<Rigidbody>();
        target = Random.onUnitSphere * (ValuesManager.radius + height);
        player = FindObjectOfType<Player>().transform;
    }

    void Update() {
        if (!isAwake && Vector3.Distance(rb.position, player.position) < awakeRadius) {
            isAwake = true;
            source.Play();
            GetComponentInChildren<Animator>().enabled = true; // to start the animation after awaking
        }
    }

    void FixedUpdate() {
        if (isAwake) {
            Move();
        }
    }

    void Move() {
        if (Vector3.Distance(rb.position, target) <= 5f) { // TODO: put it in variable
            target = Random.onUnitSphere * (ValuesManager.radius + height); // TODO: put it in variable
        }

        Vector3 newPos = Vector3.MoveTowards(rb.position, target, speed * Time.deltaTime);
        Vector3 originalPos = rb.position.normalized;

        // fix position
        Vector3 constrained = newPos.normalized * (ValuesManager.radius + height);
        rb.position = constrained;

        // fix rotation
        rb.rotation = Quaternion.FromToRotation(transform.up, rb.position.normalized) *
            Quaternion.FromToRotation(transform.forward, rb.position.normalized - originalPos) *
            rb.rotation;
    }

    float currentTime = 0;
    public void Countdown(float time) {
        if (currentTime < time) {
            currentTime += Time.deltaTime;
        } else {
            pausemenu.sackUIrefresh(false);

            isDisappeard = true;
        }
    }
}