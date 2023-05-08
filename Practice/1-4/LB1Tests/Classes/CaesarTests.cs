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
    public class CaesarTests
    {
        [TestMethod()]
        public void TestEngEncryption()
        {
            string input = "Olia";
            string expected = "Rold";
            int step = 3;
            string output = Caesar.EngEncrypt(input, step);
            Assert.AreEqual(expected, output);
        }

        [TestMethod]
        public void TestEngEncryptionFail()
        {
            string input = "Olia";
            string expected = "old";
            int step = 3;
            string output = Caesar.EngEncrypt(input, step);
            Assert.AreNotEqual(expected, output);
        }

        [TestMethod]
        public void TestUkrEncryption()
        {
            string input = "Кравець";
            string expected = "ФьиїмВЕ";
            int step = 10;
            string output = Caesar.UkrEncrypt(input, step);
            Assert.AreEqual(expected, output);
        }

        [TestMethod]
        public void TestUkrEncryptionFail()
        {
            string input = "Кравець";
            string expected = "їмВЕ";
            int step = 10;
            string output = Caesar.UkrEncrypt(input, step);
            Assert.AreNotEqual(expected, output);
        }

        [TestMethod]
        public void TestEngDecryption()
        {
            string input = "Rold";
            string expected = "Olia";
            int step = 3;
            string output = Caesar.EngDecrypt(input, step);
            Assert.AreEqual(expected, output);
        }

        [TestMethod]
        public void TestUkrDecryption()
        {
            string input = "ФьиїмВЕ";
            string expected = "Кравець";
            int step = 10;
            string output = Caesar.UkrDecrypt(input, step);
            Assert.AreEqual(expected, output);
        }

    }
}