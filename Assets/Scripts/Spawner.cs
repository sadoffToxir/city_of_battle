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
    void Start()
    {
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
    tank = Instantiate(tanks[Random.Range(0,tanks.Length)], transform.position, transform.rotation);
    }
    public void SpawnNewTank()
    {
    if (tank != null) tank.SetActive(true);
    }
}
