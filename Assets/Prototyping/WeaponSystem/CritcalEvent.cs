using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WeaponSystem
{
    public class CritcalEvent
    {
        private enum WhatEntity
        {
            player,
            enemy,
        }
        private enum EventRequirement
        {
            isAirborne,
            afflictedWith,
        }

        [SerializeField] WhatEntity whatEntity;
        [SerializeField] EventRequirement requirement;
        [SerializeField] StatusType status;

        public bool isCritical(StandardEntity player, StandardEntity enemy = null)
        {
            if (whatEntity == WhatEntity.enemy)
            {
                if (enemy == null)
                    return false;
                else
                {
                    switch (requirement)
                    {
                        case EventRequirement.isAirborne:
                            return false;
                        case EventRequirement.afflictedWith:
                            // if entity has the required status effect
                            //true
                            //else
                            return false;
                        default:
                            return false;
                    }
                }
            }
            else
            {
                switch (requirement)
                {
                    case EventRequirement.isAirborne:
                        return false;
                    case EventRequirement.afflictedWith:
                        // if entity has the required status effect
                        //true
                        //else
                        return false;
                    default:
                        return false;
                }
            }
        }
    }
}
