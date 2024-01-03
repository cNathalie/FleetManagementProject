using System.Runtime.Serialization;

namespace FM.Infrastructure.Exceptions
{
    [Serializable]
    internal class BrandstofTypeRepoException : Exception
    {
        public BrandstofTypeRepoException()
        {
        }

        public BrandstofTypeRepoException(string? message) : base(message)
        {
        }

        public BrandstofTypeRepoException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        //protected BrandstofTypeRepoException(SerializationInfo info, StreamingContext context) : base(info, context)
        //{
        //}
    }
}