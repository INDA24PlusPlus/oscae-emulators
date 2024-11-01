using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oscae_emulators
{
    internal class Memory
    {
        Dictionary<Int16, Register> registers = new Dictionary<Int16, Register>();

        public Int16 Get(Int16 address)
        {
            if (registers.TryGetValue(address, out Register? register))
                return register.Get();

            return 0;
        }

        public void Set(Int16 address, Int16 value)
        {
            if (registers.TryGetValue(address, out Register? register))
            {
                register.Set(value);
            }
            else
            {
                registers.Add(address, new Register(value));
            }
        }
    }
}
