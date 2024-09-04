using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Weapon_SnakeDrone_Scr : MonoBehaviour
{
    //public static Weapon_SnakeDrone_Scr instance;

    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float rotationSpeed = 30f;
    [SerializeField] private float circlingDistance = 0.3f;
    [SerializeField] private float cosAmp = 1f;
    [SerializeField] private float cosTimeScale = 1f;
    private Vector3 circlingVector = Vector3.up;

    [SerializeField] private GameObject tailSegmentPrefab;
    [SerializeField] private List<Transform> tailTransforms = new List<Transform>();
    [SerializeField] private float tailDistance = 0.45f;

    private Transform target;

    /*private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }*/
    private void Start()
    {
        foreach (Transform tailTransform in tailTransforms)
            tailTransform.parent = null;
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
        //Debug.Log(enemies.Length);
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
    private void MoveToTarget(Vector3 movementVector)
    {
        transform.position += Vector3.Normalize(movementVector) * Time.deltaTime * movementSpeed;
        UpdateTailPosition();
    }
    private void CircleAroundTarget()
    {
        Vector3 movementVector = target.position - transform.position;
        if (movementVector.sqrMagnitude <= 1)
        {
            MoveToTarget(target.position - transform.position + circlingVector);
            circlingVector = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, Vector3.forward) * circlingVector;
            Debug.DrawLine(target.position, target.position + circlingVector, Color.red, 0.1f);
        }
        else
        {
            Vector3 perpendicularVector = Vector3.Cross(movementVector, Vector3.back).normalized;
            float cos = Mathf.Cos(Time.time * cosTimeScale) * cosAmp;
            MoveToTarget(movementVector + perpendicularVector * cos);
            Debug.DrawLine(target.position, target.position + perpendicularVector * cos, Color.red, 0.05f);
        }
    }
    private void UpdateTailPosition()
    {
        for (int i = 0; i < tailTransforms.Count; i++)
        {
            if (i == 0)
                UpdateTailSegment(transform, tailTransforms[i]);
            else
                UpdateTailSegment(tailTransforms[i - 1], tailTransforms[i]);
        }
    }
    private void UpdateTailSegment(Transform firstTransform, Transform secondTransform)
    {
        Vector3 difference = secondTransform.position - firstTransform.position;
        if (difference.sqrMagnitude > tailDistance * tailDistance)
        {
            Vector3 constraintVector = Vector3.Normalize(secondTransform.position - firstTransform.position) * tailDistance;
            secondTransform.position = firstTransform.position + constraintVector;
        }
    }


    public void AddTailSegments()
    {
        GameObject newTailSegment = Instantiate(tailSegmentPrefab, tailTransforms[tailTransforms.Count - 1].position, Quaternion.identity);
        newTailSegment.transform.localScale = tailTransforms[tailTransforms.Count - 1].localScale;
        tailTransforms.Add(newTailSegment.transform);
        newTailSegment = Instantiate(tailSegmentPrefab, tailTransforms[tailTransforms.Count - 1].position, Quaternion.identity);
        newTailSegment.transform.localScale = tailTransforms[tailTransforms.Count - 1].localScale;
        tailTransforms.Add(newTailSegment.transform);
    }
}
