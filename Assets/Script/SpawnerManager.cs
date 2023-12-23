using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
// using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public WayPoints santaStoreWaypoint;
    public WayPoints homeWaypoint;

    public Vector3[] waitQueue;
    public Vector3[] homeQueue;
    public List<GameObject> child = new List<GameObject>();
    public List<GameObject> childHome = new List<GameObject>();
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
        if (ingameTime > 5)
        {
            SetTimeSpawn();
            MoveChildToNextPoints();
            ChildToChildHome();
            MoveChildToHome();
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

    }
    void MoveChildToHome()
    {
        float speed = 0.5f;
        for (int i = 0; i < childHome.Count; i++)
        {
            for (int j = 0; j < homeQueue.Length - 1; j++)
            {
                if (childHome[i].transform.position == homeQueue[0]) childHome[i].GetComponent<ChildControler>().Flip();
                if (childHome[i].transform.position != homeQueue[childHome[i].GetComponent<ChildControler>().currentHomePoint])
                {
                    childHome[i].transform.position = Vector3.MoveTowards(childHome[i].transform.position, homeQueue[childHome[i].GetComponent<ChildControler>().currentHomePoint], speed * Time.deltaTime);
                }
                else if (childHome[i].GetComponent<ChildControler>().currentHomePoint < homeQueue.Length - 1)
                {
                    childHome[i].GetComponent<ChildControler>().currentHomePoint++;
                }
                else
                {
                    childHome[i].SetActive(false);
                    childHome.RemoveAt(i);
                }
            }
        }
    }
    void ChildToChildHome()
    {
        if (child.Count > 0)
        {
            if (child[0].GetComponent<ChildControler>().isTaken || child[0].GetComponent<ChildControler>().expectedTime <= 0)
            {
                childHome.Add(child[0]);
                child.RemoveAt(0);
            }
        }
    }
    void MoveChildToNextPoints()
    {
        float speed = 0.5f;
        int index = waitQueue.Length - 1;
        for (int i = 0; i < child.Count; i++)
        {
            if (child[i].transform.position != waitQueue[index])
            {
                for (int j = 0; j < index; j++)
                {
                    var current = child[i].GetComponent<ChildControler>().currentWayPoint;
                    if (child[i].transform.position == waitQueue[j])
                    {
                        child[i].GetComponent<ChildControler>().currentWayPoint++;
                    }
                    child[i].transform.position = Vector3.MoveTowards(child[i].transform.position, waitQueue[current], speed * Time.deltaTime);
                }
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
        if (ingameTime > 240) timeSpawn = -3 + timeToStore; Debug.Log("timeSpawn = " + timeSpawn);
    }
    void SetExpectedTime(GameObject child)
    {
        // child.GetComponent<ChildControler>().expectedTime = ingameTime;
        int timeToStore = 6;
        if (ingameTime > 0) child.GetComponent<ChildControler>().expectedTime = 10 + timeToStore;
        if (ingameTime > 60) child.GetComponent<ChildControler>().expectedTime = 8 + timeToStore;
        if (ingameTime > 120) child.GetComponent<ChildControler>().expectedTime = 8 + timeToStore;
        if (ingameTime > 180) child.GetComponent<ChildControler>().expectedTime = 7 + timeToStore;
        if (ingameTime > 240) child.GetComponent<ChildControler>().expectedTime = 5 + timeToStore;

    }
    void Spawn()
    {
        // child.Add(Instantiate(childPrefab, waitQueue[0], Quaternion.identity));
        GameObject newChild = PoolingObject.instance.GetPooledObject();
        if (newChild != null)
        {
            newChild.transform.position = waitQueue[0];
            newChild.GetComponent<ChildControler>().createValue();
            newChild.SetActive(true);
            child.Add(newChild);
        }
        SetExpectedTime(child[child.Count - 1]);
    }

}
