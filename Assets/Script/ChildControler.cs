using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChildControler : MonoBehaviour
{
    public Item[] itemsResult;
    public int currentWayPoint = 0;
    private Item wantedItem;
    public float expectedTime;
    public Item receivedItem;
    public GameObject thinkingImage;
    public Image[] ReactionImages;
    public SpawnerManager spawnerManager;
    public bool isTaken { get; private set; } = false;
    bool isFacingRight = false;
    bool isWaited = false;

    private void Start()
    {
        //start from the first waypoint

        // random the wanted item
        wantedItem = itemsResult[Random.Range(0, itemsResult.Length)];
        // set the wanted item image to the thinking image ( or popup image , whenever you want to call)
        thinkingImage.GetComponent<Image>().sprite = wantedItem.gameObject.GetComponent<Image>().sprite;
    }
    private void Update()
    {
        if (!isTaken) expectedTime -= Time.deltaTime;
        if (expectedTime <= 0) thinkingImage.GetComponent<Image>().sprite = ReactionImages[1].sprite;

        // Move to the waypoint index = 1 when the item is not taken
        // if (!isTaken)
        // {
        //     // transform.position = Vector3.MoveTowards(transform.position, wayPoints.points[currentWayPoint + 1], 2 * Time.deltaTime);
        // }
        // // move to the end of the waypoint when the item is taken
        // if (isTaken && currentWayPoint < wayPoints.points.Length - 1)
        // {
        //     // flip in waypoint index = 2
        //     if (currentWayPoint == 2 && !isFacingRight)
        //     {
        //         Flip();
        //     }
        //     // if not wait, wait and move to the next waypoint and go to the end of the waypoint
        //     if (isWaited)
        //     {
        //         isWaited = false;
        //         if (transform.position == wayPoints.points[currentWayPoint + 1])
        //         {
        //             currentWayPoint++;
        //         }
        //         transform.position = Vector3.MoveTowards(transform.position, wayPoints.points[currentWayPoint + 1], 2 * Time.deltaTime);

        //     }
        //     else
        //     {
        //         StartCoroutine(WaitAndMove());
        //     }
        // }

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
                thinkingImage.GetComponent<Image>().sprite = ReactionImages[0].sprite;
            }
            else
            {
                Debug.Log("Wrong item");
                thinkingImage.GetComponent<Image>().sprite = ReactionImages[1].sprite;
            }
        }

    }


    private IEnumerator WaitAndMove()
    {
        yield return new WaitForSeconds(1.5f);
        isWaited = true;
    }
    public void Flip()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
    }
}

