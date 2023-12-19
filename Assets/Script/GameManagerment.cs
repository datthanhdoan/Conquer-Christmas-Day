using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerment : MonoBehaviour
{
    public GameObject GameOverPanel;
    public int score = 0;
    public int highScore = 0;
    public int remainingLives = 5;
    public Sprite[] pauseSprites;
    void Update()
    {
        CheckHighScore();
        CheckRemainingLives();
    }

    void CheckHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
        }
    }
    void CheckRemainingLives()
    {
        if (remainingLives <= 0)
        {
            Debug.Log("Game Over");
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);
        }
    }

    public void Pauser(Image Image)
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            Image.sprite = pauseSprites[1];
        }
        else
        {
            Time.timeScale = 1;
            Image.sprite = pauseSprites[0];
        }
    }


}
