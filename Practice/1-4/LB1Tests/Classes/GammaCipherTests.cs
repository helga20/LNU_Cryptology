using Microsoft.VisualStudio.TestTools.UnitTesting;
using LB1.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1.Classes.Tests
{
    [TestClass()]
    public class GammaCipherTests
    {
        [TestMethod()]
        public void GEncryptTest()
        {
            GammaCipher g = new GammaCipher();
            string input = "test";
            string expected = "ϊƍŢν";
            string gamma = "ξǨđω";
            string output = g.GEncrypto(input, gamma);
            Assert.AreEqual(expected, output);
        }

        [TestMethod()]
        public void GDecryptTest()
        {
            GammaCipher g = new GammaCipher();
            string input = "ϊƍŢν";
            string expected = "test";
            string gamma = "ξǨđω";
            string output = g.GDecrypto(input, gamma);
            Assert.AreEqual(expected, output);
        }
    }
}