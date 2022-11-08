using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterTracker : MonoBehaviour
{

    static MasterTracker _instance;

    [SerializeField] int smallTankPoints = 100, fastTankPoints = 200, bigTankPoints = 300, armoredTankPoints = 400;

    public int SmallTankPointsWorth => smallTankPoints;

    public int FastTankPointsWorth => fastTankPoints;

    public int BigTankPointsWorth => bigTankPoints;

    public int ArmoredTankPointsWorth => armoredTankPoints;

    public static int smallTankDestroyed, fastTankDestroyed, bigTankDestroyed, armoredTankDestroyed;
    public static int stageNumber;
    public static int playerScore = 0;

    void Awake()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
}