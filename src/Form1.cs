using System.IO;
using System.Runtime.CompilerServices;

namespace _8086_microprocessor_simulator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private ushort AX = 0;
        private ushort BX = 0;
        private ushort CX = 0;
        private ushort DX = 0;

        
        private void MOV(ref ushort reg, ushort value, [CallerArgumentExpression("reg")] string reg_name ="")
        {
            reg = (ushort)(value);
        }
        private void ADD(ref ushort reg, ushort value)
        {
            reg = (ushort)(reg + value);
        }

        private void SUB(ref ushort reg, ushort value)
        {
            reg = (ushort)(reg - value);
        }

        private void refresh_all_reg()
        {
            txtb_AX_reg.Text = Convert.ToString(AX, 2).PadLeft(16, '0');
            txtb_BX_reg.Text = Convert.ToString(BX, 2).PadLeft(16, '0');
            txtb_CX_reg.Text = Convert.ToString(CX, 2).PadLeft(16, '0');
            txtb_DX_reg.Text = Convert.ToString(DX, 2).PadLeft(16, '0');
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_run_program_Click(object sender, EventArgs e)
        {
            ADD(ref AX, 16);
            MOV(ref BX, AX);
            refresh_all_reg();
        }
    }
}
