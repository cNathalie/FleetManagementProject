using System.Runtime.Serialization;

namespace FM.Infrastructure.Exceptions
{
    [Serializable]
    internal class UserRepoException : Exception
    {
        public UserRepoException()
        {
        }

        public UserRepoException(string? message) : base(message)
        {
        }

        public UserRepoException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        //protected UserRepoException(SerializationInfo info, StreamingContext context) : base(info, context)
        //{
        //}
    }
}