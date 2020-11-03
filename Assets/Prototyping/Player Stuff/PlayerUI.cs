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
    public TextMeshProUGUI weaponInfoTxt;
    public TextMeshProUGUI stanceNameTxt;
    public List<HotbarSlotUI> hotbarSprites;
    // Start is called before the first frame update
    void Start()
    {
        playerEntity = PlayerControl.instance;
        LoadWeaponSprites();
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
    private void LoadWeaponSprites()
    {
        for (int i = 0; i < 3; i++)
        {
            var type = EquipmentManager.instance.weaponItems[i].data.type;
            hotbarSprites[i].sprite.sprite = SpriteAtlas.GetWeaponTypeSprite(type);
        }
    }
    private void UpdateEquipment()
    { 
        stanceNameTxt.text = StanceManager.instance.currentStance;

        if (EquipmentManager.instance.currentWeapon != null)
            weaponInfoTxt.text = EquipmentManager.instance.currentWeapon.GetUIInfo();
        else
            weaponInfoTxt.text = "";
    }

    public void UpdateCooldown(int index, Timer timer) => hotbarSprites[index].UpdateCooldown(timer);


}
