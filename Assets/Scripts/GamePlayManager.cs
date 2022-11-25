using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField]
    Image topCurtain, bottomCurtain, blackCurtain;
    [SerializeField]
    Text stageNumberText, gameOverText;
    GameObject[] spawnPoints, spawnPlayerPoints;
    bool stageStart = false;
    bool tankReserveEmpty = false;
    private static readonly int Spawning = Animator.StringToHash("Spawning");

    void Start()
    {
        stageStart = true;
        stageNumberText.text = "STAGE " + MasterTracker.stageNumber;
        spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");
        spawnPlayerPoints = GameObject.FindGameObjectsWithTag("PlayerSpawnPoint");
        StartCoroutine(StartStage());
    }

    public void SpawnPlayer()
    {
        if (MasterTracker.playerLives > 0)
        {
            if (!stageStart)
            {
                MasterTracker.playerLives--;
            }
            stageStart = false;
            Animator anime = spawnPlayerPoints[0].GetComponent<Animator>();
            anime.SetTrigger(Spawning);
        }
        else
        {
            StartCoroutine(GameOver());
        }
        
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void SpawnEnemy()
    {
        if (LevelManager.smallTanks + LevelManager.fastTanks + LevelManager.bigTanks + LevelManager.armoredTanks > 0)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            Animator anime = spawnPoints[spawnPointIndex].GetComponent<Animator>();
            anime.SetTrigger(Spawning);
        }
        else
        {
            CancelInvoke();
            tankReserveEmpty = true;

        }
    }
    IEnumerator StartStage()
    {
        StartCoroutine(RevealStageNumber());
        yield return new WaitForSeconds(3); // таймер паузы анимации
        StartCoroutine(RevealTopStage());
        StartCoroutine(RevealBottomStage());
        yield return null;
        SpawnEnemy();
    }
    IEnumerator RevealStageNumber()
    {
        while (blackCurtain.rectTransform.localScale.y > 0)
        {
            blackCurtain.rectTransform.localScale = new Vector3(1, Mathf.Clamp(blackCurtain.rectTransform.localScale.y - Time.deltaTime, 0, 1), 1);
            yield return null;
        }
    }
    IEnumerator RevealTopStage()
    {
        stageNumberText.enabled = false;
        while (topCurtain.rectTransform.position.y < 1250)
        {
            topCurtain.rectTransform.Translate(new Vector3(0, 500 * Time.deltaTime, 0));
            yield return null;
        }
    }
    IEnumerator RevealBottomStage()
    {
        while (bottomCurtain.rectTransform.position.y > -400)
        {
            bottomCurtain.rectTransform.Translate(new Vector3(0, -500 * Time.deltaTime, 0));
            yield return null;
        }
    }
    public IEnumerator GameOver()
    {
        while (gameOverText.rectTransform.localPosition.y < 0)
        {
            gameOverText.rectTransform.localPosition = new Vector3(
                gameOverText.rectTransform.localPosition.x,
                gameOverText.rectTransform.localPosition.y + 120f * Time.deltaTime,
                gameOverText.rectTransform.localPosition.z);
            yield return null;
        }
        MasterTracker.stageCleared = false;
        LevelCompleted();
    }
    private void Update()
    {
        if (tankReserveEmpty && GameObject.FindWithTag("Small") == null && GameObject.FindWithTag("Fast") == null && GameObject.FindWithTag("Big") == null && GameObject.FindWithTag("Armored") == null)
        {
    	MasterTracker.stageCleared = true;
        LevelCompleted();
        }
    }

    private void LevelCompleted()
    {
        tankReserveEmpty = false;
        SceneManager.LoadScene("Score");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyProjectile") || collision.gameObject.CompareTag("PlayerProjectile"))
        {
        GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(true);
        GamePlayManager GPM = GameObject.Find("Canvas").GetComponent<GamePlayManager>();
        StartCoroutine(GPM.GameOver());
        }
    }
}
