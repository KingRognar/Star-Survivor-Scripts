using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Scr : MonoBehaviour
{
    public float movementSpeed = 2f;
    public float maxHealth = 10f;
    protected float curHealth;
    public int expAward = 2;

    private void Awake()
    {
        curHealth = maxHealth;
        Destroy(gameObject, 15f);
    }
    private void Update()
    {
        EnemyMovement();
    }




    public void TakeDamage(int damage)
    {
        curHealth -= damage;

        if (curHealth <= 0)
            Die();
    }
    private void Die()
    {
        UpgradeSystem_Scr.instance.AwardEXP(expAward);
        Destroy(gameObject);
    }
    protected void Disappear()
    {
        Destroy(gameObject);
    }

    protected virtual void EnemyMovement()
    {
        transform.position += -transform.up * Time.deltaTime * movementSpeed;
    }


}
