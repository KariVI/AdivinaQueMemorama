using AdivinaQue.Client.Control;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest
{
    [TestClass]
    public class ValidateTest
    {
        private Validate validate = new Validate();

        [TestMethod]
        public void TestValidateAlphanumeric()
        {
            string field = "ESIUA6sw00";
            Assert.IsTrue(validate.ValidationAlphanumeric(field));

        }

        [TestMethod]
        public void TestValidateAlphanumericFailed()
        {
            string field = "ES%&556IUA";
            Assert.IsFalse(validate.ValidationAlphanumeric(field));

        }

        [TestMethod]
        public void TestValidateAlphabetic()
        {
            string field = "Karina Valdes Iglesias";
            Assert.IsTrue(validate.ValidationString(field));

        }

        [TestMethod]
        public void TestValidateAlphabeticFailed()
        {
            string field = "Karina @12 Valdes /// Iglesias";
            Assert.IsFalse(validate.ValidationString(field));

        }

        [TestMethod]
        public void TestValidateEmail()
        {
            string field = "angelicaiglesiasestefan@gmail.com";
            Assert.IsTrue(validate.ValidationEmail(field));

        }

        [TestMethod]
        public void TestValidateEmailFailed()
        {
            string field = "angelicaiglesiasestefangmail;com";
            Assert.IsFalse(validate.ValidationEmail(field));

        }

    }
}
