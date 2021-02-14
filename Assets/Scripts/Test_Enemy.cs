using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Enemy : MonoBehaviour {

    public Transform player;
    public float speed;
    public Rigidbody rb;

    void Start() {

    }

    void Update() {
        Vector3 newPos = Vector3.MoveTowards(rb.position, player.position, speed * Time.deltaTime);
        Vector3 originalVel = rb.velocity;

        // fix position
        Vector3 constrained = newPos.normalized * 25.5f;
        rb.position = constrained;

        // fix rotation
        rb.rotation = Quaternion.FromToRotation(transform.up, rb.position.normalized) * rb.rotation;

        // fix speed -- I just copied it, I don't understand it :)
        Vector3 perpAxis = Vector3.Cross(rb.position, originalVel);

        // create a vector which is perpendicular to both our 'axis' vector and the radius vector.
        // this vector is going in the direction we want, but is likely the wrong size.
        Vector3 tangent = Vector3.Cross(perpAxis, rb.position);

        // re-scale the tangent vector so it's the same magnitude as the original.
        rb.velocity = tangent.normalized * originalVel.magnitude;
    }
}