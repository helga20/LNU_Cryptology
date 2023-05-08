using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1.Classes
{
    public class KnapsackCipher
    {
        private List<int> privateKey;
        private int m;
        private int n;

        public KnapsackCipher() { }

        public KnapsackCipher(List<int> privateKey, int m, int n)
        {
            this.privateKey = privateKey;
            this.m = m;
            this.n = n;
        }

        public List<int> PrivateKey
        {
            get { return privateKey; }
            set { privateKey = value; }
        }

        public int M
        {
            get { return m; }
            set { m = value; }
        }

        public int N
        {
            get { return n; }
            set { n = value; }
        }

        public void GenerateKeys()
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
        }

        public string Encrypt(string message)
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

        public string Decrypt(string message)
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
    }
}
