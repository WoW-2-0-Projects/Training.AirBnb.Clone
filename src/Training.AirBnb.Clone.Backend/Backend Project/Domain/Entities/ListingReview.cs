using Backend_Project.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Project.Domain.Entities
{
    internal class ListingReview : IAuditableEntity
    {
        public Guid Id { get; set; }
        public Guid WrittenBy { get; set; }
        public string Comment { get; set; }
        public double Rating { get; set; }
        public Guid ListingId { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public ListingReview(Guid writtenBy, string comment, double rating, Guid listingId)
        {
            Id = Guid.NewGuid();
            WrittenBy = writtenBy;
            Comment = comment;
            Rating = rating;
            ListingId = listingId;
            CreatedDate = DateTimeOffset.Now;
        }

    }
}
