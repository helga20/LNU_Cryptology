using LB1.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LB1
{
    public partial class KnapsackTask : Form
    {
        public KnapsackTask()
        {
            InitializeComponent();
        }

        private void створитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.WriteAllText("file.txt", назваФайлуToolStripMenuItem.Text);
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

        private void проСистемуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Криптосистема\n Розробник: Кравець Ольга\n Група: ПМО-31\n Рік: 2023");

        }

        private void допомогаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Ctrl+H - допомога\n Ctrl+C - створити файл\n Ctrl+O - відкрити файл\n Ctrl+S - зберегти файл\n " +
               "Ctrl+P - друк файлу\n Alt+A - атака методом 'грубої сили'\n Alt+F - частотні таблиці\n" +
               " Alt+P - шифрувати зображення\n Alt+D - інформація про розробника\n Alt+X - вихід\n " +
               "Alt+T - атака на шифр Тритеміуса");
        }

        private void вихідЗіСистемиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Здійснити вихід зі системи?", "Увага!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        public string EncryptKnapsack(string message)
        {
            List<int> publicKey = new List<int>();
            for (int i = 0; i < privateKey.Count; i++)
            {
                publicKey.Add((privateKey[i] * n) % m);
            }

            List<int> sums = new List<int>();
            foreach (char c in message)
            {
                int binary = (int)c;
                int sum = 0;
                for (int i = 0; i < 8; i++)
                {
                    sum += ((binary >> i) & 1) * publicKey[7 - i];
                }
                sums.Add(sum);
            }

            return string.Join(",", sums.Select(x => x.ToString()));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string message = richTextBox1.Text;
            string encryptedMessage = EncryptKnapsack(message);
            richTextBox2.Text = encryptedMessage;
        }

        private List<int> privateKey;
        private int m;
        private int n;
        
        private void GenerateKeysKnapsack()
        {
            Random rand = new Random();
            privateKey = new List<int>();
            privateKey.Add(2);
            privateKey.Add(3);
            privateKey.Add(7);
            for (int i = 3; i < 8; i++)
            {
                privateKey.Add(2 * privateKey[i - 1] + rand.Next(10));
            }
            n = rand.Next(100);
            m = n * 20 + rand.Next(n * 10);

            textBox1.Text = n.ToString();
            textBox2.Text = m.ToString();
            textBox3.Text = string.Join(", ", privateKey);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            GenerateKeysKnapsack();
        }

        public string DecryptKnapsack(string message)
        {
            List<int> values = message.Split(',').Select(x => int.Parse(x)).ToList();
            int temp_modulo = m + 1;
            int t = 0;
            while (true)
            {
                if ((temp_modulo) % n == 0)
                {
                    t = temp_modulo / n;
                    break;
                }
                else
                    temp_modulo += m;
                if (temp_modulo < 0)
                {
                    Console.WriteLine("Error");
                    break;
                }
            }

            List<char> chars = new List<char>();
            foreach (int value in values)
            {
                int sum = (value * t) % m;
                int[] bits = new int[8];
                for (int i = 7; i >= 0; i--)
                {
                    if (sum >= privateKey[i])
                    {
                        bits[i] = 1;
                        sum -= privateKey[i];
                    }
                }
                int num = 0;
                for (int i = 0; i < bits.Length; i++)
                {
                    num = (num << 1) | bits[i];
                }
                chars.Add((char)num);
            }

            return new string(chars.ToArray());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string message = richTextBox2.Text;
            string decryptedMessage = DecryptKnapsack(message);
            richTextBox3.Text = decryptedMessage;
        }

        private void KnapsackTask_Load(object sender, EventArgs e)
        {

        }
    }
}













