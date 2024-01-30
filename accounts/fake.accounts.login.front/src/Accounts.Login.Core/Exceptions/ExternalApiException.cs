using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Login.Core.Exceptions
{
    public class ExternalApiException : Exception
    {
        public ExternalApiException(string message) : base(message) { }
    }
}