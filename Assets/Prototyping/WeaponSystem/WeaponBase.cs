using UnityEngine;
using System.Collections;

namespace WeaponSystem
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [HideInInspector]
        public Animator anim;
        public AudioSource audioSource;
        public RuntimeAnimatorController controller;
        public abstract void HandleInput();
        public abstract string GetUIInfo();

        public virtual void Equip(Animator anim)
        {
            this.anim = anim;
            anim.runtimeAnimatorController = controller;
        }
        public abstract void Unequip();
    }
}
