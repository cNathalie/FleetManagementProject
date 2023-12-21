using System.Text.RegularExpressions;

namespace FM.Domain.Validators
{
    public class DriversLicenseValidator
    {
        public static bool IsValidDriversLicenseType(string driversLicense)
        {
            string pattern = @"^Type (A|B|C|D|BE|C1|CE|D1|DE|AM|G|T|C1E|CE1|L|L1|L2)$";
            if (Regex.IsMatch(driversLicense, pattern))
            {
                return true;
            }
            return false;
        }
    }
}
