using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy_Flash_Scr))]
[RequireComponent(typeof(Enemy_HitEffect_Scr))]
public class WeaponCapsule_Scr : Enemy_Scr
{
     private float cosAmpMultiplier = 6f;
     private float cosTimeMultiplier = 0.8f;
     private float sinAmpMultiplier = 1f;
     private float sinTimeMultiplier = 4f;
     private float rotationSpeed = 2f;
    [SerializeField] private float movementHeight = 3f;

    protected override void EnemyMovement()
    {
        float xPos = Mathf.Cos(Time.time * cosTimeMultiplier) * cosAmpMultiplier;
        float yPos = movementHeight + Mathf.Sin(Time.time * sinTimeMultiplier) * sinAmpMultiplier;
        transform.position = new Vector3(xPos, yPos, 0);

        transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
    }
    protected override void Die()
    {
        NewWeaponMenu_Scr.instance.OpenNewWeaponMenu();
        base.Die();
    }

}
