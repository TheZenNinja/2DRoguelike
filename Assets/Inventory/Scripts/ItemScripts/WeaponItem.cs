using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Inventory
{
    public class WeaponItem : Item
    {
        /*
        public WeaponType weaponType;

        public int weaponSubtype;

        public CrystalItem crystal;
        public List<Part> parts;

        public int damage;
        public float attackRange;
        public float attackSpeed;

        public WeaponItem(WeaponTemplate template, int weaponSubtype, Part[] parts, CrystalItem crystal = null) : this(template, weaponSubtype, new List<Part>(parts), crystal) { }
        public WeaponItem(WeaponTemplate template, int weaponSubtype, List<Part> parts, CrystalItem crystal = null)
        {
            this.crystal = crystal;
            parts.Sort();
            this.parts = parts;

        }

        public void UpdateData()
        {
            stackable = false;
            WeaponData data = WeaponData.GetWeaponData(weaponType);
            damage = data.attackDamage;
            attackRange = data.attackRange;
            attackSpeed = data.attackSpeed;
        }
        public WeaponType GetType(bool inAltMode)
        {
            return weaponType;
        }
        public override string GetDescription()
        {
            return GetName();
        }

        public override string GetName()
        {
            return WeaponBuilder.GetWeaponSubtypeName(weaponType, weaponSubtype);
        }

        public override Sprite GetSprite()
        {
            return ItemSpriteAtlas.instance.GetWeaponSprite(weaponType);
        }

        public override Rarity GetRarity()
        {
            return (Rarity)(PartMaterialAtlas.GetMaterialData(parts[0].material).materialLevel -1);
        }*/
        public override string GetDescription()
        {
            throw new NotImplementedException();
        }

        public override string GetName()
        {
            throw new NotImplementedException();
        }

        public override Sprite GetSprite()
        {
            throw new NotImplementedException();
        }
    }
}