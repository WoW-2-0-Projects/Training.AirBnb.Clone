using System.Linq.Expressions;
using AirBnB.Application.Listings.Models;
using AirBnB.Application.Listings.Services;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;
using AirBnB.Persistence.Extensions;
using AirBnB.Persistence.Repositories;
using AirBnB.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AirBnB.Infrastructure.Listings.Services;

/// <summary>
/// Foundation service for managing Listing Category related operations
/// </summary>
/// <param name="listingCategoryRepository"></param>
public class ListingCategoryService(IListingCategoryRepository listingCategoryRepository) : IListingCategoryService
{
    public IQueryable<ListingCategory> Get(Expression<Func<ListingCategory, bool>>? predicate = default,
        bool asNoTracking = false)
    => listingCategoryRepository.Get(predicate, asNoTracking);

    public IQueryable<ListingCategory> Get(ListingCategoryFilter listingCategoryFilter, bool asNoTracking = false)
    {
        var listingCategoryQuery= listingCategoryRepository.Get(asNoTracking: asNoTracking).ApplyPagination(listingCategoryFilter);
        listingCategoryQuery = listingCategoryQuery.Include(listingCategory => listingCategory.ImageStorageFile);
        return listingCategoryQuery;
    }

}