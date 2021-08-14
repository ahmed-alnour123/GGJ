using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour {

    public static int numberOfEnemies = 3;
    DifficultyManager instance;

    void Start() {
        if (instance == null) {
            instance = this;
        }
        if (instance != this) return;
        DontDestroyOnLoad(this);
        print(numberOfEnemies);
    }
}