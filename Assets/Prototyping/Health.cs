using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[Serializable]
public class Health
{
    private enum HealthType
    {
        none, armor, shield
    }
    public IntCounter hpData;

}
public class IntCounter
{
    public int min;
    public int value;
    public int max;

    public IntCounter(int value, int min, int max)
    {
        this.value = value;
        this.min = min;
        this.max = max;
    }

    public void Add(int x) => value = Mathf.Clamp(value + x, min, max);
    public void Subtract(int x) => Add(-x);

    public static IntCounter operator +(IntCounter counter, int x)
    {
        counter.Add(x);
        return counter;
    }
    public static IntCounter operator -(IntCounter counter, int x)
    {
        counter.Subtract(x);
        return counter;
    }
}
public class FloatCounter
{
    public float min;
    public float value;
    public float max;

    public FloatCounter(float value, float min, float max)
    {
        this.value = value;
        this.min = min;
        this.max = max;
    }

    public void Add(float x) => value = Mathf.Clamp(value + x, min, max);
    public void Subtract(float x) => Add(-x);

    public static FloatCounter operator +(FloatCounter counter, float x)
    {
        counter.Add(x);
        return counter;
    }
    public static FloatCounter operator -(FloatCounter counter, float x)
    {
        counter.Subtract(x);
        return counter;
    }
}
