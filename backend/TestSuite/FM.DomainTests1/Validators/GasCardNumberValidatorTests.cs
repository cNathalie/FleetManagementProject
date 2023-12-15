using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace FM.Domain.Validators.Tests
{
    [TestClass()]
    public class GasCardNumberValidatorTests
    {
        [DataTestMethod]
        [DataRow(123456789)]      // Valid card number
        [DataRow(987654321)]      // Valid card number
        [DataRow(12345678)]       // Invalid card number (less than 9 digits)
        [DataRow(1234567890)]     // Invalid card number (more than 9 digits)
        [DataRow(0)]               // Invalid card number (less than 9 digits)
        public void IsValidCardNumber_ShouldReturnCorrectValidity(int cardNumber)
        {
            // Act
            var isValid = GasCardNumberValidator.IsValidCardNumber(cardNumber);

            // Assert
            if (cardNumber.ToString().Length == GasCardNumberValidator.RequiredLength)
            {
                Assert.IsTrue(isValid, $"Card number {cardNumber} should be valid.");
            }
            else
            {
                Assert.IsFalse(isValid, $"Card number {cardNumber} should be invalid.");
            }
        }
    }
}