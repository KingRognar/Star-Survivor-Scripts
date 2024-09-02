using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Weapon_SnakeDrone_Scr : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float rotationSpeed = 30f;
    [SerializeField] private float circlingDistance = 0.3f;
    private Vector3 circlingVector = Vector3.up;

    [SerializeField] private Transform tail1;
    [SerializeField] private Transform tail2;
    [SerializeField] private float tailDistance = 0.45f;

    private Transform target;

    private void Start()
    {
        tail1.parent = null;
        tail2.parent = null;
        circlingVector *= circlingDistance;
    }
    private void Update()
    {
        if (target == null)
            if (!FindTarget())
                return;

        CircleAroundTarget();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform == target)
            return;
    }

    private bool FindTarget()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log(enemies.Length);
        if (enemies.Length == 0)
            return false;

        float targetDistance = Mathf.Infinity;
        foreach(GameObject enemy in enemies)
        {
            float nextSqrDistance = Vector3.SqrMagnitude(Player_Stats_Scr.instance.transform.position - enemy.transform.position);
            if (nextSqrDistance < targetDistance)
            {
                targetDistance = nextSqrDistance;
                target = enemy.transform;
            }
        }

        return true;
    }
    private void MoveToTarget(Vector3 moveTo)
    {
        transform.position += Vector3.Normalize(moveTo) * Time.deltaTime * movementSpeed;
        UpdateTalePosition();
    }
    private void CircleAroundTarget()
    {
        MoveToTarget(target.position - transform.position + circlingVector);
        circlingVector = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, Vector3.forward) * circlingVector;
        Debug.DrawLine(target.position, target.position + circlingVector, Color.red, 0.1f);

        UpdateTalePosition();
    }
    private void UpdateTalePosition()
    {
        Vector3 constraintVector;
        Vector3 difference = tail1.position - transform.position;
        if (difference.sqrMagnitude > tailDistance*tailDistance)
        {
            constraintVector = Vector3.Normalize(tail1.position - transform.position) * tailDistance;
            tail1.position = transform.position + constraintVector;
        }
        difference = tail2.position - tail1.position;
        if (difference.sqrMagnitude > tailDistance*tailDistance)
        {
            constraintVector = Vector3.Normalize(tail2.position - tail1.position) * tailDistance;
            tail2.position = tail1.position + constraintVector;
        }
    }
}
