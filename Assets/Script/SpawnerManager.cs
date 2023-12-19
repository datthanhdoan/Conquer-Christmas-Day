using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public WayPoints santaStoreWaypoint;
    public WayPoints homeWaypoint;

    public Vector3[] waitQueue;
    public Vector3[] homeQueue;
    public List<GameObject> child = new List<GameObject>();
    private List<GameObject> childHome = new List<GameObject>();
    public GameObject childPrefab;
    public float ingameTime;
    private float timeSpawn;
    private float _timerSpawn;
    void Start()
    {
        waitQueue = santaStoreWaypoint.points;
        homeQueue = homeWaypoint.points;
        _timerSpawn = timeSpawn;
    }

    void Update()
    {
        ingameTime += Time.deltaTime;
        SetTimeSpawn();
        MoveChildToHome();
        MoveChildToNextPoints(child, waitQueue);
        if (_timerSpawn <= 0 && child.Count < waitQueue.Length - 1)
        {
            Spawn();
            _timerSpawn = timeSpawn;
        }
        else if (child.Count < waitQueue.Length)
        {
            _timerSpawn -= Time.deltaTime;
        }

    }
    void MoveChildToHome()
    {
        float speed = 0.5f; // Define a speed for the movement

        if (child.Count > 0)
        {
            if (child[0].GetComponent<ChildControler>().isTaken || child[0].GetComponent<ChildControler>().expectedTime <= 0)
            {
                childHome.Add(child[0]);
                child.RemoveAt(0);
            }
        }

        for (int i = 0; i < childHome.Count; i++)
        {

            // int currentWayPoint = childHome[i].GetComponent<ChildControler>().currentWayPoint;
            // if (childHome[i].transform.position != homeQueue[currentWayPoint])
            // {
            //     childHome[i].transform.position = Vector3.MoveTowards(childHome[i].transform.position, homeQueue[currentWayPoint], speed * Time.deltaTime);
            // }
            // else
            // {
            //     childHome[i].GetComponent<ChildControler>().currentWayPoint++;
            // }
            for (int j = 0; j < homeQueue.Length - 1; j++)
            {
                // ChildControler currentChild = childHome[i].GetComponent<ChildControler>();
                if (childHome[i].transform.position == homeQueue[0]) childHome[i].GetComponent<ChildControler>().Flip();
                if (childHome[i].transform.position != homeQueue[childHome[i].GetComponent<ChildControler>().currentWayPoint])
                {
                    childHome[i].transform.position = Vector3.MoveTowards(childHome[i].transform.position, homeQueue[childHome[i].GetComponent<ChildControler>().currentWayPoint], speed * Time.deltaTime);
                }
                else if (childHome[i].GetComponent<ChildControler>().currentWayPoint < homeQueue.Length - 1)
                {
                    childHome[i].GetComponent<ChildControler>().currentWayPoint++;
                }
                // if (child[i].transform.position == homeQueue[homeQueue.Length - 1])
                // {
                //     child[i].GetComponent<ChildControler>().isHome = true;
                // }
            }
        }
    }
    void MoveChildToNextPoints(List<GameObject> child, Vector3[] queue)
    {
        float speed = 2f; // Define a speed for the movement
        // for (int i = 0; i < child.Count; i++)
        // {
        //     if (child[i].transform.position != waitQueue[i + 1])
        //     {
        //         child[i].transform.position = Vector3.MoveTowards(child[i].transform.position, queue[i + 1], speed * Time.deltaTime);
        //     }
        // }
        int index = queue.Length - 1;
        for (int i = 0; i < child.Count; i++)
        {
            if (child[i].transform.position != queue[index])
            {
                child[i].transform.position = Vector3.MoveTowards(child[i].transform.position, queue[index], speed * Time.deltaTime);
            }
            index--;
        }
    }
    void SetTimeSpawn()
    {
        int timeToStore = 6;
        if (ingameTime > 0) timeSpawn = 6 + timeToStore; Debug.Log("timeSpawn = " + timeSpawn);
        if (ingameTime > 30) timeSpawn = 5 + timeToStore; Debug.Log("timeSpawn = " + timeSpawn);
        if (ingameTime > 60) timeSpawn = 3 + timeToStore; Debug.Log("timeSpawn = " + timeSpawn);
        if (ingameTime > 90) timeSpawn = 2 + timeToStore; Debug.Log("timeSpawn = " + timeSpawn);
        if (ingameTime > 120) timeSpawn = 0 + timeToStore; Debug.Log("timeSpawn = " + timeSpawn);
        if (ingameTime > 180) timeSpawn = -2 + timeToStore; Debug.Log("timeSpawn = " + timeSpawn);
    }
    void SetExpectedTime(GameObject child)
    {
        // child.GetComponent<ChildControler>().expectedTime = ingameTime;
        int timeToStore = 6;
        if (ingameTime > 0) child.GetComponent<ChildControler>().expectedTime = 10 + timeToStore;
        if (ingameTime > 60) child.GetComponent<ChildControler>().expectedTime = 8 + timeToStore;
        if (ingameTime > 120) child.GetComponent<ChildControler>().expectedTime = 8 + timeToStore;
        if (ingameTime > 180) child.GetComponent<ChildControler>().expectedTime = 7 + timeToStore;

    }
    void Spawn()
    {
        child.Add(Instantiate(childPrefab, waitQueue[0], Quaternion.identity));
        SetExpectedTime(child[child.Count - 1]);
    }

}
