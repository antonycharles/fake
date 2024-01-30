using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.API.Test.Fakes
{
    public class GenericFakeException : Exception
    {
        public GenericFakeException() : base("Generic Error")
        {
            
        }
    }
}