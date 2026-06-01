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
            InitializeInstructionDocumentation();
            InitializeInterruptDocumentation();
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
        public class InterruptDoc
        {
            public string Name { get; set; }
            public string HowToInvoke { get; set; }
            public string Parameters { get; set; }
            public string Returns { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }

        public class InstructionDoc
        {
            public string Mnemonic { get; set; }
            public string Syntax { get; set; }
            public string Description { get; set; }
            public string Example { get; set; }

            public override string ToString()
            {
                return Mnemonic;
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
            if (!int.TryParse(intNumberHex, System.Globalization.NumberStyles.HexNumber, null, out int intNum))
            {
                MessageBox.Show($"Nieznany format przerwania: {intNumberHex}");
                return;
            }

            int ah = A >> 8;
            int al = A & 0xFF;

            switch (intNum)
            {
                case 0x21:
                    switch (ah)
                    {
                        case 0x02: // Wypisz pojedynczy znak z rejestru DL
                            char charToPrint = (char)(D & 0xFF);
                            outputConsoleTextBox.AppendText(charToPrint.ToString());
                            break;

                        case 0x09: // Wypisz ciąg znaków
                            outputConsoleTextBox.AppendText($"[DUMP] AX:{A:X4} BX:{B:X4} CX:{C:X4} DX:{D:X4}\n");
                            break;

                        case 0x2A: // Pobierz datę systemową
                            DateTime date = DateTime.Now;
                            C = (ushort)date.Year;
                            D = (ushort)((date.Month << 8) | date.Day);
                            break;

                        case 0x2C: // Pobierz czas systemowy
                            DateTime time = DateTime.Now;
                            C = (ushort)((time.Hour << 8) | time.Minute);
                            D = (ushort)((time.Second << 8) | 0);
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
                        case 0x00: 
                            long ticks = (long)(DateTime.Now.TimeOfDay.TotalMilliseconds / 55.0);
                            C = (ushort)((ticks >> 16) & 0xFFFF);
                            D = (ushort)(ticks & 0xFFFF);
                            break;

                        case 0x04:
                            DateTime rtcDate = DateTime.Now;
                            C = (ushort)rtcDate.Year;
                            D = (ushort)((rtcDate.Month << 8) | rtcDate.Day);
                            break;
                    }
                    break;

                case 0x10: // Przerwania BIOS - Monitor
                    switch (ah)
                    {
                        case 0x00: 
                            outputConsoleTextBox.Clear();
                            break;

                        case 0x0E: 
                            char teleChar = (char)al;
                            outputConsoleTextBox.AppendText(teleChar.ToString());
                            break;
                    }
                    break;

                case 0x16: // Przerwania BIOS - Klawiatura
                    switch (ah)
                    {
                        case 0x01: 
                            break;
                    }
                    break;

                default:
                    MessageBox.Show($"Brak implementacji dla przerwania INT {intNumberHex}H");
                    break;
            }
        }
        private void InitializeInterruptDocumentation()
        {
            List<InterruptDoc> interrupts = new List<InterruptDoc>
    {
        new InterruptDoc
        {
            Name = "INT 21H, AH = 02h (DOS) - Wypisz znak",
            HowToInvoke = "1. Wpisz 02H do rejestru AH.\n2. Wpisz kod ASCII znaku do rejestru DL.\n3. Wywołaj INT 21H.",
            Parameters = "AH = 02h\nDL = Kod ASCII znaku, który ma zostać wyświetlony (np. 21H dla '!')",
            Returns = "Brak (Znak pojawia się na konsoli wyjściowej)."
        },
        new InterruptDoc
        {
            Name = "INT 21H, AH = 09h (DOS) - Wypisz ciąg znaków",
            HowToInvoke = "1. Wpisz 09H do rejestru AH.\n2. Wywołaj INT 21H.",
            Parameters = "AH = 09h",
            Returns = "Wypisuje aktualny stan wszystkich rejestrów (DUMP) tekstowo do konsoli."
        },
        new InterruptDoc
        {
            Name = "INT 21H, AH = 2Ah (DOS) - Pobierz datę",
            HowToInvoke = "1. Wpisz 2AH do rejestru AH.\n2. Wywołaj INT 21H.",
            Parameters = "AH = 2Ah",
            Returns = "CX = Rok (w formacie dziesiętnym)\nDH = Miesiąc (1-12)\nDL = Dzień miesiąca (1-31)"
        },
        new InterruptDoc
        {
            Name = "INT 21H, AH = 2Ch (DOS) - Pobierz czas",
            HowToInvoke = "1. Wpisz 2CH do rejestru AH.\n2. Wywołaj INT 21H.",
            Parameters = "AH = 2Ch",
            Returns = "CH = Godzina (0-23)\nCL = Minuta (0-59)\nDH = Sekunda (0-59)\nDL = Setne części sekundy (0)"
        },
        new InterruptDoc
        {
            Name = "INT 21H, AH = 4Ch (DOS) - Zakończ program",
            HowToInvoke = "1. Wpisz 4CH do rejestru AH.\n2. Wywołaj INT 21H.",
            Parameters = "AH = 4Ch",
            Returns = "Zatrzymuje pętlę wykonawczą symulatora i wyświetla komunikat o zakończeniu."
        },
        new InterruptDoc
        {
            Name = "INT 1AH, AH = 00h (BIOS) - Odczytaj licznik czasu",
            HowToInvoke = "1. Wpisz 00H do rejestru AH.\n2. Wywołaj INT 1AH.",
            Parameters = "AH = 00h",
            Returns = "Mierzy liczbę taktów (55ms) od północy:\nCX = Starsze 16 bitów licznika\nDX = Młodsze 16 bitów licznika"
        },
        new InterruptDoc
        {
            Name = "INT 1AH, AH = 04h (BIOS) - Odczytaj datę RTC",
            HowToInvoke = "1. Wpisz 04H do rejestru AH.\n2. Wywołaj INT 1AH.",
            Parameters = "AH = 04h",
            Returns = "CX = Aktualny rok z zegara czasu rzeczywistego\nDH = Miesiąc\nDL = Dzień"
        },
        new InterruptDoc
        {
            Name = "INT 10H, AH = 00h (BIOS) - Zmiana trybu wideo",
            HowToInvoke = "1. Wpisz 00H do rejestru AH.\n2. Wywołaj INT 10H.",
            Parameters = "AH = 00h",
            Returns = "W tym symulatorze czyści całkowicie okno konsoli wyjściowej (Reset)."
        },
        new InterruptDoc
        {
            Name = "INT 10H, AH = 0Eh (BIOS) - Teletype output",
            HowToInvoke = "1. Wpisz 0EH do rejestru AH.\n2. Wpisz kod ASCII do rejestru AL.\n3. Wywołaj INT 10H.",
            Parameters = "AH = 0Eh\nAL = Kod ASCII znaku do wypisania",
            Returns = "Wypisuje znak z rejestru AL na ekranie konsoli i automatycznie przesuwa kursor."
        },
        new InterruptDoc
        {
            Name = "INT 16H, AH = 01h (BIOS) - Status klawiatury",
            HowToInvoke = "1. Wpisz 01H do rejestru AH.\n2. Wywołaj INT 16H.",
            Parameters = "AH = 01h",
            Returns = "Sprawdza bufor klawiatury. Zwraca informację o braku wciśniętego klawisza (brak modyfikacji rejestrów)."
        }
    };

            listBox1.DataSource = interrupts;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is InterruptDoc selectedDoc)
            {
                interruptsTextBox.Clear();

                string fullDescription =
                    $"=== DOKUMENTACJA FUNKCJI ===\n" +
                    $"{selectedDoc.Name}\n\n" +
                    $"--------------------------------------------------\n" +
                    $"JAK WYWOŁAĆ W KODZIE:\n" +
                    $"--------------------------------------------------\n" +
                    $"{selectedDoc.HowToInvoke}\n\n" +
                    $"--------------------------------------------------\n" +
                    $"PARAMETRY WEJŚCIOWE (REJESTRY):\n" +
                    $"--------------------------------------------------\n" +
                    $"{selectedDoc.Parameters}\n\n" +
                    $"--------------------------------------------------\n" +
                    $"WARTOŚCI ZWRACANE:\n" +
                    $"--------------------------------------------------\n" +
                    $"{selectedDoc.Returns}";

                interruptsTextBox.Text = fullDescription;
            }
        }

        private void InitializeInstructionDocumentation()
        {
            List<InstructionDoc> instructions = new List<InstructionDoc>
    {
        new InstructionDoc
        {
            Mnemonic = "MOV - Kopiowanie danych",
            Syntax = "MOV rejestr, źródło",
            Description = "Kopiuje wartość z elementu 'źródło' do elementu 'rejestr'.\n\n" +
                          "Wspierane formaty źródła:\n" +
                          "- Inny rejestr (np. AX, AH, AL, BX...)\n" +
                          "- Liczba dziesiętna (np. 25)\n" +
                          "- Liczba szesnastkowa (np. 10H lub 0X10)\n" +
                          "- Liczba binarna (np. 1010B)",
            Example = "MOV AX, 1234H  ; Wpisz 1234H do rejestru AX\n" +
                      "MOV BL, AH     ; Skopiuj starszy bajt A do młodszego bajtu B"
        },
        new InstructionDoc
        {
            Mnemonic = "ADD - Dodawanie",
            Syntax = "ADD rejestr, źródło",
            Description = "Dodaje wartość 'źródło' do aktualnej wartości w 'rejestr' i zapisuje tam wynik (rejestr = rejestr + źródło).\n" +
                          "Działa zarówno dla rejestrów pełnych 16-bitowych (X), jak i komórek 8-bitowych (H, L).",
            Example = "MOV AX, 5      ; AX = 5\n" +
                      "ADD AX, 10     ; AX = 15 (5 + 10)\n" +
                      "ADD BH, AL     ; Dodaj wartość AL do BH"
        },
        new InstructionDoc
        {
            Mnemonic = "SUB - Odejmowanie",
            Syntax = "SUB rejestr, źródło",
            Description = "Odejmuje wartość 'źródło' od aktualnej wartości w 'rejestr' i zapisuje tam wynik (rejestr = rejestr - źródło).\n" +
                          "W przypadku odejmowania większej liczby od mniejszej następuje naturalne przekręcenie licznika w standardzie 16-bitowym.",
            Example = "MOV CX, 20     ; CX = 20\n" +
                      "SUB CX, 5      ; CX = 15 (20 - 5)"
        },
        new InstructionDoc
        {
            Mnemonic = "PUSH - Odłożenie na stos",
            Syntax = "PUSH źródło",
            Description = "Wrzuca podaną wartość lub zawartość rejestru na wierzchołek wewnętrznego stosu procesora (cpu_stack).\n" +
                          "Zwiększa rozmiar stosu. Pozwala na tymczasowe przechowanie danych.",
            Example = "PUSH AX        ; Zapisz aktualny stan AX na stosie\n" +
                      "PUSH 55H       ; Zapisz stałą wartość na stosie"
        },
        new InstructionDoc
        {
            Mnemonic = "POP - Pobranie ze stosu",
            Syntax = "POP rejestr",
            Description = "Zdejmuje wartość z samego wierzchołka stosu procesora i zapisuje ją do wskazanego rejestru.\n" +
                          "Zmniejsza rozmiar stosu. Jeśli stos jest pusty, symulator zgłosi komunikat o błędzie.",
            Example = "POP BX         ; Przywróć ostatnio zapisaną wartość ze stosu do rejestru BX"
        },
        new InstructionDoc
        {
            Mnemonic = "INT - Przerwanie programowe",
            Syntax = "INT numerH",
            Description = "Wywołuje procedurę przerwania sprzętowego lub systemowego o danym numerze (podawanym w HEX, litera H jest opcjonalna).\n" +
                          "W tym symulatorze przed wykonaniem przerwania automatycznie zapamiętywany i odzyskiwany jest pełen stan rejestrów.\n\n" +
                          "Dokładną listę funkcji sterowanych rejestrem AH znajdziesz w zakładce 'Dokumentacja Przerwań'.",
            Example = "INT 21H        ; Wywołaj przerwanie DOS\n" +
                      "INT 10         ; Wywołaj przerwanie ekranu BIOS"
        }
    };

            listBox2.DataSource = instructions;
        }


        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem is InstructionDoc selectedInst)
            {
                assemblerTextBox.Clear();

                string fullDescription =
                    $"=== SPECYFIKACJA INSTRUKCJI ===\n" +
                    $"{selectedInst.Mnemonic}\n\n" +
                    $"--------------------------------------------------\n" +
                    $"SKŁADNIA (SYNTAX):\n" +
                    $"--------------------------------------------------\n" +
                    $"{selectedInst.Syntax}\n\n" +
                    $"--------------------------------------------------\n" +
                    $"OPIS DZIAŁANIA W SYMULATORZE:\n" +
                    $"--------------------------------------------------\n" +
                    $"{selectedInst.Description}\n\n" +
                    $"--------------------------------------------------\n" +
                    $"PRZYKŁAD UŻYCIA:\n" +
                    $"--------------------------------------------------\n" +
                    $"{selectedInst.Example}";

                assemblerTextBox.Text = fullDescription;
            }
        }
    }
}