using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManagerment : MonoBehaviour
{
    public static GameManagerment instance;
    public GameObject gameOverPanel;
    public SpawnerManager spawnerManager;
    public CraftingManager craftingManager;
    public GameObject objectToHideCrafting;
    public GameObject PauserImage;
    public GameObject bgMusic;
    public GameObject comingSignal;
    public GameObject resultItem;
    public int score = 0;
    public int highScore = 0;
    public int remainingLives = 5;
    public Sprite[] pauseSprites;
    public Sprite[] soundSprites;
    private bool isPause = false;
    [Header("Effect")]
    public GameObject santaSlotGameObject;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) return;
        CheckHighScore();
        GameOver();
        VisualComingSignal();
        Effect();
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
    public void Pauser()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            craftingManager.isPause = true;
            isPause = true;
        }
        else
        {
            Time.timeScale = 1;
            craftingManager.isPause = false;
            isPause = false;
        }
    }

    public void ToggleGameObject(GameObject gameObject)
    {
        if (gameObject.activeSelf == true)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
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

    public void togglePause(bool isShowSetting)
    {
        if (Time.timeScale == 0 && isPause && !isShowSetting)
        {
            Time.timeScale = 1;
            isPause = false;
        }
        else
        {
            Time.timeScale = 0;
            isPause = true;
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

    public void Effect()
    {
        SantaSlotEffect();
        ChanePauseSprite();
    }
    public void ChanePauseSprite()
    {
        if (Time.timeScale == 0)
        {
            PauserImage.GetComponent<Image>().sprite = pauseSprites[1];
        }
        else
        {
            PauserImage.GetComponent<Image>().sprite = pauseSprites[0];
        }
    }
    public void SantaSlotEffect()
    {
        if (craftingManager.resultSlot.item != null)
        {
            santaSlotGameObject.GetComponent<Animator>().SetBool("haveResultItem", true);
        }
        else
        {
            santaSlotGameObject.GetComponent<Animator>().SetBool("haveResultItem", false);
        }
    }


    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
