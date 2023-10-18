using Backend_Project.Domain.Common;
using Backend_Project.Domain.Enums;

namespace Backend_Project.Domain.Entities;

public class ListingPropertyType : SoftDeletedEntity
{
    public Guid CategoryId { get; set; }
    public Guid TypeId { get; set; }
    public int? FloorsCount { get; set; }
    public int? ListingFloor {  get; set; }
    public int? YearBuilt { get; set; }
    public int? PropertySize { get; set; }
    public UnitsOfSize? UnitOfSize { get; set; }
}