using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StanceManager : MonoBehaviour
{
    public enum CombatStance
    {
        Fire = 0,
        Water = 1,
        Air = 2,
        Arcane = 3,
    }
    public static StanceManager instance;
    public StanceManager() => instance = this;


    public int stance;
    //parry, throw
    public string[] names = { "Water", "Fire", "Air" };//, "Arcane"};

    public string currentStance => names[stance];

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            stance++;
            if (stance >= names.Length)
                stance = 0;
        }
    }
}
