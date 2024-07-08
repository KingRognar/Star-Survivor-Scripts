using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Scr : MonoBehaviour
{
    public float movementSpeed = 2f;
    public float maxHealth = 10f;
    private float curHealth;
    public int expAward = 2;

    private void Awake()
    {
        curHealth = maxHealth;
    }
    void Update()
    {
        transform.position += -transform.up * Time.deltaTime * movementSpeed;
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


}
