using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using WeaponSystem;
using Pathfinding;
using ZenUtil;

public class EquipmentUI : MonoBehaviour
{
    public TextMeshProUGUI infoTxt;
    WeaponBase currentWeapon;

    public List<Image> cooldownSprites;
    public void SetWeapon(WeaponBase weapon) => currentWeapon = weapon;
    public void Update()
    {
        if (currentWeapon != null)
            infoTxt.text = currentWeapon.GetUIInfo();
        else
            infoTxt.text = "";
    }
    public void UpdateCooldown(int index, Timer timer)
    {
        cooldownSprites[index].fillAmount = timer.finished? 0 : 1 - timer.percent;
    }
}