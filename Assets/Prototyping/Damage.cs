using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum DamageType
{
    none,       //bullets/mana
    impact,     //hammers
    peircing,   //spears
    slash,      //swords
    shatter,    //pickaxe
}

public class Damage
{
    public int damage;
    public Element element;
    public DamageType type;

    public static implicit operator int(Damage d) => d.damage;
    public static implicit operator Element(Damage d) => d.element;
    public static implicit operator DamageType(Damage d) => d.type;
}
