using FM.Domain.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FM.DomainTests.Validators
{
    [TestClass()]
    public class PasswordValidatorTests
    {
        [TestMethod()]
        public void IsValidPasswordTest()
        {
            string? invalidPassword1 = null;
            string? invalidPassword2 = "   r";
            string? invalidPassword3 = "worstjes";
            string? validPassword1 = "test1";
            string? validPassword2 = "2test";

            Assert.IsFalse(PasswordValidator.IsValidPassword(invalidPassword1!));
            Assert.IsFalse(PasswordValidator.IsValidPassword(invalidPassword2!));
            Assert.IsFalse(PasswordValidator.IsValidPassword(invalidPassword3!));
            Assert.IsTrue(PasswordValidator.IsValidPassword(validPassword1!));
            Assert.IsTrue(PasswordValidator.IsValidPassword(validPassword2!));
        }
    }
}