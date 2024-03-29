﻿using UnityEngine;

public class Eagle : MonoBehaviour
{

    public bool eagleDeath = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyProjectile") || collision.gameObject.CompareTag("PlayerProjectile"))
        {
            eagleDeath = true;
            GetComponent<SpriteRenderer>().enabled = false;
            GamePlayManager GPM = GameObject.Find("Canvas").GetComponent<GamePlayManager>();
            StartCoroutine(GPM.GameOver());
        }

    }

}