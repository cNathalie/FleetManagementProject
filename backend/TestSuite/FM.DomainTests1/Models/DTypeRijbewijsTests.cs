using FM.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FM.Domain.Models.Tests
{
    [TestClass()]
    public class DTypeRijbewijsTests
    {
        [TestMethod]
        public void SetType_WithValidType_ShouldSetType()
        {
            // Arrange
            var typeRijbewijs = new DTypeRijbewijs();

            // Act
            typeRijbewijs.Type = "Type B";

            // Assert
            Assert.AreEqual("Type B", typeRijbewijs.Type);
        }

        [TestMethod]
        public void SetType_WithInvalidType_ShouldThrowTypeRijbewijsException()
        {
            // Arrange
            var typeRijbewijs = new DTypeRijbewijs();
            var invalidType = "InvalidType";

            // Act & Assert
            Assert.ThrowsException<TypeRijbewijsException>(() => typeRijbewijs.Type = invalidType);
        }

        [TestMethod]
        public void SetType_WithEmptyType_ShouldThrowTypeRijbewijsException()
        {
            // Arrange
            var typeRijbewijs = new DTypeRijbewijs();

            // Act & Assert
            Assert.ThrowsException<TypeRijbewijsException>(() => typeRijbewijs.Type = string.Empty);
        }

        [TestMethod]
        public void SetType_WithNullType_ShouldThrowTypeRijbewijsException()
        {
            // Arrange
            var typeRijbewijs = new DTypeRijbewijs();

            // Act & Assert
            Assert.ThrowsException<TypeRijbewijsException>(() => typeRijbewijs.Type = null);
        }

        [TestMethod]
        public void SetType_WithWhiteSpaceType_ShouldTrimAndSetType()
        {
            // Arrange
            var typeRijbewijs = new DTypeRijbewijs();

            // Act
            typeRijbewijs.Type = " Type B       ";

            // Assert
            Assert.AreEqual("Type B", typeRijbewijs.Type);
        }
    }
}