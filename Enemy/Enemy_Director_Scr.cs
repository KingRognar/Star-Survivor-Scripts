using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Enemy_Director_Scr : MonoBehaviour
{
    public GameObject enemyPrefab;

    [SerializeField]private float enemySpawnDelay = 1f;
    private float lastTimeEnemySpawned = -1f;

    private int screenWidth, screenHeight;
    private float leftmostPoint, rightmostPoint, upperPoint;


    private void Awake()
    {
        screenHeight = Camera.main.pixelHeight + 100;
        screenWidth = Camera.main.pixelWidth - 100;

        leftmostPoint = Camera.main.ScreenToWorldPoint(new Vector3(100, screenHeight, 0)).x;
        rightmostPoint = Camera.main.ScreenToWorldPoint(new Vector3(screenWidth, screenWidth, 0)).x;
        upperPoint = Camera.main.ScreenToWorldPoint(new Vector3(100, screenHeight, 0)).y;

        _ = SpawnEnemiesOnTime();
    }


    private async Task SpawnEnemiesOnTime ()
    {
        float timeRateIncrDelay = 10f;
        float lastTimeRateIncr = timeRateIncrDelay;

        while (true)
        {
            if (destroyCancellationToken.IsCancellationRequested)
            {
                return;
            }

            if (Time.timeScale != 0)
            {
                if (lastTimeRateIncr <= Time.time)
                {
                    enemySpawnDelay *= 0.99f;
                    lastTimeRateIncr = Time.time + timeRateIncrDelay;
                }


                SpawnEnemy();

                await Task.Delay((int)(1000 * enemySpawnDelay));
            } else
            {
                await Task.Yield();
            }

        }


    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, new Vector3(Random.Range(leftmostPoint, rightmostPoint), upperPoint, 0), Quaternion.identity);
    }

}
