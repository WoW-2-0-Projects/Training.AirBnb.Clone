using AirBnB.Domain.Common.Queries;
using AirBnB.Domain.Entities;

namespace AirBnB.Application.Common.Identity.Queries;

/// <summary>
/// Represents a query to get the current user's roles.
/// </summary>
public class GetCurrentUserRolesQuery : IQuery<IList<Role>>;
