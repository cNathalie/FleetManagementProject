using FM.Domain.Enums;
using FM.Domain.Exceptions;
using FM.Domain.Validators;

namespace FM.Domain.Models
{
    public class DUser
    {
        public DUser(int userId, string? email, string? passwordSalt, string? passwordHash, FMRole? role)
        {
            UserId = userId;
            Email = email;
            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;
            Role = role;
        }

        public DUser() { }

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
        public string? PasswordSalt 
        {
            get { return _passwordSalt; } 
            set
            {
                if(String.IsNullOrEmpty(value)) throw new LoginException("'PasswordSalt' is Empty");
                _passwordSalt = value;   
            }
        }

        private string? _passwordHash;
        public string? PasswordHash 
        {
            get { return _passwordHash; } 
            set
            {
                if(String.IsNullOrEmpty(value)) throw new LoginException("'PasswordHash' is Empty");
                _passwordHash = value;
            } 
        }



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
    }
}
