using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour {

    public static int numberOfEnemies = 3;
    DifficultyManager instance;

    public static int playCounter = 1;

    // other managers
    [SerializeField]
    BarbarianManager barbarianManager;

    [SerializeField]
    SackManager sackManager;

    [SerializeField]
    GameManager gameManager;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        if (instance != this) return;
        DontDestroyOnLoad(this);
        print($"enemies: {numberOfEnemies}\nplay counter: {playCounter}");

        switch (playCounter) {
            case 1:
                SetVariables(0, 0, 0, 0);
                break;
            case 2:
                SetVariables(0, 0, 0, 0);
                break;
            case 3:
                SetVariables(0, 0, 0, 0);
                break;
            case 4:
                SetVariables(0, 0, 0, 0);
                break;
            case 5:
                SetVariables(0, 0, 0, 0);
                break;
            case 6:
                SetVariables(0, 0, 0, 0);
                break;
            case 7:
                SetVariables(0, 0, 0, 0);
                break;
            case 8:
                SetVariables(0, 0, 0, 0);
                break;
            case 9:
                SetVariables(0, 0, 0, 0);
                break;
            case 10:
                SetVariables(0, 0, 0, 0);
                break;
        }
    }

    void SetVariables(int numOfBarbarians, int maxSack, int sacksAtTime, int neededSacks) {
        barbarianManager.numberOfBarbarians = numOfBarbarians;
        sackManager.maxSacks = maxSack;
        sackManager.sacksAtTime = sacksAtTime;
        gameManager.neededSacks = neededSacks;

        barbarianManager.numberOfBarbarians = playCounter;
        sackManager.maxSacks = 10;
        sackManager.sacksAtTime = 5;
        gameManager.neededSacks = 3;
    }
}
