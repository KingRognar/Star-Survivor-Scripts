using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Enemy_Director_Scr : MonoBehaviour
{
    public GameObject basicEnemyPrefab;
    public GameObject arcEnemyPrefab;

    [SerializeField] private float enemySpawnDelay = 1f;
    private float lastTimeEnemySpawned = -1f;

    [SerializeField] private float waveTime = 120f;
    [SerializeField] private int waveNum = 0;
    private int numberOfArcsInSquad = 4;
    private bool spawnArcsFromLeft = true;

    private int screenWidth, screenHeight;
    private float leftmostPoint, rightmostPoint, upperPoint;


    //TODO: сделать SO уровня, который будет контролировать спавн врагов


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

                waveNum = (int)(Time.time / waveTime);
                if (waveNum % 2 == 0)
                {
                    SpawnBasicEnemy();

                    await Task.Delay((int)(1000 * enemySpawnDelay));
                }
                else
                {
                    await SpawnArcSquad();
                    await Task.Delay((int)(2000 * enemySpawnDelay));
                }

            } else
            {
                await Task.Yield();
            }

        }


    }



    private void SpawnBasicEnemy()
    {
        Instantiate(basicEnemyPrefab, new Vector3(Random.Range(leftmostPoint, rightmostPoint), upperPoint, 0), Quaternion.identity);
    }
    private void SpawnArcEnemy(bool spawnFromLeft)
    {
        if (spawnFromLeft) 
        {
            Enemy_ArcMoving_Scr arcEnemy = Instantiate(arcEnemyPrefab, new Vector3(leftmostPoint, upperPoint, 0), Quaternion.identity).GetComponent<Enemy_ArcMoving_Scr>();
            arcEnemy.moveToRight = true;
        }
        else
        {
            Enemy_ArcMoving_Scr arcEnemy = Instantiate(arcEnemyPrefab, new Vector3(-leftmostPoint, upperPoint, 0), Quaternion.identity).GetComponent<Enemy_ArcMoving_Scr>();
            arcEnemy.moveToRight = false;
        }


    }
    private async Task SpawnArcSquad()
    {
        for (int i = 0; i < numberOfArcsInSquad; i++)
        {
            if (destroyCancellationToken.IsCancellationRequested)
            {
                return;
            }

            SpawnArcEnemy(spawnArcsFromLeft);
            await Task.Delay(400);
        }
        spawnArcsFromLeft = !spawnArcsFromLeft;
    }

}
