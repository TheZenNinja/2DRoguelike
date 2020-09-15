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
}
