using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChildControler : MonoBehaviour
{
    public GameObject[] particleEffect;
    // public Transform sprite;
    public Item[] itemsResult;
    public int currentWayPoint = 0;
    public int currentHomePoint = 0;
    GameManagerment gameManagerment;
    private Item wantedItem;
    public float expectedTime;
    public Item receivedItem;
    public GameObject thinkingImage;
    public Image[] ReactionImages;
    public SpawnerManager spawnerManager;
    public bool isTaken;
    public bool isHome;

    private void Start()
    {
        gameManagerment = GameObject.Find("GameManager").GetComponent<GameManagerment>();
        //start from the first waypoint

        // random the wanted item
        wantedItem = itemsResult[Random.Range(0, itemsResult.Length)];
        // set the wanted item image to the thinking image ( or popup image , whenever you want to call)
        thinkingImage.GetComponent<Image>().sprite = wantedItem.gameObject.GetComponent<Image>().sprite;
    }
    public void createValue()
    {
        currentWayPoint = 0;
        currentHomePoint = 0;
        expectedTime = Random.Range(5, 10);
        isTaken = false;
        isHome = false;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        wantedItem = itemsResult[Random.Range(0, itemsResult.Length)];
        receivedItem = null;
        thinkingImage.GetComponent<Image>().sprite = wantedItem.gameObject.GetComponent<Image>().sprite;
        foreach (GameObject effect in particleEffect)
        {
            effect.SetActive(false);
        }
    }
    private void Update()
    {
        if (isHome) return;
        if (!isTaken) expectedTime -= Time.deltaTime;
        if (expectedTime <= 0)
        {
            thinkingImage.GetComponent<Image>().sprite = ReactionImages[1].sprite;
            isHome = true;
            gameManagerment.remainingLives--;

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Santa"))
        {
            Debug.Log("Oh.. I Santa is here!");
            // get the itemInHand from Santa
            receivedItem = collision.gameObject.GetComponent<SantaItem>().itemInHand;
            collision.gameObject.GetComponent<SantaItem>().itemInHand = null;
            if (collision.gameObject.GetComponent<SantaItem>().itemInHand == null)
            {
                Debug.Log("Santa is empty");
            }
            isTaken = true;
            if (wantedItem == receivedItem)
            {
                Debug.Log("Correct item");
                gameManagerment.score++;
                thinkingImage.GetComponent<Image>().sprite = ReactionImages[0].sprite;
                particleEffect[0].GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(0, wantedItem.gameObject.GetComponent<Image>().sprite);
                foreach (GameObject effect in particleEffect)
                {
                    effect.SetActive(true);
                }

            }
            else
            {
                Debug.Log("Wrong item");
                thinkingImage.GetComponent<Image>().sprite = ReactionImages[1].sprite;
                if (gameManagerment.score > 0)
                {
                    gameManagerment.score--;
                }
            }
        }

    }


    public void Flip()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
    }

}

