using System;
using System.Runtime.Intrinsics.Arm;
using Microsoft.VisualBasic;
using oscae_emulators;

namespace oscae_emulators_gui
{
    public partial class Form1 : Form
    {
        CPU cpu = new CPU(new Memory());

        Instruction.DisplayType instructionDisplayType = Instruction.DisplayType.Binary;

        public Form1()
        {
            InitializeComponent();

            Init("..\\..\\..\\..\\Test.hack");
        }

        void Init(string path)
        {
            listView2.Items.Clear();
            ramList.Clear();
            Memory rom = new Memory(path);
            cpu = new CPU(rom);

            InitializeROMList(instructionDisplayType);

            cpu.ram.RegisterChanged += UpdateListRAM;
            cpu.PC.register.Changed += UpdatePC;
            cpu.A.Changed += UpdateA;
            cpu.D.Changed += UpdateD;

            timer1.Tick += Update;
            timer1.Enabled = false;
            timer1.Interval = 10;
            textBox2.Text = "100";

            InitScreen();
        }

        double cyclesRest = 0;
        double cyclesPerTick = 1;
        private void Update(object sender, EventArgs e)
        {
            cyclesRest += cyclesPerTick;
            if (cyclesRest > 100)
                cyclesRest = 100; // cap to not cause stop in program
            while (cyclesRest > 0)
            {
                cyclesRest -= 1;
                cpu.Cycle();
            }
        }

        private SortedList<Int16, Int16> ramList = new SortedList<Int16, Int16>();
        private void UpdateListRAM(Int16 address)
        {
            UpdateScreen(address);

            textBox6.Text = cpu.ram.Get(cpu.A.Get()).ToString();

            string addressStr = address.ToString();

            Int16 value = cpu.ram.Get(address);

            if (ramList.TryAdd(address, value))
            {
                listView2.Items.Insert(ramList.IndexOfKey(address), new ListViewItem(new[] { addressStr, value.ToString() }));
            }
            else
            {
                ramList[address] = value;
                listView2.Items.Cast<ListViewItem>().FirstOrDefault(i => i.Text == addressStr).SubItems[1].Text = cpu.ram.Get(address).ToString();
            }

        }

        Color savedBackColor = Color.White;
        Color highlightColor = Color.Yellow;
        private void HighlightNextInstruction(Int16 previousPC)
        {
            int prev = unchecked((ushort)previousPC);
            // restore old
            if (prev < listView1.Items.Count)
                listView1.Items[prev].BackColor = savedBackColor;


            // set new
            int newPC = unchecked((ushort)cpu.PC.Get());
            if (newPC < listView1.Items.Count)
            {
                // save
                var currentInst = listView1.Items[newPC];

                savedBackColor = currentInst.BackColor;
                currentInst.BackColor = highlightColor;

                if (checkBox1.Checked)
                    currentInst.EnsureVisible();
            }
        }

        private void UpdateA(Int16 prev)
        {
            textBox3.Text = cpu.A.Get().ToString();
            textBox6.Text = cpu.ram.Get(cpu.A.Get()).ToString();
        }
        private void UpdateD(Int16 prev)
        {
            textBox4.Text = cpu.D.Get().ToString();
        }
        private void UpdatePC(Int16 prev)
        {
            HighlightNextInstruction(prev);
            textBox5.Text = cpu.PC.Get().ToString();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 1)
            {
                // change display of ROM
                switch (instructionDisplayType)
                {
                    case Instruction.DisplayType.Binary:
                        instructionDisplayType = Instruction.DisplayType.Hex;
                        break;
                    case Instruction.DisplayType.Hex:
                        instructionDisplayType = Instruction.DisplayType.Assembly;
                        break;
                    case Instruction.DisplayType.Assembly:
                        instructionDisplayType = Instruction.DisplayType.Binary;
                        break;
                }

                InitializeROMList(instructionDisplayType);
            }
        }

        void InitializeROMList(Instruction.DisplayType type)
        {
            listView1.Items.Clear();
            foreach (var inst in cpu.rom.GetAll())
            {
                ListViewItem item = new ListViewItem(new[] { inst.Key.ToString(), Instruction.AsString(inst.Value.Get(), type) });
                listView1.Items.Add(item);
            }
            HighlightNextInstruction(-1);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            try
            {
                // interval is always 10 ms
                int hz = Convert.ToInt32(textBox2.Text);
                cyclesPerTick = hz / 100;
                if (cyclesPerTick > 100)
                {
                    timer1.Interval = 1;
                    cyclesPerTick /= 10;
                }
                else
                {
                    timer1.Interval = 10;
                }
            }
            catch (Exception) { }
        }

        private void button1_Click(object sender, EventArgs e) // Start
        {
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e) // Stop
        {
            timer1.Stop();
        }

        private void button3_Click(object sender, EventArgs e) // Step
        {
            cpu.Cycle();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e) // A register
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
            try
            {
                cpu.A.Set(Convert.ToInt16(textBox3.Text));
            }
            catch (Exception)
            {

            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e) // D register
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
            try
            {
                cpu.D.Set(Convert.ToInt16(textBox4.Text));
            }
            catch (Exception)
            {

            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e) // PC
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cpu.A.Set(Convert.ToInt16(textBox3.Text));
            }
            catch (Exception) { }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cpu.D.Set(Convert.ToInt16(textBox4.Text));
            }
            catch (Exception) { }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cpu.PC.Set(Convert.ToInt16(textBox5.Text));
            }
            catch (Exception) { }
        }

        // screen
        private Bitmap screenBitmap;
        private int pixelWidth = 512;
        private int pixelHeight = 256;

        void InitScreen()
        {
            screenBitmap = new Bitmap(pixelWidth, pixelHeight);
            pictureBox1.Image = screenBitmap;
        }

        void UpdateScreen(Int16 address)
        {
            if (address < 0x4000 || address > 0x5FFF)
                return;

            int pixel0 = address - 0x4000;
            int pixelX = (pixel0 % 32) * 16;
            int pixelY = pixel0 / 32;
            Int16 pixels = cpu.ram.Get(address);
            for (int i = 0; i < 16; i++)
                screenBitmap.SetPixel(pixelX + i, pixelY, ((pixels >> i) & 1) == 1 ? Color.Black : Color.White);
            pictureBox1.Refresh();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            cpu.ram.Set(0x6000, Convert.ToInt16(e.KeyValue));
            label5.Text = "Keyboard: [" + (char)e.KeyValue + "]";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.CurrentDirectory + "\\..\\..\\..\\..\\";
                //openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                //openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;


                    Init(selectedFilePath);
                }
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            pictureBox1.Parent.Focus();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (cpu.ram.Get(0x6000) == Convert.ToInt16(e.KeyValue))
            {
                cpu.ram.Set(0x6000, 0);
                label5.Text = "Keyboard: []";
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e) // RAM[A]
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cpu.ram.Set(cpu.A.Get(), Convert.ToInt16(textBox6.Text));
            }
            catch (Exception) { }
        }
    }
}
