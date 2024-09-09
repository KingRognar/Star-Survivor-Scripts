using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ParallaxManager_Scr : MonoBehaviour
{
    [Header ("High level Parallax")]
    [SerializeField] private List<GameObject> HLScenePrefabs = new List<GameObject>();
    [SerializeField] private float timeToSpawn = 4;


    private void Awake()
    {
        _ = SpawnScenesOnTime();
    }

    private async Task SpawnScenesOnTime()
    {
        while (true)
        {
            if (destroyCancellationToken.IsCancellationRequested)
            {
                return;
            }

            if (Time.timeScale != 0)
            {
                int rnd = UnityEngine.Random.Range(0, HLScenePrefabs.Count);
                Instantiate(HLScenePrefabs[rnd], new Vector3(0, 15, 0), Quaternion.identity);
                await Task.Delay((int)timeToSpawn * 1000);
            }
            else
            {
                await Task.Yield();
            }

        }
    }
}
