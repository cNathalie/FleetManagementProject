using FM.Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PasswordHasherTests
{
    [TestMethod]
    public void ComputeHash_ShouldReturnValidHash()
    {
        // Arrange
        string password = "MySecurePassword";
        string salt = PasswordHasher.GenerateSalt();
        string pepper = "MyPepperValue";
        int iterations = 12;

        // Act
        string hashedPassword = PasswordHasher.ComputeHash(password, salt, pepper, iterations);

        // Assert
        Assert.IsNotNull(hashedPassword);
        Assert.AreNotEqual(password, hashedPassword); // Ensure the hash is different from the original password
    }

    [TestMethod]
    public void GenerateSalt_ShouldReturnValidSalt()
    {
        // Act
        string salt = PasswordHasher.GenerateSalt();

        // Assert
        Assert.IsNotNull(salt);
        Assert.IsFalse(string.IsNullOrEmpty(salt));
    }

    [TestMethod]
    public void ComputeHash_WithZeroIterations_ShouldReturnSamePassword()
    {
        // Arrange
        string password = "MySecurePassword";
        string salt = PasswordHasher.GenerateSalt();
        string pepper = "MyPepperValue";
        int iterations = 0;

        // Act
        string hashedPassword = PasswordHasher.ComputeHash(password, salt, pepper, iterations);

        // Assert
        Assert.AreEqual(password, hashedPassword);
    }
}
