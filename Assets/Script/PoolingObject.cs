using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingObject : MonoBehaviour
{
    public static PoolingObject instance;
    public GameObject objectToPool;
    public int amountToPool = 15;
    public List<GameObject> pooledObjects;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(objectToPool, transform);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

}
