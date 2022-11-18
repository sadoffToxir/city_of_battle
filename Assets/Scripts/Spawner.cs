using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    GameObject[] tanks;
    GameObject tank;
    [SerializeField]
    bool isPlayer;
    [SerializeField]
    GameObject smallTank, fastTank, bigTank, armoredTank;
    GameObject[] spawnPoints;
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");
        if (isPlayer)
        {
            tanks = new GameObject[1] { smallTank };
        }
        else
        {
            tanks = new GameObject[4] { smallTank, fastTank, bigTank, armoredTank };
        }
    }

    public void StartSpawning()
    {

        var spawnPointIndex = Random.Range(0, spawnPoints.Length);
        tank = Instantiate(tanks[Random.Range(0, tanks.Length)], transform.position, transform.rotation);
        tank.SetActive(false);
    }
    public void SpawnNewTank()
    {
        if (tank != null)
            tank.SetActive(true);
    }
}
