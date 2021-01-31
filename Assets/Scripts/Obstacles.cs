using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour {

    public int numberOfRocks, numberOfBranches;
    public Transform rocks;
    public GameObject branch;

    void Start() {
        for (int i = 0; i < numberOfBranches; i++) {
            transform.Rotate(Random.rotation.eulerAngles); // randomly rotate the parent before instantiating
            Instantiate(branch, Vector3.zero, Quaternion.identity, transform); // Intantiate a new object and get it't material
        }

        for (int i = 0; i < numberOfRocks; i++) {
            int r = Random.Range(0, rocks.childCount - 1);
            print(rocks.childCount);
            transform.Rotate(Random.rotation.eulerAngles); // randomly rotate the parent before instantiating
            Instantiate(rocks.GetChild(r).gameObject, Vector3.zero, Quaternion.identity, transform); // Intantiate a new object and get it't material
        }
    }
}