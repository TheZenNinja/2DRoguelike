using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public int weaponIndex;
    public EquipmentBase[] weapons = new EquipmentBase[3];
    private EquipmentBase currentWeapon => weapons[weaponIndex];

    public Transform playerHands;
    public PlayerControl playerControl;
    public EquipmentUI ui;
    public Animator anim;
    void Start()
    {
        SwapWeapon(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwapWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwapWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SwapWeapon(2);


        currentWeapon.HandleInput();
        ui.UpdateUI(currentWeapon.GetUIData());
    }
    public void SwapWeapon(int index)
    {
        weaponIndex = index;
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == index)
                weapons[i].Equip();
            else
                weapons[i].Unequip();
        }
    }
}
