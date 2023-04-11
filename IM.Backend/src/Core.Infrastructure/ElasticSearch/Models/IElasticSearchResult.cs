namespace Core.Infrastructure.ElasticSearch.Models;

public interface IElasticSearchResult
{
    bool Success { get; }
    string Message { get; }
}