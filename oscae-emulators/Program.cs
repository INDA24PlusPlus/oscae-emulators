namespace oscae_emulators
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Memory rom = new Memory();
            Int16 i = 0;
            foreach (string line in File.ReadAllLines("rom.txt"))
            {
                rom.Set(i, Int16.Parse(line.Trim(), System.Globalization.NumberStyles.BinaryNumber));
                i++;
            }



            CPU cpu = new CPU(rom);
            cpu.Cycle();
        }
    }
}
