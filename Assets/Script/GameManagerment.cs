using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManagerment : MonoBehaviour
{
    public GameObject gameOverPanel;
    public SpawnerManager spawnerManager;
    public GameObject bgMusic;
    public GameObject comingSignal;
    public int score = 0;
    public int highScore = 0;
    public int remainingLives = 5;
    public Sprite[] pauseSprites;
    public Sprite[] soundSprites;
    void Update()
    {
        CheckHighScore();
        GameOver();
        VisualComingSignal();
    }

    void CheckHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
        }
    }

    void VisualComingSignal()
    {
        for (int i = 0; i < spawnerManager.child.Count; i++)
        {
            if (spawnerManager.child[i].transform.position.x > spawnerManager.waitQueue[spawnerManager.waitQueue.Length - 4].x)
            {
                comingSignal.SetActive(true);
            }
            else
            {
                comingSignal.SetActive(false);
            }
        }
    }
    void GameOver()
    {
        if (remainingLives <= 0 && spawnerManager.childHome.Count != 0)
        {
            Debug.Log("Game Over");
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
        // else
        // {
        //     Time.timeScale = 1;
        //     GameOverPanel.SetActive(false);
        // }
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
    public void Restart()

    {
        PoolingObject.instance.pooledObjects.Clear();
        spawnerManager.child.Clear();
        spawnerManager.childHome.Clear();
        spawnerManager.ingameTime = 0;
        var objSpawn = GameObject.Find("ChildPooling");
        for (var i = objSpawn.transform.childCount - 1; i >= 0; i--)
        {
            var child = objSpawn.transform.GetChild(i).gameObject;
            if (child.activeInHierarchy == true)
            {
                child.SetActive(false);
            }
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void togglePause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void ToggleBgMusic(Image image)
    {
        if (bgMusic.GetComponent<AudioSource>().mute)
        {
            bgMusic.GetComponent<AudioSource>().mute = false;
            image.sprite = soundSprites[0];
        }
        else
        {
            bgMusic.GetComponent<AudioSource>().mute = true;
            image.sprite = soundSprites[1];
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void ChangeVolume(Slider slider)
    {
        bgMusic.GetComponent<AudioSource>().volume = slider.value;

    }


}
