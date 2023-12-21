using System.Text.RegularExpressions;

namespace FM.Domain.Validators
{
    public class LicencePlateValidator
    {
        public static bool IsValidEULicensePlate(string? lp)
        {
            if (lp == null) return false;
            string patternEuropean = @"^[A-Z0-9-]{1,10}$";
            return Regex.IsMatch(lp!, patternEuropean);
        }
    }
}
