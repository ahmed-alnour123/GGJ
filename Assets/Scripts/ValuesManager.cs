using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ValuesManager : MonoBehaviour {
    public static float radius = 50;
    public float localRadius; // just to show it in inspector; because static values don't show

    void Update() {
        radius = localRadius;
        // Debug.LogWarning($"radius is {radius}"); // warning so we can hide it in inspector
    }
}
