using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] objectSpawn;
    public float timeSpawn;
    private float _timerSpawn;
    void Start()
    {
        _timerSpawn = timeSpawn;
    }
    void Update()
    {
        _timerSpawn -= Time.deltaTime;
        if (_timerSpawn <= 0)
        {
            int random = Random.Range(0, objectSpawn.Length);
            Instantiate(objectSpawn[random], transform.position, Quaternion.identity);
            _timerSpawn = timeSpawn;
        }
    }

}
