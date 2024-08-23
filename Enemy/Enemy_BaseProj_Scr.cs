using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BaseProj_Scr : MonoBehaviour
{
    [SerializeField] protected float projMovementSpeed;
    public int projDamage;

    protected virtual void Movement()
    {
        transform.position += new Vector3(0, -1, 0) * projMovementSpeed * Time.deltaTime;
    }
}
