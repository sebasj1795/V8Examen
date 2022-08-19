
using System;

namespace Security.Transversal.Common.CustomException
{
    public class ControlledException : Exception
    {
        public ControlledException(string message)
            : base(message)
        {
        }

        public ControlledException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
