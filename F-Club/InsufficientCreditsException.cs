using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_Club
{
    class InsufficientCreditsException : Exception //Exception til at fange når et køb overskrider brugerens saldo
    {
        public InsufficientCreditsException()
        {
        }
        public InsufficientCreditsException(string message):base(message)
        {
        }
        public InsufficientCreditsException(string message, Exception inner) : base(message,inner)
        {

        }
    }
}
