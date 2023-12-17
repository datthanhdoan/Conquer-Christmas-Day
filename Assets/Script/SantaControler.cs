using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class SantaControler : MonoBehaviour
{
    // Start is called before the first frame update
    public WayPoints wayPoints;
    public SantaItem santaItem;
    public float speed;
    private bool isFacingRight = true;

    void Awake()
    {
        transform.position = wayPoints.Points[0];
    }

    // Update is called once per frame
    void Update()
    {
        isFacingRight = ((transform.position != wayPoints.Points[0]) && santaItem.itemInHand == null) ? false : true;

        if (santaItem.itemInHand != null && (transform.position != wayPoints.Points[1]))
        {
            transform.position = Vector3.MoveTowards(transform.position, wayPoints.Points[1], speed * Time.deltaTime);
        }
        if (santaItem.itemInHand == null && (transform.position != wayPoints.Points[0]))
        {
            transform.position = Vector3.MoveTowards(transform.position, wayPoints.Points[0], speed * Time.deltaTime);
        }
        transform.rotation = Quaternion.Euler(0, isFacingRight ? 0 : 180, 0);

        // if (santaItem.itemInHand != null && (currentWayPoint == 0))
        // {
        //     transform.position = Vector3.MoveTowards(transform.position, wayPoints.Points[1], speed * Time.deltaTime);
        // }
        // if (santaItem.itemInHand == null && (currentWayPoint == 1))
        // {
        //     transform.position = Vector3.MoveTowards(transform.position, wayPoints.Points[0], speed * Time.deltaTime);
        // }
        // {
        //     transform.position = Vector3.MoveTowards(transform.position, wayPoints.Points[0], speed * Time.deltaTime);
        //     transform.rotation = isFacingRight ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
        // }
    }
}
