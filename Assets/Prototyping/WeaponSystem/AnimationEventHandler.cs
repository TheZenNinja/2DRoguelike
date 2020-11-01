﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public void WeaponEvent(int eventID)
    {
        EquipmentManager.instance.currentWeapon.events[eventID]?.Invoke();
    }
}
