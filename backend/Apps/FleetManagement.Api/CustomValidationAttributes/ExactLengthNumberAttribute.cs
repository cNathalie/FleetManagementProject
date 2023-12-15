using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Api.CustomValidationAttributes
{
    public class ExactLengthNumberAttribute : ValidationAttribute 
    {
        private readonly int _desiredLength;

        public ExactLengthNumberAttribute(int desiredLength)
        {
            _desiredLength = desiredLength;
        }
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is int number)
            {
                string numberStr = number.ToString();

                if (numberStr.Length == _desiredLength)
                {
                    return ValidationResult.Success!;
                }
                else
                {
                    return new ValidationResult($"The number must be {_desiredLength} digits.");
                }
            }
            return new ValidationResult("Invalid data type for the property. Must be integer.");
        }
    }
}
