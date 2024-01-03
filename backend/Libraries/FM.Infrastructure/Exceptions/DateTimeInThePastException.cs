using System.Runtime.Serialization;

namespace FM.Infrastructure.Exceptions
{
    [Serializable]
    public class DateTimeInThePastException : Exception
    {
        public DateTimeInThePastException()
        {
        }

        public DateTimeInThePastException(string? message) : base(message)
        {
        }

        public DateTimeInThePastException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        //protected DateTimeInThePastException(SerializationInfo info, StreamingContext context) : base(info, context)
        //{
        //}
    }
}