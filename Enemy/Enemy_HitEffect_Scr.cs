using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HitEffect_Scr : MonoBehaviour
{
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private Color particleColor;
    [SerializeField] private int minParticlesCount = 2; [SerializeField] private int maxParticlesCount = 4;
    [SerializeField] private float scatterAngleRange = 20f;


    public void SpawnParticles(Vector3 hitDirection) //TODO: добавить direction?
    {
        Vector3 collisionPoint = GetComponent<Collider>().ClosestPoint(hitDirection);
        int particlesCount = Random.Range(minParticlesCount, maxParticlesCount);
        for (int i = 0; i < particlesCount; i++)
        {
            Transform particle = Instantiate(particlePrefab, collisionPoint, Quaternion.identity).transform;
            float colorVariability = Random.Range(-0.05f, 0.05f);
            particle.GetComponent<SpriteRenderer>().color = particleColor + new Color(colorVariability, colorVariability, colorVariability);
            particle.RotateAround(particle.position, particle.forward, Random.Range(-180, 180));
            hitDirection.Normalize();
            hitDirection = Quaternion.AngleAxis(Random.Range(-scatterAngleRange,scatterAngleRange),transform.forward) * hitDirection;
            particle.GetComponent<Enemy_HitParticles_Scr>().directionToMove = hitDirection;
        }
    }
}
