using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace LB1
{
    public partial class ImageEncryption : Form
    {
        public ImageEncryption()
        {
            InitializeComponent();
        }
        public int numericUpDownINT { get; set; }
        public bool Crypt { get; set; } // true - зашифрувати; false - розшифрувати
        public bool ChangeCrypt { get; set; } // true - змінювався радіобатон Crypt; false - не змінювався радіобатон Crypt
        public bool Language { get; set; } // true - українська мова; false - англійська мова 
        public bool ChangeLanguage { get; set; } // true - змінювалася мова; false - не змінювалася мова 

        private static RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog1.FileName;
                byte[] imageBytes = File.ReadAllBytes(imagePath);
                string base64String = Convert.ToBase64String(imageBytes);
                textBox1.Text = base64String;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string base64String = textBox1.Text;
                byte[] imageBytes = Convert.FromBase64String(base64String);
                MemoryStream memoryStream = new MemoryStream(imageBytes);
                Image image = Image.FromStream(memoryStream);
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg;*.jpeg";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    image.Save(saveFileDialog.FileName, ImageFormat.Png);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownINT = (int)numericUpDown1.Value;
        }
        const string ENG_LETTERS = " ABCDEFGHIJKLMNOPQRSTUVWXYZ";
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

        public static string EngEncrypt(string plainMessage, int key) => ENG_Code(plainMessage, key);

        public static string EngDecrypt(string encryptedMessage, int key) => ENG_Code(encryptedMessage, -key);
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Додайте картинку для шифрування!");
            }
            else
            {
                textBox2.Text = EngEncrypt(textBox1.Text, numericUpDownINT);
            }       
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Text = EngDecrypt(textBox2.Text, numericUpDownINT);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "All files (*.*)|*.*";
                dialog.InitialDirectory = "C:\\";
                dialog.Title = "Виберіть файл";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string fname = dialog.FileName;
                    Image image = Image.FromFile(dialog.FileName);
                    MemoryStream memoryStream = new MemoryStream();
                    image.Save(memoryStream, ImageFormat.Png);
                    byte[] imageBytes = memoryStream.ToArray();
                    string base64String = Convert.ToBase64String(imageBytes);
                    textBox1.Text = base64String;       
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка - " + ex.Message);
            }
        }
    }
}

