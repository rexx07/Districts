using Nest;

namespace Infrastructure.ElasticSearch.Models;

public class ElasticSearchModel
{
    public Id ElasticId { get; set; }
    public string IndexName { get; set; }
}