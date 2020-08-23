using System;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(menuName = "Protyping/New Weapon")]
    public class PrototypeWeaponItem : ScriptableObject
    {
        public WeaponData data;
        public GameObject prefab;
    }
}
