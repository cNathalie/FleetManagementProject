using FM.Domain.Exceptions;
using FM.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FM.DomainTests.Models
{
    [TestClass()]
    public class DBestuurderTests
    {
        [TestMethod]
        [ExpectedException(typeof(BestuurderException))]
        public void Constructor_WithEmptyNaam_ShouldThrowException()
        {
            // Arrange
            int bestuurderId = 1;
            string emptyNaam = string.Empty;
            string voornaam = "Doe";
            string adres = "Arbeidstraat 14, 9300 Aalst";
            string rijksregisternummer = "67110716938";
            string rijbewijs = "Type B";

            // Act
            var bestuurder = new DBestuurder(bestuurderId, emptyNaam, voornaam, adres, rijksregisternummer, rijbewijs);

            // Assert
        }

        [TestMethod]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            int bestuurderId = 1;
            string naam = "John";
            string voornaam = "Doe";
            string adres = "Arbeidstraat 14, 9300 Aalst";
            string rijksregisternummer = "67110716938";
            string rijbewijs = "Type B";

            // Act
            var bestuurder = new DBestuurder(bestuurderId, naam, voornaam, adres, rijksregisternummer, rijbewijs);

            // Assert
            Assert.AreEqual(bestuurderId, bestuurder.BestuurderId);
            Assert.AreEqual(naam, bestuurder.Naam);
            Assert.AreEqual(voornaam, bestuurder.Voornaam);
            Assert.AreEqual(adres, bestuurder.Adres);
            Assert.AreEqual(rijksregisternummer, bestuurder.Rijksregisternummer);
            Assert.AreEqual(rijbewijs, bestuurder.Rijbewijs);
        }

    }
}