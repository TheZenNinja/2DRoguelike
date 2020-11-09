using UnityEngine;
using System.Collections;
using WeaponSystem;

namespace Assets.Prototyping.WeaponSystem
{
    public class ThrownItem : MonoBehaviour, IInteractable
    {
        //take out dependency on projectile script
        //add in bouncing back to the player after hitting an enemy
        //make a dropped item class
        public PrototypeWeaponItem item;

        public void Interact(EntityBase interactingEntity = null)
        {
            if (!interactingEntity)
                return;

            if (interactingEntity is PlayerControl)
            {
                EquipmentManager.instance.EquipWeapon(item);
                Destroy(gameObject);
            }
        }
        public static ThrownItem Spawn(PrototypeWeaponItem item, Vector2 position, Vector2 vel, bool stickInTarget = false, int pierce = 0, float gravity = 5, bool isEnemy = false, float rotationOffset = 0, float glow = 0)
        {
            ProjectileScript p = ProjectileScript.Spawn(position, item.sprite, vel, stickInTarget, pierce, gravity, -1, false, rotationOffset, glow);
            ThrownItem i = p.gameObject.AddComponent<ThrownItem>();
            i.item = item;
            Debug.LogError("Test");
            return i;
        }
    }
}