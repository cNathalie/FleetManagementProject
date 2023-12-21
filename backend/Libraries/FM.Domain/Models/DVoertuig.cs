using FM.Domain.Exceptions;
using FM.Domain.Extensions;
using FM.Domain.Validators;

namespace FM.Domain.Models
{
    public class DVoertuig
    {
        public int VoertuigId { get; set; }

        private string? _merkEnModel;
        public string? MerkEnModel
        {
            get { return _merkEnModel ?? throw new VoertuigException("'MerkEnModel' was not set to a proper value"); ; }
            set
            {
                if (String.IsNullOrEmpty(value)) 
                    throw new VoertuigException("'MerkEnModel' is Empty");
                _merkEnModel = value.Trim().ToSentenceCase();
            }
        }

        private string? _chassisnummer;
        public string? Chassisnummer
        {
            get { return _chassisnummer ?? throw new VoertuigException("'Chassisnummer' was not set to a proper value"); ; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new VoertuigException("'Chassisnummer' is Empty");
                if (!VINValidator.IsValidVIN(value))
                    throw new VoertuigException("'Chassisnummer' is invalid: must be exactly 9 digits");
                _chassisnummer = value.Trim();
            }
        }

        private string? _nummerplaat;
        public string? Nummerplaat
        {
            get { return _nummerplaat; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new VoertuigException("Nummerplaat is empty");
                var lp = value.Trim();
                if (!LicencePlateValidator.IsValidEULicensePlate(lp))
                    throw new VoertuigException($"Nummerplaat ({value} is not valid.");
                _nummerplaat = value;
            }
        }

        private string? _brandstofType;
        public string? BrandstofType
        {
            get { return _brandstofType ?? throw new VoertuigException("'Brandstoftype' was not set to a proper value"); ; }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) 
                    throw new VoertuigException("'Brandstoftype' is Empty");
                _brandstofType = value!.Trim();
            }
        }

        private string? _typeWagen;
        public string? TypeWagen
        {
            get { return _typeWagen ?? throw new VoertuigException("'TypeWagen' was not set to a proper value"); ; }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) throw new VoertuigException("'TypeWagen' is Empty");
                _typeWagen = value.Trim();
            }
        }

        private string? _kleur;
        public string? Kleur
        {
            get { return _kleur ?? throw new VoertuigException("'Kleur' was not set to a proper value"); }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) throw new VoertuigException("'Kleur' is Empty");
                _kleur = value.Trim();
            }
        }

        private int _aantalDeuren;
        public int AantalDeuren
        {
            get { return _aantalDeuren; }
            set
            {
                if (!NumberOfDoorsValidator.IsValidNumber(value))
                    throw new VoertuigException("'AantalDeuren' is not a valid number (minimum 3, maximum 5)");
                _aantalDeuren = value;
            }
        }
    }
}
