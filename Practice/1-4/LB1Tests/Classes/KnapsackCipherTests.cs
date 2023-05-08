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
    public class KnapsackCipherTests
    {
        [TestMethod()]
        public void NGetterSetterTest()
        {
            KnapsackCipher cipher = new KnapsackCipher();
            cipher.N = 50;
            int expected = 50;
            int actual = cipher.N;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void MGetterSetterTest()
        {
            KnapsackCipher cipher = new KnapsackCipher();
            cipher.M = 100;
            int expected = 100;
            int actual = cipher.M;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void EncryptDecryptTest()
        {
            KnapsackCipher cipher = new KnapsackCipher();
            cipher.PrivateKey = new List<int> { 1, 2, 3 };
            List<int> expected = new List<int> { 1, 2, 3 };
            List<int> actual = cipher.PrivateKey;
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}