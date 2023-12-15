using FM.Domain.Enums;
using FM.Domain.Exceptions;
using FM.Domain.Validators;

namespace FM.Domain.Models
{
    public class DUser
    {
        public int UserId { get; set; }

        private string? _email;
        public string? Email 
        {
            get { return _email ?? throw new LoginException("'Email' was not set to a proper value"); } 
            set
            {
                if(!String.IsNullOrEmpty(value))
                {
                    var email = value.Trim().ToLower();
                    if(EmailValidator.IsValidEmail(email))
                    {
                        _email = email;
                    }
                    else
                    {
                        throw new LoginException("'Email' is not valid");
                    }
                }
                else
                {
                    throw new LoginException("'Email' is Empty");
                }
            }
        }

        private string? _passwordSalt;
        public string? PasswordSalt { get; set; }

        private string? _passwordHash { get; set; }
        public string? PasswordHash { get; set; }



        private FMRole? _role;
        public FMRole? Role
        {
            get
            {
                return _role;
            }
            set
            {
                if (value != null)
                {
                    if (Enum.IsDefined(typeof(FMRole), value))
                    {
                        _role = value;
                    }
                    else
                    {
                        _role = FMRole.None;
                    }
                }
            }
        }


        //private string? _wachtwoord;
        //public string? Wachtwoord 
        //{ 
        //    get { return _wachtwoord ?? throw new LoginException("'Wachtwoord' was not set to a proper value"); } 
        //    set
        //    {
        //        if (!String.IsNullOrEmpty(value))
        //        {
        //            var ww = value.Trim();
        //            if (PasswordValidator.IsValidPassword(ww))
        //            {
        //                _wachtwoord = ww;
        //            }
        //            else
        //            {
        //                throw new LoginException("'Wachtwoord' is Invalid (Must be at least 4 characters with at least 1 digit)");
        //            }
        //        }
        //        else
        //        {
        //            throw new LoginException("'Wachtwoord' is Empty");
        //        }
        //    }
        //}



    }
}
