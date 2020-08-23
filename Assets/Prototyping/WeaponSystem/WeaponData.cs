using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WeaponSystem
{
    [Serializable]
    /// <summary>
    /// holds the data to be placed on the UI
    /// </summary>
    public class WeaponData
    {
        public string name;
        public WeaponType type;

        public int damage = 1;
        public int crit = 1;
        [TextArea(3,100)]
        public string desc;

        //show damage and crit like in dead cells
        public string GetDamageData() => $"Damage: {damage} ({crit})";

        //explain how the crit works if it has one
        public string GetDescription() => desc;
    }
}