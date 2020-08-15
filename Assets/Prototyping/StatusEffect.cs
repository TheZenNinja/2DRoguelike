using System;
using UnityEngine;
using System.Collections.Generic;

public enum StatusType
{
    bleed,

    burning,    //fire
    frozen,     //water = slows entities, higher level stops movement entirely
    rooted,     //earth
    stunned,
    blistered,  //air = take more damage
    shock,      //lightning
    confused,   //arcane
}
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
}
