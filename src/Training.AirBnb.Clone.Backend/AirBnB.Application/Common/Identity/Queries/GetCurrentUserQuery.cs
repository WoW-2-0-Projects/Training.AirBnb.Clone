using AirBnB.Domain.Common.Queries;
using AirBnB.Domain.Entities;

namespace AirBnB.Application.Common.Identity.Queries;

/// <summary>
/// Represents get current user query
/// </summary>
public class GetCurrentUserQuery : IQuery<User>;