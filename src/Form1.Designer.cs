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
            program_display = new RichTextBox();
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
            SuspendLayout();
            // 
            // program_display
            // 
            program_display.Font = new Font("Segoe UI", 15F);
            program_display.Location = new Point(12, 12);
            program_display.Name = "program_display";
            program_display.Size = new Size(685, 455);
            program_display.TabIndex = 0;
            program_display.Text = "";
            // 
            // button_save_program
            // 
            button_save_program.Location = new Point(12, 473);
            button_save_program.Name = "button_save_program";
            button_save_program.Size = new Size(160, 60);
            button_save_program.TabIndex = 1;
            button_save_program.Text = "Zapisz";
            button_save_program.UseVisualStyleBackColor = true;
            button_save_program.Click += button_save_program_Click;
            // 
            // button_load_program
            // 
            button_load_program.Location = new Point(178, 473);
            button_load_program.Name = "button_load_program";
            button_load_program.Size = new Size(160, 60);
            button_load_program.TabIndex = 2;
            button_load_program.Text = "Wczytaj";
            button_load_program.UseVisualStyleBackColor = true;
            button_load_program.Click += button_load_program_Click;
            // 
            // button_run_program
            // 
            button_run_program.Location = new Point(344, 473);
            button_run_program.Name = "button_run_program";
            button_run_program.Size = new Size(160, 60);
            button_run_program.TabIndex = 3;
            button_run_program.Text = "Uruchom program";
            button_run_program.UseVisualStyleBackColor = true;
            button_run_program.Click += button_run_program_Click;
            // 
            // button_step_mode
            // 
            button_step_mode.Location = new Point(510, 473);
            button_step_mode.Name = "button_step_mode";
            button_step_mode.Size = new Size(160, 60);
            button_step_mode.TabIndex = 4;
            button_step_mode.Text = "Praca krokowa";
            button_step_mode.UseVisualStyleBackColor = true;
            button_step_mode.Click += button_step_mode_Click;
            // 
            // label_AX
            // 
            label_AX.AutoSize = true;
            label_AX.Font = new Font("Segoe UI", 15F);
            label_AX.Location = new Point(744, 57);
            label_AX.Name = "label_AX";
            label_AX.Size = new Size(46, 35);
            label_AX.TabIndex = 5;
            label_AX.Text = "AX";
            // 
            // txtb_AX_reg
            // 
            txtb_AX_reg.Font = new Font("Segoe UI", 18F);
            txtb_AX_reg.Location = new Point(816, 49);
            txtb_AX_reg.Name = "txtb_AX_reg";
            txtb_AX_reg.Size = new Size(282, 47);
            txtb_AX_reg.TabIndex = 6;
            txtb_AX_reg.Text = "0000000000000000";
            // 
            // txtb_BX_reg
            // 
            txtb_BX_reg.Font = new Font("Segoe UI", 18F);
            txtb_BX_reg.Location = new Point(816, 102);
            txtb_BX_reg.Name = "txtb_BX_reg";
            txtb_BX_reg.Size = new Size(282, 47);
            txtb_BX_reg.TabIndex = 8;
            txtb_BX_reg.Text = "0000000000000000";
            // 
            // label_BX
            // 
            label_BX.AutoSize = true;
            label_BX.Font = new Font("Segoe UI", 15F);
            label_BX.Location = new Point(744, 102);
            label_BX.Name = "label_BX";
            label_BX.Size = new Size(44, 35);
            label_BX.TabIndex = 7;
            label_BX.Text = "BX";
            // 
            // txtb_CX_reg
            // 
            txtb_CX_reg.Font = new Font("Segoe UI", 18F);
            txtb_CX_reg.Location = new Point(816, 155);
            txtb_CX_reg.Name = "txtb_CX_reg";
            txtb_CX_reg.Size = new Size(282, 47);
            txtb_CX_reg.TabIndex = 10;
            txtb_CX_reg.Text = "0000000000000000";
            // 
            // label_CX
            // 
            label_CX.AutoSize = true;
            label_CX.Font = new Font("Segoe UI", 15F);
            label_CX.Location = new Point(745, 155);
            label_CX.Name = "label_CX";
            label_CX.Size = new Size(45, 35);
            label_CX.TabIndex = 9;
            label_CX.Text = "CX";
            // 
            // txtb_DX_reg
            // 
            txtb_DX_reg.Font = new Font("Segoe UI", 18F);
            txtb_DX_reg.Location = new Point(816, 208);
            txtb_DX_reg.Name = "txtb_DX_reg";
            txtb_DX_reg.Size = new Size(282, 47);
            txtb_DX_reg.TabIndex = 12;
            txtb_DX_reg.Text = "0000000000000000";
            // 
            // label_DX
            // 
            label_DX.AutoSize = true;
            label_DX.Font = new Font("Segoe UI", 15F);
            label_DX.Location = new Point(745, 208);
            label_DX.Name = "label_DX";
            label_DX.Size = new Size(47, 35);
            label_DX.TabIndex = 11;
            label_DX.Text = "DX";
            // 
            // textBox_curr_line
            // 
            textBox_curr_line.Font = new Font("Segoe UI", 19F);
            textBox_curr_line.Location = new Point(912, 354);
            textBox_curr_line.Name = "textBox_curr_line";
            textBox_curr_line.Size = new Size(49, 50);
            textBox_curr_line.TabIndex = 13;
            // 
            // label_curr_line
            // 
            label_curr_line.AutoSize = true;
            label_curr_line.Font = new Font("Segoe UI", 15F);
            label_curr_line.Location = new Point(825, 365);
            label_curr_line.Name = "label_curr_line";
            label_curr_line.Size = new Size(71, 35);
            label_curr_line.TabIndex = 14;
            label_curr_line.Text = "Krok:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1150, 585);
            Controls.Add(label_curr_line);
            Controls.Add(textBox_curr_line);
            Controls.Add(txtb_DX_reg);
            Controls.Add(label_DX);
            Controls.Add(txtb_CX_reg);
            Controls.Add(label_CX);
            Controls.Add(txtb_BX_reg);
            Controls.Add(label_BX);
            Controls.Add(txtb_AX_reg);
            Controls.Add(label_AX);
            Controls.Add(button_step_mode);
            Controls.Add(button_run_program);
            Controls.Add(button_load_program);
            Controls.Add(button_save_program);
            Controls.Add(program_display);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox program_display;
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
    }
}
