using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleTesting : MonoBehaviour
{
    public Transform target;
    public Transform target2;

    public float angle;
    public float angle2;

    public float adjustedAngle;

    void Update()
    {
        angle = CalcAngle(target);
        //angle2 = CalcAngle(target2);

        //if (Mathf.Abs(angle - angle2) >= 180)
        //    adjustedAngle = angle - angle2 - 180;
        //else
            adjustedAngle = Mathf.Sin(angle / 180 * Mathf.Deg2Rad) * Mathf.Rad2Deg * 180;
    }
    public float CalcAngle(Transform target)
    {
        Vector2 diff = target.position - transform.position;
        return Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
    }
}
