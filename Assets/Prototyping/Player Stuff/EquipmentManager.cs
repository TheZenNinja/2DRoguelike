using Assets.Prototyping.WeaponSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;
using ZenUtil;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;
    public EquipmentManager() => instance = this;

    public PrototypeWeaponItem weaponItem;

    

    public WeaponBase weaponObj;
    public WeaponBase currentWeapon => weaponObj;

    public Transform[] holsterPos;

    public Transform playerHands;
    public PlayerControl playerControl;
    public Animator anim;
    public Transform weaponRoot;

    void Start()
    {
        EquipWeapon(weaponItem);
    }
    public void EquipWeapon(PrototypeWeaponItem item)
    {
        if (!weaponItem)
        {
            Unequip();
            return;
        }
        weaponItem = item;
        weaponObj = weaponItem.Instantiate(weaponRoot);
        weaponObj.Equip(weaponRoot, anim);
    }
    public void Unequip()
    {
        if (weaponObj)
        {
            Destroy(weaponObj.gameObject);
            weaponObj = null;
        }
        weaponItem = null;
    }
    void Update()
    {
        if (currentWeapon)
            currentWeapon.HandleInput();

        PlayerUI.instance.UpdateWeapon(weaponItem);
    }

    public void ThrowItem()
    {
        if (weaponItem != null)
            ThrownItem.Spawn(weaponItem, playerControl.transform.position, CursorControl.instance.GetDirFromHand() * 40);

        Unequip();
    }

    public void DropItem(Vector3 position)
    {
        weaponItem.DropItem(position);
        Unequip();
    }
}
