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
        public Memory ram = new Memory();
        public Memory rom = new Memory();
        public Register A = new Register(0);
        public Register D = new Register(0);
        public ProgramCounter PC = new ProgramCounter(0);

        public CPU(Memory rom)
        {
            this.rom = rom;
        }

        void DoInstruction(Instruction instruction)
        {
            if (instruction[15] == false)
            {   // A instruction

                // set A register with address
                A.Set(instruction.raw);
            }
            else
            {   // C instruction

                // execute C instruction
                Int16 xInput = D.Get();
                Int16 yInput = A.Get();

                if (instruction.GetABit()) // use M instead of A
                    yInput = ram.Get(yInput);

                Int16 output = ALU(xInput, yInput, instruction);

                // jump
                switch(instruction.GetJumpCondition())
                {
                    case JumpCondition.JGT:
                        if (output > 0)
                            PC.Set(A.Get());
                        break;
                    case JumpCondition.JEQ:
                        if (output == 0)
                            PC.Set(A.Get());
                        break;
                    case JumpCondition.JGE:
                        if (output >= 0)
                            PC.Set(A.Get());
                        break;
                    case JumpCondition.JLT:
                        if (output < 0)
                            PC.Set(A.Get());
                        break;
                    case JumpCondition.JNE:
                        if (output != 0)
                            PC.Set(A.Get());
                        break;
                    case JumpCondition.JLE:
                        if (output <= 0)
                            PC.Set(A.Get());
                        break;
                    case JumpCondition.JMP:
                        PC.Set(A.Get());
                        break;
                }

                // set registers
                if (instruction.GetMDest())
                    ram.Set(A.Get(), output);

                if (instruction.GetADest())
                    A.Set(output);

                if (instruction.GetDDest())
                    D.Set(output);
            }
        }

        Int16 ALU(Int16 xInput, Int16 yInput, Instruction instruction)
        {
            Int16 output;

            // zx
            if (instruction[11])
                xInput = 0;

            // nx
            if (instruction[10])
                xInput = (Int16)~xInput;

            // zy
            if (instruction[9])
                yInput = 0;

            // ny
            if (instruction[8])
                yInput = (Int16)~yInput;

            // f
            if (instruction[7])
                output = (Int16)unchecked(xInput + yInput);
            else
                output = (Int16)(xInput & yInput);

            // no
            if (instruction[6])
                output = (Int16)~output;

            return output;
        }

        public void Cycle()
        {
            DoInstruction(
                new Instruction(
                    rom.Get(
                        PC.GetInc()
                    )
                )
            );
        }
    }
}
