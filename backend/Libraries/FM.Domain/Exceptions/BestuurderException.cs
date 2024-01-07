using System.Runtime.Serialization;

namespace FM.Domain.Exceptions
{
    [Serializable]
    public class BestuurderException : Exception
    {
        public BestuurderException()
        {
        }

        public BestuurderException(string? message) : base(message)
        {
        }

        public BestuurderException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        //protected BestuurderException(SerializationInfo info, StreamingContext context) : base(info, context)
        //{
        //}
    }
}