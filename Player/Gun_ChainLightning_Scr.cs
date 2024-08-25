using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_ChainLightning_Scr : MonoBehaviour
{
    public GameObject projectilePrefab;

    private float lastBulletSpawnTime = -1f;

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
        //TODO: прибраться
        //TODO: добавлять метку тех в кого уже попали
        //TODO: сделать итеративным, чтоб можно было менять количество отскоков
        //TODO: перемещать коллайдер в место последнего попадания
        //TODO: изменять радиус коллайдера

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.up, 10f);
        GameObject newProj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        LineRenderer projLineRenderer = newProj.GetComponent<LineRenderer>();
        CircleCollider2D projCollider = newProj.GetComponent <CircleCollider2D>();
        if (hit.transform != null && hit.transform.CompareTag("Enemy"))
        {
            projLineRenderer.SetPosition(1, transform.InverseTransformPoint(hit.point));
            projCollider.offset = transform.InverseTransformPoint(hit.point);
            RaycastHit2D[] raycastHit2Ds = new RaycastHit2D[10];
            Debug.Log(projCollider.Cast(Vector2.zero, raycastHit2Ds, 0.1f));
            foreach (RaycastHit2D hito in raycastHit2Ds)
            {
                if (hito.transform == hit.transform)
                    continue;
                if (hito.transform == null || !hit.transform.CompareTag("Enemy"))
                    continue;
                projLineRenderer.positionCount++;
                projLineRenderer.SetPosition(2, transform.InverseTransformPoint(hito.transform.position));
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
