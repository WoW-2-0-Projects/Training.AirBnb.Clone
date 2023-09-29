using Backend_Project.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Project.Domain.Entities
{
    public class ListingCategory : SoftDeletedEntity
    {
        public string Name { get; set; }
        public ListingCategory(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
            CreatedDate = DateTime.UtcNow;
        }



    }
}
