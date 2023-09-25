using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Project.Domain.Exceptions.ReservationExeptions
{
    public class ReservationNotFound : Exception
    {
        public ReservationNotFound() { }
        public ReservationNotFound(string message) : base(message) { }
    }
}
