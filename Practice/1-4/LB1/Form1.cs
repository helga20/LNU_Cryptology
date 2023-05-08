using LB1.Classes;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Windows.Forms.Design;
using System.Xml.Linq;
using System.IO;
//using TextBox = System.Windows.Forms.TextBox;

namespace LB1
{
    public partial class Form1 : Form
    {
        public int numericUpDownINT { get; set; }
        public int numericUpDown2INT { get; set; }

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


        private void button2_Click(object sender, EventArgs e)
        {
            if (ChangeCrypt == false && ChangeLanguage == false)
            {
                MessageBox.Show("Увага! Оберіть мову шифру або тип шифрування");
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
                        richTextBox2.Text = Caesar.UkrEncrypt(richTextBox1.Text, numericUpDownINT);
                    }
                    else if (Language && Crypt) // укр. мова розшифрувати
                    {
                        richTextBox2.Text = Caesar.UkrDecrypt(richTextBox1.Text, numericUpDownINT);
                    }
                    else if (!Language && !Crypt)// англ. мова зашифрувати
                    {
                        richTextBox2.Text = Caesar.EngEncrypt(richTextBox1.Text, numericUpDownINT);
                    }
                    else if (!Language && Crypt)// англ. мова розшифрувати
                    {
                        richTextBox2.Text = Caesar.EngDecrypt(richTextBox1.Text, numericUpDownINT);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox2.Text;
            richTextBox2.Text = "";
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
                string decryptedText = Caesar.UA_Code_Decrypt(encryptedText, step);
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

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown2INT = (int)numericUpDown1.Value;

        }

        private void шифруватиЗображенняToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
                " Alt+P - шифрувати зображення\n Alt+D - інформація про розробника\n Alt+X - вихід\n " +
                "Alt+T - атака на шифр Тритеміуса");
        }

