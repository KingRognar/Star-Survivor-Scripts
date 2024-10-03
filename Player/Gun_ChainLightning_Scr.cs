using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Burst.CompilerServices;
using UnityEngine;


public class Gun_ChainLightning_Scr : Weapon_Scr
{
     

    public GameObject projectilePrefab;

    private float lastSpawnTime = -1f;

    //TODO: добавить звуковой эффект молнии

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && (lastSpawnTime + Player_Stats_Scr.ChainLightningGun.spawnDelay < Time.time))
        {
            SpawnProjectile();
            lastSpawnTime = Time.time;
        }
    }

    void SpawnProjectile()
    {
        InstantiateNewProjectile();
    }

    async void InstantiateNewProjectile() 
    {
        List<Transform> transformHitList = new List<Transform>();
        List<Vector3> hitPositions = new List<Vector3>();
        RaycastHit2D[] hits = new RaycastHit2D[10];

        //TODO: прибраться
        //TODO: изменять радиус коллайдера


        // пускаем рэйкаст вперёд в поисках врага
        hits[0] = Physics2D.Raycast(transform.position, Vector3.up, 10f, 1 << 8);
        // создаём снаряд и инициализируем объекты для управления им
        GameObject newProj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        LineRenderer projLineRenderer = newProj.GetComponent<LineRenderer>();

        if (hits[0].transform != null && hits[0].transform.CompareTag("Enemy"))
        {
            transformHitList.Add(hits[0].transform);
            hitPositions.Add(transform.position);
            hitPositions.Add(transformHitList[0].position);

            transformHitList[0].GetComponent<Enemy_Scr>().TakeDamage((int)(Player_Stats_Scr.ChainLightningGun.baseDamage * Player_Stats_Scr.ChainLightningGun.damageMultiplier), hitPositions[0]);
            await ChainVisuals(projLineRenderer, hitPositions[0], hitPositions[1]);
            

            bool addedNewChain = true;
            int i = 1;
            while (i < Player_Stats_Scr.ChainLightningGun.chainsCount && addedNewChain)
            {
                addedNewChain = false;
                hits = Physics2D.CircleCastAll(hitPositions[i], Player_Stats_Scr.ChainLightningGun.chainDistance, Vector2.up, 0.01f, 1 << 8);

                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.transform == null || !hit.transform.CompareTag("Enemy"))
                        continue;
                    if (transformHitList.Contains(hit.transform))
                        continue;

                    transformHitList.Add(hit.transform);
                    hitPositions.Add(hit.transform.position);
                    addedNewChain = true;
                    break;
                }

                if (addedNewChain)
                {
                    transformHitList[i].GetComponent<Enemy_Scr>().TakeDamage(Player_Stats_Scr.ChainLightningGun.baseDamage, hitPositions[i]);
                    await ChainVisuals(projLineRenderer, hitPositions[i], hitPositions[i + 1]);
                }

                i++;
            }
        }
        else
        {
            await ChainVisuals(projLineRenderer, transform.position, transform.position + Vector3.up * 10);           
        }
        Destroy(newProj);
    }
    private async Task ChainVisuals(LineRenderer lineRenderer, Vector3 chainBeginigPoint, Vector3 chainEndPoint)
    {
        lineRenderer.SetPosition(0, transform.InverseTransformPoint(chainBeginigPoint));
        lineRenderer.SetPosition(1, transform.InverseTransformPoint(chainEndPoint));
        await FadeChain(lineRenderer.material);
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


    //// Upgrade Methods
    public void IncreaseChainDistance(float distanceIncrease)
    {
        Player_Stats_Scr.ChainLightningGun.chainDistance += distanceIncrease;
    }
    public void IncreaseChainCount(float chainCountIncrease)
    {
        Player_Stats_Scr.ChainLightningGun.chainsCount += (int)chainCountIncrease;
    }
    public void IncreaseDamageMultiplier(float multiplierIncrease)
    {
        Player_Stats_Scr.ChainLightningGun.damageMultiplier += multiplierIncrease;
    }
}
