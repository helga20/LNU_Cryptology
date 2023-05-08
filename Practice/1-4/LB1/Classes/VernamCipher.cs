using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1.Classes
{
    public class VernamCipher
    {
        public List<string> GenerateGammas(int numGammas, int gammaLength)
        {
            List<string> gammas = new List<string>();
            Random random = new Random();

            for (int i = 0; i < numGammas; i++)
            {
                char[] gamma = new char[gammaLength];
                for (int j = 1; j < gammaLength; j++)
                {
                    gamma[j] = (char)random.Next(0, 128);
                }
                gammas.Add(new string(gamma));
            }

            return gammas;
        }

        public void WriteGammasToFile(string path, int numGammas, int gammaLength)
        {
            List<string> gammas = GenerateGammas(numGammas, gammaLength);

            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (string gamma in gammas)
                {
                    writer.WriteLine(gamma);
                }
            }
        }

    }
}
