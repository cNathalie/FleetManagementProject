using FM.Domain.Exceptions;
using FM.Domain.Extensions;
using FM.Domain.Validators;

namespace FM.Domain.Models
{
    public class DBestuurder
    {
        public DBestuurder() { }

        public DBestuurder(int bestuurderId, string? naam, string? voornaam, string? adres, string? rijksregisternummer, string? rijbewijs)
        {
            BestuurderId = bestuurderId;
            Naam = naam;
            Voornaam = voornaam;
            Adres = adres;
            Rijksregisternummer = rijksregisternummer;
            Rijbewijs = rijbewijs;
        }

        public int BestuurderId { get; set;}

        private string? _naam;
        public string? Naam 
        { 
            get { return _naam ?? throw new BestuurderException("'Naam' was not set to a proper value"); } 
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    var naam = StringExtensionMethods.ToSentenceCase(value);
                    _naam = naam;
                }
                else
                {
                    throw new BestuurderException("'Naam' is Empty");
                }
            }
        }

        private string? _voornaam;
        public string? Voornaam 
        { 
            get { return _voornaam ?? throw new BestuurderException("'Voornaam' was not set to a proper value"); } 
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    var vnaam = StringExtensionMethods.ToSentenceCase(value);
                    _voornaam = value;
                }
                else
                {
                    throw new BestuurderException("'Voornaam' is Empty");
                }
            }
        }


        private string? _adres;
        public string? Adres 
        { 
            get { return _adres ?? throw new BestuurderException("'Adres' was not set to a proper value"); } 
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    _adres = value;
                }
                else
                {
                    throw new BestuurderException("Empty Address");
                }
            }
        }

        private string? _rijksregisternummer;
        public string? Rijksregisternummer 
        {
            get { return _rijksregisternummer ?? throw new BestuurderException("'Rijksregisternummer' was not set to a proper value"); }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    var rrn = value.Trim();

                    if (RRNValidator.IsValidRRN(rrn))
                    {
                        _rijksregisternummer = rrn;
                    }
                    else
                    {
                        throw new RRNException("Invalid RRN");
                    }
                }
                else
                {
                    throw new BestuurderException("Empty RRN");
                }
            }
        }

        private string? _rijbewijs;
        

        public string? Rijbewijs 
        {
            get { return _rijbewijs ?? throw new BestuurderException("'Rijbewijs' was not set to a proper value"); } 
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    _rijbewijs = value.Trim();
                }
                else
                {
                    throw new BestuurderException("'Rijbewijs' is Empty");
                }
            }
        }
    }
}