using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1.Classes
{
    public class BrushForce
    {
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
    }
}
