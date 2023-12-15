using FM.Domain.Exceptions;
using FM.Domain.Validators;

namespace FM.Domain.Models
{
    public class DTankkaart
    {
        public int TankkaartId { get; set; }

        private int _kaartnummer;
        public int Kaartnummer 
        { 
            get { return _kaartnummer; } 
            set
            {
                if (GasCardNumberValidator.IsValidCardNumber(value))
                {
                    _kaartnummer = value;
                }
                else
                {
                    throw new TankkaartException("Invalid 'Kaartnummer': must be 9 digits");
                }
            }
        }
        public DateTime Geldigheidsdatum { get; set; }

        private int _pincode;
        public int Pincode 
        {
            get { return _pincode; }
            set
            {
                if (PincodeValidator.IsValidPincode(value))
                {
                    _pincode = value;
                }
                else 
                { 
                    throw new TankkaartException("'Pincode' is invalid: must be exactly 4 digits");
                }
            }
        }

        private string? _brandstoftype;
        public string? Brandstoftype 
        { 
            get { return _brandstoftype ?? throw new TankkaartException("'Brandstoftype' was not set to a proper value"); }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _brandstoftype = value.Trim();
                }
                else
                {
                    throw new TankkaartException("'Brandstoftype' is Empty");
                }
            } 
        }
        public bool? IsActief { get; set; }
    }
}
