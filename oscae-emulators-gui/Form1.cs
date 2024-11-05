using System;
using System.Runtime.Intrinsics.Arm;
using oscae_emulators;

namespace oscae_emulators_gui
{
    public partial class Form1 : Form
    {
        CPU cpu;


        public Form1()
        {
            InitializeComponent();

            Memory rom = new Memory("..\\..\\..\\..\\rom.txt");
            cpu = new CPU(rom);


            InitializeROMList(instructionDisplayType);
            if (listView1.Items.Count > 0)
                savedBackColor = listView1.Items[0].BackColor;

            cpu.ram.RegisterChanged += UpdateListRAM;
            cpu.PC.register.Changed += UpdatePC;
            cpu.A.Changed += UpdateA;
            cpu.D.Changed += UpdateD;

            timer1.Tick += Update;
            timer1.Enabled = false;
            timer1.Interval = 200;
        }

        private void Update(object sender, EventArgs e)
        {
            cpu.Cycle();
        }

        private SortedList<Int16, Int16> ramList = new SortedList<Int16, Int16>();
        private void UpdateListRAM(Int16 address)
        {
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

        Color savedBackColor;
        Color highlightColor = Color.Yellow;
        private void HighlightNextInstruction(Int16 previousPC)
        {

            // restore old
            listView1.Items[previousPC].BackColor = savedBackColor;


            // set new
            Int16 newPC = cpu.PC.Get();

            savedBackColor = listView1.Items[newPC].BackColor;
            listView1.Items[newPC].BackColor = highlightColor;

            if (checkBox1.Checked)
                listView1.Items[newPC].EnsureVisible();
        }

        private void UpdateA(Int16 prev)
        {
            textBox3.Text = cpu.A.Get().ToString();
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

        Instruction.DisplayType instructionDisplayType = Instruction.DisplayType.Binary;
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
                ListViewItem item = new ListViewItem(new[] { inst.Key.ToString(), Instruction.AsString(inst.Value.Get(), type)});
                listView1.Items.Add(item);
            }
        }

        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = listView2.HitTest(e.Location);
            ListViewItem item = info.Item;
            int subItemIndex = info.Item?.SubItems.IndexOf(info.SubItem) ?? -1;

            if (item != null && subItemIndex > 0)  // Only allow editing for subitems
            {
                Rectangle bounds = info.SubItem.Bounds;
                textBox1.SetBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);
                textBox1.Text = info.SubItem.Text;
                textBox1.Tag = new Tuple<ListViewItem, int>(item, subItemIndex);
                textBox1.Visible = true;
                textBox1.BringToFront();
                textBox1.Focus();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SaveEdit();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            SaveEdit();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveEdit();
                e.SuppressKeyPress = true;  // Prevent ding sound
            }
            else if (e.KeyCode == Keys.Escape)
            {
                textBox1.Visible = false;  // Cancel edit
            }
        }

        private void SaveEdit()
        {
            if (textBox1.Tag is Tuple<ListViewItem, int> info)
            {
                ListViewItem item = info.Item1;
                int subItemIndex = info.Item2;
                item.SubItems[subItemIndex].Text = textBox1.Text;
            }
            textBox1.Visible = false;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
            try
            {
                timer1.Interval = 1000 / Convert.ToInt32(textBox2.Text);
            }
            catch (Exception)
            {
                timer1.Interval = int.MaxValue;
            }
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
    }
}
