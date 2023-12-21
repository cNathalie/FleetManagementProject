using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FM.Domain.Extensions.Tests
{
    [TestClass()]
    public class StringExtensionMethodsTests
    {
        [TestMethod]
        public void ToSentenceCase_ShouldConvertToSentenceCase()
        {
            // Arrange
            string inputString = "this is a sample sentence.";

            // Act
            string result = inputString.ToSentenceCase();

            // Assert
            Assert.AreEqual("This is a sample sentence.", result);
        }

        [TestMethod]
        public void ToSentenceCase_WithNullOrEmptyString_ShouldThrowArgumentNullException()
        {
            // Arrange
            string nullOrEmptyString = null!;

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => nullOrEmptyString.ToSentenceCase());
        }

        [TestMethod]
        public void ToSentenceCase_WithWhiteSpaceString_ShouldThrowArgumentNullException()
        {
            // Arrange
            string whiteSpaceString = "   ";

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => whiteSpaceString.ToSentenceCase());
        }

    }
}