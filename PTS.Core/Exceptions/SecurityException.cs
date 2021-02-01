using System;

namespace PTS.Core.Exceptions
{
    public class SecurityException : Exception
    {
        public SecurityException(string message) : base(message)
        { }
    }
}
