using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Gun_ChainLightning_Scr : MonoBehaviour
{
    public GameObject projectilePrefab;

    private float lastBulletSpawnTime = -1f;
    [SerializeField] private int damage = 2;
    [SerializeField] private int chainsCount = 4;

    //TODO: добавить звуковой эффект молнии
    //TODO: Искать нового врага только после исчезания старой части цепи
    //TODO: сделать маску для рейкаста, чтоб хватал только врагов

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
            //projLineRenderer.SetPosition(1, transform.InverseTransformPoint(transToHitHS[0].position));

            int i = 0;
            while (i < chainsCount)
            {
                projCollider.offset = transform.InverseTransformPoint(transToHitHS[i].position);
                Debug.Log(projCollider.Cast(Vector2.zero, hits, 0.1f));
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.transform == null || !hit.transform.CompareTag("Enemy"))
                        continue;
                    if (transToHitHS.Contains(hit.transform))
                        continue;
                    //projLineRenderer.positionCount++;
                    transToHitHS.Add(hit.transform);
                    //projLineRenderer.SetPosition(i+2, transform.InverseTransformPoint(transToHitHS[i+1].position));
                    break;
                }

                i++;
                if (i+1 > transToHitHS.Count)
                    break;
            }

            _ = ChainVisuals(projLineRenderer, transToHitHS);
        }
        else
        {
            projLineRenderer.SetPosition(1, transform.InverseTransformPoint(transform.position + Vector3.up * 10));
            Destroy(newProj, 0.1f);
        }

    }
    private async Task ChainVisuals(LineRenderer lineRenderer, List<Transform> transforms)
    {
        transforms.Insert(0, transform);
        int chainCount = transforms.Count-1;
        for (int i = 0; i < chainCount; i++)
        {
            if (destroyCancellationToken.IsCancellationRequested)
            {
                return;
            }

            lineRenderer.SetPosition(0, transform.InverseTransformPoint(transforms[i].position));
            lineRenderer.SetPosition(1, transform.InverseTransformPoint(transforms[i+1].position));
            Enemy_Scr enemy = transforms[i+1].GetComponent<Enemy_Scr>();
            enemy.TakeDamage(damage);
            await FadeChain(lineRenderer.material);
        }
        Destroy(lineRenderer.gameObject);
    }
    private async Task FadeChain(Material material)
    {
        float fadeTime = 0.15f;
        float curTime = 0f;
        while (curTime < fadeTime)
        {
            if (destroyCancellationToken.IsCancellationRequested)
            {
                return;
            }

            material.SetFloat("_DissolvePercent", curTime / fadeTime);
            curTime += Time.deltaTime;
            await Task.Yield();
        }
    }
}
