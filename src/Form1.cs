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

        private bool isProgramTerminated = false;

        private Stack<ushort> cpu_stack = new Stack<ushort>();
        private void PushRegistersState()
        {
            cpu_stack.Push(A);
            cpu_stack.Push(B);
            cpu_stack.Push(C);
            cpu_stack.Push(D);
        }
        private void PopRegistersState()
        {
            if (cpu_stack.Count >= 4)
            {
                D = cpu_stack.Pop();
                C = cpu_stack.Pop();
                B = cpu_stack.Pop();
                A = cpu_stack.Pop();
            }
        }

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
            source = source.Trim().ToUpper();

            try
            {
                if (source.EndsWith("H") && !source.All(char.IsLetter))
                {
                    return Convert.ToUInt16(source.TrimEnd('H'), 16);
                }
                else if (source.StartsWith("0X"))
                {
                    return Convert.ToUInt16(source.Substring(2), 16);
                }
                else if (source.EndsWith("B") && !source.All(char.IsLetter))
                {
                    return Convert.ToUInt16(source.TrimEnd('B'), 2);
                }
                else if (ushort.TryParse(source, out ushort decValue))
                {
                    return decValue;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show($"Błąd formatu liczby: {source}", "Błąd składni");
                return 0;
            }
            catch (OverflowException)
            {
                MessageBox.Show($"Wartość poza zakresem 16-bit (0-65535): {source}", "Błąd przepełnienia");
                return 0;
            }

            char reg = source[0];
            char flag = source[1];

            ushort full_value = 0;
            switch (reg)
            {
                case 'A': full_value = A; break;
                case 'B': full_value = B; break;
                case 'C': full_value = C; break;
                case 'D': full_value = D; break;
                default:
                    MessageBox.Show($"Nieznany rejestr: {source}", "Błąd");
                    return 0;
            }

            if (flag == 'H') return (ushort)(full_value >> 8);
            if (flag == 'L') return (ushort)(full_value & 0xFF);
            if (flag == 'X') return full_value;

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

                ushort value = loadSourceValue(source);
                char reg = register[0];
                char flag = register[1];

                if (instruction == "MOV")
                {
                    switch (reg) { case 'A': MOV(ref A, flag, value); break; case 'B': MOV(ref B, flag, value); break; case 'C': MOV(ref C, flag, value); break; case 'D': MOV(ref D, flag, value); break; }
                }
                else if (instruction == "ADD")
                {
                    switch (reg) { case 'A': ADD(ref A, flag, value); break; case 'B': ADD(ref B, flag, value); break; case 'C': ADD(ref C, flag, value); break; case 'D': ADD(ref D, flag, value); break; }
                }
                else if (instruction == "SUB")
                {
                    switch (reg) { case 'A': SUB(ref A, flag, value); break; case 'B': SUB(ref B, flag, value); break; case 'C': SUB(ref C, flag, value); break; case 'D': SUB(ref D, flag, value); break; }
                }
                else
                {
                    MessageBox.Show($"Nieznana instrukcja 3-członowa: {code_line}");
                }
            }
            else if (parts.Length == 2)
            {
                string instruction = parts[0].ToUpper();
                string target = parts[1].ToUpper();

                if (instruction == "PUSH")
                {
                    ushort value = loadSourceValue(target);
                    cpu_stack.Push(value);
                }
                else if (instruction == "POP")
                {
                    if (cpu_stack.Count > 0)
                    {
                        ushort value = cpu_stack.Pop();
                        char reg = target[0];
                        char flag = target[1];

                        switch (reg) { case 'A': MOV(ref A, flag, value); break; case 'B': MOV(ref B, flag, value); break; case 'C': MOV(ref C, flag, value); break; case 'D': MOV(ref D, flag, value); break; }
                    }
                    else
                    {
                        MessageBox.Show("Błąd: Stos jest pusty!");
                    }
                }
                else if (instruction == "INT")
                {
                    string intNumberHex = target.Replace("H", "");
                    handleInterrupt(intNumberHex);
                }
                else
                {
                    MessageBox.Show($"Nieznana instrukcja 2-członowa: {code_line}");
                }
            }
            else
            {
                MessageBox.Show($"Błąd w składni. {code_line}");
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
            isProgramTerminated = false;

            string[] program_lines = program_display.Text
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Trim())
                .ToArray();

            foreach (string line in program_lines)
            {
                if (isProgramTerminated) break;
                instructionExec(line);
            }
            refreshAllReg();
        }

        private void button_step_mode_Click(object sender, EventArgs e)
        {
            if (isProgramTerminated)
            {
                MessageBox.Show("Program został zakończony. Zresetuj układ (zmień krok na 0), aby zacząć od nowa.");
                return;
            }

            if (curr_line == 0)
            {
                isProgramTerminated = false;
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
            cpu_stack.Clear();
            refreshAllReg();
        }

        private void button_clear_registers_Click(object sender, EventArgs e)
        {
            reset_registers();
        }

        private void handleInterrupt(string intNumberHex)
        {
            // Konwersja numeru przerwania (np. 21) z HEX na INT
            if (!int.TryParse(intNumberHex, System.Globalization.NumberStyles.HexNumber, null, out int intNum))
            {
                MessageBox.Show($"Nieznany format przerwania: {intNumberHex}");
                return;
            }

            // Wyciągnięcie wartości AH i AL z rejestru A (AX)
            int ah = A >> 8;
            int al = A & 0xFF;

            switch (intNum)
            {
                case 0x21: // Przerwania DOS
                    switch (ah)
                    {
                        case 0x02: // Wypisz pojedynczy znak z rejestru DL
                            char charToPrint = (char)(D & 0xFF);
                            outputConsoleTextBox.AppendText(charToPrint.ToString());
                            break;

                        case 0x09: // Wypisz ciąg znaków (symulacja wypisania rejestrów)
                            outputConsoleTextBox.AppendText($"[DUMP] AX:{A:X4} BX:{B:X4} CX:{C:X4} DX:{D:X4}\n");
                            break;

                        case 0x2A: // Pobierz datę systemową
                            DateTime date = DateTime.Now;
                            C = (ushort)date.Year; // CX = rok
                            D = (ushort)((date.Month << 8) | date.Day); // DH = miesiąc, DL = dzień
                            break;

                        case 0x2C: // Pobierz czas systemowy
                            DateTime time = DateTime.Now;
                            C = (ushort)((time.Hour << 8) | time.Minute); // CH = godzina, CL = minuta
                            D = (ushort)((time.Second << 8) | 0); // DH = sekunda, DL = setne sekundy (dajemy 0 dla uproszczenia)
                            break;

                        case 0x4C: // Zakończ program
                            isProgramTerminated = true;
                            outputConsoleTextBox.AppendText("\n--- Program zakończony ---\n");
                            break;
                    }
                    break;

                case 0x1A: // Przerwania BIOS - Zegar RTC
                    switch (ah)
                    {
                        case 0x00: // Odczytaj zegar (liczba "taktów" od północy)
                                   // 1 takt to ok. 55ms. Dzielimy milisekundy przez 55.
                            long ticks = (long)(DateTime.Now.TimeOfDay.TotalMilliseconds / 55.0);
                            C = (ushort)((ticks >> 16) & 0xFFFF); // Starsze słowo do CX
                            D = (ushort)(ticks & 0xFFFF);         // Młodsze słowo do DX
                            break;

                        case 0x04: // Odczytaj datę RTC z BIOSu
                            DateTime rtcDate = DateTime.Now;
                            C = (ushort)rtcDate.Year;
                            D = (ushort)((rtcDate.Month << 8) | rtcDate.Day);
                            break;
                    }
                    break;

                case 0x10: // Przerwania BIOS - Monitor
                    switch (ah)
                    {
                        case 0x00: // Zmiana trybu wideo (czyścimy ekran wyjściowy)
                            outputConsoleTextBox.Clear();
                            break;

                        case 0x0E: // Wypisz znak z AL (TeleType output)
                            char teleChar = (char)al;
                            outputConsoleTextBox.AppendText(teleChar.ToString());
                            break;
                    }
                    break;

                case 0x16: // Przerwania BIOS - Klawiatura
                    switch (ah)
                    {
                        case 0x01: // Sprawdź status bufora klawiatury
                                   // W prostym symulatorze zakładamy brak wciśniętego klawisza.
                                   // Ponieważ nie masz jeszcze rejestru FLAG (np. Zero Flag = 1), 
                                   // nic nie zmieniamy w rejestrach jako symulację pustego bufora.
                            break;
                    }
                    break;

                default:
                    MessageBox.Show($"Brak implementacji dla przerwania INT {intNumberHex}H");
                    break;
            }
        }
    }
}