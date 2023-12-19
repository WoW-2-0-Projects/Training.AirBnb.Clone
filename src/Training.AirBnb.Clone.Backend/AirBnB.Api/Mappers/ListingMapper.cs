using AirBnB.Api.Models.DTOs;
using AirBnB.Domain.Entities;
using AutoMapper;

namespace AirBnB.Api.Mappers;

/// <summary>
/// Mapper profile for mapping between ListingDto and Listing.
/// </summary>
public class ListingMapper : Profile
{
    /// <summary>
    /// Initializes a new instance of the ListingMapper class.
    /// </summary>
    public ListingMapper()
    {
        CreateMap<ListingDto, Listing>().ReverseMap();
    }
}