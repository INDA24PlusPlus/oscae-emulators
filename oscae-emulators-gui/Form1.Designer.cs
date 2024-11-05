namespace oscae_emulators_gui
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            listView1 = new ListView();
            numbers = new ColumnHeader();
            instructions = new ColumnHeader();
            listView2 = new ListView();
            number = new ColumnHeader();
            register = new ColumnHeader();
            textBox1 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            textBox2 = new TextBox();
            timer1 = new System.Windows.Forms.Timer(components);
            textBox3 = new TextBox();
            label1 = new Label();
            textBox4 = new TextBox();
            textBox5 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            checkBox1 = new CheckBox();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { numbers, instructions });
            listView1.Dock = DockStyle.Left;
            listView1.Location = new Point(0, 0);
            listView1.Name = "listView1";
            listView1.Size = new Size(198, 458);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.ColumnClick += listView1_ColumnClick;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // numbers
            // 
            numbers.Text = "#";
            numbers.Width = 50;
            // 
            // instructions
            // 
            instructions.Text = "Instructions";
            instructions.Width = 140;
            // 
            // listView2
            // 
            listView2.Columns.AddRange(new ColumnHeader[] { number, register });
            listView2.Dock = DockStyle.Left;
            listView2.Location = new Point(198, 0);
            listView2.Name = "listView2";
            listView2.Size = new Size(198, 458);
            listView2.TabIndex = 1;
            listView2.UseCompatibleStateImageBehavior = false;
            listView2.View = View.Details;
            listView2.MouseDoubleClick += listView2_MouseDoubleClick;
            // 
            // number
            // 
            number.Text = "#";
            number.Width = 50;
            // 
            // register
            // 
            register.Text = "RAM";
            register.Width = 140;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(676, 423);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 2;
            textBox1.Visible = false;
            textBox1.TextChanged += textBox1_TextChanged;
            textBox1.KeyDown += textBox1_KeyDown;
            textBox1.Leave += textBox1_Leave;
            // 
            // button1
            // 
            button1.Location = new Point(402, 12);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 3;
            button1.Text = "Start";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(483, 12);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 4;
            button2.Text = "Stop";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(564, 12);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 5;
            button3.Text = "Step";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(705, 12);
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "Clock Hz";
            textBox2.Size = new Size(83, 23);
            textBox2.TabIndex = 6;
            textBox2.Text = "5";
            textBox2.TextAlign = HorizontalAlignment.Right;
            textBox2.KeyPress += textBox2_KeyPress;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(483, 320);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(100, 23);
            textBox3.TabIndex = 7;
            textBox3.Text = "0";
            textBox3.TextChanged += textBox3_TextChanged;
            textBox3.KeyPress += textBox3_KeyPress;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(645, 16);
            label1.Name = "label1";
            label1.Size = new Size(54, 15);
            label1.TabIndex = 8;
            label1.Text = "Clock Hz";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(483, 349);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(100, 23);
            textBox4.TabIndex = 9;
            textBox4.Text = "0";
            textBox4.TextChanged += textBox4_TextChanged;
            textBox4.KeyPress += textBox4_KeyPress;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(483, 378);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(100, 23);
            textBox5.TabIndex = 10;
            textBox5.Text = "0";
            textBox5.TextChanged += textBox5_TextChanged;
            textBox5.KeyPress += textBox5_KeyPress;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(420, 323);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 11;
            label2.Text = "A register";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(420, 349);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 12;
            label3.Text = "D register";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(420, 378);
            label4.Name = "label4";
            label4.Size = new Size(22, 15);
            label4.TabIndex = 13;
            label4.Text = "PC";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Checked = true;
            checkBox1.CheckState = CheckState.Checked;
            checkBox1.Location = new Point(483, 407);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(79, 19);
            checkBox1.TabIndex = 15;
            checkBox1.Text = "Follow PC";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 458);
            Controls.Add(checkBox1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox5);
            Controls.Add(textBox4);
            Controls.Add(label1);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(listView2);
            Controls.Add(listView1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView listView1;
        private ColumnHeader numbers;
        private ColumnHeader instructions;
        private ListView listView2;
        private ColumnHeader number;
        private ColumnHeader register;
        private TextBox textBox1;
        private Button button1;
        private Button button2;
        private Button button3;
        private TextBox textBox2;
        private System.Windows.Forms.Timer timer1;
        private TextBox textBox3;
        private Label label1;
        private TextBox textBox4;
        private TextBox textBox5;
        private Label label2;
        private Label label3;
        private Label label4;
        private CheckBox checkBox1;
    }
}
