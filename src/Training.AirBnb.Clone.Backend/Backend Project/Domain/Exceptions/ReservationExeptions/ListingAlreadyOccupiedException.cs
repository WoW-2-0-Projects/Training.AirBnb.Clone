using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Project.Domain.Exceptions.ReservationExeptions
{
    public class ListingAlreadyOccupiedException : Exception
    {
        public ListingAlreadyOccupiedException() { }
        public ListingAlreadyOccupiedException(string message) : base(message) { }
    }
}
