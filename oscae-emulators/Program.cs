namespace oscae_emulators
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Memory rom = new Memory("..\\..\\..\\..\\rom.txt");


            CPU cpu = new CPU(rom);
            cpu.Cycle();
        }
    }
}
