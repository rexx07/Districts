﻿namespace Modules.BaseApplication.Pipelines.Caching;

public interface ICachableRequest
{
    bool BypassCache { get; }
    string CacheKey { get; }
    string? CacheGroupKey { get; }
    TimeSpan? SlidingExpiration { get; }
}