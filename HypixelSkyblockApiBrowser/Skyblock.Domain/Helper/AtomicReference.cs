using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyblock.Common.Helper
{
    public class AtomicReference<T> where T : class, new()
    {
        private readonly object locker = new();
        private T reference;

        public AtomicReference()
        {
            reference = new();
        }

        public T Get()
        {
            lock (locker)
            {
                return reference;
            }
        }
        public void Set(T value)
        {
            lock (locker)
            {
                reference = value;
            }
        }
    }
}
