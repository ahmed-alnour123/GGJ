using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Range(1, 100)]
    public float speed;

    void Update() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        /// the we use x-axis to go forward and backward, and y-axis to rotate, we don't need z-axis
        transform.Rotate(v * speed * Time.deltaTime, h * speed * Time.deltaTime, 0);
    }
}