using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player_BulletEmitter_Scr : MonoBehaviour
{
    public static Player_BulletEmitter_Scr instance;

    public GameObject bulletPrefab;
    private float bulletSpreadCurAngle = 0f;
    private bool spreadDirectionIsRight = true;

    private float lastBulletSpawnTime = -1f;
    private List<GameObject> bulletsSpawnList = new List<GameObject>();
    private int bulletsSpawned = 0;
    


    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && (lastBulletSpawnTime + Player_Stats_Scr.machineGun.bulletSpawnDelay < Time.time))
        {
            SpawnBullet();
            lastBulletSpawnTime = Time.time;
        }
    }

    void SpawnBullet ()
    {
        if (bulletsSpawned >= bulletsSpawnList.Count)
            InstantiateNewBullet();
    }

    void InstantiateNewBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        //newBullet.transform.RotateAround(newBullet.transform.position, Vector3.forward, Random.Range(-bulletSpreadAngle, bulletSpreadAngle));
        newBullet.transform.localScale = Vector3.one * Player_Stats_Scr.machineGun.bulletScale;
        newBullet.transform.RotateAround(newBullet.transform.position, Vector3.forward, bulletSpreadCurAngle);
        if (spreadDirectionIsRight)
        {
            bulletSpreadCurAngle++;
            if (bulletSpreadCurAngle >= Player_Stats_Scr.machineGun.bulletSpreadAngle)
                spreadDirectionIsRight = !spreadDirectionIsRight;
        }
        else
        {
            bulletSpreadCurAngle--;
            if (bulletSpreadCurAngle <= -Player_Stats_Scr.machineGun.bulletSpreadAngle)
                spreadDirectionIsRight = !spreadDirectionIsRight;
        }
            

        bulletsSpawnList.Add(newBullet);
        bulletsSpawned++;
    }
}
