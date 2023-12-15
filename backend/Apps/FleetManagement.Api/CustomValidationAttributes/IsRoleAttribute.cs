using FM.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Api.CustomValidationAttributes
{
    public class IsRoleAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            string role = value!.ToString()!;
            if (!Enum.IsDefined(typeof(FMRole), role))
            {
                return new ValidationResult("The role must be a valid role");
            }

            return ValidationResult.Success!;
        }
    }
}
