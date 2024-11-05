using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oscae_emulators
{
    public class Register
    {
        Int16 value = 0;
        public Register(Int16 value)
        {
            this.value = value;
        }

        public Int16 Get()
        {
            return value;
        }



        public event Action<Int16> Changed;
        public void Set(Int16 value)
        {
            Int16 prev = this.value;
            this.value = value;
            Changed?.Invoke(prev);
        }
    }
}
