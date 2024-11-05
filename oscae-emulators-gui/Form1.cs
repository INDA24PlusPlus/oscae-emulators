using System;
using oscae_emulators;

namespace oscae_emulators_gui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Memory rom = new Memory("..\\..\\..\\..\\rom.txt");

            foreach (var inst in rom.GetAll())
            {
                ListViewItem item = new ListViewItem(new[] { inst.Key.ToString(), Convert.ToString(inst.Value.Get(), 2).PadLeft(16, '0') } );
                listView1.Items.Add(item);
            }

            CPU cpu = new CPU(rom);
            cpu.Cycle();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 1)
            {
                MessageBox.Show("TODO: toggle hex");
            }
        }
    }
}
