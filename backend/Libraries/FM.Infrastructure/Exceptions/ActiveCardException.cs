using System.Runtime.Serialization;

namespace FM.Infrastructure.Exceptions
{
    [Serializable]
    public class ActiveCardException : Exception
    {
        public ActiveCardException()
        {
        }

        public ActiveCardException(string? message) : base(message)
        {
        }

        public ActiveCardException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        //protected ActiveCardException(SerializationInfo info, StreamingContext context) : base(info, context)
        //{
        //}
    }
}