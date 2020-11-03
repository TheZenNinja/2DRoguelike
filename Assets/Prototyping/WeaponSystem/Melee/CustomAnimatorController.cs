using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private const int combatAnimLayer = 0;


    public void PlayLightAttack(int combo, StanceManager.CombatStance stance = StanceManager.CombatStance.Fire)
    {
        string clipName = "Light ";
        switch (stance)
        {
            default:
            case StanceManager.CombatStance.Fire:
                clipName += "F ";
                break;
            case StanceManager.CombatStance.Water:
                clipName += "W ";
                break;
            case StanceManager.CombatStance.Air:
                clipName += "A ";
                break;
            case StanceManager.CombatStance.Arcane:
                clipName += "M ";
                break;
        }
        clipName += combo.ToString();

        anim.Play(clipName, combatAnimLayer);
    }
    public void PlaySpecialAttack(int combo)
    {
        string clipName = "Light " + combo.ToString();
        anim.Play(clipName, combatAnimLayer);
    }
    public void PlayCombatAnim(string clipName, int combo)
    { 
        anim.Play(clipName + combo.ToString(),0);
    }
    public void PlayAnim(string clipName)
    {
        anim.Play(clipName,0);
    }
}
