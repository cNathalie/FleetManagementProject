using Microsoft.VisualStudio.TestTools.UnitTesting;
using FM.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.DomainTests.Validators
{
    [TestClass()]
    public class VINValidatorTests
    {
        [TestMethod()]
        public void IsValidVINTest()
        {
            string? invalidVIN1 = string.Empty;
            string? invalidVIN2 = "abc58946231254874"; // length ok, but no capital letters
            string? invalidVIN3 = "ABC5894623125487"; //length not ok
            string? validVIN1 = "ABC5894623D125487";


            Assert.IsFalse(VINValidator.IsValidVIN(invalidVIN1));
            Assert.IsFalse(VINValidator.IsValidVIN(invalidVIN2));
            Assert.IsFalse(VINValidator.IsValidVIN(invalidVIN3));
            Assert.IsTrue(VINValidator.IsValidVIN(validVIN1));
        }
    }
}