using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInCanvas : MonoBehaviour
{
    public bool isScore;
    public bool isHighScore;
    public bool isRemainingLives;
    private GameManagerment gameManagerment;
    void Start()
    {
        gameManagerment = GameObject.Find("GameManager").GetComponent<GameManagerment>();
        if (isScore)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = gameManagerment.score.ToString();
        }
        if (isHighScore)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = gameManagerment.highScore.ToString();
        }
        if (isRemainingLives)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = gameManagerment.remainingLives.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isScore)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "Score : " + gameManagerment.score.ToString();
        }
        if (isHighScore)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "HighScore : " + gameManagerment.highScore.ToString();
        }
        if (isRemainingLives)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = gameManagerment.remainingLives.ToString();
        }
    }
}
