using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_Club
{
    class ProductNotActiveException : Exception
    {
        public ProductNotActiveException()
        {}
        public ProductNotActiveException(string message) : base(message)
        {}
        public ProductNotActiveException(string message,Exception inner) : base(message, inner)
        { }
    }
}
