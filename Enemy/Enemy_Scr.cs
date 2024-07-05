using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Scr : MonoBehaviour
{
    public float movementSpeed = 2f;
    public float maxHealth = 10f;
    private float curHealth;
    public float expAward = 2f;

    [SerializeField] private AudioClip[] hitAudioClips;

    private void Awake()
    {
        curHealth = maxHealth;
    }
    void Update()
    {
        transform.position += -transform.up * Time.deltaTime * movementSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player_Bullets"))
            return;


        Vector3 collisionPoint = gameObject.GetComponent<Collider2D>().ClosestPoint(collision.transform.position);
        Vector3 bulletPosition = collision.transform.position;
        gameObject.GetComponent<Enemy_HitEffect_Scr>().SpawnParticles(collisionPoint, bulletPosition);

        Sound_FXManager_Scr.instance.PlayRandomFXClip(hitAudioClips, transform, 1);

        TakeDamage(Player_Stats_Scr.machineGun.bulletDamage); // TODO: изменить в зависимости от снаряда

        gameObject.GetComponent<Enemy_Flash_Scr>().StartFlash();

        Destroy(collision.gameObject);
    }


    private void TakeDamage(int damage)
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
