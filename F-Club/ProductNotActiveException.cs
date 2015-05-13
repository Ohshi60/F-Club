using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_Club
{
    class ProductNotActiveException : Exception //Exception klasse til at fange når der forsøges at købe et produkt der ikke er aktivt
    {
        public ProductNotActiveException()
        {}
        public ProductNotActiveException(string message) : base(message)
        {}
        public ProductNotActiveException(string message,Exception inner) : base(message, inner)
        { }
    }
}
