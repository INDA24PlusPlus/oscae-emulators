using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace oscae_emulators
{
    public class Memory
    {
        Dictionary<Int16, Register> registers = new Dictionary<Int16, Register>();

        public event Action<Int16> RegisterChanged;

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
            RegisterChanged?.Invoke(address);
        }

        public Memory() { }
        public Memory(string path)
        {
            Int16 i = 0;
            foreach (string line in File.ReadAllLines(path))
            {
                Set(i, Int16.Parse(line.Trim(), System.Globalization.NumberStyles.BinaryNumber));
                i++;
            }
        }

        public ReadOnlyDictionary<Int16, Register> GetAll()
        {
            return new ReadOnlyDictionary<Int16, Register>(registers);
        }
    }
}
