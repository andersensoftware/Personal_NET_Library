using System;

namespace NetExtentions
{
    namespace Exceptions
    {
        public class TimeoutException : Exception
        {
            public TimeoutException(string aMessage) : base(aMessage) { }
            public TimeoutException(string aMessage, Exception aInnerException) : base(aMessage, aInnerException) { }
            public TimeoutException(Exception aInnerException) : base("", aInnerException) { }
        }
    }
}
