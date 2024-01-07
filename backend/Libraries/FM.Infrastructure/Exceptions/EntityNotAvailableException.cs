using System.Runtime.Serialization;

namespace FM.Infrastructure.Exceptions
{
    [Serializable]
    public class EntityNotAvailableException : Exception
    {
        public EntityNotAvailableException()
        {
        }

        public EntityNotAvailableException(string? message) : base(message)
        {
        }

        public EntityNotAvailableException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        //protected EntityNotAvailableException(SerializationInfo info, StreamingContext context) : base(info, context)
        //{
        //}
    }
}