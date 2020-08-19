using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentBase : MonoBehaviour
{
    public abstract void HandleInput();
    public abstract EquipmentUIData GetUIData();

    public abstract void Equip();
    public abstract void Unequip();
}
