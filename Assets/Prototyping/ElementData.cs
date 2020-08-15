using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ElementData
{
    private static readonly float badMulti = 0.8f; 
    private static readonly float goodMulti = 1.6f;
    /// <summary>
    /// Damage of type "damage" hitting health or armor of type "health"
    /// </summary>
    /// <param name="health"></param>
    /// <param name="damage"></param>
    /// <returns></returns>
    public static float GetElementDamageMulti(Element health, Element damage)
    {
        int val = CompareElements(damage, health);
        if (val == 1)
            return goodMulti;
        if (val == -1)
            return badMulti;
        else
            return 1;
    }

    private static int CompareElements(Element x, Element y)
    {
        switch (x)
        {
            case Element.fire:
                if (y == Element.water)
                    return 1;
                else if (y == Element.fire)
                    return -1;
                break;
            case Element.water:
                if (y == Element.fire)
                    return 1;
                else if (y == Element.water)
                    return -1;
                break;

            case Element.earth:
                if (y == Element.air)
                    return 1;
                else if (y == Element.earth)
                    return -1;
                break;
            case Element.air:
                if (y == Element.earth)
                    return 1;
                else if (y == Element.air)
                    return -1;
                break;

            case Element.arcane:
                if (y == Element.lightning)
                    return 1;
                else if (y == Element.arcane)
                    return -1;
                break;
            case Element.lightning:
                if (y == Element.arcane)
                    return 1;
                else if (y == Element.lightning)
                    return -1;
                break;

            case Element.none:
            default:
                break;
        } 
        return 0;
    }
}