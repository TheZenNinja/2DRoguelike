using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenUtil
{
    [Serializable]
    public class Counter
    {
        public int max;
        public int min;
        public int current;
        public bool atMax => current >= max;
        public bool atMin => current <= min;

        public Counter(int max) : this(max, 0, 0)
        { }
        public Counter(int max, int min) : this(max, min, min)
        { }
        public Counter(int max, int min, int current)
        {
            this.max = max;
            this.min = min;
            this.current = current;
        }
        public void SetToMax() => current = max;
        public void SetToMin() => current = min;

        public static implicit operator int(Counter c) => c.current;

        public static Counter operator -(Counter c, int i)
        {
            c.current -= i;
            if (c.current < c.min)
                c.current = c.min;
            return c;
        }
        public static Counter operator --(Counter c)
        {
            c.current -= 1;
            if (c.current < c.min)
                c.current = c.min;
            return c;
        }
        public static Counter operator +(Counter c, int i)
        {
            c.current += i;
            if (c.current > c.max)
                c.current = c.max;
            return c;
        }
        public static Counter operator ++(Counter c)
        {
            c.current += 1;
            if (c.current > c.max)
                c.current = c.max;
            return c;
        }
    }
}
