using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Scr : MonoBehaviour
{
    public float bulletSpeed = 2f;

    void Update()
    {
        transform.position += transform.up * Time.deltaTime * bulletSpeed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
