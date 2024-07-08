using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_CircleBots_Scr : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 200f;
    private float lastBulletSpawnTime = 0f;
    private int nextBotToShoot = 0;

    [SerializeField] private GameObject bulletPrefab;
    private List<Transform> botsTransforms = new List<Transform>();
    //private List<Weapon_CircleBot_Scr> bots = new List<Weapon_CircleBot_Scr>();




    private void Start()
    {
        //bots.AddRange(gameObject.GetComponentsInChildren<Weapon_CircleBot_Scr>());
        for (int i = 0; i < transform.childCount; i++)
        {
            botsTransforms.Add(transform.GetChild(i));
        }
    }
    void Update()
    {
        transform.Rotate(transform.forward, rotationSpeed * Time.deltaTime, Space.Self);

        if (lastBulletSpawnTime + Player_Stats_Scr.circleBots.bulletSpawnDelay < Time.time)
        {
            SpawnBullet();
            lastBulletSpawnTime = Time.time;
        }
    }

    void SpawnBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab, botsTransforms[nextBotToShoot].position, Quaternion.identity); // TODO: подправить чтоб стрелял откуда надо
        nextBotToShoot++;
        if (nextBotToShoot >= botsTransforms.Count)
            nextBotToShoot = 0;
    }
}
