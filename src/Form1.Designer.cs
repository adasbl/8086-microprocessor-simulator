namespace _8086_microprocessor_simulator
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
            button_save_program = new Button();
            button_load_program = new Button();
            button_run_program = new Button();
            button_step_mode = new Button();
            label_AX = new Label();
            txtb_AX_reg = new TextBox();
            txtb_BX_reg = new TextBox();
            label_BX = new Label();
            txtb_CX_reg = new TextBox();
            label_CX = new Label();
            txtb_DX_reg = new TextBox();
            label_DX = new Label();
            textBox_curr_line = new TextBox();
            label_curr_line = new Label();
            program_display = new RichTextBox();
            button_clear_registers = new Button();
            outputConsoleTextBox = new RichTextBox();
            tabControl = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            interruptsTextBox = new RichTextBox();
            listBox1 = new ListBox();
            tabControl.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // button_save_program
            // 
            button_save_program.Location = new Point(918, 456);
            button_save_program.Margin = new Padding(4);
            button_save_program.Name = "button_save_program";
            button_save_program.Size = new Size(200, 75);
            button_save_program.TabIndex = 1;
            button_save_program.Text = "Zapisz";
            button_save_program.UseVisualStyleBackColor = true;
            button_save_program.Click += button_save_program_Click;
            // 
            // button_load_program
            // 
            button_load_program.Location = new Point(918, 561);
            button_load_program.Margin = new Padding(4);
            button_load_program.Name = "button_load_program";
            button_load_program.Size = new Size(200, 75);
            button_load_program.TabIndex = 2;
            button_load_program.Text = "Wczytaj";
            button_load_program.UseVisualStyleBackColor = true;
            button_load_program.Click += button_load_program_Click;
            // 
            // button_run_program
            // 
            button_run_program.Location = new Point(1151, 561);
            button_run_program.Margin = new Padding(4);
            button_run_program.Name = "button_run_program";
            button_run_program.Size = new Size(200, 75);
            button_run_program.TabIndex = 3;
            button_run_program.Text = "Uruchom program";
            button_run_program.UseVisualStyleBackColor = true;
            button_run_program.Click += button_run_program_Click;
            // 
            // button_step_mode
            // 
            button_step_mode.Location = new Point(1151, 456);
            button_step_mode.Margin = new Padding(4);
            button_step_mode.Name = "button_step_mode";
            button_step_mode.Size = new Size(200, 75);
            button_step_mode.TabIndex = 4;
            button_step_mode.Text = "Praca krokowa";
            button_step_mode.UseVisualStyleBackColor = true;
            button_step_mode.Click += button_step_mode_Click;
            // 
            // label_AX
            // 
            label_AX.AutoSize = true;
            label_AX.Font = new Font("Segoe UI", 15F);
            label_AX.Location = new Point(928, 30);
            label_AX.Margin = new Padding(4, 0, 4, 0);
            label_AX.Name = "label_AX";
            label_AX.Size = new Size(55, 41);
            label_AX.TabIndex = 5;
            label_AX.Text = "AX";
            // 
            // txtb_AX_reg
            // 
            txtb_AX_reg.Font = new Font("Segoe UI", 18F);
            txtb_AX_reg.Location = new Point(999, 21);
            txtb_AX_reg.Margin = new Padding(4);
            txtb_AX_reg.Name = "txtb_AX_reg";
            txtb_AX_reg.Size = new Size(352, 55);
            txtb_AX_reg.TabIndex = 6;
            txtb_AX_reg.Text = "0000000000000000";
            // 
            // txtb_BX_reg
            // 
            txtb_BX_reg.Font = new Font("Segoe UI", 18F);
            txtb_BX_reg.Location = new Point(999, 84);
            txtb_BX_reg.Margin = new Padding(4);
            txtb_BX_reg.Name = "txtb_BX_reg";
            txtb_BX_reg.Size = new Size(352, 55);
            txtb_BX_reg.TabIndex = 8;
            txtb_BX_reg.Text = "0000000000000000";
            // 
            // label_BX
            // 
            label_BX.AutoSize = true;
            label_BX.Font = new Font("Segoe UI", 15F);
            label_BX.Location = new Point(930, 93);
            label_BX.Margin = new Padding(4, 0, 4, 0);
            label_BX.Name = "label_BX";
            label_BX.Size = new Size(53, 41);
            label_BX.TabIndex = 7;
            label_BX.Text = "BX";
            // 
            // txtb_CX_reg
            // 
            txtb_CX_reg.Font = new Font("Segoe UI", 18F);
            txtb_CX_reg.Location = new Point(999, 147);
            txtb_CX_reg.Margin = new Padding(4);
            txtb_CX_reg.Name = "txtb_CX_reg";
            txtb_CX_reg.Size = new Size(352, 55);
            txtb_CX_reg.TabIndex = 10;
            txtb_CX_reg.Text = "0000000000000000";
            // 
            // label_CX
            // 
            label_CX.AutoSize = true;
            label_CX.Font = new Font("Segoe UI", 15F);
            label_CX.Location = new Point(930, 152);
            label_CX.Margin = new Padding(4, 0, 4, 0);
            label_CX.Name = "label_CX";
            label_CX.Size = new Size(55, 41);
            label_CX.TabIndex = 9;
            label_CX.Text = "CX";
            // 
            // txtb_DX_reg
            // 
            txtb_DX_reg.Font = new Font("Segoe UI", 18F);
            txtb_DX_reg.Location = new Point(999, 210);
            txtb_DX_reg.Margin = new Padding(4);
            txtb_DX_reg.Name = "txtb_DX_reg";
            txtb_DX_reg.Size = new Size(352, 55);
            txtb_DX_reg.TabIndex = 12;
            txtb_DX_reg.Text = "0000000000000000";
            // 
            // label_DX
            // 
            label_DX.AutoSize = true;
            label_DX.Font = new Font("Segoe UI", 15F);
            label_DX.Location = new Point(930, 210);
            label_DX.Margin = new Padding(4, 0, 4, 0);
            label_DX.Name = "label_DX";
            label_DX.Size = new Size(56, 41);
            label_DX.TabIndex = 11;
            label_DX.Text = "DX";
            // 
            // textBox_curr_line
            // 
            textBox_curr_line.Font = new Font("Segoe UI", 19F);
            textBox_curr_line.Location = new Point(1039, 323);
            textBox_curr_line.Margin = new Padding(4);
            textBox_curr_line.Name = "textBox_curr_line";
            textBox_curr_line.Size = new Size(60, 58);
            textBox_curr_line.TabIndex = 13;
            // 
            // label_curr_line
            // 
            label_curr_line.AutoSize = true;
            label_curr_line.Font = new Font("Segoe UI", 15F);
            label_curr_line.Location = new Point(946, 334);
            label_curr_line.Margin = new Padding(4, 0, 4, 0);
            label_curr_line.Name = "label_curr_line";
            label_curr_line.Size = new Size(85, 41);
            label_curr_line.TabIndex = 14;
            label_curr_line.Text = "Krok:";
            // 
            // program_display
            // 
            program_display.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            program_display.HideSelection = false;
            program_display.Location = new Point(4, 4);
            program_display.Margin = new Padding(4);
            program_display.Name = "program_display";
            program_display.Size = new Size(855, 458);
            program_display.TabIndex = 15;
            program_display.Text = "";
            // 
            // button_clear_registers
            // 
            button_clear_registers.Location = new Point(1182, 334);
            button_clear_registers.Margin = new Padding(4);
            button_clear_registers.Name = "button_clear_registers";
            button_clear_registers.Size = new Size(169, 44);
            button_clear_registers.TabIndex = 16;
            button_clear_registers.Text = "Wyczyść Rejestry";
            button_clear_registers.UseVisualStyleBackColor = true;
            button_clear_registers.Click += button_clear_registers_Click;
            // 
            // outputConsoleTextBox
            // 
            outputConsoleTextBox.BackColor = SystemColors.MenuText;
            outputConsoleTextBox.ForeColor = SystemColors.InactiveCaption;
            outputConsoleTextBox.Location = new Point(4, 478);
            outputConsoleTextBox.Name = "outputConsoleTextBox";
            outputConsoleTextBox.ReadOnly = true;
            outputConsoleTextBox.Size = new Size(855, 227);
            outputConsoleTextBox.TabIndex = 17;
            outputConsoleTextBox.Text = "";
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPage1);
            tabControl.Controls.Add(tabPage2);
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1438, 759);
            tabControl.TabIndex = 18;
            // 
            // tabPage1
            // 
            tabPage1.BorderStyle = BorderStyle.Fixed3D;
            tabPage1.Controls.Add(program_display);
            tabPage1.Controls.Add(outputConsoleTextBox);
            tabPage1.Controls.Add(label_AX);
            tabPage1.Controls.Add(button_step_mode);
            tabPage1.Controls.Add(button_clear_registers);
            tabPage1.Controls.Add(button_load_program);
            tabPage1.Controls.Add(button_run_program);
            tabPage1.Controls.Add(txtb_AX_reg);
            tabPage1.Controls.Add(label_curr_line);
            tabPage1.Controls.Add(button_save_program);
            tabPage1.Controls.Add(txtb_CX_reg);
            tabPage1.Controls.Add(textBox_curr_line);
            tabPage1.Controls.Add(txtb_BX_reg);
            tabPage1.Controls.Add(label_DX);
            tabPage1.Controls.Add(txtb_DX_reg);
            tabPage1.Controls.Add(label_CX);
            tabPage1.Controls.Add(label_BX);
            tabPage1.Location = new Point(4, 34);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1430, 721);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "SIMULATOR";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(interruptsTextBox);
            tabPage2.Controls.Add(listBox1);
            tabPage2.Location = new Point(4, 34);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1430, 721);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Interrupts instructions";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // interruptsTextBox
            // 
            interruptsTextBox.Location = new Point(493, 6);
            interruptsTextBox.Name = "interruptsTextBox";
            interruptsTextBox.ReadOnly = true;
            interruptsTextBox.Size = new Size(929, 700);
            interruptsTextBox.TabIndex = 1;
            interruptsTextBox.Text = "";
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(8, 2);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(479, 704);
            listBox1.TabIndex = 0;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1438, 758);
            Controls.Add(tabControl);
            Margin = new Padding(4);
            Name = "Form1";
            tabControl.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Button button_save_program;
        private Button button_load_program;
        private Button button_run_program;
        private Button button_step_mode;
        private Label label_AX;
        private TextBox txtb_AX_reg;
        private TextBox txtb_BX_reg;
        private Label label_BX;
        private TextBox txtb_CX_reg;
        private Label label_CX;
        private TextBox txtb_DX_reg;
        private Label label_DX;
        private TextBox textBox_curr_line;
        private Label label_curr_line;
        private RichTextBox program_display;
        private Button button_clear_registers;
        private RichTextBox outputConsoleTextBox;
        private TabControl tabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private RichTextBox interruptsTextBox;
        private ListBox listBox1;
    }
}
