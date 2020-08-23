using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public interface IWeaponInterface
    {
        void HandleInput();
        string GetUIInfo();

        void Equip();
        void Unequip();
    }
}