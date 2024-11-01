using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace oscae_emulators
{
    internal class CPU
    {
        Memory ram = new Memory();
        Memory rom = new Memory();
        Register A = new Register(0);
        Register D = new Register(0);
        ProgramCounter PC = new ProgramCounter(0);

        void DoInstruction(Instruction instruction)
        {
            switch (instruction)
            {
                case AInstruction Ainst:
                    // set A register with address
                    A.Set(Ainst.address);
                    break;
                case CInstruction Cinst:
                    // execute C instruction
                    break;
            }
        }

        public void Cycle()
        {
            DoInstruction(
                Instruction.Create(
                    rom.Get(
                        PC.GetInc()
                    )
                )
            );
        }
    }
}
