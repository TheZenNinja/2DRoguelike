using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EquipmentUI : MonoBehaviour
{
    public GameObject gunUI;
    public TextMeshProUGUI ammoTxt;
    public GameObject bowUI;
    public Slider bowCharge;
    public Image bowChargeSprite;

    public void UpdateUI(EquipmentUIData data)
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


    }
}
public enum EquipmentType
{
    gun,
    bow,
    thrown,
}
public class EquipmentUIData
{
    public EquipmentType type;
    public int currentAmmo;
    public int reserveAmmo;

    public float chargePercent;
    public bool overDrawn;

    public static EquipmentUIData NewGunData(Vector2Int currentAmmo, Vector2Int reserveAmmo)
    {
        EquipmentUIData data = new EquipmentUIData();
        data.type = EquipmentType.gun;
        data.currentAmmo = currentAmmo.x;
        data.reserveAmmo = currentAmmo.y;
        return data;
    }
    public static EquipmentUIData NewBowData(float chargePercent, bool overDrawn, int reserveAmmo = 0)
    {
        EquipmentUIData data = new EquipmentUIData();
        data.type = EquipmentType.bow;
        data.chargePercent = chargePercent;
        data.overDrawn = overDrawn;
        return data;
    }
    public static EquipmentUIData NewThrownData(int currentAmmo, int reserveAmmo)
    {
        EquipmentUIData data = new EquipmentUIData();
        data.type = EquipmentType.thrown;
        data.currentAmmo = currentAmmo;
        data.reserveAmmo = reserveAmmo;
        return data;
    }
}
