using System.IO;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Xml.Serialization;

namespace _8086_microprocessor_simulator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private ushort A = 0;
        private ushort B = 0;
        private ushort C = 0;
        private ushort D = 0;
        private int curr_line = 0;
        private string[] program_lines_step_work = new string[0];

        private void MOV(ref ushort reg, char flag, ushort value)
        {
            if (flag == 'X')
            {
                reg = (ushort)(value);
            }
            else if (flag == 'H')
            {
                reg = (ushort)((reg & 0x00FF) | (value << 8));
            }
            else if (flag == 'L')
            {
                reg = (ushort)((reg & 0xFF00) | (value & 0x00FF));
            }
        }
        private void ADD(ref ushort reg, char flag, ushort value)
        {
            if (flag == 'X')
            {
                reg = (ushort)(reg + value);
            }
            else if (flag == 'H')
            {
                reg = (ushort)((reg & 0x00FF) | (reg + (value << 8)));
            }
            else if (flag == 'L')
            {
                reg = (ushort)((reg & 0xFF00) | ((reg + value) & 0x00FF));
            }
        }
        private void SUB(ref ushort reg, char flag, ushort value)
        {
            if (flag == 'X')
            {
                reg = (ushort)(reg - value);
            }
            else if (flag == 'H')
            {
                reg = (ushort)((reg & 0x00FF) | (reg - (value << 8)));
            }
            else if (flag == 'L')
            {
                reg = (ushort)((reg & 0xFF00) | ((reg - value) & 0x00FF));
            }
        }

        private void refreshAllReg()
        {
            txtb_AX_reg.Text = Convert.ToString(A, 2).PadLeft(16, '0');
            txtb_BX_reg.Text = Convert.ToString(B, 2).PadLeft(16, '0');
            txtb_CX_reg.Text = Convert.ToString(C, 2).PadLeft(16, '0');
            txtb_DX_reg.Text = Convert.ToString(D, 2).PadLeft(16, '0');
        }
        private ushort loadSourceValue(string source)
        {
            if (ushort.TryParse(source, out ushort value)) return value;

            char reg = source[0];
            char flag = source[1];

            ushort full_value = 0;
            switch (reg)
            {
                case 'A': full_value = A; break;
                case 'B': full_value = B; break;
                case 'C': full_value = C; break;
                case 'D': full_value = D; break;
            }

            if (flag == 'H') return (ushort)(full_value >> 8);
            if (flag == 'L') return (ushort)(full_value & 0xFF);

            return full_value;
        }
        private void instructionExec(string code_line)
        {
            string[] parts = code_line.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 3)
            {
                string instruction = parts[0].ToUpper();
                string register = parts[1].ToUpper();
                string source = parts[2].ToUpper();

                if (instruction == "MOV")
                {
                    ushort value = loadSourceValue(source);

                    char reg = register[0];
                    char flag = register[1];

                    switch (reg)
                    {
                        case 'A': MOV(ref A, flag, value); break;
                        case 'B': MOV(ref B, flag, value); break;
                        case 'C': MOV(ref C, flag, value); break;
                        case 'D': MOV(ref D, flag, value); break;
                    }
                }
                else if (instruction == "ADD")
                {
                    ushort value = loadSourceValue(source);

                    char reg = register[0];
                    char flag = register[1];

                    switch (reg)
                    {
                        case 'A': ADD(ref A, flag, value); break;
                        case 'B': ADD(ref B, flag, value); break;
                        case 'C': ADD(ref C, flag, value); break;
                        case 'D': ADD(ref D, flag, value); break;
                    }
                }
                else if (instruction == "SUB")
                {
                    ushort value = loadSourceValue(source);

                    char reg = register[0];
                    char flag = register[1];

                    switch (reg)
                    {
                        case 'A': SUB(ref A, flag, value); break;
                        case 'B': SUB(ref B, flag, value); break;
                        case 'C': SUB(ref C, flag, value); break;
                        case 'D': SUB(ref D, flag, value); break;
                    }
                }

            }
            else
            {
                MessageBox.Show($"Bład w skłądni. {code_line}");
            }
        }
        private void button_save_program_Click(object sender, EventArgs e)
        {
            SaveFileDialog save_file_dialog = new SaveFileDialog();

            save_file_dialog.Filter = "Pliki tekstowe(*.txt) | *.txt | Pliki assemblera(*.asm) | *.asm | Wszystkie pliki(*.*) | *.* ";
            save_file_dialog.Title = "Zapisz program";
            save_file_dialog.DefaultExt = "txt";

            if (save_file_dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string file_path = save_file_dialog.FileName;
                    string code = program_display.Text;
                    File.WriteAllText(file_path, code);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_load_program_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_file_dialog = new OpenFileDialog();

            open_file_dialog.Filter = "Pliki tekstowe(*.txt) | *.txt";
            open_file_dialog.Title = "Wczytaj program";
            open_file_dialog.DefaultExt = "txt";

            if (open_file_dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string file_path = open_file_dialog.FileName;
                    string code = File.ReadAllText(file_path);
                    program_display.Text = code;
                    curr_line = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_run_program_Click(object sender, EventArgs e)
        {
            string[] program_lines = program_display.Text
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Trim())
                .ToArray();

            foreach (string line in program_lines)
            {
                instructionExec(line);
            }
            refreshAllReg();
        }

        private void button_step_mode_Click(object sender, EventArgs e)
        {
            if (curr_line == 0)
            {
                program_lines_step_work = program_display.Text
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Trim())
                .ToArray();
            }

            if (curr_line >= program_lines_step_work.Length)
            {
                curr_line = 0;
                return;
            }

            textBox_curr_line.Text = (curr_line + 1).ToString();

            highlight_current_line(curr_line);

            instructionExec(program_lines_step_work[curr_line]);
            refreshAllReg();
            curr_line++;
        }

        private void highlight_current_line(int lineIndex)
        {
            if (lineIndex < 0 || lineIndex >= program_display.Lines.Length) return;

            program_display.SelectAll();
            program_display.SelectionBackColor = program_display.BackColor;

            int startIndex = program_display.GetFirstCharIndexFromLine(lineIndex);
            int lineLength = program_display.Lines[lineIndex].Length;

            program_display.Select(startIndex, lineLength);
            program_display.SelectionBackColor = Color.Gray;

            program_display.DeselectAll();
        }

        private void reset_registers()
        {
            A = 0;
            B = 0;
            C = 0;
            D = 0;
            refreshAllReg();
        }

        private void button_clear_registers_Click(object sender, EventArgs e)
        {
            reset_registers();
        }
    }
}