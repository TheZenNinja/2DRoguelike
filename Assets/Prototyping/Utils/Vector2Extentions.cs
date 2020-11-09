using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public static class Vector2Extentions
{
    public static Vector2 Rotate(this Vector2 v, float angle)
    {
        return GetDirection(v.GetAngle() + angle);
    }
    public static float GetAngle(this Vector2 v) => Mathf.Atan2(v.y , v.x) * Mathf.Rad2Deg;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="angle">Angle in degrees</param>
    /// <returns></returns>
    public static Vector2 RandomizeAngle(this Vector2 v, float angle)
    {
        float a = v.GetAngle();
        a += UnityEngine.Random.Range(-angle, angle);
        return GetDirection(a);
    }
    public static Vector2 GetDirection(float angle) => new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
}
