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
    public class TrithemiusTests
    {
        Trithemius t = new Trithemius();

        [TestMethod()]
        public void TestEncryptLinearAB()
        {
            string input = "kravets";
            string expected = "nwhepgh";

            int step1 = 2;
            int step2 = 3;
            string output = t.EncryptLinearAB(input, step1, step2);
            Assert.AreEqual(expected, output);
        }

        [TestMethod()]
        public void TestUKREncryptLinearAB()
        {
            string input = "кравець";
            string expected = "нхєінеї";

            int step1 = 2;
            int step2 = 3;
            string output = t.UKREncryptLinearAB(input, step1, step2);
            Assert.AreEqual(expected, output);
        }

        [TestMethod()]
        public void TestDecryptLinearAB()
        {
            string input = "nwhepgh";
            string expected = "kravets";

            int step1 = 2;
            int step2 = 3;
            string output = t.DecryptLinearAB(input, step1, step2);
            Assert.AreEqual(expected, output);
        }

        [TestMethod()]
        public void TestUKRDecryptLinearAB()
        {
            string input = "нхєінеї";
            string expected = "кравець";

            int step1 = 2;
            int step2 = 3;
            string output = t.UKRDecryptLinearAB(input, step1, step2);
            Assert.AreEqual(expected, output);
        }

        [TestMethod()]
        public void TestEncryptNonLinearABC()
        {
            string input = "kravets";
            string expected = "oasaaki";

            int step1 = 2;
            int step2 = 3;
            int step3 = 4;

            string output = t.EncryptNonLinearABC(input, step1, step2, step3);
            Assert.AreEqual(expected, output);
        }

        [TestMethod()]
        public void TestUKREncryptNonLinearABC()
        {
            string input = "кравець";
            string expected = "ощоасщх";

            int step1 = 2;
            int step2 = 3;
            int step3 = 4;

            string output = t.UKREncryptNonLinearABC(input, step1, step2, step3);
            Assert.AreEqual(expected, output);
        }

        [TestMethod()]
        public void TestDecryptNonLinearABC()
        {
            string input = "oasaaki";
            string expected = "kravets";

            int step1 = 2;
            int step2 = 3;
            int step3 = 4;

            string output = t.DecryptNonLinearABC(input, step1, step2, step3);
            Assert.AreEqual(expected, output);
        }

        [TestMethod()]
        public void TestUKRDecryptNonLinearABC()
        {
            string input = "ощоасщх";
            string expected = "кравець";

            int step1 = 2;
            int step2 = 3;
            int step3 = 4;

            string output = t.UKRDecryptNonLinearABC(input, step1, step2, step3);
            Assert.AreEqual(expected, output);
        }

        [TestMethod()]
        public void TestEncryptMotto()
        {
            string input = "kravets";
            string pass = "olia";
            string expected = "zcjwtea";

            string output = t.EncryptMotto(input, pass);
            Assert.AreEqual(expected, output);
        }

        [TestMethod()]
        public void TestDecryptMotto()
        {
            string input = "zcjwtea";
            string pass = "olia";
            string expected = "kravets";

            string output = t.DecryptMotto(input, pass);
            Assert.AreEqual(expected, output);
        }
    }
}