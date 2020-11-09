using Assets.Prototyping.WeaponSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(menuName = "Protyping/New Weapon")]
    public class PrototypeWeaponItem : ScriptableObject
    {
        public GameObject prefab;
        public string itemName;
        public WeaponType type;
        public Sprite sprite;
        public int damage = 1;
        public int crit = 1;
        [TextArea(3, 100)]
        public string desc;

        //show damage and crit like in dead cells
        public string GetDamageData() => $"Damage: {damage} ({crit})";

        //explain how the crit works if it has one
        public string GetDescription() => desc;

        public WeaponBase Instantiate(Transform position)
        {
            return Instantiate(prefab, position).GetComponent<WeaponBase>();
        }

        public GameObject DropItem(Vector2 position)
        {
            return ThrownItem.Spawn(this, position, Vector2.zero).gameObject;
        }
    }
}
