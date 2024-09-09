using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxElement_Scr : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 8f;
    [SerializeField] private float destroyTime = 3f;

    private void Awake()
    {
        Destroy(gameObject, destroyTime);
    }
    void Update()
    {
        transform.position += Vector3.down * movementSpeed * Time.deltaTime;
    }
}
