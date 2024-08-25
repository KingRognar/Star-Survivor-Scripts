using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Gun_ChainLightning_Scr : MonoBehaviour
{
    public GameObject projectilePrefab;

    private float lastBulletSpawnTime = -1f;
    [SerializeField] private int chainsCount = 4;

    //TODO: добавить звуковой эффект молнии
    //TODO: добавить fadeout визуальный эффект

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && (lastBulletSpawnTime + Player_Stats_Scr.Machinegun.bulletSpawnDelay < Time.time))
        {
            SpawnProjectile();
            lastBulletSpawnTime = Time.time;
        }
    }

    void SpawnProjectile()
    {
        InstantiateNewProjectile();
    }

    void InstantiateNewProjectile() 
    {
        List<Transform> transToHitHS = new List<Transform>();
        RaycastHit2D[] hits = new RaycastHit2D[10];

        //TODO: прибраться
        //TODO: добавлять метку тех в кого уже попали
        //TODO: сделать итеративным, чтоб можно было менять количество отскоков
        //TODO: перемещать коллайдер в место последнего попадания
        //TODO: изменять радиус коллайдера


        // пускаем рэйкаст вперёд в поисках врага
        hits[0] = Physics2D.Raycast(transform.position, Vector3.up, 10f);
        // создаём снаряд и инициализируем объекты для управления им
        GameObject newProj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        LineRenderer projLineRenderer = newProj.GetComponent<LineRenderer>();
        CircleCollider2D projCollider = newProj.GetComponent <CircleCollider2D>();

        if (hits[0].transform != null && hits[0].transform.CompareTag("Enemy"))
        {
            transToHitHS.Add(hits[0].transform);
            projLineRenderer.SetPosition(1, transform.InverseTransformPoint(transToHitHS[0].position));

            int i = 0;
            bool thereIsAnotherTarget = true;
            while (i < chainsCount)
            {
                projCollider.offset = transform.InverseTransformPoint(transToHitHS[i].position);
                //hits = new RaycastHit2D[10];
                Debug.Log(projCollider.Cast(Vector2.zero, hits, 0.1f));
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.transform == null || !hit.transform.CompareTag("Enemy"))
                        continue;
                    if (transToHitHS.Contains(hit.transform))
                        continue;
                    projLineRenderer.positionCount++;
                    transToHitHS.Add(hit.transform);
                    projLineRenderer.SetPosition(i+2, transform.InverseTransformPoint(transToHitHS[i+1].position));
                    break;
                }

                i++;
                if (i+1 > transToHitHS.Count)
                    break;
            }


        }
        else
        {
            projLineRenderer.SetPosition(1, transform.InverseTransformPoint(transform.position + Vector3.up * 10));
        }
        Destroy(newProj, 0.2f);
    }
}
