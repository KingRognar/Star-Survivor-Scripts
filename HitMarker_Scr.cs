using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMarker_Scr : MonoBehaviour
{
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float animationTime = 1f;
    [SerializeField] private float startTime = 2f;
    [SerializeField] private float animationDelay = 2f;
    private Vector3 initialScale;


    private void Start()
    {
        initialScale = transform.localScale;
    }
    private void Update()
    {
        if (Time.time > startTime)
        {
            transform.localScale = initialScale * animCurve.Evaluate((Time.time - startTime) / animationTime);
        }
        if (Time.time > startTime + animationTime)
            startTime += animationDelay;
    }
}
