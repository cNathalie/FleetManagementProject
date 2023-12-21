namespace FM.Domain.Validators
{
    public class PincodeValidator
    {
        private const int _requieredLength = 4;
        public static bool IsValidPincode(int pincode)
        {
            var pinString = pincode.ToString().Trim();
            if(pinString.Length == _requieredLength && pinString.All(Char.IsDigit))
            {
                return true;
            }
            return false;
        }
    }
}
