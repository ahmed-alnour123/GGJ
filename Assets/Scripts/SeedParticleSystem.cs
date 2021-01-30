using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedParticleSystem : MonoBehaviour {

    public ParticleSystem ps;
    List<ParticleSystem.Particle> ls = new List<ParticleSystem.Particle>();
    public int counter = 0; // change it to GameManager value;

    private void Start() {;
        ps = GetComponent<ParticleSystem>();
        ps.trigger.SetCollider(0, FindObjectOfType<Player>().GetComponentInChildren<BoxCollider>()); // find player's collider
    }

    private void OnParticleTrigger() {
        int n = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, ls);
        for (int i = 0; i < n; i++) {
            var p = ls[i];
            p.remainingLifetime = 0;
            ls[i] = p;
        }
        counter += n;
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, ls);
    }
}