using UnityEngine;
using System.Collections;
using ZenUtil;

namespace WeaponSystem
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [HideInInspector]
        public Animator anim;
        public AudioSource audioSource;
        public RuntimeAnimatorController controller;
        public Timer swapAbilityCooldown = new Timer(10);
        public virtual void Awake()
        {
            swapAbilityCooldown.AttachHookToObj(gameObject);
        }
        public abstract void HandleInput();
        public abstract string GetUIInfo();

        public virtual void Equip(Animator anim, bool suppressSwapEvent = false)
        {
            this.anim = anim;
            anim.runtimeAnimatorController = controller;

            if (!suppressSwapEvent)
            if (swapAbilityCooldown.finished)
            {
                SwapAbility();
                swapAbilityCooldown.Start();
            }

        }
        public abstract void SwapAbility();
        public abstract void Unequip();
    }
}
