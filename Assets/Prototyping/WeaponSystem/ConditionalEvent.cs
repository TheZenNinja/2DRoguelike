using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalEvent
{
    public enum TestEntityType
    { 
        none,
        any,
        player,
        enemy,
        ally
    }

    //if x happens to to entity y then run z
    public bool Test(StandardEntity host, StandardEntity target)
    {
        return false;
    }
}