        private void зображенняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageEncryption ie = new ImageEncryption();
            ie.ShowDialog(this);
        }

        private void цезаряToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            numericUpDown1.Visible = true;
            button2.Visible = true;
            groupBox2.Visible = true;

            label7.Visible = false;
            textBox4.Visible = false;


            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;

            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;

            button3.Visible = false;
            groupBox3.Visible = false;
            button6.Visible = false;
            groupBox4.Visible = false;

            label8.Visible = false;
            textBox5.Visible = false;
            button5.Visible = false;

            button4.Visible = false;
            label9.Visible = false;
            numericUpDown2.Visible = false;
            button7.Visible = false;
        }

        private void тритеміусаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = true;
            label1.Visible = false;
            numericUpDown1.Visible = false;
            groupBox2.Visible = true;

            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;

            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;

            button2.Visible = false;
            button3.Visible = true;
            button6.Visible = false;
            groupBox4.Visible = false;

            label8.Visible = false;
            textBox5.Visible = false;
            button5.Visible = false;

            button4.Visible = false;
            label9.Visible = false;
            numericUpDown2.Visible = false;
            button7.Visible = false;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            label4.Visible = true;
            label5.Visible = true;

            label1.Visible = false;
            numericUpDown1.Visible = false;

            textBox1.Visible = true;
            textBox2.Visible = true;

            textBox4.Visible = false;
            label7.Visible = false;

            textBox3.Visible = false;
            label6.Visible = false;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;

            label1.Visible = false;
            numericUpDown1.Visible = false;

            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;

            textBox4.Visible = false;
            label7.Visible = false;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown1.Visible = false;
            label1.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;

            label1.Visible = false;
            numericUpDown1.Visible = false;

            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;

            textBox4.Visible = true;
            label7.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(richTextBox1.Text))
            {
                MessageBox.Show("Текстове поле порожнє", "Помилка");
                return;
            }
            else if (radioButton5.Checked)
            {
                if (!radioButton5.Checked && !radioButton6.Checked && !radioButton7.Checked)
                {
                    MessageBox.Show("Оберіть тип", "Помилка");
                    return;
                }
                if (radioButton5.Checked)
                {
                    if (string.IsNullOrEmpty(textBox1.Text))
                    {
                        MessageBox.Show("Введіть ключ", "Помилка");
                        return;
                    }
                    else if (string.IsNullOrEmpty(textBox2.Text))
                    {
                        MessageBox.Show("Введіть ключ", "Помилка");
                        return;
                    }
                    if (!Language && !Crypt)
                    {
                        Trithemius t = new Trithemius();
                        richTextBox2.Text = t.EncryptLinearAB(richTextBox1.Text, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
                    }
                    if (!Language && Crypt)
                    {
                        Trithemius t = new Trithemius();
                        richTextBox2.Text = t.DecryptLinearAB(richTextBox1.Text, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
                    }
                    if (Language && !Crypt)
                    {
                        Trithemius t = new Trithemius();
                        richTextBox2.Text = t.UKREncryptLinearAB(richTextBox1.Text, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
                    }
                    if (Language && Crypt)
                    {
                        Trithemius t = new Trithemius();
                        richTextBox2.Text = t.UKRDecryptLinearAB(richTextBox1.Text, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
                    }
                }
            }
            if (radioButton6.Checked)
            {
                if (!radioButton5.Checked && !radioButton6.Checked && !radioButton7.Checked)
                {
                    MessageBox.Show("Оберіть тип", "Помилка");
                    return;
                }
                else if (radioButton6.Checked)
                {
                    if (string.IsNullOrEmpty(textBox1.Text))
                    {
                        MessageBox.Show("Введіть ключ", "Помилка");
                        return;
                    }
                    else if (string.IsNullOrEmpty(textBox2.Text))
                    {
                        MessageBox.Show("Введіть ключ", "Помилка");
                        return;
                    }
                    else if (string.IsNullOrEmpty(textBox3.Text))
                    {
                        MessageBox.Show("Введіть ключ", "Помилка");
                        return;
                    }
                    if (!Language && !Crypt)
                    {
                        Trithemius t = new Trithemius();
                        richTextBox2.Text = t.EncryptNonLinearABC(richTextBox1.Text, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
                    }
                    if (!Language && Crypt)
                    {
                        Trithemius t = new Trithemius();
                        richTextBox2.Text = t.DecryptNonLinearABC(richTextBox1.Text, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
                    }
                    if (Language && !Crypt)
                    {
                        Trithemius t = new Trithemius();
                        richTextBox2.Text = t.UKREncryptNonLinearABC(richTextBox1.Text, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
                    }
                    if (Language && Crypt)
                    {
                        Trithemius t = new Trithemius();
                        richTextBox2.Text = t.UKRDecryptNonLinearABC(richTextBox1.Text, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
                    }
                }
            }
            if (radioButton7.Checked)
            {
                if (!radioButton5.Checked && !radioButton6.Checked && !radioButton7.Checked)
                {
                    MessageBox.Show("Оберіть тип", "Помилка");
                    return;
                }
                else if (radioButton7.Checked)
                {
                    if (string.IsNullOrEmpty(textBox4.Text))
                    {
                        MessageBox.Show("Введіть ключ", "Помилка");
                        return;
                    }
                }
                if (!Language && !Crypt)
                {
                    Trithemius t = new Trithemius();
                    richTextBox2.Text = t.EncryptMotto(richTextBox1.Text, textBox4.Text);
                }
                if (!Language && Crypt)
                {
                    Trithemius t = new Trithemius();
                    richTextBox2.Text = t.DecryptMotto(richTextBox1.Text, textBox4.Text);
                }
                if (Language && !Crypt)
                {
                    Trithemius t = new Trithemius();
                    richTextBox2.Text = t.UKREncryptMotto(richTextBox1.Text, textBox4.Text);
                }
                if (Language && Crypt)
                {
                    Trithemius t = new Trithemius();
                    richTextBox2.Text = t.UKRDecryptMotto(richTextBox1.Text, textBox4.Text);
                }
            }
        }

        private void атакаНаШифрТритеміусаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trithemius t = new Trithemius();

            string encryptedText = richTextBox2.Text.ToLower().Replace("*", " ");
            string originalText = richTextBox1.Text.ToLower();

            const string en_lang_lower = " abcdefghijklmnopqrstuvwxyz";
            const string ua_lang_lower = " абвгґдеєжзиіїйклмнопрстуфхцчшщьюя";

            if (!Language && radioButton5.Checked)
            {
                int[] result = new int[3];

                if (encryptedText.Length >= 0 && originalText.Length >= 0)
                {
                    for (int i = 0; i < encryptedText.Length; i++)
                    {
                        if (!Char.IsLetter(originalText[i]) || !Char.IsLetter(encryptedText[i]))
                        { continue; }

                        result[2] = en_lang_lower.IndexOf(encryptedText[0]) - en_lang_lower.IndexOf(originalText[i]);
                        result[1] = en_lang_lower.IndexOf(encryptedText[i]) - en_lang_lower.IndexOf(originalText[i]);
                        result[0] = en_lang_lower.IndexOf(encryptedText[(i + 1) % encryptedText.Length]) - en_lang_lower.IndexOf(originalText[(i + 1) % originalText.Length]) - result[1];

                        if (result[1] < 0)
                        {
                            result[1] += en_lang_lower.Length;
                        }
                        if (result[0] < 0)
                        {
                            result[0] += en_lang_lower.Length;
                        }

                        string linear = t.DecryptLinearAB(encryptedText, result[0], result[1]);

                        if (linear == originalText)
                        {
                            MessageBox.Show($"Лінійний ключ: {result[0]}, {result[1]}");
                            break;
                        }
                    }
                }
            }

            if (!Language && radioButton6.Checked)
            {
                int[] result = new int[3];
                int temp1;
                int temp2;
                if (encryptedText.Length >= 0 && originalText.Length >= 0)
                {
                    for (int i = 0; i < encryptedText.Length; i++)
                    {
                        if (!Char.IsLetter(originalText[i]) || !Char.IsLetter(encryptedText[i]))
                        { continue; }

                        result[2] = en_lang_lower.IndexOf(encryptedText[0]) - en_lang_lower.IndexOf(originalText[0]);
                        temp1 = en_lang_lower.IndexOf(encryptedText[1]) - en_lang_lower.IndexOf(originalText[1]) - result[2];
                        temp2 = en_lang_lower.IndexOf(encryptedText[2]) - en_lang_lower.IndexOf(originalText[2]) - result[2];
                        result[1] = (4 * temp1 - temp2) / 2;
                        result[0] = temp1 - result[1];

                        string NonLinear = t.DecryptNonLinearABC(encryptedText, result[0], result[1], result[2]);

                        if (NonLinear == originalText)
                        {
                            MessageBox.Show($"Нелінійний ключ: {result[0]}, {result[1]}, {result[2]}");
                            break;
                        }
                    }
                }
            }

            if (!Language && radioButton7.Checked)
            {
                bool doit = true;
                int text_pos = -1;
                string res = "";
                while (doit)
                {
                    text_pos++;

                    if (en_lang_lower.IndexOf(encryptedText[text_pos]) != -1)
                    {
                        int temp = (en_lang_lower.IndexOf(encryptedText[text_pos]) - en_lang_lower.IndexOf(originalText[text_pos])) % en_lang_lower.Length;
                        if (temp < 0)
                        {
                            temp += en_lang_lower.Length;
                        }
                        res += en_lang_lower[temp];

                        string org = t.DecryptMotto(encryptedText, res);

                        if (org == originalText)
                        {
                            MessageBox.Show($"Гасло ключ: {res}");
                            break;
                        }
                    }
                }
            }

            if (Language && radioButton5.Checked)
            {
                int[] result = new int[3];

                if (encryptedText.Length >= 0 && originalText.Length >= 0)
                {
                    for (int i = 0; i < encryptedText.Length; i++)
                    {
                        if (!Char.IsLetter(originalText[i]) || !Char.IsLetter(encryptedText[i]))
                        { continue; }

                        result[2] = ua_lang_lower.IndexOf(encryptedText[0]) - ua_lang_lower.IndexOf(originalText[i]);
                        result[1] = ua_lang_lower.IndexOf(encryptedText[i]) - ua_lang_lower.IndexOf(originalText[i]);
                        result[0] = ua_lang_lower.IndexOf(encryptedText[(i + 1) % encryptedText.Length]) - ua_lang_lower.IndexOf(originalText[(i + 1) % originalText.Length]) - result[1];

                        if (result[1] < 0)
                        {
                            result[1] += ua_lang_lower.Length;
                        }
                        if (result[0] < 0)
                        {
                            result[0] += ua_lang_lower.Length;
                        }

                        string linear = t.UKRDecryptLinearAB(encryptedText, result[0], result[1]);

                        if (linear == originalText)
                        {
                            MessageBox.Show($"Лінійний ключ: {result[0]}, {result[1]}");
                            break;
                        }
                    }
                }
            }

            if (Language && radioButton6.Checked)
            {
                int[] result = new int[3];
                int temp1;
                int temp2;
                if (encryptedText.Length >= 0 && originalText.Length >= 0)
                {
                    for (int i = 0; i < encryptedText.Length; i++)
                    {
                        if (!Char.IsLetter(originalText[i]) || !Char.IsLetter(encryptedText[i]))
                        { continue; }

                        result[2] = ua_lang_lower.IndexOf(encryptedText[0]) - ua_lang_lower.IndexOf(originalText[0]);
                        temp1 = ua_lang_lower.IndexOf(encryptedText[1]) - ua_lang_lower.IndexOf(originalText[1]) - result[2];
                        temp2 = ua_lang_lower.IndexOf(encryptedText[2]) - ua_lang_lower.IndexOf(originalText[2]) - result[2];
                        result[1] = (4 * temp1 - temp2) / 2;
                        result[0] = temp1 - result[1];

                        string NonLinear = t.UKRDecryptNonLinearABC(encryptedText, result[0], result[1], result[2]);

                        if (NonLinear == originalText)
                        {
                            MessageBox.Show($"Нелінійний ключ: {result[0]}, {result[1]}, {result[2]}");
                            break;
                        }
                    }
                }
            }

            if (Language && radioButton7.Checked)
            {
                bool doit = true;
                int text_pos = -1;
                string res = "";
                while (doit)
                {
                    text_pos++;

                    if (ua_lang_lower.IndexOf(encryptedText[text_pos]) != -1)
                    {
                        int temp = (ua_lang_lower.IndexOf(encryptedText[text_pos]) - ua_lang_lower.IndexOf(originalText[text_pos])) % ua_lang_lower.Length;
                        if (temp < 0)
                        {
                            temp += ua_lang_lower.Length;
                        }
                        res += ua_lang_lower[temp];

                        string org = t.UKRDecryptMotto(encryptedText, res);

                        if (org == originalText)
                        {
                            MessageBox.Show($"Гасло ключ: {res}");
                            break;
                        }
                    }
                }
            }
        }

        private void гамуванняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            button3.Visible = false;
            numericUpDown1.Visible = false;
            groupBox3.Visible = false;
            textBox4.Visible = false;
            groupBox2.Visible = false;
            groupBox1.Visible = false;

            groupBox4.Visible = true;
            button6.Visible = true;
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            label8.Visible = true;
            textBox5.Visible = true;
            button5.Visible = true;
            button8.Visible = true;
            button9.Visible = true;

            button7.Visible = false;
            button4.Visible = false;
            label9.Visible = false;
            numericUpDown2.Visible = false;
            button10.Visible = false;
            button6.Visible = false;
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            button4.Visible = true;
            label9.Visible = true;
            numericUpDown2.Visible = true;
            button7.Visible = true;
            label11.Visible = true;
            button10.Visible = true;
            button6.Visible = true;
            label8.Visible = true;
            textBox5.Visible = true;

            button9.Visible = false;
            button5.Visible = false;
            button8.Visible = false;
            label11.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GammaCipher gammaCipher = new GammaCipher();
            string plaintext = richTextBox1.Text;
            textBox5.Text = gammaCipher.GenerateKey(plaintext);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private byte[] _key;
        private int _keyLength;

        public string GenerateKeys(string text)
        {
            Random random = new Random();
            string key = "";
            for (int i = 0; i < text.Length; i++)
            {
                int asciiSymbol = random.Next(10, 1000);
                while ((char)asciiSymbol == text[i])
                {
                    asciiSymbol = random.Next(10, 1000);
                }
                key += (char)asciiSymbol;
            }
            return key;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            GammaCipher gammaCipher = new GammaCipher();
            string plaintext = richTextBox1.Text;

            int numKeys = (int)numericUpDown2.Value;
            string filename = filePath;

            using (StreamWriter writer = new StreamWriter(filename))
            {
                for (int i = 0; i < numKeys; i++)
                {
                    string key = gammaCipher.GenerateKey(plaintext);
                    writer.WriteLine(key);
                }
            }

            MessageBox.Show($"Згенеровано {numKeys} ключів і збережено їх до {filename}");
        }

        public string filePath;
        public int lastLineIndex = 0;
        public string fileName;
        public string shortFileName;

        private void button4_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    string fileName = openFileDialog.FileName;
                    string shortFileName = Path.GetFileName(fileName);
                    label9.Text = shortFileName;
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {

            string gamma = textBox5.Text;
            string plaintext = richTextBox1.Text;
            string result = GEncrypt(plaintext, gamma);
            richTextBox2.Text = result;
        }

        public string GEncrypt(string plaintext, string gamma)
        {
            string result = "";
            for (int i = 0; i < plaintext.Length; i++)
            {
                result += (char)(plaintext[i] ^ gamma[i % gamma.Length]);
            }

            return result;
        }

        public string GDecrypt(string plaintext, string gamma)
        {
            string result = "";
            for (int i = 0; i < plaintext.Length; i++)
            {
                result += (char)(plaintext[i] ^ gamma[i % gamma.Length]);
            }

            return result;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string gamma = textBox5.Text;
            string plaintext = richTextBox1.Text;
            string result = GDecrypt(plaintext, gamma);
            richTextBox2.Text = result;
        }

        private int index = 0;

        private void button6_Click_1(object sender, EventArgs e)
        {
            string gamma_file;
            if (File.Exists(filePath))
            {
                string[] gammaLines = File.ReadAllLines(filePath);
                gamma_file = gammaLines[index % gammaLines.Length];
                index++;
                textBox5.Text = gamma_file;         
            }
            else
            {
                MessageBox.Show("Файла не існує", "Помилка");
                return;
            }
            string plaintext = richTextBox1.Text;
            string result = GEncrypt(plaintext, gamma_file);
            richTextBox2.Text = result;
        }
   
        private int index2 = 0;

        private void button10_Click(object sender, EventArgs e)
        {

            string gamma_file;
            if (File.Exists(filePath))
            {
                string[] gammaLines = File.ReadAllLines(filePath);
                gamma_file = gammaLines[index2 % gammaLines.Length];
                index2++;
                textBox5.Text = gamma_file;
            }
            else
            {
                MessageBox.Show("Файла не існує", "Помилка");
                return;
            }
            string plaintext = richTextBox1.Text;
            string result = GDecrypt(plaintext, gamma_file);
            richTextBox2.Text = result;
        }

        private void задачаРюкзакаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KnapsackTask knapsack = new KnapsackTask();
            knapsack.ShowDialog(this);
        }
    }
}