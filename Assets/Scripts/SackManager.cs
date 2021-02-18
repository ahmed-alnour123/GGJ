using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SackManager : MonoBehaviour {

    public GameObject sack;
    public int maxSacks;
    public int sacksAtTime;
    public float lifeTime; // life time for a single sack
    public bool outOfSacks; // for GameManager

    private int createdSacks;
    [HideInInspector] public int sacksNow;
    Sack[] currentSacks;

    [Header("For Children")]
    public float speed;
    public float awakeRadius;
    public float height; // the height above the planet radius

    public void Start() {
        currentSacks = new Sack[sacksAtTime];
        sacksNow = 0;
        outOfSacks = false;
        for (int i = 0; i < currentSacks.Length; i++) {
            currentSacks[i] = CreateNew();
        }
    }

    public void Update() {
        for (int i = 0; i < currentSacks.Length; i++) {
            if (currentSacks[i] != null) {
                if (currentSacks[i].isAwake) {
                    currentSacks[i].Countdown(lifeTime);
                }
                if (currentSacks[i].isDisappeard) {
                    Destroy(currentSacks[i].gameObject);
                    sacksNow--;
                    if (createdSacks < maxSacks) {
                        currentSacks[i] = CreateNew();
                    }
                }
            }
        }

        if (createdSacks == maxSacks) {
            outOfSacks = true;
        }
    }

    private Sack CreateNew() {
        createdSacks++;
        sacksNow++;
        Rigidbody rb = Instantiate(sack,
            Random.onUnitSphere * (ValuesManager.radius + height),
            Quaternion.identity, transform).GetComponent<Rigidbody>();
        rb.rotation = Quaternion.FromToRotation(rb.transform.up, rb.position.normalized) * rb.rotation;
        return rb.GetComponent<Sack>();
    }
}