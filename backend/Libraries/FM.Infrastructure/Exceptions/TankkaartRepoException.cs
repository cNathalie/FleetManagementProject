using System.Runtime.Serialization;

namespace FM.Infrastructure.Exceptions
{
    [Serializable]
    internal class TankkaartRepoException : Exception
    {
        public TankkaartRepoException()
        {
        }

        public TankkaartRepoException(string? message) : base(message)
        {
        }

        public TankkaartRepoException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        //protected TankkaartRepoException(SerializationInfo info, StreamingContext context) : base(info, context)
        //{
        //}
    }
}