using UnityEngine;
using System.Collections;
using ZenUtil;
using System.Collections.Generic;
using UnityEngine.Events;

namespace WeaponSystem
{
    public abstract class WeaponBase : MonoBehaviour
    {
        public Vector3 holsterPos, holsterRot;
        public List<UnityEvent> events;
        [HideInInspector] public Animator anim;
        public AudioSource audioSource;
        public RuntimeAnimatorController controller;
        public ConditionalEvent swapConditional;
        public Timer swapAbilityCooldown = new Timer(10);
        public virtual void Awake()
        {
            swapAbilityCooldown.AttachHookToObj(gameObject);
        }
        public abstract void HandleInput();
        public abstract string GetUIInfo();

        public virtual void Equip(Transform root, Animator anim, bool swapEvent = false)
        {
            transform.SetParent(root);
            ResetTransforms();
            CursorControl.SetLookAtCursor(true);
            this.anim = anim;
            anim.runtimeAnimatorController = controller;

            if (swapEvent && swapAbilityCooldown.finished)
            {
                SwapAbility();
                swapAbilityCooldown.Restart();
            }

        }
        public abstract void SwapAbility();
        public virtual void Unequip(Transform root)
        {
            transform.SetParent(root);
            transform.localPosition = holsterPos;
            transform.localEulerAngles = holsterRot;
            transform.localScale = Vector3.one;
        }
        protected void ResetTransforms()
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = new Quaternion(0, 0, 0, 0);
            transform.localScale = Vector3.one;
        }
    }
}
