using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pooling
{
    public class ObjectPool
    {
        protected Queue<object> queue;
        public T SpawnObjectFromPool<T>()
        {
            throw new NotImplementedException();
        }
        public object ReturnObjectToPool(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
