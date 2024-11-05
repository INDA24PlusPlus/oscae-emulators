namespace oscae_emulators
{
    public class ProgramCounter
    {
        Register register;

        public ProgramCounter(Int16 value)
        {
            register = new Register(0);
        }

        // returns current value than increments
        public Int16 GetInc()
        {
            Int16 ret = register.Get();
            register.Set(unchecked((Int16)(ret + 1)));
            return ret;
        }

        public void Set(Int16 value)
        {
            register.Set(value);
        }

        public void Reset()
        {
            register.Set(0);
        }
    }
}