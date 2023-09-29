using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Project.Domain.Exceptions.CityExceptions
{
    public class CityNotFoundException : Exception
    {
        public CityNotFoundException(string message) : base(message) { }
        public CityNotFoundException() { }
    }
}
