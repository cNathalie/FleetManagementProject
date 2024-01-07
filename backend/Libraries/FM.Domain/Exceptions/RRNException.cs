using System.Runtime.Serialization;

namespace FM.Domain.Exceptions
{
    [Serializable]
    public class RRNException : Exception
    {
        public RRNException()
        {
        }

        public RRNException(string? message) : base(message)
        {
        }

        public RRNException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        //protected RRNException(SerializationInfo info, StreamingContext context) : base(info, context)
        //{
        //}
    }
}