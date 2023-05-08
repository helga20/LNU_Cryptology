using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1.Classes
{
    public class Caesar
    {
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
    }
}
