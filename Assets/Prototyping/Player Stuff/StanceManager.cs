using Assets.Prototyping.WeaponSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StanceManager : MonoBehaviour
{
    public enum CombatStance
    {
        //special
        Fire = 0,
        //grapple
        Water = 1,
        //parry
        Earth = 2,
        //throw
        Air = 3,
        //elemental attack
        Arcane = 4,
    }
    public static StanceManager instance;
    public StanceManager() => instance = this;


    public int stance;

    public string currentStance => ((CombatStance)stance).ToString();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            stance++;
            if (stance >= 4)
                stance = 0;
        }
        if (Input.GetKeyDown(KeyCode.B))
            UseStanceAbility();
    }
    public void UseStanceAbility()
    {
        switch ((CombatStance)stance)
        {
            case CombatStance.Fire:
                break;
            case CombatStance.Water:
                break;
            case CombatStance.Earth:
                break;
            case CombatStance.Air:
                EquipmentManager.instance.ThrowItem();
                break;
            case CombatStance.Arcane:
                break;
            default:
                return;
        }
    }
}
