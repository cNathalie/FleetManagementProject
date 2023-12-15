using FM.Domain.Exceptions;

namespace FM.Domain.Models
{
    public class DBrandstofType
    {
        public int BrandstofTypeId { get; set; }

        private string? _type;
        public string? Type 
        { 
            get {  return _type ?? throw new BrandstofTypeException("'Type' was not set to a proper value"); }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _type = value.Trim();
                }
                else
                {
                    throw new BrandstofTypeException("'Type' is Empty");
                }
            }
        }
    }
}
