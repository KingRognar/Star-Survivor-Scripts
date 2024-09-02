using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCapsule_Scr : MonoBehaviour
{
    [SerializeField] private float cosAmpMultiplier = 6f;
    [SerializeField] private float cosTimeMultiplier = 0.2f;
    [SerializeField] private float sinAmpMultiplier = 0.5f;
    [SerializeField] private float sinTimeMultiplier = 1f;
    [SerializeField] private float rotationSpeed = 2f;

    void Update()
    {
        CapsuleMovement();
    }

    private void CapsuleMovement()
    {
        float xPos = Mathf.Cos(Time.time * cosTimeMultiplier) * cosAmpMultiplier;
        float yPos = 3 + Mathf.Sin(Time.time * sinTimeMultiplier) * sinAmpMultiplier;
        transform.position = new Vector3(xPos, yPos, 0);

        transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
    }
}
