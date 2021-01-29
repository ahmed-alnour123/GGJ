using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour {

    public GameObject obstacle;
    public int n;

    void Start() {
        for (int i = 0; i < n; i++) {
            transform.Rotate(Random.insideUnitSphere * 360); // randomly rotate the parent before instantiating
            var m = Instantiate(obstacle, Vector3.up * 50.5f, Quaternion.identity, transform).GetComponent<MeshRenderer>().material; // Intantiate a new object and get it't material
            m.color = Random.ColorHSV(0, 1, 0, 1, 1, 1); // change it to random color that has value of 1
        }
    }
}