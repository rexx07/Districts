namespace Infrastructure.Common.Logging.Serilog.ConfigurationModels;

public class ElasticSearchConfiguration
{
    public ElasticSearchConfiguration()
    {
        ConnectionString = string.Empty;
    }

    public string ConnectionString { get; set; }
}