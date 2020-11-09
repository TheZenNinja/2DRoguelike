using UnityEngine;
using System.Collections;
using ZenUtil;

namespace WeaponSystem
{
    public class BasicThrowingWeapon : BasicGun
    {
        public override void SwapAbility()
        {
            anim.SetTrigger("Swap");
        }
    }
}