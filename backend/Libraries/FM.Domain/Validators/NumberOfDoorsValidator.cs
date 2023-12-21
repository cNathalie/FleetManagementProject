namespace FM.Domain.Validators
{
    public class NumberOfDoorsValidator
    {
        public static bool IsValidNumber(int number)
        {
            if (number >= 3 && number <= 5) return true;
            return false;
        }
    }
}
