using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Enemy_RailProj_Scr : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Material shaderMaterial;

    private bool isGlowStarted = false;
    private bool isMovementStarted = false;
    private float currentGlowIntensity = 0f;

    //TODO: сделать базовый класс для вражеских снарядов

    private void Awake()
    {
        Destroy(gameObject, 4f);
        spriteRenderer = GetComponent<SpriteRenderer>();
        shaderMaterial = spriteRenderer.material;
    }

    private void Update()
    {
        if (isGlowStarted && !isMovementStarted)
            RiseGlowIntensity();
        if (isMovementStarted)
            Movement();
    }

    public void startGlow()
    {
        isGlowStarted = true;
    }
    private void RiseGlowIntensity()
    {
        currentGlowIntensity = Mathf.MoveTowards(currentGlowIntensity, 1, 1 * Time.deltaTime);
        shaderMaterial.SetFloat("_GlowIntensity", currentGlowIntensity);
        if (currentGlowIntensity == 1)
        {
            isMovementStarted = true;
            transform.parent = null;
        }
    }
    private void Movement()
    {
        transform.position += new Vector3(0, -1, 0) * 20 * Time.deltaTime;
    }
}
