using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ZenUtil;
using TMPro;
using UnityEngine.UI;

public class ComboController : MonoBehaviour
{
    public enum AttackType
    { 
        light= 0,
        special = 1,
        riser = 2,
        slam = 3,
        QTE = 4,
        stanceAbility = 5,
    }
    //public TextMeshProUGUI text;

    public CustomAnimatorController anim;

    [Space]
    public bool canAct;
    [SerializeField] private int maxCombo = 3;
    public int combo;

    public Timer comboTimer;


    private void Start()
    {
        canAct = true;
        comboTimer.AttachHookToObj(this);
        comboTimer.onTimeEnd = ResetCombo;
    }

    void Update()
    {
        //}HandleInput();
    }

    /// <summary>
    /// A testing/debugging method to independently control this script
    /// </summary>
    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            HandleInput(AttackType.light);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (Input.GetKey(KeyCode.W))
                                HandleInput(AttackType.riser);
            else if (Input.GetKey(KeyCode.S))
                                HandleInput(AttackType.slam);
                    else
                        HandleInput(AttackType.special);
        }
    }

    public void LightAttack()
    {
        if (canAct)
            HandleInput(AttackType.light);
    }
    public void HeavyAttack()
    {
        if (canAct)
        {
            if (Input.GetKey(KeyCode.W))
                HandleInput(AttackType.riser);
            else if (Input.GetKey(KeyCode.S))
                HandleInput(AttackType.slam);
            else
                HandleInput(AttackType.special);
        }
    }
    public void StanceAbility()
    {
        if (canAct)
            Debug.Log("Stance Ability");
    }
    public void HandleInput(AttackType input)
    {
        if (input == AttackType.light || input == AttackType.special)
        {
            combo++;
            comboTimer.Restart();
        }

        if (combo >= maxCombo)
            ResetCombo();

        switch (input)
        {

            default:
            case AttackType.light:
                anim.PlayLightAttack(combo);
                Debug.Log("Light " + combo);
                break;
            case AttackType.special:
                Debug.Log("Special " + combo);
                anim.PlaySpecialAttack(combo);
                break;
            case AttackType.riser:
                break;
            case AttackType.slam:
                break;
        }
    }
    public void ResetCombo()
    {
        Debug.Log("Combo End");
        comboTimer.Stop();
        combo = 0;
    }
}
