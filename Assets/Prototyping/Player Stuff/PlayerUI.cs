using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WeaponSystem;
using ZenUtil;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI instance;
    public PlayerUI() => instance = this;

    [Header("Player")]
    EntityBase playerEntity;
    public Slider hpBar;
    [Header("Equipment")]
    public GameObject weaponInfoBox;
    public TextMeshProUGUI weaponInfoTxt;
    public TextMeshProUGUI stanceNameTxt;
    public HotbarSlotUI weaponSprite;
    // Start is called before the first frame update
    void Start()
    {
        playerEntity = PlayerControl.instance;
    }
    public void Update()
    {
        UpdatePlayer();
        UpdateEquipment();
    }
    // Update is called once per frame
    private void UpdatePlayer()
    { 
        hpBar.value = playerEntity.healthPercent;
    }
    private void UpdateEquipment()
    {
        stanceNameTxt.text = StanceManager.instance.currentStance;

        WeaponBase currentWep = EquipmentManager.instance.currentWeapon;

        if (currentWep && currentWep.GetUIInfo() != "null")
        {
            weaponInfoBox.SetActive(true);
            weaponInfoTxt.text = EquipmentManager.instance.currentWeapon.GetUIInfo();
        }
        else
        {
            weaponInfoBox.SetActive(false);
            weaponInfoTxt.text = "";
        }
    }

    public void UpdateWeapon(PrototypeWeaponItem item)
    {
        if (item == null)
            return;

        weaponSprite.sprite.sprite = SpriteAtlas.GetWeaponTypeSprite(item.type);
    }


}
