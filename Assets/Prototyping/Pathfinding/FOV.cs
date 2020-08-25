using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;

[Serializable]
public class FOV
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public List<T> GetObjectsInView<T>(Transform root, Vector2 dir, LayerMask layerMask)
    {
        List<T> list = new List<T>();

        Collider2D[] cols = Physics2D.OverlapCircleAll(root.position, radius);

        foreach (var c in cols)
        {
            T t = c.GetComponent<T>();
            if (t != null)
            {
                Vector2 dirToTarget = (c.transform.position - root.position).normalized;
                if (Vector2.Angle(dir, dirToTarget) <= angle / 2)
                    if (!Physics2D.Linecast(root.position, c.transform.position, layerMask))
                        if (!list.Contains(t))
                            list.Add(t);
            }
        }

        return list;
    }

    public float GetAngleFromDir(Vector2 dir)
    {
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }
    public static Vector2 GetDirFromAngle(Transform obj, float angle, bool worldSpace = false)
    {
        if (!worldSpace)
            angle += obj.eulerAngles.z;
        return new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle)).normalized;
    }
    public void DrawDebug(Transform obj)
    {
        Handles.color = Color.green;
        Handles.DrawLine(obj.position, (Vector2)obj.transform.position + GetDirFromAngle(obj, angle/2) * radius);
        Handles.DrawLine(obj.position, (Vector2)obj.transform.position + GetDirFromAngle(obj, -angle/2) * radius);
        Handles.DrawWireArc(obj.position, Vector3.forward, GetDirFromAngle(obj,-angle/2), angle, radius);
    }
}