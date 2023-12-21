using FM.Domain.Exceptions;
using FM.Domain.Validators;

namespace FM.Domain.Models
{
    public class DTypeRijbewijs
    {
        public DTypeRijbewijs()
        {

        }
        public int TypeRijbewijsId { get; set; }

        private string? _type;
        public string? Type
        {
            get { return _type ?? throw new TypeRijbewijsException("'Type' was not set to a proper value"); }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new TypeRijbewijsException("'Type' is Empty");
                var type = value.Trim();
                if (!DriversLicenseValidator.IsValidDriversLicenseType(type)) throw new TypeRijbewijsException("'Type' is not valid");
                _type = type;
            }
        }
    }
}
