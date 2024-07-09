using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_CircleBots_Scr : MonoBehaviour
{
    private float rotationSpeed = 100f;
    private float lastBulletSpawnTime = 0f;
    private int nextBotToShoot = 0;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject circleBotPrefab;
    private List<Transform> botsTransforms = new List<Transform>();
    private int curBotsCount;


    private void Start()
    {
        //bots.AddRange(gameObject.GetComponentsInChildren<Weapon_CircleBot_Scr>());
        curBotsCount = transform.childCount;
        for (int i = 0; i < transform.childCount; i++)
        {
            botsTransforms.Add(transform.GetChild(i));
        }
        UpdateBotsRadialPositions();
    }
    private void Update()
    {
        transform.Rotate(transform.forward, rotationSpeed * Time.deltaTime, Space.Self);

        if (lastBulletSpawnTime + Player_Stats_Scr.CircleBotsStats.bulletSpawnDelay / curBotsCount < Time.time)
        {
            SpawnBullet();
            lastBulletSpawnTime = Time.time;
        }
    }

    private void SpawnBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab, botsTransforms[nextBotToShoot].position, Quaternion.identity); // TODO: подправить чтоб стрелял откуда надо
        nextBotToShoot++;
        if (nextBotToShoot >= botsTransforms.Count)
            nextBotToShoot = 0;
    }
    private void UpdateBotsRadialPositions()
    {
        for (int i = 0; i < curBotsCount; i++)
        {
            botsTransforms[i].localPosition = Quaternion.AngleAxis(360 / curBotsCount * i, Vector3.forward) * Vector3.right;
            botsTransforms[i].rotation = Quaternion.identity;
        }
    }
    public void AddBots(int numOfBotsToAdd)
    {
        botsTransforms.Add(Instantiate(circleBotPrefab, transform).transform);
        curBotsCount++;
        UpdateBotsRadialPositions();
    }
}
