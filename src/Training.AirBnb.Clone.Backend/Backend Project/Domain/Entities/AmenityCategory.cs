#pragma warning disable CS8618

using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;
public class AmenityCategory : SoftDeletedEntity
{
    public string CategoryName { get; set; }
}