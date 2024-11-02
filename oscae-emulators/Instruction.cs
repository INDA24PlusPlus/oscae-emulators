using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace oscae_emulators
{
    class Instruction
    {
        public Int16 raw = 0;
        public Instruction(Int16 instruction)
        {
            raw = instruction;
        }

        public bool this[int i]
        {
            get
            {
                return ((raw >> i) & 1) == 1;
            }
            set
            {
                if (value)
                    raw |= (Int16)(1 << i);
                else
                    raw &= (Int16)~(1 << i);
            }
        }

        public JumpCondition GetJumpCondition()
        {
            return (raw & 0b0000_0000_0000_0111) switch
            {
                0b000 => JumpCondition.None,
                0b001 => JumpCondition.JGT,
                0b010 => JumpCondition.JEQ,
                0b011 => JumpCondition.JGE,
                0b100 => JumpCondition.JLT,
                0b101 => JumpCondition.JNE,
                0b110 => JumpCondition.JLE,
                0b111 => JumpCondition.JMP,
                _ => throw new Exception(), // this should not happen
            };
        }

        public bool GetABit()
        {
            return (raw & 0b1_0000_0000_0000) != 0;
        }

        public bool GetADest()
        {
            return (raw & 0b100000) != 0;
        }
        public bool GetDDest()
        {
            return (raw & 0b010000) != 0;
        }
        public bool GetMDest()
        {
            return (raw & 0b001000) != 0;
        }
    }
}
