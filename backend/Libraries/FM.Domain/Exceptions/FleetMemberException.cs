using System.Runtime.Serialization;

namespace FM.Domain.Exceptions
{
    [Serializable]
    internal class FleetMemberException : Exception
    {
        public FleetMemberException()
        {
        }

        public FleetMemberException(string? message) : base(message)
        {
        }

        public FleetMemberException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        //protected FleetMemberException(SerializationInfo info, StreamingContext context) : base(info, context)
        //{
        //}
    }
}