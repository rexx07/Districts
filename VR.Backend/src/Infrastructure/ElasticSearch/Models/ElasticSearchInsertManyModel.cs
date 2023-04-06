namespace Infrastructure.ElasticSearch.Models;

public class ElasticSearchInsertManyModel : ElasticSearchModel
{
    public object[] Items { get; set; }
}