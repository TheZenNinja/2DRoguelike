using UnityEngine;
using System.Collections;
using WeaponSystem;

[CreateAssetMenu(menuName = "Singleton SOs/Sprite Atlas")]
public class SpriteAtlas : ScriptableObject
{
    public static SpriteAtlas instance;
    public SpriteAtlas() => instance = this;

    public void Awake()
    {
        instance = this;
    }

    public Sprite[] weaponTypeSprites;

    public static Sprite GetWeaponTypeSprite(WeaponType type)
    {
        //if (!instance)
        //    instance = Resources.Load <SpriteAtlas>("Sprite Atlas");

#pragma warning disable IDE0066 // Doesnt work
        switch (type)
#pragma warning restore IDE0066 // Convert switch statement to expression
        {
            case WeaponType.gun:
                return instance.weaponTypeSprites[0];
            case WeaponType.bow:
                return instance.weaponTypeSprites[1];
            case WeaponType.daggers:
                return instance.weaponTypeSprites[2];
            case WeaponType.sword:
            default:
                return instance.weaponTypeSprites[3];
            case WeaponType.spear:
                return instance.weaponTypeSprites[4];
            case WeaponType.scythe:
                return instance.weaponTypeSprites[5];
        }
    }
}
