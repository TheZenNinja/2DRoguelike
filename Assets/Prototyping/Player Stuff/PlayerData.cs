using UnityEngine;
using System.Collections;
using WeaponSystem;

public class PlayerData : MonoBehaviour
{
    public EquipmentManager equipment;
    public void AddToInventory(PrototypeWeaponItem item)
    {
        if (equipment.weaponItem != null)
            equipment.DropItem(transform.position + transform.forward);

        equipment.weaponItem = item;
    }
}
