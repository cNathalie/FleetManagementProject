using FM.Domain.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FM.DomainTests.Validators
{
    [TestClass()]
    public class RRNValidatorTests
    {
        [TestMethod()]
        public void IsValidRRNTest()
        {
            string? invalidRRN1 = null;
            string? invalidRRN2 = "12345678901";
            string? validRRN = "67110716938";

            Assert.IsFalse(RRNValidator.IsValidRRN(invalidRRN1!));
            Assert.IsFalse(RRNValidator.IsValidRRN(invalidRRN2!));
            Assert.IsTrue(RRNValidator.IsValidRRN(validRRN!));
        }
    }
}