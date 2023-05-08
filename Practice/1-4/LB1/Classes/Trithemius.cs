using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace LB1.Classes
{
    public class Trithemius
    {
        const string ENG_LETTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string UKR_LETTERS = "АБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯ";

        public string EncryptLinearAB(string text, int A, int B)
        {
            var retVal = "";
            var lower_a = ENG_LETTERS.ToLower();
            for (int i = 0; i < text.Length; i++)
            {
                int pos_lower = lower_a.IndexOf(text[i]);
                int pos_upper = ENG_LETTERS.IndexOf(text[i]);
                if (lower_a.Contains(text[i]))
                {
                    if (pos_lower != -1)
                    {
                        int text1 = (A * i + B + pos_lower) % lower_a.Length;
                        
                        if (text1 < 0)
                        {
                            text1 += lower_a.Length;
                        }
                        retVal += lower_a[text1];

                    }
                }
                else if (ENG_LETTERS.Contains(text[i]))
                {
                    if (pos_upper != -1)
                    {
                        int text1 = (A * i + B + pos_upper) % lower_a.Length;
                        if (text1 < 0)
                        {
                            text1 += ENG_LETTERS.Length;
                        }
                        retVal += ENG_LETTERS[text1];

                    }
                }
                else
                {
                    retVal += text[i];
                }
            }
            return retVal;
        }

        public string UKREncryptLinearAB(string text, int A, int B)
        {
            var retVal = "";
            var lower_a = UKR_LETTERS.ToLower();
            for (int i = 0; i < text.Length; i++)
            {
                int pos_lower = lower_a.IndexOf(text[i]);
                int pos_upper = UKR_LETTERS.IndexOf(text[i]);
                if (lower_a.Contains(text[i]))
                {
                    if (pos_lower != -1)
                    {
                        int text1 = (A * i + B + pos_lower) % lower_a.Length;

                        if (text1 < 0)
                        {
                            text1 += lower_a.Length;
                        }
                        retVal += lower_a[text1];

                    }
                }
                else if (UKR_LETTERS.Contains(text[i]))
                {
                    if (pos_upper != -1)
                    {
                        int text1 = (A * i + B + pos_upper) % lower_a.Length;
                        if (text1 < 0)
                        {
                            text1 += UKR_LETTERS.Length;
                        }
                        retVal += UKR_LETTERS[text1];

                    }
                }
                else
                {
                    retVal += text[i];
                }
            }
            return retVal;
        }


        public string DecryptLinearAB(string text, int A, int B)
        {
            var retVal = "";
            var lower_a = ENG_LETTERS.ToLower();
            for (int i = 0; i < text.Length; i++)
            {
                int pos_lower = lower_a.IndexOf(text[i]);
                int pos_upper = ENG_LETTERS.IndexOf(text[i]);
                if (lower_a.Contains(text[i]))
                {
                    if (pos_lower != -1)
                    {
                        int text1 = (pos_lower + lower_a.Length - ((A * i + B) % lower_a.Length)) % lower_a.Length;
                        if (text1 < 0)
                        {
                            text1 += lower_a.Length;
                        }
                        retVal += lower_a[text1];
                    }
                }
                else if (ENG_LETTERS.Contains(text[i]))
                {
                    if (pos_upper != -1)
                    {
                        int text1 = (pos_upper + lower_a.Length - ((A * i + B) % lower_a.Length)) % lower_a.Length;
                        if (text1 < 0)
                        {
                            text1 += ENG_LETTERS.Length;
                        }
                        retVal += ENG_LETTERS[text1];
                    }
                }
                else
                {
                    retVal += text[i];
                }
            }
            return retVal;
        }

        public string UKRDecryptLinearAB(string text, int A, int B)
        {
            var retVal = "";
            var lower_a = UKR_LETTERS.ToLower();
            for (int i = 0; i < text.Length; i++)
            {
                int pos_lower = lower_a.IndexOf(text[i]);
                int pos_upper = UKR_LETTERS.IndexOf(text[i]);
                if (lower_a.Contains(text[i]))
                {
                    if (pos_lower != -1)
                    {
                        int text1 = (pos_lower + lower_a.Length - ((A * i + B) % lower_a.Length)) % lower_a.Length;
                        if (text1 < 0)
                        {
                            text1 += lower_a.Length;
                        }
                        retVal += lower_a[text1];
                    }
                }
                else if (UKR_LETTERS.Contains(text[i]))
                {
                    if (pos_upper != -1)
                    {
                        int text1 = (pos_upper + lower_a.Length - ((A * i + B) % lower_a.Length)) % lower_a.Length;
                        if (text1 < 0)
                        {
                            text1 += UKR_LETTERS.Length;
                        }
                        retVal += UKR_LETTERS[text1];
                    }
                }
                else
                {
                    retVal += text[i];
                }
            }
            return retVal;
        }

        public string EncryptNonLinearABC(string text, int A, int B, int C)
        {
            var retVal = "";
            var lower_a = ENG_LETTERS.ToLower();
            for (int i = 0; i < text.Length; i++)
            {
                int pos_lower = lower_a.IndexOf(text[i]);
                int pos_upper = ENG_LETTERS.IndexOf(text[i]);
                if (lower_a.Contains(text[i]))
                {
                    if (pos_lower != -1)
                    {
                        int text1 = (A * i * i + B * i + C + pos_lower) % lower_a.Length;

                        if (text1 < 0)
                        {
                            text1 += lower_a.Length;
                        }
                        retVal += lower_a[text1];

                    }
                }
                else if (ENG_LETTERS.Contains(text[i]))
                {
                    if (pos_upper != -1)
                    {
                        int text1 = (A * i * i + B * i + C + pos_upper) % lower_a.Length;
                        if (text1 < 0)
                        {
                            text1 += ENG_LETTERS.Length;
                        }
                        retVal += ENG_LETTERS[text1];

                    }
                }
                else
                {
                    retVal += text[i];
                }
            }
            return retVal;
        }

        public string UKREncryptNonLinearABC(string text, int A, int B, int C)
        {
            var retVal = "";
            var lower_a = UKR_LETTERS.ToLower();
            for (int i = 0; i < text.Length; i++)
            {
                int pos_lower = lower_a.IndexOf(text[i]);
                int pos_upper = UKR_LETTERS.IndexOf(text[i]);
                if (lower_a.Contains(text[i]))
                {
                    if (pos_lower != -1)
                    {
                        int text1 = (A * i * i + B * i + C + pos_lower) % lower_a.Length;

                        if (text1 < 0)
                        {
                            text1 += lower_a.Length;
                        }
                        retVal += lower_a[text1];

                    }
                }
                else if (UKR_LETTERS.Contains(text[i]))
                {
                    if (pos_upper != -1)
                    {
                        int text1 = (A * i * i + B * i + C + pos_upper) % lower_a.Length;
                        if (text1 < 0)
                        {
                            text1 += UKR_LETTERS.Length;
                        }
                        retVal += UKR_LETTERS[text1];

                    }
                }
                else
                {
                    retVal += text[i];
                }
            }
            return retVal;
        }

        public string DecryptNonLinearABC(string text, int A, int B, int C)
        {
            var retVal = "";
            var lower_a = ENG_LETTERS.ToLower();
            for (int i = 0; i < text.Length; i++)
            {
                int pos_lower = lower_a.IndexOf(text[i]);
                int pos_upper = ENG_LETTERS.IndexOf(text[i]);
                if (lower_a.Contains(text[i]))
                {
                    if (pos_lower != -1)
                    {
                        int text1 = (pos_lower + lower_a.Length - ((A * i * i + B * i + C) % lower_a.Length)) % lower_a.Length;
                        if (text1 < 0)
                        {
                            text1 += lower_a.Length;
                        }
                        retVal += lower_a[text1];
                    }
                }
                else if (ENG_LETTERS.Contains(text[i]))
                {
                    if (pos_upper != -1)
                    {
                        int text1 = (pos_upper + lower_a.Length - ((A * i * i + B * i + C) % lower_a.Length)) % lower_a.Length;
                        if (text1 < 0)
                        {
                            text1 += ENG_LETTERS.Length;
                        }
                        retVal += ENG_LETTERS[text1];
                    }
                }
                else
                {
                    retVal += text[i];
                }
            }
            return retVal;
        }

        public string UKRDecryptNonLinearABC(string text, int A, int B, int C)
        {
            var retVal = "";
            var lower_a = UKR_LETTERS.ToLower();
            for (int i = 0; i < text.Length; i++)
            {
                int pos_lower = lower_a.IndexOf(text[i]);
                int pos_upper = UKR_LETTERS.IndexOf(text[i]);
                if (lower_a.Contains(text[i]))
                {
                    if (pos_lower != -1)
                    {
                        int text1 = (pos_lower + lower_a.Length - ((A * i * i + B * i + C) % lower_a.Length)) % lower_a.Length;
                        if (text1 < 0)
                        {
                            text1 += lower_a.Length;
                        }
                        retVal += lower_a[text1];
                    }
                }
                else if (ENG_LETTERS.Contains(text[i]))
                {
                    if (pos_upper != -1)
                    {
                        int text1 = (pos_upper + lower_a.Length - ((A * i * i + B * i + C) % lower_a.Length)) % lower_a.Length;
                        if (text1 < 0)
                        {
                            text1 += UKR_LETTERS.Length;
                        }
                        retVal += UKR_LETTERS[text1];
                    }
                }
                else
                {
                    retVal += text[i];
                }
            }
            return retVal;
        }

        public string EncryptMotto(string plainText, string password)
        {
            string ciphertext = "";

            string password_lower = password.ToLower();
            string password_upper = password.ToUpper();

            int password_pos = 0;

            string ENG_LETTER_UP = "*ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string ENG_LETTER_LOW = " abcdefghijklmnopqrstuvwxyz";

            for (int i = 0; i < plainText.Length; i++)
            {
                int pos_upper = ENG_LETTER_UP.IndexOf(plainText[i]);
                int pos_lower = ENG_LETTER_LOW.IndexOf(plainText[i]);

                if (ENG_LETTER_LOW.Contains(plainText[i]))
                {
                    if (pos_lower != -1)
                    {
                        int text = ENG_LETTER_LOW.IndexOf(password_lower[password_pos]) + pos_lower;
                        text %= ENG_LETTER_LOW.Length;

                        if (text < 0) text += ENG_LETTER_LOW.Length;

                        ciphertext += ENG_LETTER_LOW[text];

                        password_pos++;

                        if (password_pos >= password.Length) password_pos = 0;
                    }
                }
                else if (ENG_LETTER_UP.Contains(plainText[i]))
                {
                    if (pos_upper != -1)
                    {
                        int text = ENG_LETTER_UP.IndexOf(password_upper[password_pos]) + pos_upper;
                        text %= ENG_LETTER_UP.Length;

                        if (text < 0) text += ENG_LETTER_UP.Length;

                        ciphertext += ENG_LETTER_UP[text];

                        password_pos++;

                        if (password_pos >= password.Length) password_pos = 0;
                    }
                }
                else
                {
                    ciphertext += plainText[i];
                }
            }
            return ciphertext;
        }
        public string DecryptMotto(string plainText, string password)
        {
            string ciphertext = "";

            string password_lower = password.ToLower();
            string password_upper = password.ToUpper();

            int password_pos = 0;

            const string ENG_LETTER_UP = "*ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string ENG_LETTER_LOW = " abcdefghijklmnopqrstuvwxyz";

            for (int i = 0; i < plainText.Length; i++)
            {
                int pos_upper = ENG_LETTER_UP.IndexOf(plainText[i]);
                int pos_lower = ENG_LETTER_LOW.IndexOf(plainText[i]);

                if (ENG_LETTER_LOW.Contains(plainText[i]))
                {
                    if (pos_lower != -1)
                    {
                        int text = pos_lower - ENG_LETTER_LOW.IndexOf(password_lower[password_pos]);
                        text %= ENG_LETTER_LOW.Length;

                        if (text < 0) text += ENG_LETTER_LOW.Length;

                        ciphertext += ENG_LETTER_LOW[text];

                        password_pos++;

                        if (password_pos >= password.Length) password_pos = 0;
                    }
                }
                else if (ENG_LETTER_UP.Contains(plainText[i]))
                {
                    if (pos_upper != -1)
                    {
                        int text = pos_upper - ENG_LETTER_UP.IndexOf(password_upper[password_pos]);
                        text %= ENG_LETTER_UP.Length;

                        if (text < 0) text += ENG_LETTER_UP.Length;

                        ciphertext += ENG_LETTER_UP[text];

                        password_pos++;

                        if (password_pos >= password.Length) password_pos = 0;
                    }
                }
                else
                {
                    ciphertext += plainText[i];
                }
            }
            return ciphertext;
        }

        public string UKREncryptMotto(string plainText, string password)
        {
            string ciphertext = "";

            string password_lower = password.ToLower();
            string password_upper = password.ToUpper();

            int password_pos = 0;

            string ENG_LETTER_UP = "*АБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯ";
            string ENG_LETTER_LOW = " абвгґдеєжзиіїйклмнопрстуфхцчшщьюя";

            for (int i = 0; i < plainText.Length; i++)
            {
                int pos_upper = ENG_LETTER_UP.IndexOf(plainText[i]);
                int pos_lower = ENG_LETTER_LOW.IndexOf(plainText[i]);

                if (ENG_LETTER_LOW.Contains(plainText[i]))
                {
                    if (pos_lower != -1)
                    {
                        int text = ENG_LETTER_LOW.IndexOf(password_lower[password_pos]) + pos_lower;
                        text %= ENG_LETTER_LOW.Length;

                        if (text < 0) text += ENG_LETTER_LOW.Length;

                        ciphertext += ENG_LETTER_LOW[text];

                        password_pos++;

                        if (password_pos >= password.Length) password_pos = 0;
                    }
                }
                else if (ENG_LETTER_UP.Contains(plainText[i]))
                {
                    if (pos_upper != -1)
                    {
                        int text = ENG_LETTER_UP.IndexOf(password_upper[password_pos]) + pos_upper;
                        text %= ENG_LETTER_UP.Length;

                        if (text < 0) text += ENG_LETTER_UP.Length;

                        ciphertext += ENG_LETTER_UP[text];

                        password_pos++;

                        if (password_pos >= password.Length) password_pos = 0;
                    }
                }
                else
                {
                    ciphertext += plainText[i];
                }
            }
            return ciphertext;
        }
        public string UKRDecryptMotto(string plainText, string password)
        {
            string ciphertext = "";

            string password_lower = password.ToLower();
            string password_upper = password.ToUpper();

            int password_pos = 0;

            string ENG_LETTER_UP = "*АБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯ";
            string ENG_LETTER_LOW = " абвгґдеєжзиіїйклмнопрстуфхцчшщьюя";

            for (int i = 0; i < plainText.Length; i++)
            {
                int pos_upper = ENG_LETTER_UP.IndexOf(plainText[i]);
                int pos_lower = ENG_LETTER_LOW.IndexOf(plainText[i]);

                if (ENG_LETTER_LOW.Contains(plainText[i]))
                {
                    if (pos_lower != -1)
                    {
                        int text = pos_lower - ENG_LETTER_LOW.IndexOf(password_lower[password_pos]);
                        text %= ENG_LETTER_LOW.Length;

                        if (text < 0) text += ENG_LETTER_LOW.Length;

                        ciphertext += ENG_LETTER_LOW[text];

                        password_pos++;

                        if (password_pos >= password.Length) password_pos = 0;
                    }
                }
                else if (ENG_LETTER_UP.Contains(plainText[i]))
                {
                    if (pos_upper != -1)
                    {
                        int text = pos_upper - ENG_LETTER_UP.IndexOf(password_upper[password_pos]);
                        text %= ENG_LETTER_UP.Length;

                        if (text < 0) text += ENG_LETTER_UP.Length;

                        ciphertext += ENG_LETTER_UP[text];

                        password_pos++;

                        if (password_pos >= password.Length) password_pos = 0;
                    }
                }
                else
                {
                    ciphertext += plainText[i];
                }
            }
            return ciphertext;
        }

    }
}
