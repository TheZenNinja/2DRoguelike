using UnityEngine;
using System.Collections;
using WeaponSystem;

[CreateAssetMenu(menuName = "Singleton SOs/Sprite Atlas")]
public class SpriteAtlas : ScriptableObject
{
    public static SpriteAtlas instance;
    public SpriteAtlas() => instance = this;

    public Sprite[] weaponTypeSprites;

    public static Sprite GetWeaponTypeSprite(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.gun:
                return instance.weaponTypeSprites[0];
            case WeaponType.bow:
                return instance.weaponTypeSprites[1];
            case WeaponType.daggers:
                return instance.weaponTypeSprites[2];
            default:
            case WeaponType.sword:
                return instance.weaponTypeSprites[3];
            case WeaponType.spear:
                return instance.weaponTypeSprites[4];
            case WeaponType.scythe:
                return instance.weaponTypeSprites[5];
        }
    }
}
