using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oscae_emulators
{
    abstract class Instruction
    {
        public static Instruction Create(Int16 instruction)
        {
            if ((instruction & 0b1000_0000_0000_0000) == 0)
            {
                // A instruction
                return new AInstruction(instruction);
            }
            else
            {
                // C instruction
                return new CInstruction(instruction);
            }
        }
    }
}
