using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponSystem
{
    public class DamageEvent
    {
        public int damageTaken;
        public bool killedEnemy;
        public Entity target;

        public DamageEvent(Entity target, int damageTaken, bool killedEnemy = false)
        {
            this.target = target;
            this.damageTaken = damageTaken;
            this.killedEnemy = killedEnemy;
        }
    }
}
