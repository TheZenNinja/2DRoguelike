using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class WeaponData
{
     public string GetDamageData()
    {
        //show damage and crit like in dead cells
        return "Damage: 5-10 (20-30)";
    }
    public string GetDescription()
    {
        //explain how the crit works if it has one
        throw new NotImplementedException();
    }
}