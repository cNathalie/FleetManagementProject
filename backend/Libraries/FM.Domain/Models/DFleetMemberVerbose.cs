using FM.Domain.Exceptions;
using FM.Domain.Validators;

namespace FM.Domain.Models
{
    public class DFleetMemberVerbose
    {
        public int FleetId { get; set; }

        private string? _bestuurderNaam;
        public string? BestuurderNaam 
        { 
            get { return _bestuurderNaam ?? throw new FleetMemberException("'BestuurderNaam' was not set to a proper value"); } 
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    _bestuurderNaam = value.Trim();
                }
                else
                {
                    throw new FleetMemberException("'BestuurderNaam' is Empty");
                }
            }
        }

        private string? _bestuurderVoornaam;
        public string? BestuurderVoornaam 
        { 
            get { return _bestuurderVoornaam ?? throw new FleetMemberException("'BestuurderVoornaam' was not set to a proper value"); }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    _bestuurderVoornaam = value.Trim();
                }
                else
                {
                    throw new FleetMemberException("'BestuurderVoornaam' is Empty");
                }
            }
        }
        public int TankaartId { get; set; }

        private string? _voertuigMerkModel;
        public string? VoertuigMerkModel 
        {
            get { return _voertuigMerkModel ?? throw new FleetMemberException("'VoertuigMerkModel' was not set to a proper value"); } 
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    _voertuigMerkModel = value.Trim();
                }
                else
                {
                    throw new FleetMemberException("'VoertuigMerkModel' is Empty");
                }
            }
        }

        private string? _voertuigNummerplaat;
        public string? VoertuigNummerplaat 
        {
            get { return _voertuigNummerplaat ?? throw new FleetMemberException("'VoertuigNummerplaat' was not set to a proper value"); } 
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    _voertuigNummerplaat = value.Trim();
                }
                else
                {
                    throw new FleetMemberException("'VoertuigNummerplaat' is Empty");
                }
            }
        }

        private string? _voertuigChassisnummer;
        public string? VoertuigChassisnummer 
        { 
            get { return _voertuigChassisnummer ?? throw new FleetMemberException("'VoertuigChassisnummer' was not set to a proper value"); }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    var vin = value.Trim();
                    if (VINValidator.IsValidVIN(vin))
                    {
                        _voertuigChassisnummer = vin;
                    }
                    else
                    {
                        throw new FleetMemberException("'Chassisnummer' is invalid");
                    }
                }
                else
                {
                    throw new FleetMemberException("'Chassisnummer' is Empty");
                }
            } 
        }
    }
}
