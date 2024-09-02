using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy_Flash_Scr))]
[RequireComponent(typeof(Enemy_HitEffect_Scr))]
public class Enemy_Scr : MonoBehaviour
{
    public float movementSpeed = 2f;
    public float maxHealth = 10f;
    protected float curHealth;
    public int expAward = 2;

    private bool becameVisible = false;

    protected virtual void Awake()
    {
        curHealth = maxHealth;
        Destroy(gameObject, 15f);
    }
    private void Update()
    {
        EnemyMovement();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player_Bullets"))
            return;

        
        TakeDamage(Player_Stats_Scr.Machinegun.bulletDamage, collision.transform.position); // TODO: изменить в зависимости от снаряда
    }

    public void TakeDamage(int damage, Vector3 dmgTakenFromPos)
    {
        curHealth -= damage;

        GetComponent<Enemy_Flash_Scr>().StartFlash();
        GetComponent<Enemy_HitEffect_Scr>().SpawnParticles(dmgTakenFromPos);
        Pushback(damage);

        if (curHealth <= 0)
            Die();
    }
   protected virtual void Die()
    {
        UpgradeSystem_Scr.instance.AwardEXP(expAward);
        Disappear();
    }
    protected void Disappear()
    {
        Destroy(gameObject);
    }
    protected virtual void Pushback(int damage)
    {
        transform.position += Vector3.up * ((float)damage / maxHealth);
    }

    protected virtual void EnemyMovement()
    {
        transform.position += -transform.up * Time.deltaTime * movementSpeed;
    }

    private void OnBecameVisible()
    {
        becameVisible = true;
    }
    private void OnBecameInvisible()
    {
        if (becameVisible == true)
            Disappear();
    }

}
