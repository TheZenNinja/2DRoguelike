using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponSystem
{
    public class DamageEvent
    {
        Damage damage;
        Entity sender, receiver;

        public DamageEvent(Damage damage, Entity sender, Entity receiver)
        {
            this.damage = damage;
            this.sender = sender;
            this.receiver = receiver;
        }
        //in the case of environmental dmg
        public DamageEvent(Damage damage, Entity receiver)
        {
            this.damage = damage;
            this.sender = null;
            this.receiver = receiver;
        }
    }
}
