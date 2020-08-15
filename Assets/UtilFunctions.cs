using System;
using System.Collections.Generic;


namespace ZenClasses
{
    public static class UtilFunctions
    {
        public static float Remap(float oldMin, float oldMax, float oldVal, float newMin, float newMax)
        {
            float oldRange = oldMax - oldMin;
            float newRange = newMax - newMin;
            return ((oldVal - oldMin) * newRange) / oldRange + newMin;
        }
        public static float Remap01(float value, float newMin, float newMax)
        {
            return Remap(0, 1, value, newMin, newMax);
        }
    }
}
