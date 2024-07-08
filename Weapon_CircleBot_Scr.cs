using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_CircleBot_Scr : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 200f;

    void Update()
    {
        transform.Rotate(transform.forward, -rotationSpeed * Time.deltaTime, Space.Self);
    }
}
