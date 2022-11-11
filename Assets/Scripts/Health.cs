using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] 
    private int actualHealth;
    int _currentHealth;

    void Start()
    {
        SetHealth();
    }

    public void TakeDamage()
    {
        _currentHealth--;
        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    private void SetHealth()
    {
        _currentHealth = actualHealth;
    }

    public void SetInvincible()
    {
        _currentHealth = 1000;
    }

    void Death()
    {
        if (gameObject.CompareTag("Player"))
        {
            //Spawn Player
        }
        else
        {
            if (gameObject.CompareTag($"Small")) MasterTracker.smallTankDestroyed++;
            else if (gameObject.CompareTag($"Fast")) MasterTracker.fastTankDestroyed++;
            else if (gameObject.CompareTag($"Big")) MasterTracker.bigTankDestroyed++;
            else if (gameObject.CompareTag($"Armored")) MasterTracker.armoredTankDestroyed++;
        }

        Destroy(gameObject);
    }
}