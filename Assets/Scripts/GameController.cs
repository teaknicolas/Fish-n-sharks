using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static float distance = 0;
    private static int points = 0;
    private static int bonuspoints = 0;

    private static float difficultyMultiplier = 1;

    private static float difficultyOffset = 100f;

    private static int ennemyCount = 0;

    private static int bonusCount = 0;

    public static float Distance
    {
        get => distance; set => distance = value;
    }

    public static int Points { get => points; set => points = value; }

    public static int Bonuspoints { get => bonuspoints; set => bonuspoints = value; }

    public static float DifficultyMultiplier { get => difficultyMultiplier; set => difficultyMultiplier = value; }
    public static float DifficultyOffset { get => difficultyOffset; set => difficultyOffset = value; }
    public static int EnnemyCount { get => ennemyCount; set => ennemyCount = value; }
    public static int BonusCount { get => bonusCount; set => bonusCount = value; }
   

    public static GameController instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        difficultyMultiplier = 1 + (Time.realtimeSinceStartup/ difficultyOffset);
        //Debug.Log("Difficulty Mult : " + difficultyMultiplier);

        //Debug.Log("Ennemy counts : " + GameController.EnnemyCount);
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER!");
    }
}
