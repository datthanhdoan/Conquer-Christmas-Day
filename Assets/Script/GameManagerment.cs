using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerment : MonoBehaviour
{
    public int score = 0;
    public int highScore = 0;
    public int remainingLives = 5;
    void Update()
    {
        if (score > highScore)
        {
            highScore = score;
        }
        if (remainingLives <= 0)
        {
            Debug.Log("Game Over");
            Time.timeScale = 0;
        }
    }

}
