﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Movement : MonoBehaviour {

    public float speed, rotSpeed;
    private float h, v;
    Rigidbody rb;
    Material material;

    void Start() {
        material = GetComponent<Renderer>().material;
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        Vector3 originalVel = rb.velocity;

        Vector3 newPos = rb.position + (transform.forward * v * speed * Time.deltaTime);
        // transform.Rotate(0, h * rotSpeed * Time.deltaTime, 0);
        Quaternion newRot = rb.rotation * Quaternion.Euler(0, h * rotSpeed * Time.deltaTime, 0);

        // fix position
        Vector3 constrained = newPos.normalized * 25.5f;
        rb.position = constrained;

        // fix rotation
        rb.rotation = Quaternion.FromToRotation(transform.up, rb.position.normalized) * newRot;

        // fix speed -- I just copied it, I don't understand it :)
        Vector3 perpAxis = Vector3.Cross(rb.position, originalVel);

        // create a vector which is perpendicular to both our 'axis' vector and the radius vector.
        // this vector is going in the direction we want, but is likely the wrong size.
        Vector3 tangent = Vector3.Cross(perpAxis, rb.position);

        // re-scale the tangent vector so it's the same magnitude as the original.
        rb.velocity = tangent.normalized * originalVel.magnitude;
    }

    private void OnCollisionEnter(Collision other) {
        material.color = Random.ColorHSV();
    }
}