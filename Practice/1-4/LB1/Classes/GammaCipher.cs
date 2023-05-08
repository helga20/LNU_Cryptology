using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LB1.Classes
{
    public class GammaCipher
    {
        public string GenerateKey(string text)
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

        public string GEncrypto(string plaintext, string gamma)
        {
            string result = "";
            for (int i = 0; i < plaintext.Length; i++)
            {
                result += (char)(plaintext[i] ^ gamma[i % gamma.Length]);
            }

            return result;
        }

        public string GDecrypto(string plaintext, string gamma)
        {
            string result = "";
            for (int i = 0; i < plaintext.Length; i++)
            {
                result += (char)(plaintext[i] ^ gamma[i % gamma.Length]);
            }

            return result;
        }

    }
}
