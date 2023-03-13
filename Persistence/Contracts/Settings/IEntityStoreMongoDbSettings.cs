namespace Shine.Backend.Persistence.Contracts.Settings
{
    public interface IEntityStoreMongoDbSettings
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}