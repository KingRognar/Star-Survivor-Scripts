using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Scr : MonoBehaviour
{
    public float bulletSpeed = 2f;

    [SerializeField] private AudioClip[] hitAudioClips;

    void Update()
    {
        transform.position += transform.up * Time.deltaTime * bulletSpeed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
            return;

        //TODO: перенести на скрипт врага в отдельных функциях
        
        Vector3 collisionPoint = gameObject.GetComponent<Collider2D>().ClosestPoint(collision.transform.position);

        collision.gameObject.GetComponent<Enemy_HitEffect_Scr>().SpawnParticles(collisionPoint, transform.position);

        Sound_FXManager_Scr.instance.PlayRandomFXClip(hitAudioClips, transform, 1);

        collision.GetComponent<Enemy_Scr>().TakeDamage(Player_Stats_Scr.Machinegun.bulletDamage); // TODO: изменить в зависимости от снаряда

        collision.GetComponent<Enemy_Flash_Scr>().StartFlash();

        Destroy(gameObject);
    }
}
