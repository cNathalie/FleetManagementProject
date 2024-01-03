using System.Runtime.Serialization;

namespace FM.Infrastructure.Exceptions
{
    [Serializable]
    internal class EFBestuurderRepoException : Exception
    {
        public EFBestuurderRepoException()
        {
        }

        public EFBestuurderRepoException(string? message) : base(message)
        {
        }

        public EFBestuurderRepoException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        //protected EFBestuurderRepoException(SerializationInfo info, StreamingContext context) : base(info, context)
        //{
        //}
    }
}