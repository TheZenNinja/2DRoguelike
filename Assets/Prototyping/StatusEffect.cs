using System;
using UnityEngine;
using System.Collections.Generic;

public enum StatusType
{
    bleed,
    stunned,
    floating,
    /*
    burning,    //fire
    frozen,     //water = slows entities, higher level stops movement entirely
    rooted,     //earth
    blistered,  //air = take more damage
    shock,      //lightning
    confused,   //arcane
    */
}
[Serializable]
public class StatusEffect
{
    public StatusType type;
    public int level;
    public float duration;

    public void Decrease()
    {
        duration -= Time.deltaTime;
    }
    public void IncreaseToMax(float time, float max)
    {
        if (duration < max)
        {
            if (duration + time > max)
                time = max - duration;

            Increase(time);
        }
        else
            Debug.Log("Duration is above max");
    }
    public void Increase(float time)
    {
        duration += time;
    }
    public static StatusEffect operator +(StatusEffect a, StatusEffect b)
    {
        if (a.type != b.type)
            throw new Exception("Not matching status types");

        a.level += b.level;
        a.duration += b.duration;
        return a;
    }
    public static StatusEffect InflictStatus(StatusType type, float length)
    {
        return new StatusEffect() { type = type, level = 1, duration = length };
    }
}
