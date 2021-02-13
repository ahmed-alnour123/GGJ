using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Movement : MonoBehaviour {

    public float speed, rotSpeed;
    private float h, v;

    void Start() {

    }

    void Update() {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 newPos = transform.position + (transform.forward * v * speed * Time.deltaTime);
        transform.Rotate(0, h * rotSpeed * Time.deltaTime, 0);

        // fix position
        Vector3 constrained = newPos.normalized * 25.5f;
        transform.position = constrained;

        // fix rotation
        transform.rotation = Quaternion.FromToRotation(transform.up, transform.position.normalized) * transform.rotation;
    }
}