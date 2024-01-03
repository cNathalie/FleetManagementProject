using System.Runtime.Serialization;

namespace FM.Domain.Exceptions
{
    [Serializable]
    internal class BrandstofTypeException : Exception
    {
        public BrandstofTypeException()
        {
        }

        public BrandstofTypeException(string? message) : base(message)
        {
        }

        public BrandstofTypeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        //protected BrandstofTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        //{
        //}
    }
}