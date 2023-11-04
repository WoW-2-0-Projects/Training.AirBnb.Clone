using AutoMapper;
using Backend_Project.Application.Entity;
using Backend_Project.Application.Listings.Dtos;
using Backend_Project.Application.Listings.Services;
using Backend_Project.Application.Listings.Settings;
using Backend_Project.Domain.Entities;
using Microsoft.Extensions.Options;

namespace Backend_Project.Infrastructure.CompositionServices;

public class ListingRegistrationService : IListingRegistrationService
{
    private readonly IMapper _mapper;
    private readonly IEntityBaseService<Listing> _listingService;
    private readonly IEntityBaseService<ListingRegistrationProgress> _listingRegistrationProgressService;
    private readonly ListingRulesSettings _listingRuleSettings;
    private readonly ListingRegistrationProgressSettings _listingRegistrationProgressSettings;

    public ListingRegistrationService(IEntityBaseService<Listing> listingService, IEntityBaseService<ListingRegistrationProgress> listingRegistrationProgressService, IMapper mapper, IOptions<ListingRulesSettings> listingRulesSettings, IOptions<ListingRegistrationProgressSettings> listingRegistrationProgressSettings)
    {
        _listingService = listingService;
        _listingRegistrationProgressService = listingRegistrationProgressService;
        _mapper = mapper;
        _listingRuleSettings = listingRulesSettings.Value;
        _listingRegistrationProgressSettings = listingRegistrationProgressSettings.Value;

    }

    public async ValueTask<ListingRegistrationInfoDto> CreateListing(string title)
    {
        var listing = await _listingService.CreateAsync(new Listing { Title = title });

        var listingRegistrationProgress = new ListingRegistrationProgress(_listingRegistrationProgressSettings.ProgressMinValue, listing.Id);

        await _listingRegistrationProgressService.CreateAsync(listingRegistrationProgress);

        var listingRegistrationDto = new ListingRegistrationInfoDto
        {
            Listing = _mapper.Map<ListingRegistrationDto>(listing),
            RegistrationProgress = _mapper.Map<RegistrationProgressDto>(listingRegistrationProgress),
            ListingRules = new ListingRulesRegistrationDto 
            { 
                GuestsMinCount = _listingRuleSettings.GuestsMinCount, 
                GuestsMaxCount = _listingRuleSettings.GuestsMaxCount
            }
        };

        return listingRegistrationDto;
    }

    public ValueTask<ListingRegistrationInfoDto> UpdateRegistrationListing(ListingRegistrationInfoDto listingRegistration)
    {
        throw new NotImplementedException();
    }

    public ValueTask<ListingRegistrationDto> FinishListingRegistration(ListingRegistrationInfoDto listingRegistration)
    {
        throw new NotImplementedException();
    }
}