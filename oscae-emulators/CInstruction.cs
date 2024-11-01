using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oscae_emulators
{
    internal class CInstruction : Instruction
    {
        JumpCondition jumpCondition;

        public CInstruction(Int16 instruction)
        {
            
        }

        static JumpCondition ExtractJumpCondition(Int16 instruction)
        {
            return (instruction & 0b0000_0000_0000_0111) switch
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
    }
}
