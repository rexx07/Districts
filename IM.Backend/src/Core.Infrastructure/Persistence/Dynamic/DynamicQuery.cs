﻿namespace Core.Infrastructure.Persistence.Dynamic;

public class DynamicQuery
{
    public DynamicQuery()
    {
    }

    public DynamicQuery(IEnumerable<Sort>? sort, Filter? filter)
    {
        Sort = sort;
        Filter = filter;
    }

    public IEnumerable<Sort>? Sort { get; set; }
    public Filter? Filter { get; set; }
}