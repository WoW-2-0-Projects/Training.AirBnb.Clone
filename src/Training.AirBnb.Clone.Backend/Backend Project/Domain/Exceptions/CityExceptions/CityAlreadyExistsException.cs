using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Project.Domain.Exceptions.CityExceptions
{
    public class CityAlreadyExistsException : Exception
    {
        public CityAlreadyExistsException(string message) : base(message) { }
        public CityAlreadyExistsException() { }
    }
}
