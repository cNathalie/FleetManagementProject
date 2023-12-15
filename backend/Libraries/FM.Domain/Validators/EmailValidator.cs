using System.ComponentModel.DataAnnotations;

namespace FM.Domain.Validators
{
    public class EmailValidator
    {
        public static bool IsValidEmail(string emailAddress)
        {
            if(string.IsNullOrWhiteSpace(emailAddress)) {  return false; }
            var emailValidation = new EmailAddressAttribute();
            return emailValidation.IsValid(emailAddress);
        }
    }
}
