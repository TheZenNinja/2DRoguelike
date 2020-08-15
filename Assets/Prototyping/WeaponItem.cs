using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponItem : ScriptableObject
{
    public Sprite sprite;

    public int damage;
    public float critMulti;

    public float range;
}