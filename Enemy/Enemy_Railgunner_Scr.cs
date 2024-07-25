using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.UI.ScrollRect;

public class Enemy_Railgunner_Scr : Enemy_Scr
{
    [SerializeField] private float yPosition;
    private Transform playerTrans;


    [SerializeField] private float timeBetweenShots;
    private float lastShotTime = 3f;
    private bool shootFromRightSpawnPoint = true;
    [SerializeField] private GameObject railPrefab;
    private Transform railSpawnPoint1, railSpawnPoint2;

    protected override void Awake()
    {
        curHealth = maxHealth;
        lastShotTime = Time.time + 3f;
    }
    private void Start()
    {
        playerTrans = Player_Stats_Scr.instance.transform;
        railSpawnPoint1 = transform.GetChild(0);
        railSpawnPoint2 = transform.GetChild(1);
    }

    protected override void EnemyMovement()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(playerTrans.position.x, yPosition, 0), movementSpeed * Time.deltaTime);

        //TODO: нужен ли базовый метод в Enemy_Scr?
        if (lastShotTime < Time.time)
        {
            lastShotTime += timeBetweenShots;

            _ = ShootRail();
        }
    }
    private async Task ShootRail()
    {
        Transform newRail;
        if (shootFromRightSpawnPoint)
        {
            newRail = Instantiate(railPrefab, railSpawnPoint1.position, Quaternion.identity, transform).transform;
            shootFromRightSpawnPoint = !shootFromRightSpawnPoint;
        } else
        {
            newRail = Instantiate(railPrefab, railSpawnPoint2.position, Quaternion.identity, transform).transform;
            shootFromRightSpawnPoint = !shootFromRightSpawnPoint;
        }

        Vector3 midLocalPosition = new Vector3(0, railSpawnPoint1.localPosition.y, 0);
        Vector3 strtLocalPos = newRail.localPosition;
        float t = 0;
        while (t != 1)
        {
            if (destroyCancellationToken.IsCancellationRequested) //TODO: возможно надо добавить свой токен тоже
            {
                return;
            }
 
            t = Mathf.MoveTowards(t, 1, 1 * Time.deltaTime);
            newRail.localPosition = Vector3.Lerp(strtLocalPos, midLocalPosition, t);
            await Task.Yield();
        }

        newRail.GetComponent<Enemy_RailProj_Scr>().startGlow();
    }
}
