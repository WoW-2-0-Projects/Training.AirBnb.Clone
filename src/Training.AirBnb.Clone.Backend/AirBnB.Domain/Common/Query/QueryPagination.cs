﻿namespace AirBnB.Domain.Common.Query;

public class QueryPagination(uint pageSize, uint pageToken, bool asNoTracking) : FilterPagination(pageSize, pageToken)
{
    public bool AsNoTracking { get; set; } = asNoTracking;

    public override QuerySpecification ToQuerySpecification()
        => new QuerySpecification(PageSize, PageToken, AsNoTracking);
}