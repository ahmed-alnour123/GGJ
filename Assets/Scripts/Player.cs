using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Range(1, 100)]
    public float verticalSpeed;
    [Range(1, 100)]
    public float horizontalSpeed;
    public int health = 2;
    public bool isDead;
    public bool onVehicle = false;
    public List<Transform> barbarians = new List<Transform>();
    [HideInInspector] public int gotSacks = 0;

    void Update() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // the we use x-axis to go forward and backward, and y-axis to rotate, we don't need z-axis
        transform.Rotate(0, h * horizontalSpeed * Time.deltaTime, 0);
        foreach (var barbarian in barbarians) {
            barbarian.Rotate(0, h * horizontalSpeed * Time.deltaTime, 0);
        }

        transform.Rotate(v * verticalSpeed * Time.deltaTime, 0, 0);

        if (health == 1) { } else if (health == 0) {
            isDead = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        switch (other.tag) {
            case "Vehicle":
                onVehicle = true;
                break;
            case "Sack":
                Destroy(other.gameObject);
                gotSacks++;
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