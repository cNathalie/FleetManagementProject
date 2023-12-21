using FM.Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FM.Domain.Models.Tests
{
    [TestClass()]
    public class DUserTests
    {
        [TestMethod]
        public void SetRole_WithValidRole_ShouldSetRole()
        {
            // Arrange
            var user = new DUser();

            // Act
            user.Role = FMRole.Admin;

            // Assert
            Assert.AreEqual(FMRole.Admin, user.Role);
        }

        [TestMethod]
        public void SetRole_WithInvalidRole_ShouldSetRoleToNone()
        {
            // Arrange
            var user = new DUser();

            // Act
            user.Role = (FMRole)100; // 100 is not a valid FMRole

            // Assert
            Assert.AreEqual(FMRole.None, user.Role);
        }

    }
}