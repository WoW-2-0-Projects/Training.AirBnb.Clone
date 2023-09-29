using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Project.Domain.Exceptions.CityExceptions
{
    public class CityFormatException : Exception
    {
        public CityFormatException() { }
        public CityFormatException(string message) : base(message) { }
    }
}
