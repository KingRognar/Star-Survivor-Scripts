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

        Sound_FXManager_Scr.instance.PlayRandomFXClip(hitAudioClips, transform, 1);
        Destroy(gameObject);
    }
}
