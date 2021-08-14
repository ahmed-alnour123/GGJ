using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;
    [Range(0, 1)]
    public float slowDownFactor;
    public float rotSpeed;
    public float height;
    public float ghostTime;
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
    private float newSpeed;
    private bool isGhost = false;
    private Collider playerCollider;
    public CanvasScript canvas;

    // just play start
    public static Player current;
    public event System.Action onPlayerHit;
    public void PlayerHit() {
        if (onPlayerHit != null) {
            onPlayerHit();
        }
    }
    private void Awake() {
        current = this;
    }
    // just play end

    private void Start() {
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
    }
    void Update() {
        h = Input.GetAxisRaw("Horizontal");
        // v = Input.GetAxisRaw("Vertical");
        v = 1;
        newSpeed = (h == 0) ? speed : speed * slowDownFactor;
        if (true || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            transform.Find("Dust").gameObject.SetActive(true);
        } else {
            transform.Find("Dust").gameObject.SetActive(false);
        }

        if (health == 0) {
            isDead = true;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    void FixedUpdate() {
        if (!isDead) {
            Move();

            if (isGhost) {
                StartCoroutine(BecomeGhost(ghostTime)); // this is not working perfectly
            }
        }
    }

    void Move() {
        Vector3 newPos = rb.position + (transform.forward * v * newSpeed * Time.deltaTime);
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
                canvas.sackUIrefresh(true);
                break;
        }
    }

    private void OnTriggerExit(Collider other) {
        onVehicle = false;
    }

    private void OnCollisionEnter(Collision other) {

        switch (other.gameObject.tag) {
            case "Barbarian":
                health--;
                isGhost = true;
                PlayerHit();
                break;
        }
    }

    IEnumerator BecomeGhost(float time) {
        playerCollider.isTrigger = true;
        GetComponentInChildren<Renderer>().enabled = !GetComponentInChildren<Renderer>().enabled; // TODO: optimize this
        yield return new WaitForSeconds(time);
        isGhost = false;
        playerCollider.isTrigger = false;
        GetComponentInChildren<Renderer>().enabled = true;
    }

}