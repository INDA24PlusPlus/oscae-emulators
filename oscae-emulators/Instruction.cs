using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace oscae_emulators
{
    public class Instruction
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

        public static string AsString(Int16 inst, DisplayType type)
        {
            switch (type)
            {
                case DisplayType.Hex:
                    return Convert.ToString(inst, 16).ToUpper().PadLeft(4, '0');
                case DisplayType.Assembly:
                    Instruction instr = new Instruction(inst);
                    if (instr[15]) // c inst
                    {
                        string ass = "";
                        if (instr.GetADest())
                            ass += "A";
                        if (instr.GetMDest())
                            ass += "M";
                        if (instr.GetDDest())
                            ass += "D";
                        if (ass.Length > 0)
                            ass += "=";
                        ass += instr.DissassembleALU();
                        ass += instr.GetJumpCondition() switch
                        {
                            JumpCondition.None => "",
                            JumpCondition.JGT => ";JGT",
                            JumpCondition.JEQ => ";JEQ",
                            JumpCondition.JGE => ";JGE",
                            JumpCondition.JLT => ";JLT",
                            JumpCondition.JNE => ";JNE",
                            JumpCondition.JLE => ";JLE",
                            JumpCondition.JMP => ";JMP",
                            _ => "",
                        };
                        return ass;
                    }
                    else // a inst
                    {
                        return "@" + inst;
                    }

                default:
                    return Convert.ToString(inst, 2).PadLeft(16, '0');
            }
        }

        string DissassembleALU()
        {
            string y = "A";
            if (GetABit())
                y = "M";

            switch ((raw >> 6) & 0b111111)
            {
                case 0b101010: return "0";
                case 0b111111: return "1";
                case 0b111010: return "-1";
                case 0b001100: return "D";
                case 0b110000: return y;
                case 0b001101: return "!D";
                case 0b110001: return "!" + y;
                case 0b001111: return "-D";
                case 0b110011: return "-" + y;
                case 0b011111: return "D+1";
                case 0b110111: return y + "+1";
                case 0b001110: return "D-1";
                case 0b110010: return y + "-1";
                case 0b000010: return "D+" + y;
                case 0b010011: return "D-" + y;
                case 0b000111: return y + "-D";
                case 0b000000: return "D&" + y;
                case 0b010101: return "D|" + y;
                default: return "???";
            }
        }

        public enum DisplayType
        {
            Binary,
            Hex,
            Assembly
        }
    }
}
