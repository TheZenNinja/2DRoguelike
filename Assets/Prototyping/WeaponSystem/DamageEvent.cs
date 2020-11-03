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
        public EntityBase target;

        public DamageEvent(EntityBase target, int damageTaken, bool killedEnemy = false)
        {
            this.target = target;
            this.damageTaken = damageTaken;
            this.killedEnemy = killedEnemy;
        }
    }
}
