using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using WeaponSystem;

public class EquipmentUI : MonoBehaviour
{
    public GameObject gunUI;
    public TextMeshProUGUI ammoTxt;
    public GameObject bowUI;
    public Slider bowCharge;
    public Image bowChargeSprite;
    WeaponBase currentWeapon;

    public void SetWeapon(WeaponBase weapon) => currentWeapon = weapon;
    public void Update()
    {
        if (currentWeapon != null)
            ammoTxt.text = currentWeapon.GetUIInfo();
        else
            ammoTxt.text = "";
    }
    /*public void UpdateUI(EquipmentUIData data)
    {
        switch (data.type)
        {
            default:
            case EquipmentType.gun:
                gunUI.SetActive(true);
                bowUI.SetActive(false);
            ammoTxt.text = data.currentAmmo + "/" + data.reserveAmmo;
                break;
            case EquipmentType.bow:
                gunUI.SetActive(false);
                bowUI.SetActive(true);
                bowCharge.value = data.chargePercent;
                bowChargeSprite.color = data.overDrawn ? Color.red : Color.white;
                break;
            case EquipmentType.thrown:
                break;
        }
    }*/
}
public enum EquipmentType
{
    gun,
    bow,
    thrown,
}
