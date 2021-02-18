using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbarianManager : MonoBehaviour {

    public GameObject barbarian;
    public int numberOfBarbarians;

    [Header("For Children")]
    public float speed;
    public float searchRadius;
    public float height;

    void Start() {
        var player = GameObject.FindObjectOfType<Player>();
        for (int i = 0; i < numberOfBarbarians; i++) {
            var rb = Instantiate(barbarian,
                Random.onUnitSphere * (ValuesManager.radius + height),
                Quaternion.identity,
                transform).GetComponent<Rigidbody>();
            rb.rotation = Quaternion.FromToRotation(rb.transform.up, rb.position.normalized);
        }
    }
}