using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ZenUtil;

[Serializable]
public class Health
{
    private enum HealthType
    {
        none, armor, shield
    }
    public Counter hpData;

}