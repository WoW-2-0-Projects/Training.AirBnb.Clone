#pragma warning disable CS8618

using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities
{
    public class ListingCategory : SoftDeletedEntity
    {
        public string Name { get; set; }
    }
}