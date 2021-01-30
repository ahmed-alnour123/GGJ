using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Range(1, 100)]
    public float verticalSpeed;
    [Range(1, 100)]
    public float horizontalSpeed;
    public Transform barbarian;

    void Update() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // the we use x-axis to go forward and backward, and y-axis to rotate, we don't need z-axis
        transform.Rotate(0, h * horizontalSpeed * Time.deltaTime, 0);
        barbarian.Rotate(0, h * horizontalSpeed * Time.deltaTime, 0);
        transform.Rotate(v * verticalSpeed * Time.deltaTime,0, 0);
    }
}
    
