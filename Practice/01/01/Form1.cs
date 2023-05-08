using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LB1
{
    public partial class Form1 : Form
    {
        public int numericUpDownINT { get; set; }

        public bool Crypt { get; set; } // true - зашифрувати; false - розшифрувати
        public bool ChangeCrypt { get; set; } // true - змінювався радіобатон Crypt; false - не змінювався радіобатон Crypt
        public bool Language { get; set; } // true - українська мова; false - англійська мова 
        public bool ChangeLanguage { get; set; } // true - змінювалася мова; false - не змінювалася мова 

        private Dictionary<string, int> enDict = new Dictionary<string, int>(); 
        private Dictionary<string, int> ukDict = new Dictionary<string, int>();

        public Form1()
        {
            InitializeComponent();
        }

        private void проРозробникаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Криптосистема\n Розробник: Кравець Ольга\n Група: ПМО-31\n Рік: 2023");
        }

        private void вихідЗіСистемиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Здійснити вихід зі системи?", "Увага!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void відкритиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter =
               "All files (*.*)|*.*";
            dialog.InitialDirectory = "C:\\";
            dialog.Title = "Виберіть текстовий файл";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fname = dialog.FileName;
                richTextBox1.Text = File.ReadAllText(fname, Encoding.UTF8);
            }
        }

        private void зберегтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog oSaveFileDialog = new SaveFileDialog();
            oSaveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (oSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = oSaveFileDialog.FileName;
                string extesion = Path.GetExtension(fileName);
                string fullPath = Path.GetFullPath(fileName);
                switch (extesion)
                {
                    case ".txt":
                        File.WriteAllText(fullPath, richTextBox2.Text);
                        break;
                    default:
                        break;
                }
            }
        }

        private void створитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
             File.WriteAllText("file.txt", назваФайлуToolStripMenuItem.Text);
        }

        private void друкToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBox1.Text, new Font("Times New Roman", 14), Brushes.Black, new PointF(100, 100));
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Crypt = false;
            ChangeCrypt = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Crypt = true;
            ChangeCrypt = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Language = true;
            ChangeLanguage = true;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Language = false;
            ChangeLanguage = true;
        }
        const string ENG_LETTERS = " ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string UA_LETTERS = " АБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯ";
        public static string ENG_Code(string text, int k)
        {
            var fullEngletters = ENG_LETTERS + ENG_LETTERS.ToLower();
            var letterQty = fullEngletters.Length;
            var retVal = "";
            for (int i = 0; i < text.Length; i++)
            {
                var c = text[i];
                var index = fullEngletters.IndexOf(c);
                if (index < 0)
                {
                    retVal += c.ToString(); //якщо літера не знайдена, додаємо її в незмінному вигляді
                }
                else
                {
                    var codeIndex = (letterQty + index + k) % letterQty;
                    retVal += fullEngletters[codeIndex];
                }
            }
            return retVal;
        }

        public static string ENG_Code_Decrypt(string text, int k)
        {
            var fullEngletters = ENG_LETTERS + ENG_LETTERS.ToLower();
            var letterQty = fullEngletters.Length;
            var retVal = "";
            for (int i = 0; i < text.Length; i++)
            {
                var c = text[i];
                var index = fullEngletters.IndexOf(c);
                if (index < 0)
                {
                    retVal += c.ToString(); //якщо літера не знайдена, додаємо її в незмінному вигляді
                }
                else
                {
                    var codeIndex = (letterQty + index - k) % letterQty;
                    retVal += fullEngletters[codeIndex];
                }
            }
            return retVal;
        }

        public static string UA_Code(string text, int k)
        {
            var fullUkrletters = UA_LETTERS + UA_LETTERS.ToLower();
            var letterQty = fullUkrletters.Length;
            var retVal = "";
            for (int i = 0; i < text.Length; i++)
            {
                var c = text[i];
                var index = fullUkrletters.IndexOf(c);
                if (index < 0)
                {
                    retVal += c.ToString(); //якщо літера не знайдена, додаємо її в незмінному вигляді
                }
                else
                {
                    var codeIndex = (letterQty + index + k) % letterQty;
                    retVal += fullUkrletters[codeIndex];
                }
            }
            return retVal;
        }

        public static string UA_Code_Decrypt(string text, int k)
        {
            var fullUkrletters = UA_LETTERS + UA_LETTERS.ToLower();
            var letterQty = fullUkrletters.Length;
            var retVal = "";
            for (int i = 0; i < text.Length; i++)
            {
                var c = text[i];
                var index = fullUkrletters.IndexOf(c);
                if (index < 0)
                {
                    retVal += c.ToString(); //якщо літера не знайдена, додаємо її в незмінному вигляді
                }
                else
                {
                    var codeIndex = (letterQty + index - k) % letterQty;
                    retVal += fullUkrletters[codeIndex];
                }
            }
            return retVal;
        }

        public static string EngEncrypt(string plainMessage, int key) => ENG_Code(plainMessage, key);

        public static string EngDecrypt(string encryptedMessage, int key) => ENG_Code(encryptedMessage, -key);

        public static string UkrEncrypt(string plainMessage, int key) => UA_Code(plainMessage, key);

        public static string UkrDecrypt(string encryptedMessage, int key) => UA_Code(encryptedMessage, -key);
        private void button2_Click(object sender, EventArgs e)
        {
            if (ChangeCrypt == false && ChangeLanguage == false)
            {
                MessageBox.Show("Увагі! Оберіть мову шифру або тип шифрування");
            }
            else
            {
                if (richTextBox1.Text == "")
                {
                    MessageBox.Show("Уведіть текст для шифрування!");
                }
                else
                {
                    if (Language && !Crypt)// укр. мова зашифрувати
                    {
                        richTextBox2.Text = UkrEncrypt(richTextBox1.Text, numericUpDownINT);
                    }
                    else if (Language && Crypt) // укр. мова розшифрувати
                    {
                        richTextBox2.Text = UkrDecrypt(richTextBox1.Text, numericUpDownINT);
                    }
                    else if (!Language && !Crypt)// англ. мова зашифрувати
                    {
                        richTextBox2.Text = EngEncrypt(richTextBox1.Text, numericUpDownINT);
                    }
                    else if (!Language && Crypt)// англ. мова розшифрувати
                    {
                        richTextBox2.Text = EngDecrypt(richTextBox1.Text, numericUpDownINT);
                    }
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox2.Text;
            richTextBox2.Text = "";
        }
        public delegate bool DelBruteForceAlgo(ref char[] inputs);
        public static int BrForce(string key, bool language)
        {
            int keyLength = key.Length;
            string alphabet = "";

            int steps = 0;
            char[] currentGuess = new char[keyLength];

            if (language)
            {
                alphabet = "абвгґдеєжзиіїйклмнопрстуфхцчшщьюяАБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯ ";

                alphabet = alphabet + alphabet.ToUpper();
            }
            else
            {
                alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ";

                alphabet = alphabet + alphabet.ToUpper();
            }

            int alphabetLength = alphabet.Length;

            for (int i = 0; i < keyLength; i++)
            {
                currentGuess[i] = alphabet[0];
            }

            while (true)
            {
                string guess = new string(currentGuess);
                if (guess == key)
                {
                    return steps;
                }

                int index = keyLength - 1;
                while (index >= 0 && currentGuess[index] == alphabet[alphabetLength - 1])
                {
                    currentGuess[index] = alphabet[0];
                    index--;
                }

                if (index < 0)
                {
                    break;
                }

                int nextCharIndex = alphabet.IndexOf(currentGuess[index]) + 1;
                currentGuess[index] = alphabet[nextCharIndex];
                steps++;
            }
            return steps;
        }
        private void атакаНаШифрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            string encryptedText = richTextBox2.Text;

            if (string.IsNullOrEmpty(encryptedText))
            {
                MessageBox.Show("Текстове поле для шифрування порожнє", "Помилка");
                return;
            }

            for (int step = 1; step <= 1000; step++)
            {
                string decryptedText = UA_Code_Decrypt(encryptedText, step);
                if (decryptedText == richTextBox1.Text)
                {
                    stopwatch.Stop();
                    MessageBox.Show($"Витрачений час: {stopwatch.ElapsedMilliseconds} мс, Ключ: {step}", "Атака 'грубою силою'");

                    richTextBox2.Text = decryptedText;
                    return;
                }
            }

            MessageBox.Show("Ключ не знайдено", "Помилка");
        }

        private void частотніТаблиціToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form tableForm = new Form();
            tableForm.Text = "Частотна таблиця";
            tableForm.Size = new Size(500, 700);

            string fileName = "Dict.json";
            string directoryPath = @"D:\Навчання\6 семестр\ДВ Математичні основи криптології\Практичні\01\LB1\Files";
            string filePath = Path.Combine(directoryPath, fileName);
            string json = File.ReadAllText(filePath);
            Dictionary<string, int> dict = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
            SortedDictionary<string, int> sortedDict = new SortedDictionary<string, int>(dict, new KeyComparer());

            DataGridView enTable = new DataGridView();
            DataTable table = new DataTable();

            table.Columns.Add("Ключ", typeof(string));
            table.Columns.Add("Частота", typeof(int));

            foreach (var item in sortedDict)
            {
                table.Rows.Add(item.Key, item.Value);
            }

            enTable.DataSource = table;
            enTable.Size = new Size(500, 700);
            tableForm.Controls.Add(enTable);
            tableForm.ShowDialog();
        }
        public void WriteJsonFile(Dictionary<string, int> keyValuePairs, string filePath)
        {
            string json = JsonConvert.SerializeObject(keyValuePairs, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        class KeyComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                return x.CompareTo(y);
            }
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownINT = (int)numericUpDown1.Value;
        }

        private void шифруватиЗображенняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageEncryption ie = new ImageEncryption();
            ie.ShowDialog(this);
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            string directoryPath = @"D:\Навчання\6 семестр\ДВ Математичні основи криптології\Практичні\01\LB1\Files";
            string text = richTextBox2.Text.ToString();
            ukDict = CountLetters(text);
            string filePath = Path.Combine(directoryPath, "Dict.json");
            WriteJsonFile(ukDict, filePath);

        }

        public static Dictionary<string, int> CountLetters(string input)
        {
            Dictionary<string, int> letterCounts = new Dictionary<string, int>();
            foreach (char c in input)
            {
                if (Char.IsLetter(c))
                {
                    if (letterCounts.ContainsKey(c.ToString()))
                    {
                        letterCounts[c.ToString()]++;
                    }
                    else
                    {
                        letterCounts.Add(c.ToString(), 1);
                    }
                }
            }
            return letterCounts;
        }

        private void допомогаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Ctrl+H - допомога\n Ctrl+C - створити файл\n Ctrl+O - відкрити файл\n Ctrl+S - зберегти файл\n " +
                "Ctrl+P - друк файлу\n Alt+A - атака методом 'грубої сили'\n Alt+F - частотні таблиці\n" +
                " Alt+P - шифрувати зображення\n Alt+D - інформація про розробника\n Alt+X - вихід\n ");
        }
    }
}