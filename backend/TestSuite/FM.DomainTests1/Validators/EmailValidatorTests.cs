using FM.Domain.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FM.DomainTests.Validators
{
    [TestClass()]
    public class EmailValidatorTests
    {
        [TestMethod()]
        public void IsValidEmailTest()
        {
            string? invalidEmail1 = null;
            string? invalidEmail2 = "worstjes.be";
            string? invalidEmail3 = "@";
            string? invalidEmail4 = "@worstjes";
            string? invalidEmail5 = "    ";
            string? validEmail1 = "tester@worstjes.be";

            Assert.IsFalse(EmailValidator.IsValidEmail(invalidEmail1!));
            Assert.IsFalse(EmailValidator.IsValidEmail(invalidEmail2));
            Assert.IsFalse(EmailValidator.IsValidEmail(invalidEmail3));
            Assert.IsFalse(EmailValidator.IsValidEmail(invalidEmail4));
            Assert.IsFalse(EmailValidator.IsValidEmail(invalidEmail5));
            Assert.IsTrue(EmailValidator.IsValidEmail(validEmail1));
        }
    }
}