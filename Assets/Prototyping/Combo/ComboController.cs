using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ZenClasses;
using TMPro;
using UnityEngine.UI;

public class ComboController : MonoBehaviour
{
    //public TextMeshProUGUI text;

    public Slider comboDecay;

    public Animator anim;

    [Space]
    public bool canAct;
    [SerializeField] private int maxCombo = 3;
    public int combo;

    public Timer comboTimer;

    private List<int> prevAttacks = new List<int>();

    private void Start()
    {
        canAct = true;
        comboTimer.onTimeEnd = ResetCombo;
    }

    void Update()
    {
        comboTimer.Update(Time.deltaTime);
        comboDecay.value = comboTimer.percent;
    }
    public void LightAttack()
    {
        if (canAct)
        HandleClick(0);
    }
    public void HeavyAttack()
    {
        if (canAct)
            HandleClick(1);
    }
    public void Parry()
    {
        if (canAct)
            Debug.Log("Parry");
    }
    public void HandleClick(int mouseID)
    {
        if (combo >= maxCombo)
            ResetCombo();

        comboTimer.Restart();
        if (!(combo == 1 && prevAttacks.Count == 1 && mouseID != prevAttacks[0]))
            combo++;
        anim.SetInteger("Combo", combo);
        switch (mouseID)
        {
            case 0:
            default:
                anim.SetTrigger("Light Attack");
                Debug.Log("Light " + combo);
                break;
            case 1:
                Debug.Log("Heavy " + combo);
                anim.SetTrigger("Heavy Attack");
                break;
        }
        prevAttacks.Add(mouseID);
    }
    public void ResetCombo()
    {
        Debug.Log("Combo End");
        comboTimer.Stop();
        combo = 0;
        prevAttacks.Clear();
    }
}
