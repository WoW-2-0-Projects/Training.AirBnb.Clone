#pragma warning disable CS8618

using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class City : SoftDeletedEntity
{
    public string Name { get; set; }
    public Guid CountryId { get; set; }
}