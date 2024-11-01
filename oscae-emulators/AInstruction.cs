using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oscae_emulators
{
    internal class AInstruction : Instruction
    {
        public Int16 address = 0;
        public AInstruction(Int16 address)
        {
            this.address = address;
        }
    }
}
