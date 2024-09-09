using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Enemy_Director_Scr : MonoBehaviour
{
    public static Enemy_Director_Scr instance;


    [SerializeField] private GameObject basicEnemyPrefab;
    [SerializeField] private GameObject arcEnemyPrefab;
    [SerializeField] private GameObject railgunEnemyPrefab;
    public static List<Transform> railgunEnemiesList = new List<Transform>();

    [SerializeField] private float enemySpawnDelay = 1f;
    //private float lastTimeEnemySpawned = -1f;

    [SerializeField] private float waveTime = 120f;
    [SerializeField] private int waveNum = 0;
    private int numberOfArcsInSquad = 4;
    private bool spawnArcsFromLeft = true;

    private int screenWidth, screenHeight;
    private float leftmostPoint, rightmostPoint, upperPoint;


    //TODO: сделать SO уровня, который будет контролировать спавн врагов


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        screenHeight = Camera.main.pixelHeight + 100;
        screenWidth = Camera.main.pixelWidth - 100;

        float cameraZPos = Camera.main.transform.position.z;
        leftmostPoint = Camera.main.ScreenToWorldPoint(new Vector3(100, screenHeight, -cameraZPos)).x;
        rightmostPoint = Camera.main.ScreenToWorldPoint(new Vector3(screenWidth, screenWidth, -cameraZPos)).x;
        upperPoint = Camera.main.ScreenToWorldPoint(new Vector3(100, screenHeight, -cameraZPos)).y;

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
                    if (Random.Range(0,100) < 40)
                    {
                        railgunEnemiesList.Add(SpawnRandomOnLane(railgunEnemyPrefab).transform);
                    }

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
        SpawnRandomOnLane(basicEnemyPrefab);
    }
    private async Task SpawnArcSquad()
    {
        for (int i = 0; i < numberOfArcsInSquad; i++)
        {
            if (destroyCancellationToken.IsCancellationRequested)
            {
                return;
            }

            Enemy_ArcMoving_Scr arcEnemy = SpawnOnUpperCorner(arcEnemyPrefab, spawnArcsFromLeft).GetComponent<Enemy_ArcMoving_Scr>();
            arcEnemy.moveToRight = !spawnArcsFromLeft;

            await Task.Delay(400);
        }
        spawnArcsFromLeft = !spawnArcsFromLeft;
    }

    private GameObject SpawnRandomOnLane(GameObject enemyPrefab)
    {
        return Instantiate(enemyPrefab, new Vector3(Random.Range(leftmostPoint, rightmostPoint), upperPoint, 0), Quaternion.identity);
    }
    private GameObject SpawnOnUpperCorner(GameObject enemyPrefab, bool spawnFromRightCorner)
    {
        if (spawnFromRightCorner)
            return Instantiate(enemyPrefab, new Vector3(-leftmostPoint, upperPoint, 0), Quaternion.identity);
        else
            return Instantiate(enemyPrefab, new Vector3(leftmostPoint, upperPoint, 0), Quaternion.identity);
    }

}
